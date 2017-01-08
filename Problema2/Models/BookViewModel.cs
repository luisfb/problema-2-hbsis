using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Application.Requests;
using Domain.Models.Entities;
using Microsoft.Ajax.Utilities;

namespace Problema2.Models
{
    public class BookViewModel
    {
        public int Id { get; set; }

        [MaxLength(100, ErrorMessage = "O título do livro deve ter no máximo 100 caracteres.")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Campo obrigatório")]
        [Display(Name = "Título")]
        public string Title { get; set; }

        [MaxLength(50, ErrorMessage = "O código do livro deve ter no máximo 50 caracteres.")]
        [Display(Name = "Código")]
        public string Code { get; set; }

        [MaxLength(255, ErrorMessage = "O nome do autor deve ter no máximo 255 caracteres.")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Campo obrigatório")]
        [Display(Name = "Autor")]
        public string Author { get; set; }

        [MaxLength(255, ErrorMessage = "O nome da editora deve ter no máximo 255 caracteres.")]
        [Display(Name = "Editora")]
        public string Publisher { get; set; }

        [MaxLength(255, ErrorMessage = "O nome do local deve ter no máximo 255 caracteres.")]
        [Display(Name = "Local/Prateleira")]
        public string Location { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Campo obrigatório")]
        [Display(Name = "Qtd. em Estoque")]
        public int QuantityInStock { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Campo obrigatório")]
        [Display(Name = "Preço")]
        public decimal Price { get; set; }

        public BookViewModel()
        {
            
        }
        public BookViewModel(Book book)
        {
            Id = book.Id;
            Title = book.Title;
            Author = book.Author;
            Code = book.Code;
            Publisher = book.Publisher;
            Location = book.Location;
            QuantityInStock = book.QuantityInStock;
            Price = book.Price;
        }

        public BookRequest GetBookRequest()
        {
            return new BookRequest()
            {
                Id = this.Id,
                Title = this.Title,
                Author = this.Author,
                Code = this.Code,
                Publisher = this.Publisher,
                Location = this.Location,
                QuantityInStock = this.QuantityInStock,
                Price = this.Price
            };
        }
        
    }
}