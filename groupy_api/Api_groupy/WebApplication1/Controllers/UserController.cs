using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Interfaces;
using WebApplication1.Models;
using WebApplication1.Services;
using WebApplication1.Tools.DataBase;
using BC = BCrypt.Net.BCrypt;

namespace WebApplication1.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly Context _context;
        private readonly IUserRepository _userRepository;
        public UserController(Context context, IUserRepository userRepository)
        {
            _context = context;
            _userRepository = userRepository;
        }

        // GET: api/User
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetAll()
        {
            return await _userRepository.GetAll();
        }

        // GET: api/User/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetById(int id)
        {
            var user = await _userRepository.GetById(id);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, User user)
        {
            if (id != user.Id)
            {
                return BadRequest();
            }

            try
            {
                await _userRepository.Update(id, user);
                return Ok(new { message = "Atualizado com Sucesso" });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public async Task<ActionResult<dynamic>> PostUser([FromForm] UserDto userDto)
        {
            if (await _context.User.Where(u => u.Email == userDto.Email).FirstOrDefaultAsync() != null)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return BadRequest(new { message = "Login de usuário já cadastrado na base" });
            }

            var user = await _userRepository.Create(userDto);
            if (user == null)
                return BadRequest(new { message = "Ocorreu um erro ao salvar usuário" });

            //return CreatedAtAction("GetById", )
            //return StatusCode(StatusCodes.Status201Created, TokenServices.GenerateToken(user));
            Response.StatusCode = (int)HttpStatusCode.Created;
            return new { user = user, token = TokenServices.GenerateToken(user) };

        }

        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<dynamic>> Login([FromBody] DadosLogin model)
        {

            var user = _context.User.Where(user => user.Email == model.Email).FirstOrDefault();

            if (user == null || !BC.Verify(model.Password, user.Password))
            {
                return BadRequest(new { message = "Dados incorretos", status = (int)HttpStatusCode.BadRequest });
            }

            user.Password = string.Empty;
            var token = TokenServices.GenerateToken(user);
            return Ok(new { user = user, token = token });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            await _userRepository.Delete(id);
            return NoContent();
        }

        private bool UserExists(int id)
        {
            return _context.User.Any(e => e.Id == id);
        }
    }
}
