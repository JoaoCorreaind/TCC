using Abp.Linq.Expressions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting.Internal;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using WebApplication1.Interfaces;
using WebApplication1.Models;
using WebApplication1.Tools;
using WebApplication1.Tools.DataBase;

namespace WebApplication1.Repositories
{
    public class GroupRepository : IGroupRepository
    {
        private readonly Context _context;
        private readonly IHostingEnvironment _host;

        public GroupRepository(Context context, IHostingEnvironment host)
        {
            _context = context;
            _host = host;
        }
        public async Task<dynamic> Create(CreateGrupoDto dto)
        {
            try
            {
                List<Tag> listTag = new List<Tag>();
                if (dto.Tags != null && dto.Tags.Count > 0)
                {
                    foreach (int id in dto.Tags)
                    {
                        listTag.Add(await _context.Tags.FindAsync(id));
                    }
                }
                Group grupo = new Group
                {
                    Title = dto.Title,
                    CreatedAt = dto.CreatedAt,
                    IsDeleted = false,
                    UserLimit = dto.UserLimit,
                    Description = dto.Descrition,
                    IsPresencial = dto.IsPresencial,
                    Tags = listTag,
                };
                if (grupo.IsPresencial)
                {
                    grupo.Address = new Address
                    {
                        City = await _context.City.FindAsync(dto.CityId),
                        Complement = dto.Complement,
                        District = dto.District,
                        Number = dto.Number,
                        PublicPlace = dto.PublicPlace,
                        ReferencePoint = dto.ReferencePoint,
                        Latitude = dto.Latitude,
                        Longitude = dto.Longitude,

                    };
                }
                
                grupo.LeaderId = dto.LiderId;
                grupo.Participants = new List<User>();
                grupo.Participants.Add(await _context.User.FindAsync(dto.LiderId));

                if (!string.IsNullOrEmpty(dto.GroupyMainImage))
                {
                    //byte[] imageBytes = Convert.FromBase64String(dto.GroupyMainImage);
                    //var fileName = $"{dto.Title}-main-image-{DateTime.UtcNow:yyyy-MM-dd-HH-mm-ss}.jpg";

                    //using (var stream = new MemoryStream(imageBytes))
                    //{
                    //    var file = new FormFile(stream, 0, imageBytes.Length, "image", fileName);

                    //    grupo.GroupMainImage = await Functions.SaveImageInDisk(file, _host.WebRootPath);

                    //}
                    var result = await Functions.UploadImage(dto.Title + "-GroupyMainImage", dto.GroupyMainImage);
                    grupo.GroupMainImage = new ImageModel
                    {
                        Name = result.FileName,
                        Path = result.PathToFile,
                    };
                }

                if (dto.GroupyImages != null && dto.GroupyImages.Count > 0)
                {

                    grupo.GroupImages = new List<ImageModel>();
                    int index = 0;
                    foreach (string fileString in dto.GroupyImages)
                    {
                        //byte[] imageBytes = Convert.FromBase64String(fileString);
                        //var fileName = $"{dto.Title}-image-{index}-{DateTime.UtcNow:yyyy-MM-dd-HH-mm-ss}.jpg";

                        //using (var stream = new MemoryStream(imageBytes))
                        //{
                        //    var file = new FormFile(stream, 0, imageBytes.Length, "image", fileName);
                        //    grupo.GroupImages.Add(await Functions.SaveImageInDisk(file, _host.WebRootPath));
                        //}

                        var result = await Functions.UploadImage(dto.Title + $"-group-image-{index}", fileString);
                        grupo.GroupImages.Add(new ImageModel
                        {
                            Name = result.FileName,
                            Path = result.PathToFile,
                        });
                        index++;
                    }
                }
                if (listTag.Count > 0)
                {
                    grupo.Tags = listTag;
                }
                _context.Group.Add(grupo);
                var response = await _context.SaveChangesAsync();

                if (grupo.Id != 0)
                    return grupo;
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> Delete(int id)
        {
            Group grupo = _context.Group.SingleOrDefault(b => b.Id == id);
            grupo.IsDeleted = true;
            grupo.DeletedAt = DateTime.Now;
            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateConcurrencyException ex)
            {

                throw ex;
            }
        }

        public async Task<List<Group>> Find(HttpRequest request)
        {
            try
            {
                string userId = Functions.GetStringFromUrl(request.QueryString.ToString(), "userId");
                bool participant = Boolean.Parse(Functions.GetStringFromUrl(request.QueryString.ToString(), "participant"));
                DateTime? initialDate = Functions.GetDateFromUrl(request.QueryString.ToString(), "initialDate");
                DateTime? finalDate = Functions.GetDateFromUrl(request.QueryString.ToString(), "finalDate");
                string keyword = Functions.GetStringFromUrl(request.QueryString.ToString(), "keyword");
                var user = await _context.User.FindAsync(userId);
                var whereClause = PredicateBuilder.New<Group>(true);

                if (participant)
                {
                    whereClause.And(x => x.Participants.Contains(user));
                }
                else
                {
                    whereClause.And(x => !x.Participants.Contains(user));

                }
                if (!string.IsNullOrEmpty(keyword))
                {
                    whereClause.And(x => x.Title.Contains(keyword));
                }
                var response = await  _context.Group.Where(whereClause).Include(x=> x.Participants).Include(x=> x.GroupImages).Include(x=> x.GroupMainImage).ToListAsync();

                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Group> GetById(int id)
        {
            try
            {
                //return await _context.Group.FindAsync(id);
                var response =  await _context.Group
                    .Where(g => g.Id == id)      
                    .Include(c=> c.Leader)
                    .Include(c => c.Participants)
                    .Include(c => c.Tags)
                    .Include(c=> c.GroupImages)
                    .Include(c => c.GroupMainImage)
                    .Include(c => c.Tags)
                    .Include(c=> c.Address)
                    .ThenInclude(a=> a.City)
                    .FirstOrDefaultAsync();
                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool GrupoExist(int id)
        {
            return _context.Group.Any(e => e.Id == id);
        }

        public async Task<bool> Update(int id, CreateGrupoDto dto)
        {
            try
            {
                List<Tag> listTag = new List<Tag>();

                Group grupo = await GetById(id);
                if (dto.Tags != null && dto.Tags.Count > 0 )
                {
                    foreach (int idTag in dto.Tags)
                    {
                        listTag.Add(await _context.Tags.FindAsync(idTag));
                    }
                }
                
                grupo.LeaderId = dto.LiderId;
                grupo.Leader = _context.User.Find(dto.LiderId);
    
                grupo.CreatedAt = dto.CreatedAt;
                grupo.UserLimit = dto.UserLimit;
                grupo.Description = dto.Descrition;
                grupo.Tags = listTag;
                if (!string.IsNullOrEmpty(dto.GroupyMainImage))
                {
                    //byte[] imageBytes = Convert.FromBase64String(dto.GroupyMainImage);
                    //var fileName = $"{dto.Title}-main-image-{DateTime.UtcNow:yyyy-MM-dd-HH-mm-ss}.jpg";

                    //using (var stream = new MemoryStream(imageBytes))
                    //{
                    //    var file = new FormFile(stream, 0, imageBytes.Length, "image", fileName);

                    //    grupo.GroupMainImage = await Functions.SaveImageInDisk(file, _host.WebRootPath);

                    //}
                    var result = await Functions.UploadImage(dto.Title + "-GroupyMainImage", dto.GroupyMainImage);
                    grupo.GroupMainImage = new ImageModel
                    {
                        Name = result.FileName,
                        Path = result.PathToFile,
                    };
                }

                if (dto.GroupyImages != null && dto.GroupyImages.Count > 0)
                {

                    grupo.GroupImages = new List<ImageModel>();
                    int index = 0;
                    foreach (string fileString in dto.GroupyImages)
                    {
                        //byte[] imageBytes = Convert.FromBase64String(fileString);
                        //var fileName = $"{dto.Title}-image-{index}-{DateTime.UtcNow:yyyy-MM-dd-HH-mm-ss}.jpg";

                        //using (var stream = new MemoryStream(imageBytes))
                        //{
                        //    var file = new FormFile(stream, 0, imageBytes.Length, "image", fileName);
                        //    grupo.GroupImages.Add(await Functions.SaveImageInDisk(file, _host.WebRootPath));
                        //}
                        var result = await Functions.UploadImage(dto.Title + $"-group-image-{index}", fileString);
                        grupo.GroupImages.Add(new ImageModel
                        {
                            Name = result.FileName,
                            Path = result.PathToFile,
                        });
                        index++;
                    }
                }
                //var oldUser = await _context.User.FindAsync(id);
                _context.Entry(grupo).State = EntityState.Modified;

                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw ex;
            }
        }
        public async Task<Group> AddParticipante(string userId, int groupId)
        {
            var grupo = await _context.Group.Where(g=> g.Id == groupId).Include(g=> g.Participants).FirstOrDefaultAsync();
            if (grupo == null)
                return null;

            var user = await _context.User.FindAsync(userId);
            if (user == null)
                return null;

            grupo.Participants.Add(user);
            await _context.SaveChangesAsync();

            return grupo;
        }

        public async Task<Group> RemoveParticipante(AddUserDto dto)
        {
            var grupo = await _context.Group.Where(g => g.Id == dto.GrupoId).Include(g => g.Participants).FirstOrDefaultAsync();
            if (grupo == null)
                return null;

            var user = await _context.User.FindAsync(dto.UserId);
            if (user == null)
                return null;

            grupo.Participants.Remove(user);
            await _context.SaveChangesAsync();

            return grupo;
        }
        public async Task<List<Group>> GetByUser(string id)
        {
            try
            {
                //return await _context.Group.FindAsync(id);
                var response = await _context.Group
                    .Where(a => a.Participants.Any(x => x.Id == id))
                    .Include(c => c.Leader)
                    .Include(c => c.Participants)
                    .Include(c => c.Tags)
                    .Include(c=> c.GroupImages)
                    .Include(c => c.GroupMainImage)
                    .ToListAsync();
                return response;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<List<Group>> GetByLeader(string id)
        {
            try
            {
                //return await _context.Group.FindAsync(id);
                var response = await _context.Group
                    .Where(g => g.LeaderId == id)
                    .Include(c => c.Leader)
                    .Include(c => c.Participants)
                    .Include(c => c.Tags)
                    .ToListAsync();
                return response;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<User>> GetMembers(int idGrupo)
        {
            try
            {
                var grupo = await _context.Group.Where(g => g.Id == idGrupo).Include(g => g.Participants).FirstOrDefaultAsync();
                return grupo.Participants;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<List<User>> GroupsByUser(string userId)
        {
            try
            {
                var grupo = await _context.Group.Where(g => g.Participants.Any(g=> g.Id == userId)).Include(g => g.Participants).FirstOrDefaultAsync();
                return grupo.Participants;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
