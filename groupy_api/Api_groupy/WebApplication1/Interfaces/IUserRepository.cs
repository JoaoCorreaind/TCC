using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace WebApplication1.Interfaces
{
    public interface IUserRepository 
    {
        Task<List<User>> GetAll();
        Task<User> GetById(int id);
        Task<User> Create(UserDto user);
        Task Delete(int id);
        Task<bool> Update(int id , User user);
        bool UserExist(int id);
        
    }
}
