using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VestCareApi.Data;
using VestCareApi.DTOs;
using VestCareApi.Models;

namespace VestCareApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PecasController : ControllerBase
    {
        private readonly VestCareContext _context;

        public PecasController(VestCareContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Peca>>> GetPecas()
        {
            return await _context.Pecas
                .AsNoTracking()
                .ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Peca>> GetPeca(int id)
        {
            var peca = await _context.Pecas.FindAsync(id);

            if (peca == null)
            {
                return NotFound("Peça não encontrada.");
            }

            return peca;
        }

        [HttpGet("usuario/{idUsuario}")]
        public async Task<ActionResult<IEnumerable<Peca>>> GetPecasDoUsuario(int idUsuario)
        {
            return await _context.Pecas
                .Where(p => p.IdUsuario == idUsuario)
                .AsNoTracking()
                .ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<Peca>> CadastrarPeca(PecaCreateDto dto)
        {
            var usuarioExiste = await _context.Usuarios
                .AnyAsync(u => u.IdUsuario == dto.IdUsuario);

            if (!usuarioExiste)
            {
                return BadRequest("Usuário informado não existe.");
            }

            var peca = new Peca
            {
                NomePeca = dto.NomePeca,
                Ocasiao = dto.Ocasiao,
                Categoria = dto.Categoria,
                Cor = dto.Cor,
                Estilo = dto.Estilo,
                Clima = dto.Clima,
                UrlFoto = dto.UrlFoto,
                IdUsuario = dto.IdUsuario
            };

            _context.Pecas.Add(peca);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetPeca), new { id = peca.IdPeca }, peca);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> AtualizarPeca(int id, PecaCreateDto dto)
        {
            var peca = await _context.Pecas.FindAsync(id);

            if (peca == null)
            {
                return NotFound("Peça não encontrada.");
            }

            peca.NomePeca = dto.NomePeca;
            peca.Ocasiao = dto.Ocasiao;
            peca.Categoria = dto.Categoria;
            peca.Cor = dto.Cor;
            peca.Estilo = dto.Estilo;
            peca.Clima = dto.Clima;
            peca.UrlFoto = dto.UrlFoto;
            peca.IdUsuario = dto.IdUsuario;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletarPeca(int id)
        {
            var peca = await _context.Pecas.FindAsync(id);

            if (peca == null)
            {
                return NotFound("Peça não encontrada.");
            }

            _context.Pecas.Remove(peca);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}