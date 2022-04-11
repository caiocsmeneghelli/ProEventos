using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProEventos.API.Models;

namespace ProEventos.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventoController : ControllerBase
    {
        private IEnumerable<Evento> _evento = new Evento[]
        {
            new Evento(){
                EventoId = 1,
                Tema = "Angular 11 e .NET 5",
                Local = "Belo Horizonte",
                Lote="1 Lote",
                QtdPessoas = 250,
                DataEvento = DateTime.Now.AddDays(5).ToString()
                },
            new Evento(){
                EventoId = 2,
                Tema = "Temados",
                Local = "São Paulo",
                Lote= "2 Lote",
                QtdPessoas = 350,
                DataEvento = DateTime.Now.AddDays(23).ToString()
                }
        };
        public EventoController()
        {

        }

        [HttpGet]
        public IEnumerable<Evento> Get()
        {
            return _evento;
        }
    }
}
