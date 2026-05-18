using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VestCareApi.Data;
using VestCareApi.DTOs;
using VestCareApi.Models;

namespace VestCareApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly VestCareContext _context;

        public UsuariosController(VestCareContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Usuario>>> GetUsuarios()
        {
            return await _context.Usuarios
                .AsNoTracking()
                .ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Usuario>> GetUsuario(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);

            if (usuario == null)
            {
                return NotFound("Usuário não encontrado.");
            }

            return usuario;
        }

        [HttpPost]
        public async Task<ActionResult<Usuario>> CadastrarUsuario(UsuarioCreateDto dto)
        {
            var emailJaExiste = await _context.Usuarios
                .AnyAsync(u => u.Email == dto.Email);

            if (emailJaExiste)
            {
                return BadRequest("Esse email já está cadastrado.");
            }

            var usuario = new Usuario
            {
                Nome = dto.Nome,
                Email = dto.Email,
                Senha = dto.Senha,
                DataCadastro = DateTime.Now
            };

            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetUsuario), new { id = usuario.IdUsuario }, usuario);
        }

        [HttpPost("login")]
        public async Task<ActionResult<Usuario>> Login(LoginDto dto)
        {
            var usuario = await _context.Usuarios
                .FirstOrDefaultAsync(u => u.Email == dto.Email && u.Senha == dto.Senha);

            if (usuario == null)
            {
                return Unauthorized("Email ou senha incorretos.");
            }

            return usuario;
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletarUsuario(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);

            if (usuario == null)
            {
                return NotFound("Usuário não encontrado.");
            }

            _context.Usuarios.Remove(usuario);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}