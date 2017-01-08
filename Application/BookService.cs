using System;
using Application.Requests;
using Domain.Interfaces;
using Domain.Models.Entities;

namespace Application
{
    public class BookService
    {
        private readonly IBookRepository _bookRepository;
        public BookService(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }
        
        public Book CreateNewBook(BookRequest request)
        {
            var book = new Book(request.Title, request.Author, request.QuantityInStock, request.Price);
            book.SetCode(request.Code);
            book.SetLocation(request.Location);
            book.SetPublisher(request.Publisher);
            _bookRepository.Save(book);
            return book;
        }

        public Book UpdateBook(BookRequest request)
        {
            Book book = _bookRepository.GetById(request.Id);
            if (book == null)
                throw new Exception("Livro não encontrado.");

            book.SetTitle(request.Title);
            book.SetAuthor(request.Author);
            book.SetQuantityInStock(request.QuantityInStock);
            book.SetPrice(request.Price);
            book.SetCode(request.Code);
            book.SetLocation(request.Location);
            book.SetPublisher(request.Publisher);
            _bookRepository.Save(book);
            return book;
        }

        public int DeleteBook(int bookId)
        {
            return _bookRepository.Delete(bookId);
        }
    }
}
