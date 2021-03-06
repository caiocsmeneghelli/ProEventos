using System.Threading.Tasks;
using ProEventos.Application.Contratos;
using ProEventos.Domain.Models;
using ProEventos.Persistence.Contratos;

namespace ProEventos.Application
{
    public class EventoService : IEventoService
    {
        private readonly IProEventosPersist proEventosPersist;
        private readonly IEventoPersist eventoPersist;

        public EventoService(IProEventosPersist ProEventosPersist,
                             IEventoPersist EventoPersist)
        {
            proEventosPersist = ProEventosPersist;
            eventoPersist = EventoPersist;
        }
        public async Task<Evento> AddEventos(Evento model)
        {
            try
            {
                proEventosPersist.Add<Evento>(model);
                if(await proEventosPersist.SaveChangesAsync())
                    return await eventoPersist.GetEventoByIdAsync(model.Id, false);
                return null;
            }
            catch (System.Exception ex)
            {
                throw new System.Exception(ex.Message);
            }
        }

        public async Task<Evento> UpdateEvento(int eventoId, Evento model)
        {
            try
            {
                var evento = await eventoPersist.GetEventoByIdAsync(eventoId, false);
                if(evento == null) return null;
                model.Id = evento.Id;

                proEventosPersist.Update<Evento>(model);
                if(await proEventosPersist.SaveChangesAsync())
                    return await eventoPersist.GetEventoByIdAsync(model.Id, false);
                return null;
            }
            catch (System.Exception ex)
            {
                throw new System.Exception(ex.Message);
            }
        }
        public async Task<bool> DeleteEvento(int eventoId)
        {
            try
            {
                var evento = await eventoPersist.GetEventoByIdAsync(eventoId, false);
                if(evento == null) 
                    throw new System.Exception("Evento para delete não foi encontrado");

                proEventosPersist.Delete<Evento>(evento);
                return await proEventosPersist.SaveChangesAsync();
            }
            catch (System.Exception ex)
            {
                throw new System.Exception(ex.Message);
            }
        }

        public async Task<Evento[]> GetAllEventosAsync(bool includePalestrantes = false)
        {
            try
            {
                var eventos = await eventoPersist.GetAllEventosAsync(includePalestrantes);
                if(eventos == null) return null;

                return eventos;
            }
            catch (System.Exception ex)
            {
                throw new System.Exception(ex.Message);
            }
        }

        public async Task<Evento[]> GetAllEventosByTemaAsync(string tema, bool includePalestrantes = false)
        {
            try
            {
                var eventos = await eventoPersist.GetAllEventosByTemaAsync(tema, includePalestrantes);
                if(eventos == null) return null;

                return eventos;
            }
            catch (System.Exception ex)
            {
                throw new System.Exception(ex.Message);
            }
        }

        public async Task<Evento> GetEventoByIdAsync(int eventoId, bool includePalestrantes = false)
        {
            try
            {
                var evento = await eventoPersist.GetEventoByIdAsync(eventoId, includePalestrantes);
                if(evento == null) return null;

                return evento;
            }
            catch (System.Exception ex)
            {
                throw new System.Exception(ex.Message);
            }
        }

    }
}