using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Interfaces;
using WebApplication1.Models.Localidade;
using WebApplication1.Tools.DataBase;

namespace WebApplication1.Repositories
{
    public class CidadeRepository : ICidadeRepository
    {
        private readonly Context _context;

        public CidadeRepository(Context context)
        {
            _context = context;
        }
        public async Task<List<Cidade>> GetAll()
        {
            return await _context.Cidade.ToListAsync();
        }

        public async Task<Cidade> GetById(int cidadeId)
        {
            return await _context.Cidade.FindAsync(cidadeId);
        }

        public async Task<List<Cidade>> GetByUf(string uf)
        {
            return await _context.Cidade.Where(g => g.Estado.Uf.Equals(uf)).ToListAsync();
        }

        public async Task<Cidade> GetByUser(int userId)
        {
            return await _context.Cidade.Where(g => g.Users.Any(x => x.Id == userId)).FirstOrDefaultAsync();
        }
    }
}
