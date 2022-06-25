using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting.Internal;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Interfaces;
using WebApplication1.Models;
using WebApplication1.Tools;
using WebApplication1.Tools.DataBase;

namespace WebApplication1.Repositories
{
    public class GrupoRepository : IGrupoRepository
    {
        private readonly Context _context;
        private readonly IHostingEnvironment _host;

        public GrupoRepository(Context context, IHostingEnvironment host)
        {
            _context = context;
            _host = host;
        }
        public async Task<dynamic> Create(CreateGrupoDto dto)
        {
            try
            {
                List<Tag> listTag = new List<Tag>();
                foreach (int id in dto.Tags)
                {
                    listTag.Add(await _context.Tags.FindAsync(id));
                }
                Grupo grupo = new Grupo
                {
                    CreatedAt = dto.CreatedAt,
                    IsDeleted = false,
                    MaximoUsuarios = dto.MaximoUsuarios,
                    Descricao = dto.Descricao,
                    Tags = listTag,
                };
                grupo.LiderId = dto.LiderId;
                grupo.Participantes = new List<User>();
                //grupo.Participantes.Add(await _context.User.FindAsync(dto.LiderId));
                if (dto.GroupyMainImage != null)
                {
                    grupo.GrupoMainImage = Functions.SaveImageInDisk(dto.GroupyMainImage, _host.WebRootPath).Result.Path;
                };
                if (dto.GroupyImages != null)
                {
                    grupo.GrupoImages = new List<ImageModel>();
                    foreach (IFormFile file in dto.GroupyImages)
                    {
                        grupo.GrupoImages.Add(await Functions.SaveImageInDisk(file, _host.WebRootPath));
                    }
                }
                
                _context.Grupo.Add(grupo);
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
            Grupo grupo = _context.Grupo.SingleOrDefault(b => b.Id == id);
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

        public async Task<List<Grupo>> GetAll()
        {
            try
            {
                return await _context.Grupo.Include(g=> g.Tags).Include(g=>g.Participantes).Include(g=>g.Lider).Include(g=> g.GrupoImages).ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Grupo> GetById(int id)
        {
            try
            {
                //return await _context.Grupo.FindAsync(id);
                var response =  await _context.Grupo
                    .Where(g => g.Id == id)      
                    .Include(c=> c.Lider)
                    .Include(c => c.Participantes)
                    .Include(c => c.Tags)
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
            return _context.Grupo.Any(e => e.Id == id);
        }

        public async Task<bool> Update(int id, CreateGrupoDto dto)
        {
            try
            {
                List<Tag> listTag = new List<Tag>();
                List<User> listParticipantes = new List<User>();

                Grupo grupo = await GetById(id);
                foreach (int idTag in dto.Tags)
                {
                    listTag.Add(await _context.Tags.FindAsync(idTag));
                }
                grupo.LiderId = dto.LiderId;
                //grupo.Lider = _context.User.Find(dto.LiderId);
    
                grupo.CreatedAt = dto.CreatedAt;
                grupo.MaximoUsuarios = dto.MaximoUsuarios;
                grupo.Descricao = dto.Descricao;
                grupo.Tags = listTag;
                grupo.Participantes = listParticipantes;
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
        public async Task<Grupo> AddParticipante(AddUserDto dto)
        {
            var grupo = await _context.Grupo.Where(g=> g.Id == dto.GrupoId).Include(g=> g.Participantes).FirstOrDefaultAsync();
            if (grupo == null)
                return null;

            var user = await _context.User.FindAsync(dto.UserId);
            if (user == null)
                return null;

            grupo.Participantes.Add(user);
            await _context.SaveChangesAsync();

            return grupo;
        }

        public async Task<Grupo> RemoveParticipante(AddUserDto dto)
        {
            var grupo = await _context.Grupo.Where(g => g.Id == dto.GrupoId).Include(g => g.Participantes).FirstOrDefaultAsync();
            if (grupo == null)
                return null;

            var user = await _context.User.FindAsync(dto.UserId);
            if (user == null)
                return null;

            grupo.Participantes.Remove(user);
            await _context.SaveChangesAsync();

            return grupo;
        }
        public async Task<List<Grupo>> GetByUser(int id)
        {
            try
            {
                //return await _context.Grupo.FindAsync(id);
                var response = await _context.Grupo
                    .Where(a => a.Participantes.Any(x => x.Id == id))
                    .Include(c => c.Lider)
                    .Include(c => c.Participantes)
                    .Include(c => c.Tags)
                    .Include(c=> c.GrupoImages)
                    .ToListAsync();
                return response;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<List<Grupo>> GetByLeader(int id)
        {
            try
            {
                //return await _context.Grupo.FindAsync(id);
                var response = await _context.Grupo
                    .Where(g => g.LiderId == id)
                    .Include(c => c.Lider)
                    .Include(c => c.Participantes)
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
                var grupo = await _context.Grupo.Where(g => g.Id == idGrupo).Include(g => g.Participantes).FirstOrDefaultAsync();
                return grupo.Participantes;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
