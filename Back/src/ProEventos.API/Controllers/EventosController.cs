using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProEventos.Domain.Models;
using ProEventos.Persistence;

namespace ProEventos.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventosController : ControllerBase
    {
        private readonly ProEventosContext _context;
        public EventosController(ProEventosContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("")]
        public IEnumerable<Evento> Get()
        {
            return _context.Eventos.AsNoTracking().ToList();
        }

        [HttpGet]
        [Route("{id}")]
        public Evento Get(int id)
        {
            return _context
                .Eventos
                .Where(reg => reg.Id == id)
                .AsNoTracking()
                .SingleOrDefault();
        }

        [HttpPost]
        [Route("")]
        public void Create([FromBody]Evento model)
        {
            if(ModelState.IsValid)
            {
                _context.Eventos.Add(model);
                _context.SaveChanges();
            }
        }
        
        [HttpDelete]
        [Route("{id}")]
        public ActionResult Delete(int id)
        {
            var evento = _context.Eventos.Single(reg => reg.Id == id);
            _context.Eventos.Remove(evento);
            _context.SaveChanges();
            return Ok();
        }
    }
}
