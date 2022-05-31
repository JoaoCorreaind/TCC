﻿using Microsoft.EntityFrameworkCore;
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

namespace WebApplication1.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly Context _context;
        public UserRepository(Context context)
        {
            _context = context;
        }
        public async Task<bool> Create(User user)
        {
            try
            {
                user.Password = BC.HashPassword(user.Password);
                _context.User.Add(user);
                var response = await _context.SaveChangesAsync();
                if (response != 1)
                    return false;
                return true;
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