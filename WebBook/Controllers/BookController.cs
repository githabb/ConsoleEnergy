using ConsoleEnergy;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebBook.Models;

namespace WebBook.Controllers
{
    public class BookController : ApiController
    {
        public IEnumerable<Book> GetBooks()
        {
            using (EnergyEntities db = new EnergyEntities())
            {
                return (from b in db.Книга
                    select new Book
                    {
                        CodBook = b.Код_книги,
                        Name = b.Название,
                        Price = b.Цена,
                        Author = b.Авторы
                    }).ToList();
            }
        }

        public Book GetBook(int id)
        {
            using (EnergyEntities db = new EnergyEntities())
            {
                // db.Книга.Find(id) так тоже можно

                var result = (from b in db.Книга
                    where b.Код_книги == id
                    select new Book
                    {
                        CodBook = b.Код_книги,
                        Name = b.Название,
                        Price = b.Цена,
                        Author = b.Авторы
                    }).FirstOrDefault();
                return result;
            }
        }

        [HttpPost]
        public Book CreateBook([FromBody] Book book)
        {
            if (!ModelState.IsValid)
                return null;

            Книга dbКнига = new Книга()
            {
                Название = book.Name,
                Цена = book.Price,
                Авторы = book.Author
            };
            using (EnergyEntities db = new EnergyEntities())
            {
                db.Книга.Add(dbКнига);
                db.SaveChanges();
            }

            return new Book
            {
                CodBook = dbКнига.Код_книги,
                Name = dbКнига.Название,
                Price = dbКнига.Цена,
                Author = dbКнига.Авторы
            };
        }

        [HttpPut]
        public Book EditBook(int id, [FromBody] Book book)
        {
            if (!ModelState.IsValid)
                return null;

            if (id != book.CodBook)
            {
                return null;
            }
            Книга dbКнига = new Книга()
            {
                Код_книги = book.CodBook,
                Название = book.Name,
                Цена = book.Price,
                Авторы = book.Author
            };
            using (EnergyEntities db = new EnergyEntities())
            {
                db.Entry(dbКнига).State = EntityState.Modified;

                db.SaveChanges();
                return new Book
                {
                    CodBook = dbКнига.Код_книги,
                    Name = dbКнига.Название,
                    Price = dbКнига.Цена,
                    Author = dbКнига.Авторы
                };
            }
        }

        public void DeleteBook(int id)
        {
            using (EnergyEntities db = new EnergyEntities())
            {
                Книга book = db.Книга.Find(id);
                if (book != null)
                {

                    db.Книга.Remove(book);
                    db.SaveChanges();
                }
            }
        }
    }

    }



