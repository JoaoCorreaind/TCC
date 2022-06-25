using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using WebApplication1.Interfaces;
using WebApplication1.Models;
using WebApplication1.Models.Localidade;
using WebApplication1.Tools.DataBase;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CidadeController : ControllerBase
    {
        private readonly Context _context;
        private readonly ICidadeRepository _cidadeRepository;
        public CidadeController(Context context, ICidadeRepository cidadeRepository)
        {
            _context = context;
            _cidadeRepository = cidadeRepository;
        }

        // GET: api/cidade
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cidade>>> GetAll()
        {
            return await _cidadeRepository.GetAll();
        }

        // GET: api/cidade/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Cidade>> GetById(int id)
        {
            var response = await _cidadeRepository.GetById(id);


            if (response == null)
                return NotFound();

            return response;
        }

        // GetbyUser: api/cidade/5/user
        [HttpGet("{id}/user")]
        public async Task<ActionResult<Cidade>> GetByUser(int id)
        {
            var response = await _cidadeRepository.GetByUser(id);


            if (response == null)
                return NotFound();

            return response;
        }

        // GET MEMBERS: api/cidade/1/uf
        [HttpGet("{id}/uf")]
        public async Task<ActionResult<List<Cidade>>> GetMembers(string uf)
        {
            var response = await _cidadeRepository.GetByUf(uf);


            if (response == null)
                return NotFound();

            return response;
        }
    }
}
