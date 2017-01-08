using System;
using System.Linq;
using Domain.Models.Entities;
using FluentAssertions;
using Infrastructure.Repositories;
using Infrastructure.UnitOfWork;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class IntegrationTests
    {
        private Book GivenABook()
        {
            var newBook = new Book("Lord of The Rings", "J.R.R. Tolkien", 5, 49.90M);
            newBook.SetLocation("Prateleira do Condado");
            return newBook;
        }
        private Book GivenAPersistedBook()
        {
            var newBook = GivenABook();
            using (var uOw = new UnitOfWork())
            {
                var bookRepository = new BookRepository(uOw);
                uOw.BeginTransaction();
                bookRepository.Save(newBook);
                uOw.Commit();
            }
            return newBook;
        }

        [Test]
        public void GivenABook_WhenCallRepositorySave_ShouldPersistOnDatabase()
        {
            using (var uOw = new UnitOfWork())
            {
                var bookRepository = new BookRepository(uOw);
                var newBook = GivenABook();

                uOw.BeginTransaction();
                bookRepository.Save(newBook);
                uOw.Commit();
                
                Book lotrBook = bookRepository.GetById(1);
                lotrBook.Title.Should().Be("Lord of The Rings");
                lotrBook.Location.Should().Be("Prateleira do Condado");
                lotrBook.Author.Should().Be("J.R.R. Tolkien");
                lotrBook.QuantityInStock.Should().Be(5);
                lotrBook.Price.Should().Be(49.90M);
            }
        }

        [Test]
        public void GivenAPersistedBook_WhenCallRepositoryDelete_ShouldDeleteTheBook()
        {
            var persistedBook = GivenAPersistedBook();
            using (var uOw = new UnitOfWork())
            {
                var bookRepository = new BookRepository(uOw);
                uOw.BeginTransaction();
                bookRepository.Delete(persistedBook);
                uOw.Commit();
                Book book = bookRepository.GetById(persistedBook.Id);
                book.Should().BeNull();
            }
        }

        [Test]
        public void GivenAPersistedBook_WhenCallRepositorySave_ShouldUpdateTheBook()
        {
            var persistedBook = GivenAPersistedBook();
            persistedBook.SetTitle("Harry Potter and the Philosopher's Stone");
            persistedBook.SetAuthor("J. K. Rowling");
            persistedBook.SetPrice(19.90M);

            using (var uOw = new UnitOfWork())
            {
                var bookRepository = new BookRepository(uOw);
                uOw.BeginTransaction();
                bookRepository.Save(persistedBook);
                uOw.Commit();
                Book book = bookRepository.GetById(persistedBook.Id);
                book.Title.Should().Be("Harry Potter and the Philosopher's Stone");
                book.Author.Should().Be("J. K. Rowling");
                book.Price.Should().Be(19.90M);
            }
        }
    }
}
