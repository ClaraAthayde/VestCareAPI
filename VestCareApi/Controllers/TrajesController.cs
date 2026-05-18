using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VestCareApi.Data;
using VestCareApi.DTOs;
using VestCareApi.Models;

namespace VestCareApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrajesController : ControllerBase
    {
        private readonly VestCareContext _context;

        public TrajesController(VestCareContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Traje>>> GetTrajes()
        {
            return await _context.Trajes
                .AsNoTracking()
                .ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Traje>> GetTraje(int id)
        {
            var traje = await _context.Trajes.FindAsync(id);

            if (traje == null)
            {
                return NotFound("Traje não encontrado.");
            }

            return traje;
        }

        [HttpGet("usuario/{idUsuario}")]
        public async Task<ActionResult<IEnumerable<Traje>>> GetTrajesDoUsuario(int idUsuario)
        {
            return await _context.Trajes
                .Where(t => t.IdUsuario == idUsuario)
                .AsNoTracking()
                .ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<Traje>> CadastrarTraje(TrajeCreateDto dto)
        {
            var usuarioExiste = await _context.Usuarios
                .AnyAsync(u => u.IdUsuario == dto.IdUsuario);

            if (!usuarioExiste)
            {
                return BadRequest("Usuário informado não existe.");
            }

            var traje = new Traje
            {
                NomeTraje = dto.NomeTraje,
                OcasiaoDestino = dto.OcasiaoDestino,
                ClimaDestino = dto.ClimaDestino,
                Favorito = dto.Favorito,
                IdUsuario = dto.IdUsuario
            };

            _context.Trajes.Add(traje);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetTraje), new { id = traje.IdTraje }, traje);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> AtualizarTraje(int id, TrajeCreateDto dto)
        {
            var traje = await _context.Trajes.FindAsync(id);

            if (traje == null)
            {
                return NotFound("Traje não encontrado.");
            }

            traje.NomeTraje = dto.NomeTraje;
            traje.OcasiaoDestino = dto.OcasiaoDestino;
            traje.ClimaDestino = dto.ClimaDestino;
            traje.Favorito = dto.Favorito;
            traje.IdUsuario = dto.IdUsuario;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletarTraje(int id)
        {
            var traje = await _context.Trajes.FindAsync(id);

            if (traje == null)
            {
                return NotFound("Traje não encontrado.");
            }

            _context.Trajes.Remove(traje);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPost("{idTraje}/pecas/{idPeca}")]
        public async Task<IActionResult> AdicionarPecaNoTraje(int idTraje, int idPeca)
        {
            var trajeExiste = await _context.Trajes.AnyAsync(t => t.IdTraje == idTraje);
            var pecaExiste = await _context.Pecas.AnyAsync(p => p.IdPeca == idPeca);

            if (!trajeExiste)
            {
                return NotFound("Traje não encontrado.");
            }

            if (!pecaExiste)
            {
                return NotFound("Peça não encontrada.");
            }

            var jaExiste = await _context.TrajePecas
                .AnyAsync(tp => tp.IdTraje == idTraje && tp.IdPeca == idPeca);

            if (jaExiste)
            {
                return BadRequest("Essa peça já está nesse traje.");
            }

            var trajePeca = new TrajePeca
            {
                IdTraje = idTraje,
                IdPeca = idPeca
            };

            _context.TrajePecas.Add(trajePeca);
            await _context.SaveChangesAsync();

            return Ok("Peça adicionada ao traje.");
        }

        [HttpDelete("{idTraje}/pecas/{idPeca}")]
        public async Task<IActionResult> RemoverPecaDoTraje(int idTraje, int idPeca)
        {
            var trajePeca = await _context.TrajePecas
                .FirstOrDefaultAsync(tp => tp.IdTraje == idTraje && tp.IdPeca == idPeca);

            if (trajePeca == null)
            {
                return NotFound("Essa peça não está nesse traje.");
            }

            _context.TrajePecas.Remove(trajePeca);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}