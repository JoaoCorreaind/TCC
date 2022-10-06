using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using WebApplication1.Interfaces;
using WebApplication1.Models;
using WebApplication1.Tools.DataBase;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupController : ControllerBase
    {
        private readonly Context _context;
        private readonly IGroupRepository _grupoRepository;
        public GroupController(Context context, IGroupRepository grupoRepository)
        {
            _context = context;
            _grupoRepository = grupoRepository;
        }

        // GET: api/grupo
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Group>>> GetAll()
        {
            return await _grupoRepository.GetAll();
        }

        // GET: api/grupo/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Group>> Get(int id)
        {
            var response = await _grupoRepository.GetById(id);
            

            if (response == null)
                return NotFound();

            return response;
        }

        // POST: api/grupo
        [HttpPost]
        public async Task<ActionResult<IEnumerable<Group>>> Create([FromBody] CreateGrupoDto request)
        {

            var response = await _grupoRepository.Create(request);
            if (response == null)
                return NotFound();

            Response.StatusCode = (int)HttpStatusCode.Created;
            return Ok(new { user = response });
        }

        // GET: api/grupo/5
        [HttpPut("{id}")]
        public async Task<ActionResult<IEnumerable<Group>>> Update([FromBody] CreateGrupoDto request, int id)
        {
            var response = await _grupoRepository.Update(id, request);
            if (!response)
                return BadRequest();

            return Ok(new { message = "Atualizado com Sucesso" });
        }

        // GET: api/grupo/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<IEnumerable<Group>>> Delete(int id)
        {
            if (!_grupoRepository.GrupoExist(id))
            {
                return NotFound();
            }

            var response = await _grupoRepository.Delete(id);

            if (!response)
            {
                return BadRequest();

            }
            return Ok(new { message = "Atualizado com Sucesso" });
        }
        // GET: api/grupo/5
        [HttpPost("addMember")]
        public async Task<ActionResult<IEnumerable<Group>>> AddUser([FromBody] AddUserDto dto)
        {
            var response = await _grupoRepository.AddParticipante(dto);
            if (response == null)
            {
                return NotFound();
            }

            return Ok(new { message = "Membro adicionado com sucesso", grupo = response });
        }

        // GET: api/grupo/5
        [HttpPost("removeMember")]
        public async Task<ActionResult<IEnumerable<Group>>> RemoveParticipante([FromBody] AddUserDto dto)
        {
            var response = await _grupoRepository.RemoveParticipante(dto);
            if (response == null)
            {
                return NotFound();
            }

            return Ok(new { message = "Membro removido com sucesso", grupo = response });
        }
        // GETBYUSER: api/grupo/5/user
        [HttpGet("{id}/user")]
        public async Task<ActionResult<List<Group>>> GetByUser(string id)
        {
            var response = await _grupoRepository.GetByUser(id);


            if (response == null)
                return NotFound();

            return response;
        }
        // GETBYUSER: api/grupo/5/leader
        [HttpGet("{id}/leader")]
        public async Task<ActionResult<List<Group>>> GetByLeader(string id)
        {
            var response = await _grupoRepository.GetByLeader(id);


            if (response == null)
                return NotFound();

            return response;
        }

        // GET MEMBERS: api/getMembers/1
        [HttpGet("getMembers/{idGrupo}")]
        public async Task<ActionResult<List<User>>> GetMembers(int idGrupo)
        {
            var response = await _grupoRepository.GetMembers(idGrupo);


            if (response == null)
                return NotFound();

            return response;
        }
    }
}
