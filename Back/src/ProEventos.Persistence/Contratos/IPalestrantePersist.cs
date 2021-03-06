using System.Threading.Tasks;
using ProEventos.Domain.Models;

namespace ProEventos.Persistence.Contratos
{
    public interface IPalestrantePersis
    {
        Task<Palestrante[]> GetAllPalestrantesByNomeAsync(string nome, bool includeEventos);
        Task<Palestrante[]> GetAllPalestrantesAsync(bool includeEventos);
        Task<Palestrante> GetPalestranteByIdAsync(int palestranteId, bool includeEventos);
    }
}