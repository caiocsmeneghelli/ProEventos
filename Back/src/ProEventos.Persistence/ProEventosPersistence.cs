using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProEventos.Domain.Models;

namespace ProEventos.Persistence
{
    public class ProEventosPersistence : IProEventosPersistence
    {
        public ProEventosContext _context { get; }

        public ProEventosPersistence(ProEventosContext Context)
        {
            _context = Context;
        }
        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }
        public void Update<T>(T entity) where T : class
        {
            _context.Update(entity);
        }
        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }
        public void DeleteRange<T>(T[] entityArray) where T : class
        {
            _context.RemoveRange(entityArray);
        }
        // Ao realizar o SaveChanges Async, caso seja maior que 0, foi salvo.
        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync() > 0);
        }

        // Eventos
        public async Task<Evento> GetEventoByIdAsync(int eventoId, bool includePalestrantes = false)
        {
            IQueryable<Evento> query = _context.Eventos
                .Include(reg => reg.Lotes)
                .Include(reg => reg.RedesSociais);
            
            if(includePalestrantes)
                query = query.Include(reg => reg.PalestrantesEventos)
                .ThenInclude(reg => reg.Palestrante);

            query = query
                .OrderBy(reg => reg.Id)
                .Where(reg => reg.Id == eventoId);
            return await query.FirstOrDefaultAsync();
        }
        public async Task<Evento[]> GetAllEventosAsync(bool includePalestrantes = false)
        {
            IQueryable<Evento> query = _context.Eventos
                .Include(reg => reg.Lotes)
                .Include(reg => reg.RedesSociais);
            
            if(includePalestrantes)
                query = query.Include(reg => reg.PalestrantesEventos)
                .ThenInclude(reg => reg.Palestrante);

            query = query.OrderBy(reg => reg.Id);
            return await query.ToArrayAsync();
        }
        public async Task<Evento[]> GetAllEventosByTemaAsync(string tema, bool includePalestrantes = false)
        {
            IQueryable<Evento> query = _context.Eventos
                .Include(reg => reg.Lotes)
                .Include(reg => reg.RedesSociais);
            
            if(includePalestrantes)
                query = query.Include(reg => reg.PalestrantesEventos)
                .ThenInclude(reg => reg.Palestrante);

            query = query
                .OrderBy(reg => reg.Id)
                .Where(reg => reg.Tema.ToLower().Contains(tema.ToLower()));
            return await query.ToArrayAsync();
        }

        // Paleestrantes
        public async Task<Palestrante> GetPalestranteByIdAsync(int palestranteId, bool includeEventos)
        {
            IQueryable<Palestrante> query = _context.Palestrantes
                .Include(reg => reg.RedesSociais);
            
            if(includeEventos)
                query = query.Include(reg => reg.PalestrantesEventos)
                .ThenInclude(reg => reg.Evento);

            return await query.FirstOrDefaultAsync();
        }
        public async Task<Palestrante[]> GetAllPalestrantesAsync(bool includeEventos)
        {
            IQueryable<Palestrante> query = _context.Palestrantes
                .Include(reg => reg.RedesSociais);
            
            if(includeEventos)
                query = query.Include(reg => reg.PalestrantesEventos)
                .ThenInclude(reg => reg.Evento);

            query = query.OrderBy(reg => reg.Id);
            return await query.ToArrayAsync();
        }
        public async Task<Palestrante[]> GetAllPalestrantesByNomeAsync(string nome, bool includeEventos)
        {
            IQueryable<Palestrante> query = _context.Palestrantes
                .Include(reg => reg.RedesSociais);
            
            if(includeEventos)
                query = query.Include(reg => reg.PalestrantesEventos)
                .ThenInclude(reg => reg.Evento);

            query = query.OrderBy(reg => reg.Id)
                .Where(reg => reg.Nome.ToLower().Contains(nome.ToLower()));
            return await query.ToArrayAsync();
        }
    }
}