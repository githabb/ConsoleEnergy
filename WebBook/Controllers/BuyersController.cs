using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ConsoleEnergy;
using WebBook.Models;

namespace WebBook.Controllers
{
    public class BuyersController : BaseApiController
    {
        public IEnumerable<Buyer> GetBuyers()
        {
            using (EnergyEntities db = new EnergyEntities())
            {
                return (from b in db.Покупатель
                    select new Buyer
                    {
                        CodBuyer = b.Код_покупателя,
                        Surname = b.Фамилия,
                        Phone = b.Телефон,
                        PochtovuyAdress = b.Почтовый_адрес,

                        Name = b.Имя,
                        Otchectvo = b.Отчество,
                        Floor = b.Пол,
                        Email = b.Адрес_электронной_почты,
                        CityId = b.Код_города
                    }).ToList();
            }
        }

        public IHttpActionResult GetBuyer(long id)
        {
            if (id <= 0)
                return BadRequest("Id должно быть больше нуля");

            using (EnergyEntities db = new EnergyEntities())
            {


                var result = (from b in db.Покупатель
                    where b.Код_покупателя == id
                    select new Buyer()
                    {
                        CodBuyer = b.Код_покупателя,
                        Surname = b.Фамилия,
                        Phone = b.Телефон,
                        PochtovuyAdress = b.Почтовый_адрес,

                        Name = b.Имя,
                        Otchectvo = b.Отчество,
                        Floor = b.Пол,
                        Email = b.Адрес_электронной_почты,
                        CityId = b.Код_города

                    }).FirstOrDefault();
                if (result == null)
                    return NotFound();

                return Ok(result);
            }
        }

        [HttpPost]
        public IHttpActionResult CreateBuyer([FromBody] Buyer buyer)
        {
            if (buyer == null)
                return BadRequest("Нет объекта покупатель");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            Покупатель dbПокупатель = new Покупатель()
            {
                Код_покупателя = buyer.CodBuyer,
                Фамилия = buyer.Surname,
                Телефон = buyer.Phone,
                Почтовый_адрес = buyer.PochtovuyAdress,
                Код_города = buyer.CityId,

                Имя = buyer.Name,
                Отчество = buyer.Otchectvo,
                Пол = buyer.Floor,
                Адрес_электронной_почты = buyer.Email,
            };
            using (EnergyEntities db = new EnergyEntities())
            {
                db.Покупатель.Add(dbПокупатель);
                db.SaveChanges();
            }

            var result = new Buyer
            {

                CodBuyer = dbПокупатель.Код_покупателя,
                Surname = dbПокупатель.Фамилия,
                Phone = dbПокупатель.Телефон,
                PochtovuyAdress = dbПокупатель.Почтовый_адрес,

                Name = dbПокупатель.Имя,
                Otchectvo = dbПокупатель.Отчество,
                Floor = dbПокупатель.Пол,
                Email = dbПокупатель.Адрес_электронной_почты,
                CityId = dbПокупатель.Код_города

            };
            var newUri = new Uri(Request.RequestUri + "/" + result.CodBuyer.ToString());
            return Created(newUri, result);
        }

        [HttpPut]
        public IHttpActionResult EditBuyer(int id, [FromBody] Buyer buyer)
        {
            if (id <= 0)
                return BadRequest("Id должно быть больше нуля");

            if (buyer == null)
                return BadRequest("Нет объекта книга");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (id != buyer.CodBuyer)
            {
                return BadRequest("Id в строке запроса должно соответствовать коду книги в теле запроса");
            }
            Покупатель dbПокупатель = new Покупатель()
            {
                Код_покупателя = buyer.CodBuyer,
                Фамилия = buyer.Surname,
                Телефон = buyer.Phone,
                Почтовый_адрес = buyer.PochtovuyAdress,

                Имя = buyer.Name,
                Отчество = buyer.Otchectvo,
                Пол = buyer.Floor,
                Адрес_электронной_почты = buyer.Email,

            };
            using (EnergyEntities db = new EnergyEntities())
            {
                db.Entry(dbПокупатель).State = EntityState.Modified;

                db.SaveChanges();
                var result = new Buyer
                {

                    CityId = dbПокупатель.Код_города,
                    Surname = dbПокупатель.Фамилия,
                    Phone = dbПокупатель.Телефон,
                    PochtovuyAdress = dbПокупатель.Почтовый_адрес,

                    Name = dbПокупатель.Имя,
                    Otchectvo = dbПокупатель.Отчество,
                    Floor = dbПокупатель.Пол,
                    Email = dbПокупатель.Адрес_электронной_почты,
                };
                return Ok(result);
            }
        }

        public IHttpActionResult DeleteBuyer(int id)
        {
            if (id <= 0)
                return BadRequest("Id должно быть больше нуля");

            using (EnergyEntities db = new EnergyEntities())
            {
                Покупатель buyer = db.Покупатель.Find(id);
                if (buyer == null)

                {
                    return Ok(new {wasDeleted = false});
                }
                try
                {
                    db.Покупатель.Remove(buyer);
                    db.SaveChanges();
                    return Ok(new {wasDeleted = true});
                }
                catch (Exception ex)
                {
                    return Conflict("Нельзя удалить этого покупателя, т.к. она уже используется в заказах", ex);
                }
            }
        }
    }
}
    




