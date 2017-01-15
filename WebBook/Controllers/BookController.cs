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
    public class BookController : BaseApiController
    {
        public IHttpActionResult GetBooks()
        {
            using (EnergyEntities db = new EnergyEntities())
            {
                var result = (from b in db.Книга
                              select new Book
                              {
                                  CodBook = b.Код_книги,
                                  Name = b.Название,
                                  Price = b.Цена,
                                  Author = b.Авторы
                              }).ToList();

                return Ok(result);
            }
        }

        public IHttpActionResult GetBook(int id)
        {
            if (id <= 0)
                return BadRequest("Id должно быть больше нуля");


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

                if (result == null)
                    return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.NotFound, "Книга не найдена с таким id"));

                return Ok(result);
            }
        }

        [HttpPost]
        public IHttpActionResult CreateBook([FromBody] Book book)
        {
            if (book == null)
                return BadRequest("Нет объекта книга");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

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

            var result = new Book
            {
                CodBook = dbКнига.Код_книги,
                Name = dbКнига.Название,
                Price = dbКнига.Цена,
                Author = dbКнига.Авторы
            };
            var newUri = new Uri(Request.RequestUri + "/" + result.CodBook.ToString());
            return Created(newUri, result);
        }

        [HttpPut]
        public IHttpActionResult EditBook(int id, [FromBody] Book book)
        {
            if (id <= 0)
                return BadRequest("Id должно быть больше нуля");

            if (book == null)
                return BadRequest("Нет объекта книга");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (id != book.CodBook)
            {
                return BadRequest("Id в строке запроса должно соответствовать коду книги в теле запроса");
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
                var result = new Book
                {
                    CodBook = dbКнига.Код_книги,
                    Name = dbКнига.Название,
                    Price = dbКнига.Цена,
                    Author = dbКнига.Авторы
                };
                return Ok(result);
            }
        }

        public IHttpActionResult DeleteBook(int id)
        {
            if (id <= 0)
                return BadRequest("Id должно быть больше нуля");

            using (EnergyEntities db = new EnergyEntities())
            {
                Книга book = db.Книга.Find(id);
                if (book == null)
                {
                    return Ok(new { wasDeleted = false });
                }
                try
                {
                    db.Книга.Remove(book);
                    db.SaveChanges();
                    return Ok(new { wasDeleted = true });
                }
                catch (Exception ex)
                {
                    return Conflict("Нельзя удалить эту книгу, т.к. она уже используется в заказах", ex);
                }
            }
        }

        
    }

}



