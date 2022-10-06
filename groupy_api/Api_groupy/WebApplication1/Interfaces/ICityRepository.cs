using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;
using WebApplication1.Models.Localidade;

namespace WebApplication1.Interfaces
{
    public interface ICityRepository
    {
        Task<List<City>> GetAll();
        Task<City> GetById(int cidadeId);
        Task<List<City>> GetByUf(string uf);
        Task<City> GetByUser(string userId);

        Task<List<State>> GetAllUf();

    }
}
