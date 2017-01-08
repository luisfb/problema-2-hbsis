using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Application;
using Domain.Models.Entities;
using Infrastructure.Repositories;
using Infrastructure.UnitOfWork;
using Problema2.Models;

namespace Problema2.Controllers
{
    public class BooksController : Controller
    {
        // GET: Books
        public ActionResult Index()
        {
            List<BookViewModel> bookList = new List<BookViewModel>();
            
            using (var uOw = new UnitOfWork())
                new BookRepository(uOw).Query(true).OrderBy(x => x.Title).Select(x => x).ToList().ForEach(x => bookList.Add(new BookViewModel(x)));
            
            return View(bookList);
        }

        // GET: Books/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Book book = null;
            using (var uOw = new UnitOfWork())
                book = new BookRepository(uOw).GetById((int)id);
            
            if (book == null)
                return HttpNotFound();
            
            return View(new BookViewModel(book));
        }

        // GET: Books/Create
        public ActionResult Create()
        {
            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(BookViewModel viewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View(viewModel);
                
                using (var uOw = new UnitOfWork())
                {
                    uOw.BeginTransaction();
                    var bookRepository = new BookRepository(uOw);
                    var bookService = new BookService(bookRepository);
                    bookService.CreateNewBook(viewModel.GetBookRequest());
                    uOw.Commit();
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.Exception = ex.Message;
                return View(viewModel);
            }
        }

        // GET: Books/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            
            Book book = null;
            using (var uOw = new UnitOfWork())
                book = new BookRepository(uOw).GetById((int)id);

            if (book == null)
                return HttpNotFound();
            
            return View(new BookViewModel(book));
        }

        // POST: Books/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(BookViewModel viewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View(viewModel);

                using (var uOw = new UnitOfWork())
                {
                    uOw.BeginTransaction();
                    var bookRepository = new BookRepository(uOw);
                    var bookService = new BookService(bookRepository);
                    bookService.UpdateBook(viewModel.GetBookRequest());
                    uOw.Commit();
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.Exception = ex.Message;
                return View(viewModel);
            }
        }

        // GET: Books/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Book book = null;
            using (var uOw = new UnitOfWork())
                book = new BookRepository(uOw).GetById((int) id);

            if(book == null)
                return HttpNotFound();
            
            return View(new BookViewModel(book));
        }

        // POST: Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            using (var uOw = new UnitOfWork())
                new BookRepository(uOw).Delete(id);

            return RedirectToAction("Index");
        }
        
    }
}
