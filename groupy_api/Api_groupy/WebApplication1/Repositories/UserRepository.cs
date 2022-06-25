using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Interfaces;
using WebApplication1.Models;
using WebApplication1.Services;
using WebApplication1.Tools;
using WebApplication1.Tools.DataBase;
using BC = BCrypt.Net.BCrypt;
using Microsoft.AspNetCore.Hosting;

namespace WebApplication1.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly Context _context;
        private readonly IHostingEnvironment _host;

        public UserRepository(Context context, IHostingEnvironment host)
        {
            _context = context;
            _host = host;

        }
        public async Task<User> Create(UserDto userDto)
        {
            try
            {
                User user = new User {
                    Email = userDto.Email,
                    Cpf = userDto.Cpf,
                    Rg = userDto.Rg,
                    CreatedAt = DateTime.Now,
                    Nome = userDto.Nome,
                };
                user.Password = BC.HashPassword(userDto.Password);
                if (userDto.Image != null)
                {
                    user.Image = Functions.SaveImageInDisk(userDto.Image, _host.WebRootPath).Result.Path;
                };
                if(userDto.CidadeId != 0)
                {
                    user.Cidade = await _context.Cidade.FindAsync(userDto.CidadeId);
                }
                _context.User.Add(user);
                var response = await _context.SaveChangesAsync();
                if (response == 1)
                    return user;
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }


        public async Task<List<User>> GetAll()
        {
            try
            {
                return await _context.User.ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<User> GetById(int id)
        {
            try
            {
                return await _context.User.FindAsync(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task Delete(int id)
        {
            User user = _context.User.SingleOrDefault(b => b.Id == id);
            if (user != null)
            {
                user.IsDeleted = true;
                user.DeletedAt = DateTime.Now;
                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException ex)
                {

                    throw ex;
                }
            }
        }

        public async Task<bool> Update(int id, User updatedUser)
        {
            try
            {
                //var oldUser = await _context.User.FindAsync(id);
                _context.Entry(updatedUser).State = EntityState.Modified;

                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateConcurrencyException ex)
            {

                throw ex;
            }
            
        }

        public bool UserExist(int id)
        {
            return  _context.User.Any(e => e.Id == id);
        }
    }
}
