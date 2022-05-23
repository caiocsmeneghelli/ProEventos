using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProEventos.Domain.Models;

namespace ProEventos.Persistence
{
    public class PaleestrantesPersistence : ProEventosPersistence
    {
        public PaleestrantesPersistence(ProEventosContext Context) : base(Context)
        { }
        
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