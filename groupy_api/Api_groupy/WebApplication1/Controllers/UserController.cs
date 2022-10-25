﻿using System;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Interfaces;
using WebApplication1.Models;
using WebApplication1.Services;
using WebApplication1.Tools.DataBase;
using BC = BCrypt.Net.BCrypt;
using Microsoft.AspNetCore.Authorization;
using WebApplication1.ViewModels;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace WebApplication1.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly Context _context;
        private readonly IUserRepository _userRepository;
        private readonly IEmailRepository _emailRepository;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        public UserController(Context context, IUserRepository userRepository, IEmailRepository emailRepository, UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _context = context;
            _userRepository = userRepository;
            _emailRepository = emailRepository;
            _signInManager = signInManager;
            _userManager = userManager;
        }

        // GET: api/User
        [HttpGet("{id}/userList")]
        public async Task<ActionResult<List<User>>> GetAll(string id)
        {
            return await _userRepository.GetUserList(id);
        }

        // GET: api/User/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetById(string id)
        {
            var user = await _userRepository.GetById(id);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(string id, UserDto userDto)
        {
            if (id != userDto.Id)
            {
                return BadRequest();
            }

            try
            {
                await _userRepository.Update(id, userDto);
                return Ok(new { message = "Atualizado com Sucesso" });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public async Task<ActionResult<dynamic>> PostUser([FromBody] UserDto userDto)
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
            await EnviarLinkConfirmacaoEmailAsync(user);
            Response.StatusCode = (int)HttpStatusCode.Created;
            return new { user = user, token = TokenServices.GenerateToken(user), message="Novo usuário registrado com sucesso, para efetuar login por favor conmfirme seu cadastro com o email que enviamos" };

        }

        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<dynamic>> Login([FromBody] DadosLogin model)
        {

            var user = await _userManager.FindByEmailAsync(model.Email);
            

            if (user == null || !BC.Verify(model.Password, user.Password))
            {
                return BadRequest(new { message = "Dados incorretos", status = (int)HttpStatusCode.BadRequest });
            }
            user.Password = string.Empty;

            if (!user.EmailConfirmed)
            {
                return new ObjectResult(new {message = "Confirme o email para realizar login", user = user}) { StatusCode = 403 };
            }
            var token = TokenServices.GenerateToken(user);
            return Ok(new { user = user, token = token });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            await _userRepository.Delete(id);
            return NoContent();
        }

        [HttpDelete("groups/{userId}")]
        public async Task<IActionResult> GroupsByUser(string userId)
        {
            await _userRepository.GroupsByUser(userId);
            return NoContent();
        }

        [HttpGet("sendTestEmail")]
        public async Task<ActionResult<dynamic>> SendTestEmailToUser()
        {
            var html = new StringBuilder();
            html.Append("<h1> Teste do serviço de envio de E-mail do aplicativo Seek Activity App </h1>");
            html.Append("<h1> Este é um teste do serviço de envio de e-emails usando ASP.NET Core. </p>");

            await _emailRepository.SendEmailAsync("indemonhado654@gmail.com", "Teste do Serviço de E-mail", string.Empty, html.ToString());
            return Ok(new { message = "Teste enviado com sucesso" });
        }

        [HttpPost("forgotPassword")]
        public async Task<IActionResult> EsqueciSenha([FromBody] string email)
        {
            if (_userManager.Users.AsNoTracking().Any(u => u.Email == email.ToUpper().Trim()))
            {
                var user = await _userManager.FindByEmailAsync(email);
                var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                var urlConfirmacao = Url.Action(nameof(RedefinirSenha), "User", new { token = token, email = email }, Request.Scheme);
                var mensagem = new StringBuilder();
                mensagem.Append($"<p>Olá, {user.FirstName} {user.LastName}.</p>");
                mensagem.Append("<p>Houve uma solicitação de redefinição de senha para seu usuário em nosso site. Se não foi você que fez a solicitação, ignore essa mensagem. Caso tenha sido você, clique no link abaixo para criar sua nova senha:</p>");
                mensagem.Append($"<p><a href='{urlConfirmacao}'>Redefinir Senha</a></p>");
                mensagem.Append("<p>Atenciosamente,<br>Equipe de Suporte</p>");
                await _emailRepository.SendEmailAsync(user.Email,
                    "Redefinição de Senha", "", mensagem.ToString());
                return Ok(new { message = "O e-mail de recuperação foi enviado com sucesso" });
            }
            else
            {
                return BadRequest(new { message = "Erro ao enviar email de recuperação" });
            }
        }

        [HttpGet]
        [Route("ResetPassword", Name = "ResetPassword")]

        public IActionResult RedefinirSenha(string token, string email)
        {
            var modelo = new RedefinirSenhaViewModel();
            modelo.Token = token;
            modelo.Email = email;
            return View(modelo);

        }

        [HttpPost("ResetPassword")]
        public async Task<IActionResult> RedefinirSenha( RedefinirSenhaViewModel dados)
        {
            var user = await _userManager.FindByEmailAsync(dados.Email);
            var result = await _userManager.ResetPasswordAsync(
                user, dados.Token, dados.NovaSenha);
            if (result.Succeeded)
            {
                return Ok(new { message = $"Senha redefinida com sucesso! Agora você já pode fazer login com a nova senha." });
            }
            else
            {
                return BadRequest(new { message = $"Não foi possível redefinir a senha. Verifique se preencheu a senha corretamente. Se o problema persistir, entre em contato com o suporte." , erro = result?.Errors?.First().Description});
            }
        }

        

        [HttpPost("recoverPassword")]
        public async Task<IActionResult> AlterarSenha([FromForm] dynamic dados)
        {

            var usuario = await _userManager.FindByEmailAsync(HttpContext.User.Identity.Name);
            var resultado = await _userManager.ChangePasswordAsync(usuario, dados.SenhaAtual, dados.NovaSenha);
            if (resultado.Succeeded)
            {
                await _signInManager.SignOutAsync();
                return Ok(new { message = $"Sua senha foi alterada com sucesso. Identifique-se usando a nova senha." });
            }
            else
            {
                return BadRequest(new { message = $"Não foi possível alterar sua senha. Confira os dados informados e tente novamente." });
            }

        }
        [HttpPost("resendEmail")]
        public async Task<IActionResult> ReSendEmail([FromBody] ResendEmailDto dto)
        {

            var user =  _userManager.FindByEmailAsync(dto.Email).Result;
            if (user.EmailConfirmed)
            {
                return BadRequest(new { message = "Usuário já possui email confirmado" });
            }
            await EnviarLinkConfirmacaoEmailAsync(user);
            return Ok(new { message = "Email enviado com sucesso" });
        }

        private async Task EnviarLinkConfirmacaoEmailAsync(User user)
        {
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var urlConfirmacao = Url.Action("ConfirmarEmail",
                "User", new { email = user.Email, token = token }, Request.Scheme);

            var mensagem = new StringBuilder();
            mensagem.Append($"<p>Olá, {user.FirstName} {user.LastName}.</p>");
            mensagem.Append("<p>Recebemos seu cadastro em nosso sistema. Para concluir o processo de cadastro, clique no link a seguir:</p>");
            mensagem.Append($"<p><a href='{urlConfirmacao}'>Confirmar Cadastro</a></p>");
            mensagem.Append("<p>Atenciosamente,<br>Equipe de Suporte</p>");
            await _emailRepository.SendEmailAsync(user.Email,
                "Confirmação de Cadastro", "", mensagem.ToString());
        }

        [HttpGet]
        [Route("ConfirmarEmail", Name = "ConfirmarEmail")]
        public async Task<IActionResult> ConfirmarEmail(string email, string token)
        {
            var usuario = await _userManager.FindByEmailAsync(email);
            if (usuario == null)
            {
                return NotFound(new { message = "Usuário não encontrado na base" });
            }
            var result = await _userManager.ConfirmEmailAsync(usuario, token);
            if (result.Succeeded)
            {
                return new ContentResult
                {
                    ContentType = "text/html",
                    StatusCode = (int)HttpStatusCode.OK,
                    Content = "<html><body> <h1>Email confirmado com sucesso, agora voce ja pode efetuar login no site :) </h1> </body></html>"
                };
            }
            else
            {
                return new ContentResult
                {
                    ContentType = "text/html",
                    StatusCode = (int)HttpStatusCode.OK,
                    Content = "<html><body> <h1>Nao foi possível validar seu e-mail. Tente novamente em alguns minutos. Se o problema persistir, entre em contato com o suporte. :) </h1> </body></html>"
                };
            }
        }

        private bool UserExists(string id)
        {
            return _context.User.Any(e => e.Id == id);
        }
    }

    public class ResendEmailDto
    {
        public string Email { get; set; }
    }
}
