using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Interfaces;
using WebApplication1.Models;
using WebApplication1.Tools.DataBase;

namespace WebApplication1.Repositories
{
    public class CityRepository : ICityRepository
    {
        private readonly Context _context;

        public CityRepository(Context context)
        {
            _context = context;
        }
        public async Task<List<City>> GetAll()
        {
            return await _context.City.ToListAsync();
        }

        public async Task<City> GetById(int cidadeId)
        {
            return await _context.City.FindAsync(cidadeId);
        }

        public async Task<List<City>> GetByUf(string uf)
        {
            return await _context.City.Where(g => g.StateId.Equals(uf)).ToListAsync();
        }

        public async Task<List<State>> GetAllUf()
        {
            var response =  await _context.State.ToListAsync();
            return response;
        }
    }
}
