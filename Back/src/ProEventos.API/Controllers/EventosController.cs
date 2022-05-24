using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProEventos.Application.Contratos;
using ProEventos.Domain.Models;
using ProEventos.Persistence.Contextos;

namespace ProEventos.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventosController : ControllerBase
    {
        private readonly IEventoService _eventoService;

        public EventosController(IEventoService eventoService)
        {
            _eventoService = eventoService;
        }

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> Get()
        {
            try
            {
                var eventos = await _eventoService.GetAllEventosAsync(true);
                if(eventos == null) return NotFound("Nenhum evento encontrado.");
                return Ok(eventos);
            }
            catch (System.Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, 
                    $"Erro ao tentar recuperar eventos. Erro: {ex.Message}");
            }
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var evento = await _eventoService.GetEventoByIdAsync(id, true);
                if(evento == null) 
                    return NotFound($"Nenhum evento encontrado com o Id: {id}.");
                return Ok(evento);
            }
            catch (System.Exception ex)
            {
                 return this.StatusCode(StatusCodes.Status500InternalServerError, 
                    $"Erro ao tentar recuperar evento. Erro: {ex.Message}");
            }
        }

        [HttpGet]
        [Route("tema/{tema}")]
        public async Task<IActionResult> GetByTema(string tema)
        {
            try
            {
                var evento = await _eventoService.GetAllEventosByTemaAsync(tema, true);
                if(evento == null) 
                    return NotFound($"Nenhum evento encontrado com o Tema: {tema}.");
                return Ok(evento);
            }
            catch (System.Exception ex)
            {
                 return this.StatusCode(StatusCodes.Status500InternalServerError, 
                    $"Erro ao tentar recuperar eventos. Erro: {ex.Message}");
            }
        }

        // [HttpPost]
        // [Route("")]
        // public async Task<IActionResult> Create([FromBody]Evento model)
        // {
        //     try
        //     {
        //         if(!ModelState.IsValid)
        //             return BadRequest()
        //     }
        //     catch (System.Exception)
        //     {
                
        //         throw;
        //     }
        // }
        
        // [HttpDelete]
        // [Route("{id}")]
        // public ActionResult Delete(int id)
        // {
        //     var evento = _context.Eventos.Single(reg => reg.Id == id);
        //     _context.Eventos.Remove(evento);
        //     _context.SaveChanges();
        //     return Ok();
        // }
    }
}
