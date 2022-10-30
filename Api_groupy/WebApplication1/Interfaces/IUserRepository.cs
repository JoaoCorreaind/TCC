using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace WebApplication1.Interfaces
{
    public interface IUserRepository 
    {
        Task<List<User>> GetUserList(string Id);
        Task<User> GetById(string id);
        Task<User> Create(UserDto user);
        Task<User> GetByEmail(string email);

        Task Delete(string id);
        Task<User> Update(string id , UserDto userDto);
        Task<List<Group>> GroupsByUser (string userId);
        Task<bool> CreateFriendRelationship(string userA, string userB);
        bool UserExist(string id);
        
    }
}
