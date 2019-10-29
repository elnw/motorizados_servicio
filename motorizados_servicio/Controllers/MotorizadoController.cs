using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Newtonsoft;
using motorizados_servicio.Models;

namespace motorizados_servicio.Controllers
{
    public class MotorizadoController : ApiController
    {
        private motorizadosEntities losMotorizados = new motorizadosEntities();

        public IEnumerable<motorizado> getMotorizados()
        {
            string token;
            var respuestaHeader = Request.Headers;
            if (respuestaHeader.Contains("token"))
            {
                token = respuestaHeader.GetValues("token").First();
            }



            return losMotorizados.motorizado.ToList();
        }

        [HttpPost]
        public bool AgregarMotorizado([FromBody] motorizado order)
        {
            try
            {
                order.id = losMotorizados.Database.SqlQuery<int>("select max(id) + 1 from motorizado").ElementAt(0);
                losMotorizados.motorizado.Add(order);
                losMotorizados.SaveChanges();
                    return true;
            }catch(Exception ex)
            {
                return false;
            }
        }

    }
}
