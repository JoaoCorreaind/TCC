﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace WebApplication1.Interfaces
{
    public interface IGroupRepository
    {
        Task<List<Group>> Find(HttpRequest parameters);
        Task<Group> GetById(int id);
        Task<dynamic> Create(CreateGrupoDto grupo);
        Task<bool> Delete(int id);
        Task<bool> Update(int id, CreateGrupoDto grupo);
        bool GrupoExist(int id); 
        Task<Group> AddParticipante(string userId, int groupId);
        Task<Group> RemoveParticipante(AddUserDto dto);
        Task<List<Group>> GetByUser(string id);
        Task<List<Group>> GetByLeader(string id);
        Task<List<User>> GetMembers(int idGrupo);

    }
}
