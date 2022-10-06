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
using Microsoft.AspNetCore.Identity;
using System.Text;
using System.IO;
using Microsoft.AspNetCore.Http;

namespace WebApplication1.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly Context _context;
        private readonly IHostingEnvironment _host;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IEmailRepository _emailRepository;

        public UserRepository(Context context, IHostingEnvironment host, IEmailRepository emailRepository, UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _context = context;
            _host = host;
            _userManager = userManager;
            _signInManager = signInManager;
            _emailRepository = emailRepository;
        }
        public async Task<User> Create(UserDto userDto)
        {
            try
            {
                User user = await  PopulateUser(userDto);
                var result = await _userManager.CreateAsync(user, user.Password);
                if (result.Succeeded)
                {
                    return user;
                }
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

        public async Task<User> GetById(string id)
        {
            try
            {
                return await _context.User.Where(u => u.Id == id).Include(u => u.Groups).Include(u=> u.Address).ThenInclude(u=> u.City).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task Delete(string id)
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

        public async Task<bool> Update(string id, UserDto userDto)
        {
            try
            {
                //var oldUser = await _context.User.FindAsync(id);
                User user = await _userManager.FindByIdAsync(userDto.Id);

                user.Cpf = userDto.Cpf;
                user.Rg = userDto.Rg;
                user.FirstName = userDto.FirstName;
                user.LastName = userDto.LastName;
                user.About = userDto.About;
                user.UserName = userDto.Email;
                user.NormalizedUserName = $"{userDto.FirstName} {userDto.LastName}";

                if (!string.IsNullOrEmpty(userDto.Email))
                    user.Email = userDto.Email;
                if (!string.IsNullOrEmpty(userDto.Id))
                    user.Id = userDto.Id;

                user.Address = new Address
                {
                    City = await _context.City.FindAsync(userDto.CityId),
                    Complement = userDto.Complement,
                    District = userDto.District,
                    Number = userDto.Number,
                    PublicPlace = userDto.PublicPlace,
                    ReferencePoint = userDto.ReferencePoint,
                    Latitude = userDto.Latitude,
                    Longitude = userDto.Longitude,
                    ZipCode = userDto.ZipCode,
                };
                if (!string.IsNullOrEmpty(userDto.Image))
                {
                    byte[] imageBytes = Convert.FromBase64String(userDto.Image);
                    var fileName = $"{userDto.FirstName}-${userDto.LastName}-perfil-image-{DateTime.UtcNow:yyyy-MM-dd-HH-mm-ss}.jpg";

                    using (var stream = new MemoryStream(imageBytes))
                    {
                        var file = new FormFile(stream, 0, imageBytes.Length, "image", fileName);

                        user.Image = Functions.SaveImageInDisk(file, _host.WebRootPath).Result.Path;

                    }
                }

                var result = await _userManager.UpdateAsync(user);
                //_context.Entry(updatedUser).State = EntityState.Modified;

                if (result.Succeeded)
                {
                    return true;
                }
                return false;
            }
            catch (DbUpdateConcurrencyException ex)
            {

                throw ex;
            }

        }

        public bool UserExist(string id)
        {
            return _context.User.Any(e => e.Id == id);
        }

        /* METODOS PRIVADOS*/

        private async Task<User> PopulateUser(UserDto userDto)
        {
            try
            {
                User user = new User
                {
                    Cpf = userDto.Cpf,
                    Rg = userDto.Rg,
                    CreatedAt = DateTime.Now,
                    FirstName = userDto.FirstName,
                    LastName = userDto.LastName,
                    About = userDto.About,
                    UserName = userDto.Email,
                    NormalizedUserName = $"{userDto.FirstName} {userDto.LastName}"
                };

                if (!string.IsNullOrEmpty(userDto.Email))
                    user.Email = userDto.Email;
                if (!string.IsNullOrEmpty(userDto.Id))
                    user.Id = userDto.Id;

                user.Address = new Address
                {
                    City = await _context.City.FindAsync(userDto.CityId),
                    Complement = userDto.Complement,
                    District = userDto.District,
                    Number = userDto.Number,
                    PublicPlace = userDto.PublicPlace,
                    ReferencePoint = userDto.ReferencePoint,
                    Latitude = userDto.Latitude,
                    Longitude = userDto.Longitude,

                };
                if(!string.IsNullOrEmpty(userDto.Password))
                    user.Password = BC.HashPassword(userDto.Password);
                //if (userDto.Image != null)
                //{
                //    user.Image = Functions.SaveImageInDisk(userDto.Image, _host.WebRootPath).Result.Path;
                //};
                if (!string.IsNullOrEmpty(userDto.Image))
                {
                    byte[] imageBytes = Convert.FromBase64String(userDto.Image);
                    var fileName = $"{userDto.FirstName}-${userDto.LastName}-perfil-image-{DateTime.UtcNow:yyyy-MM-dd-HH-mm-ss}.jpg";

                    using (var stream = new MemoryStream(imageBytes))
                    {
                        var file = new FormFile(stream, 0, imageBytes.Length, "image", fileName);

                        user.Image = Functions.SaveImageInDisk(file, _host.WebRootPath).Result.Path;

                    }
                }
                return user;
            }
            catch (Exception e)
            {

                throw;
            }
        }

        public Task<List<Group>> GroupsByUser(string userId)
        {
            throw new NotImplementedException();
        }

        public Task<User> GetByEmail(string email)
        {
            return _context.User.FirstAsync(x => x.Email == email);
        }


    }
}
