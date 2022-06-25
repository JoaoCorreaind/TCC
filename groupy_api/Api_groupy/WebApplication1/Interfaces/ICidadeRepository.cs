using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;
using WebApplication1.Models.Localidade;

namespace WebApplication1.Interfaces
{
    public interface ICidadeRepository
    {
        Task<List<Cidade>> GetAll();
        Task<Cidade> GetById(int cidadeId);
        Task<List<Cidade>> GetByUf(string uf);
        Task<Cidade> GetByUser(int userId);

    }
}
