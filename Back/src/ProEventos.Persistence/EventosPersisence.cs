using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProEventos.Domain.Models;
using ProEventos.Persistence.Contextos;
using ProEventos.Persistence.Contratos;

namespace ProEventos.Persistence
{
    public class EventosPersistence : IEventoPersist
    {
        public ProEventosContext _context { get; }
        public EventosPersistence(ProEventosContext Context)
        {
            _context = Context;
            // _context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        public async Task<Evento> GetEventoByIdAsync(int eventoId, bool includePalestrantes = false)
        {
            IQueryable<Evento> query = _context.Eventos
                .Include(reg => reg.Lotes)
                .Include(reg => reg.RedesSociais);

            if (includePalestrantes)
                query = query.Include(reg => reg.PalestrantesEventos)
                .ThenInclude(reg => reg.Palestrante);

            query = query
                .OrderBy(reg => reg.Id)
                .Where(reg => reg.Id == eventoId);
            return await query.AsNoTracking().FirstOrDefaultAsync();
        }


        public async Task<Evento[]> GetAllEventosAsync(bool includePalestrantes = false)
        {
            IQueryable<Evento> query = _context.Eventos
                .Include(reg => reg.Lotes)
                .Include(reg => reg.RedesSociais);

            if (includePalestrantes)
                query = query.Include(reg => reg.PalestrantesEventos)
                .ThenInclude(reg => reg.Palestrante);

            query = query.OrderBy(reg => reg.Id);
            return await query.AsNoTracking().ToArrayAsync();
        }
        public async Task<Evento[]> GetAllEventosByTemaAsync(string tema, bool includePalestrantes = false)
        {
            IQueryable<Evento> query = _context.Eventos
                .Include(reg => reg.Lotes)
                .Include(reg => reg.RedesSociais);

            if (includePalestrantes)
                query = query.Include(reg => reg.PalestrantesEventos)
                .ThenInclude(reg => reg.Palestrante);

            query = query
                .OrderBy(reg => reg.Id)
                .Where(reg => reg.Tema.ToLower().Contains(tema.ToLower()));
            return await query.AsNoTracking().ToArrayAsync();
        }
    }
}