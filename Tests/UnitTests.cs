using System;
using System.Linq;
using Application;
using Application.Requests;
using Domain.Interfaces;
using Domain.Models.Entities;
using FluentAssertions;
using Infrastructure.Repositories;
using Infrastructure.UnitOfWork;
using NSubstitute;
using NSubstitute.Core;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class UnitTests
    {
        /*
        Não cobri todo o codigo com testes por falta de tempo.
        */
        private BookRequest GivenAValidBookRequestData()
        {
            return new BookRequest()
            {
                Author = "John Doe",
                Code = "1234",
                Location = "P-25-R-12",
                Price = 10,
                Publisher = "My Publisher",
                QuantityInStock = 40,
                Title = "The Book Is On The Table"
            };
        }

        [Test]
        public void GivenSomeInvalidData_WhenCreateANewBook_ShouldRaiseAnException()
        {
            Book book = null;
            var ex = Assert.Throws<Exception>(() => book = new Book("", "", -5, -5));
            var ex2 = Assert.Throws<Exception>(() => book = new Book("abcde", "", -5, -5));
            var ex3 = Assert.Throws<Exception>(() => book = new Book("abcde", "abcde", -5, -5));
            var ex4 = Assert.Throws<Exception>(() => book = new Book("abcde", "abcde", 5, -5));
            ex.Message.Should().Be("O título do livro é inválido");
            ex2.Message.Should().Be("Informe um nome de autor válido.");
            ex3.Message.Should().Be("A quantidade em estoque deve ser maior ou igual a zero.");
            ex4.Message.Should().Be("O preço deve ser maior que zero.");
        }

        [Test]
        public void GivenAValidBookRequestData_WhenCreateTheBookThroughApplicationService_ShouldCreateTheBook()
        {
            IBookRepository bookRepository = Substitute.For<IBookRepository>();
            bookRepository.Save(null).ReturnsForAnyArgs(1);
            
            var bookRequestData = GivenAValidBookRequestData();

            var bookService = new BookService(bookRepository);
            Book book = bookService.CreateNewBook(bookRequestData);

            book.Title.Should().Be("The Book Is On The Table");
            book.Author.Should().Be("John Doe");
            book.QuantityInStock.Should().Be(40);
            book.Price.Should().Be(10);
        }

        [Test]
        public void GivenAValidBookRequestData_WhenUpdateTheBookThroughApplicationService_ShouldUpdateTheBook()
        {
            IBookRepository bookRepository = Substitute.For<IBookRepository>();
            bookRepository.Save(null).ReturnsForAnyArgs(1);
            bookRepository.GetById(1).ReturnsForAnyArgs(new Book("Book To Be Updated", "Batman", 100, 950));

            var bookRequestData = GivenAValidBookRequestData();
            bookRequestData.Id = 1;

            var bookService = new BookService(bookRepository);
            Book book = bookService.UpdateBook(bookRequestData);

            book.Title.Should().Be("The Book Is On The Table");
            book.Author.Should().Be("John Doe");
            book.QuantityInStock.Should().Be(40);
            book.Price.Should().Be(10);
        }
    }
}
