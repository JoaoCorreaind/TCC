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
        Task<User> GetById(string id);
        Task<User> Create(UserDto user);
        Task<User> GetByEmail(string email);

        Task Delete(string id);
        Task<bool> Update(string id , UserDto userDto);
        Task<List<Group>> GroupsByUser (string userId);
        bool UserExist(string id);
        
    }
}
