using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace WebApplication1.Interfaces
{
    public interface IGrupoRepository
    {
        Task<List<Grupo>> GetAll();
        Task<Grupo> GetById(int id);
        Task<dynamic> Create(CreateGrupoDto grupo);
        Task<bool> Delete(int id);
        Task<bool> Update(int id, CreateGrupoDto grupo);
        bool GrupoExist(int id); 
        Task<Grupo> AddParticipante(AddUserDto dto);
        Task<Grupo> RemoveParticipante(AddUserDto dto);

    }
}
