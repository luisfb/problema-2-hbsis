using System;

namespace Domain.Models.Entities
{
    public class Book : Entity
    {
        public string Title { get; private set; }
        public string Code { get; private set; }
        public string Author { get; private set; }
        public string Publisher { get; private set; }
        public string Location { get; private set; }
        public int QuantityInStock { get; private set; }
        public decimal Price { get; private set; }
        public DateTime InsertDate { get; private set; }

        public Book(string title, string author, int quantityInStock, decimal price)
        {
            SetTitle(title);
            SetAuthor(author);
            SetQuantityInStock(quantityInStock);
            SetPrice(price);
        }

        //EF requires an empty constructor 
        public Book()
        { }

        public void SetTitle(string title)
        {
            if(string.IsNullOrWhiteSpace(title))
                throw new Exception("O título do livro é inválido");
            if (title.Length > 100)
                title = title.Substring(0, 100);
            Title = title;
        }

        public void SetCode(string code)
        {
            if (code == null) code = string.Empty;
            if (code.Length > 50)
                throw new Exception("O código do livro deve ter no máximo 50 caracteres.");
            Code = code;
        }

        public void SetAuthor(string author)
        {
            if (string.IsNullOrWhiteSpace(author))
                throw new Exception("Informe um nome de autor válido.");
            if (author.Length > 255)
                throw new Exception("O autor do livro deve ter no máximo 255 caracteres");
            Author = author;
        }

        public void SetPublisher(string publisher)
        {
            if (publisher == null) publisher = string.Empty;
            if (publisher.Length > 255)
                throw new Exception("O nome da editora deve ter no máximo 255 caracteres.");
            Publisher = publisher;
        }

        public void SetLocation(string location)
        {
            if (location == null) location = string.Empty;
            if (location.Length > 255)
                throw new Exception("O local do livro deve ter no máximo 255 caracteres.");
            Location = location;
        }

        public void SetQuantityInStock(int quantity)
        {
            if(quantity < 0)
                throw new Exception("A quantidade em estoque deve ser maior ou igual a zero.");
            QuantityInStock = quantity;
        }

        public void SetPrice(decimal price)
        {
            if (price <= 0)
                throw new Exception("O preço deve ser maior que zero.");
            Price = price;
        }

    }
}
