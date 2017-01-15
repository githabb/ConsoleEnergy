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
    public class CityController : BaseApiController
    {

        public IEnumerable<City> GetCitys()
        {
            using (EnergyEntities db = new EnergyEntities())
            {
                return (from b in db.Город
                    select new City
                    {
                        CodCity = b.Код_города,
                        Name = b.Название,
                    }).ToList();
            }
        }

        public IHttpActionResult GetCity(int id)
        {
            if (id <= 0)
                return BadRequest("Id должно быть больше нуля ");

            using (EnergyEntities db = new EnergyEntities())
            {


                var result = (from b in db.Город
                    where b.Код_города == id
                    select new City
                    {
                        CodCity = b.Код_города,
                        Name = b.Название,
                    }).FirstOrDefault();
                if (result == null)


                    //return NotFound ("Город не найдена с таким id");
                    return
                        ResponseMessage(Request.CreateResponse(HttpStatusCode.NotFound,
                            new {message = "Город не найдена с таким id"}));

                return Ok(result);
            }
        }

        [HttpPost]
        public IHttpActionResult CreateCity([FromBody] City city)
        {
            if (city == null)
                return BadRequest("Нет объекта город");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            Город dbГород = new Город()
            {
                Код_города = city.CodCity,
                Название = city.Name,

            };
            using (EnergyEntities db = new EnergyEntities())
            {
                db.Город.Add(dbГород);
                db.SaveChanges();
            }

            var result = new City
            {
                CodCity = dbГород.Код_города,
                Name = dbГород.Название,

            };
            var newUri = new Uri(Request.RequestUri + "/" + result.CodCity.ToString());
            return Created(newUri, result);
        }



        [HttpPut]
        public IHttpActionResult EditCity(int id, [FromBody] City city)
        {
            if (id <= 0)
                return BadRequest("Id должно быть больше нуля");

            if (city == null)
                return BadRequest("Нет объекта город");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (id != city.CodCity)
            {
                return BadRequest("Id в строке запроса должно соответствовать коду город в теле запроса");
            }
            Город dbГород = new Город()
            {
                Код_города = city.CodCity,
                Название = city.Name,

            };
            using (EnergyEntities db = new EnergyEntities())
            {
                db.Entry(dbГород).State = EntityState.Modified;

                db.SaveChanges();
                var result = new City()
                {
                    CodCity = dbГород.Код_города,
                    Name = dbГород.Название,

                };
                return Ok(result);
            }
        }

        public IHttpActionResult DeleteCity(int id)
        {
            if (id <= 0)
                return BadRequest("Id должно быть больше нуля");

            using (EnergyEntities db = new EnergyEntities())
            {
                Город city = db.Город.Find(id);
                if (city == null)
                {
                    return Ok(new {wasDeleted = false});
                }
                try
                {

                    db.Город.Remove(city);
                    db.SaveChanges();
                    return Ok(new {wasDeleted = true});
                }
                catch (Exception ex)
                {
                    return Conflict("Нельзя удалить этот город, т.к. она уже используется в заказах", ex);
                }
            }
        }
    }

}