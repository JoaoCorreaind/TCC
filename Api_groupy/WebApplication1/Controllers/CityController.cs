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
    public class CityController : ControllerBase
    {
        private readonly Context _context;
        private readonly ICityRepository _cidadeRepository;
        public CityController(Context context, ICityRepository cidadeRepository)
        {
            _context = context;
            _cidadeRepository = cidadeRepository;
        }

        // GET: api/cidade
        [HttpGet]
        public async Task<ActionResult<IEnumerable<City>>> GetAll()
        {
            return await _cidadeRepository.GetAll();
        }

        // GET: api/cidade/5
        [HttpGet("{id}")]
        public async Task<ActionResult<City>> GetById(int id)
        {
            var response = await _cidadeRepository.GetById(id);


            if (response == null)
                return NotFound();

            return response;
        }


        // GET MEMBERS: api/cidade/1/uf
        [HttpGet("{id}/uf")]
        public async Task<ActionResult<List<City>>> GetByUf(string id)
        {
            var response = await _cidadeRepository.GetByUf(id);


            if (response == null)
                return NotFound();

            return response;
        }

        // GET MEMBERS: api/cidade/1/uf
        [HttpGet("ufs")]
        public async Task<ActionResult<List<State>>> GetAllUfs()
        {
            var response = await _cidadeRepository.GetAllUf();


            if (response == null)
                return NotFound();

            return response;
        }
    }
}
