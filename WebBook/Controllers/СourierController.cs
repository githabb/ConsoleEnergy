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
    public class CourierController : ApiController
    {
        public IEnumerable<Courier> GetСouriers()
        {
            using (EnergyEntities db = new EnergyEntities())
            {
                return (from b in db.Курьер
                        select new Courier()
                        {
                            CodCourier = b.Код_курьера,
                            Surname = b.Фамилия,
                            CodCity = b.Код_города,
                            Name = b.Имя,
                            Otchectvo = b.Отчество,
                            Gender = b.Пол,
                            Datebirth = b.Дата_рождения

                        }).ToList();
            }
        }
        public IHttpActionResult GetСourier(long id)
        {
            if (id <= 0)
                return BadRequest("Id должно быть больше нуля");

            using (EnergyEntities db = new EnergyEntities())
            {


                var result = (from b in db.Курьер
                              where b.Код_курьера == id
                              select new Courier()
                              {
                                  CodCourier = b.Код_курьера,
                                  Surname = b.Фамилия,
                                  CodCity = b.Код_города,
                                  Name = b.Имя,
                                  Otchectvo = b.Отчество,
                                  Gender = b.Пол,
                                  Datebirth = b.Дата_рождения

                              }).FirstOrDefault();
                if (result == null)
                    return NotFound();

                return Ok(result);
            }
        }
        [HttpPost]
        public IHttpActionResult CreateСourier([FromBody] Courier courier)
        {
            if (courier == null)
                return BadRequest("Нет такого курьера");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            Курьер dbКурьер = new Курьер()
            {
                Фамилия = courier.Surname,
                Имя = courier.Name,
                Отчество = courier.Otchectvo,
                Пол = courier.Gender,
                Дата_рождения = courier.Datebirth,
                Код_города = courier.CodCity,


            };
            using (EnergyEntities db = new EnergyEntities())
            {
                db.Курьер.Add(dbКурьер);
                db.SaveChanges();
            }

            var result = new Courier
            {
                CodCourier = dbКурьер.Код_курьера,
                Surname = dbКурьер.Фамилия,
                CodCity = dbКурьер.Код_города,
                Name = dbКурьер.Имя,
                Otchectvo = dbКурьер.Отчество,
                Gender = dbКурьер.Пол,
                Datebirth = dbКурьер.Дата_рождения

            };
            var newUri = new Uri(Request.RequestUri + "/" + result.CodCourier.ToString());
            return Created(newUri, result);
        }
    }
}
