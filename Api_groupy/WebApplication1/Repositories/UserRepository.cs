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
using Abp.Linq.Expressions;
using WebApplication1.Models.Tools;
using Microsoft.AspNetCore.SignalR;

namespace WebApplication1.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly Context _context;
        private readonly IHostingEnvironment _host;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IEmailRepository _emailRepository;
        private readonly IHubContext<ChatHub> _chatHub;

        private bool UserHasFriendShip(User user, string friendId)
        {
            bool friend = user.FriendShips.Find(f => f.Users.Find(u => u.Id == friendId) != null) != null;
            return friend;
        }
        public UserRepository(Context context , IHostingEnvironment host, IHubContext<ChatHub> hubContext, IEmailRepository emailRepository, UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _context = context;
            _host = host;
            _userManager = userManager;
            _signInManager = signInManager;
            _emailRepository = emailRepository;
            _chatHub = hubContext;
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


        public async Task<List<User>> GetUserList(string id)
        {
            try
            {
                var response = await _context.User.Where(u => u.Id != id).Include(x => x.InterestTags).Include(u => u.FriendShips).ThenInclude(x=> x.Users).ToListAsync();
                //response.FindAll(x => x.FriendShips.Find(x => x.Users.Find(x => x.Id == id) == null));
                //var r = response.FindAll(x=> x.FriendShips.Find(x=> x.Users.Find(x=> x.Id == id) == null))
                //var result = response.FindAll(x => x.FriendShips.Exists(x => x.Users.Find(x => x.Id == id)==null));
                var result = response.FindAll(x=> !x.FriendShips.Exists(x=> x.Users.Exists(x=> x.Id == id)));

                return result;
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
                return await _context.User.Where(u => u.Id == id).Include(u => u.Groups).Include(u=> u.InterestTags).Include(u=> u.Address).ThenInclude(u=> u.City).FirstOrDefaultAsync();
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

        public async Task<User> Update(string id, UserDto userDto)
        {
            try
            {
                //var oldUser = await _context.User.FindAsync(id);
                //User user = await _userManager.FindByIdAsync(userDto.Id);
                User user = await _context.User.Where(x => x.Id == userDto.Id).Include(x => x.InterestTags).FirstOrDefaultAsync();
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

                    var response = await Functions.UploadImage(user.Id + "-perfil-image", userDto.Image);
                    user.Image = response.PathToFile;
                }
                 
                if (!string.IsNullOrEmpty(userDto.BackgroundImage))
                {
                    var response = await Functions.UploadImage(user.Id + "-background-image", userDto.BackgroundImage);
                    user.BackgroundImage = response.PathToFile;
                }
                if(userDto.Tags != null && userDto.Tags.Count > 0)
                {
                    user.InterestTags.Clear();
                    foreach (var tagId in userDto.Tags)
                    {
                        user.InterestTags.Add(await _context.Tags.FindAsync(tagId));
                    }
                }
                await _context.SaveChangesAsync();
                //_context.Entry(updatedUser).State = EntityState.Modified;

                return user;
            }
            catch (Exception ex)
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
                    FullName = $"{userDto.FirstName} {userDto.LastName}"
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
                    ZipCode = userDto.ZipCode,
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

        public async Task<bool> CreateFriendRelationship(string userAId, string userBId)
        {
            try
            {
                //User userA = await _context.User.Where(x => x.Id == userAId).Include(x=> x.Friends).FirstOrDefaultAsync();
                //User userB = await _context.User.Where(x => x.Id == userBId).Include(x => x.Friends).FirstOrDefaultAsync();
                //userA.Friends.Add(userB);
                //userB.Friends.Add(userA);
                User userA = await _context.User.Where(x => x.Id == userAId).FirstOrDefaultAsync();
                User userB = await _context.User.Where(x => x.Id == userBId).FirstOrDefaultAsync();
                FriendShip ship = new FriendShip();
                ship.Users = new List<User>();                
                ship.Users.Add(userA);
                ship.Users.Add(userB);

                await _context.FriendShip.AddAsync(ship);
                if (await _context.SaveChangesAsync() > 0)
                {
                    return true;
                }
                return false;
            }
            catch (Exception e)
            {

                throw;
            }
            

        }

        public async Task<List<User>> Find(HttpRequest request)
        {
            try
            {
                string userId = Functions.GetStringFromUrl(request.QueryString.ToString(), "userId");
                string keyword = Functions.GetStringFromUrl(request.QueryString.ToString(), "keyword");
                bool filterTagsAnd = Functions.GetBoolFromUrl(request.QueryString.ToString(), "filterAnd");
                List<string> tags = Functions.GetListFromUrl(request.QueryString.ToString(), "tag");

                var user = await _context.User.FindAsync(userId);
                var whereClause = PredicateBuilder.New<User>(true);


                whereClause.And(x => x.Id != userId);

                if (tags != null && tags.Count > 0)
                {
                    var i = 0;
                    foreach (string tag in tags)
                    {
                        if (filterTagsAnd)
                        {
                            Tag _tag = await _context.Tags.FindAsync(int.Parse(tag));
                            whereClause.And(x => x.InterestTags.Contains(_tag));
                        }
                        else
                        {
                            if (i == 0)
                            {
                                Tag _tag = await _context.Tags.FindAsync(int.Parse(tag));
                                whereClause.And(x => x.InterestTags.Contains(_tag));

                            }
                            else
                            {
                                Tag _tag = await _context.Tags.FindAsync(int.Parse(tag));
                                whereClause.Or(x => x.InterestTags.Contains(_tag));
                            }
                        }
                        i++;

                    }
                }

                if (!string.IsNullOrEmpty(keyword))
                {
                    whereClause.And(x => x.FullName.ToLower().Contains(keyword.ToLower()));
                    whereClause.Or(x => x.About.ToLower().Contains(keyword.ToLower()));
                }

                var response = await _context.User.Where(whereClause).Include(x=> x.InterestTags).Include(x => x.FriendShips).ThenInclude(x => x.Users).ToListAsync();
                var result = response.FindAll(x => !x.FriendShips.Exists(x => x.Users.Exists(x => x.Id == userId)));

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
