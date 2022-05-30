using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProEventos.Domain.Models;
using ProEventos.Persistence.Contextos;
using ProEventos.Persistence.Contratos;

namespace ProEventos.Persistence
{
    public class PalestrantesPersistence : IPalestrantePersis
    {
        public ProEventosContext _context { get; }

        public PalestrantesPersistence(ProEventosContext Context)
        {
            _context = Context;
        }
        
        public async Task<Palestrante> GetPalestranteByIdAsync(int palestranteId, bool includeEventos)
        {
            IQueryable<Palestrante> query = _context.Palestrantes
                .Include(reg => reg.RedesSociais);
            
            if(includeEventos)
                query = query.Include(reg => reg.PalestrantesEventos)
                .ThenInclude(reg => reg.Evento);

            return await query.AsNoTracking().FirstOrDefaultAsync();
        }
        public async Task<Palestrante[]> GetAllPalestrantesAsync(bool includeEventos)
        {
            IQueryable<Palestrante> query = _context.Palestrantes
                .Include(reg => reg.RedesSociais);
            
            if(includeEventos)
                query = query.Include(reg => reg.PalestrantesEventos)
                .ThenInclude(reg => reg.Evento);

            query = query.OrderBy(reg => reg.Id);
            return await query.AsNoTracking().ToArrayAsync();
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
            return await query.AsNoTracking().ToArrayAsync();
        }
    }
}