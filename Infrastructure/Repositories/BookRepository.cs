using Domain.Interfaces;
using Domain.Models.Entities;
using Infrastructure.UnitOfWork;

namespace Infrastructure.Repositories
{
    public class BookRepository : BaseRepository<Book>, IBookRepository
    {
        public BookRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
        //nada a ser colocado aqui, uma vez que esta aplicação é um simples CRUD e os métodos do BaseRepository são suficientes
    }
}
