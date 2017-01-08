using System;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.Entity.Validation;
using System.IO;
using System.Linq;

namespace Infrastructure.UnitOfWork
{
    public sealed class Context : DbContext
    {
        static Context()
        {
#if (DEBUG)
            if (AppDomain.CurrentDomain.FriendlyName == "NUnit Tests")
                Database.SetInitializer(new DropCreateDatabaseAlways<Context>());
            else
                Database.SetInitializer(new NullDatabaseInitializer<Context>());
            
#endif
#if (!DEBUG)
            Database.SetInitializer(new NullDatabaseInitializer<Context>());
            Database.SetInitializer<Context>(null);
#endif
        }

        private void SetDataDirectoryForLocalDB()
        {
            var baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string relative = @"..\..\App_Data\";
            string absolute = Path.GetFullPath(Path.Combine(baseDirectory, relative));
            var dirInfo = new DirectoryInfo(absolute);
            if (!dirInfo.Exists)
                dirInfo.Create();
            AppDomain.CurrentDomain.SetData("DataDirectory", absolute);
        }

        public Context() : base("name=DefaultConnectionString")
        {
            if (AppDomain.CurrentDomain.FriendlyName == "NUnit Tests")
                SetDataDirectoryForLocalDB();
#if (DEBUG)
            //this.Database.Log = Console.WriteLine;
            this.Database.Log = message => System.Diagnostics.Trace.Write(message);
#endif
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();

            modelBuilder.Properties<string>()
                .Configure(p => p.HasColumnType("varchar"));
            modelBuilder.Properties<string>()
                .Configure(p => p.HasMaxLength(255));

            modelBuilder.Configurations.AddFromAssembly(typeof(Context).Assembly);
        }
        public override int SaveChanges()
        {
            try
            {
                foreach (var entry in ChangeTracker.Entries())
                {
                    if (entry.State == EntityState.Added)
                    {
                        //fiz isso pra no caso de vincular um registro existente a um novo registro, evitar q o entity tente inserir o registro existente
                        if ((int)entry.Property("Id").CurrentValue > 0)
                        {
                            entry.State = EntityState.Unchanged;
                        }
                    }
                }
                var entriesWithInsertDate =
                    ChangeTracker.Entries().Where(x => x.Entity.GetType().GetProperty("InsertDate") != null).ToList();
                if (entriesWithInsertDate.Count > 0)
                {
                    foreach (var entry in entriesWithInsertDate)
                    {
                        if (entry.State == EntityState.Added)
                        {
                            entry.Property("InsertDate").CurrentValue = DateTime.Now;
                        }
                        if (entry.State == EntityState.Modified)
                        {
                            entry.Property("InsertDate").IsModified = false;
                        }
                    }
                }
                return base.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                var errorsList = string.Empty;
                foreach (var entityValidationError in e.EntityValidationErrors)
                {
                    errorsList += string.Join(", ", entityValidationError.ValidationErrors.Select(x => x.ErrorMessage).ToList());
                }
                throw new Exception(errorsList);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public System.Data.Entity.DbSet<Domain.Models.Entities.Book> Books { get; set; }
    }
}
