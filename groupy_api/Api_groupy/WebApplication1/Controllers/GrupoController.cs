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
    public class GrupoController : ControllerBase
    {
        private readonly Context _context;
        private readonly IGrupoRepository _grupoRepository;
        public GrupoController(Context context, IGrupoRepository grupoRepository)
        {
            _context = context;
            _grupoRepository = grupoRepository;
        }

        // GET: api/grupo
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Grupo>>> GetAll()
        {
            return await _grupoRepository.GetAll();
        }

        // GET: api/grupo/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Grupo>> Get(int id)
        {
            var response = await _grupoRepository.GetById(id);
            

            if (response == null)
                return NotFound();

            return response;
        }

        // POST: api/grupo
        [HttpPost]
        public async Task<ActionResult<IEnumerable<Grupo>>> Create([FromBody] CreateGrupoDto request)
        {
            var response = await _grupoRepository.Create(request);
            if (response == null)
                return NotFound();

            Response.StatusCode = (int)HttpStatusCode.Created;
            return Ok(new { user = response });
        }

        // GET: api/grupo/5
        [HttpPut("{id}")]
        public async Task<ActionResult<IEnumerable<Grupo>>> Update([FromBody] CreateGrupoDto request, int id)
        {
            var response = await _grupoRepository.Update(id, request);
            if (!response)
                return BadRequest();

            return Ok(new { message = "Atualizado com Sucesso" });
        }

        // GET: api/grupo/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<IEnumerable<Grupo>>> Delete(int id)
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
        [HttpPost("addParticipante")]
        public async Task<ActionResult<IEnumerable<Grupo>>> AddUser([FromBody] AddUserDto dto)
        {
            var response = await _grupoRepository.AddParticipante(dto);
            if (response == null)
            {
                return NotFound();
            }

            return Ok(new { message = "Atualizado com Sucesso", grupo = response });
        }

        // GET: api/grupo/5
        [HttpPost("removeParticipante")]
        public async Task<ActionResult<IEnumerable<Grupo>>> RemoveParticipante([FromBody] AddUserDto dto)
        {
            var response = await _grupoRepository.RemoveParticipante(dto);
            if (response == null)
            {
                return NotFound();
            }

            return Ok(new { message = "Atualizado com Sucesso", grupo = response });
        }
    }
}
