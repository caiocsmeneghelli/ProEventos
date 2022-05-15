using System.Threading.Tasks;
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
        public Task<Evento> GetEventoByIdAsync(int eventoId, bool includePalestrantes)
        {
            throw new System.NotImplementedException();
        }
        public Task<Evento[]> GetAllEventosAsync(bool includePalestrantes)
        {
            throw new System.NotImplementedException();
        }
        public Task<Evento[]> GetAllEventosByTemaAsync(string tema, bool includePalestrantes)
        {
            throw new System.NotImplementedException();
        }

        // Paleestrantes
        public Task<Palestrante> GetPalestranteByIdAsync(int palestranteId, bool includeEventos)
        {
            throw new System.NotImplementedException();
        }
        public Task<Palestrante[]> GetAllPalestrantesAsync(bool includeEventos)
        {
            throw new System.NotImplementedException();
        }
        public Task<Palestrante[]> GetAllPalestrantesByNomeAsync(string nome, bool includeEventos)
        {
            throw new System.NotImplementedException();
        }
    }
}