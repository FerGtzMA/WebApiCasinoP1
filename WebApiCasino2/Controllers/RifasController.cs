using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiCasino2.DTOs;
using WebApiCasino2.Entidades;

namespace WebApiCasino2.Controllers
{
    [ApiController]
    [Route("api/rifas")]
    public class RifasController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IMapper mapper;
        private readonly ILogger<RifasController> logger;

        public RifasController(ApplicationDbContext context, IMapper mapper, ILogger<RifasController> logger)
        {
            this.dbContext = context;
            this.mapper = mapper;
            this.logger = logger;
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<GetRifaDTO>> Get(int id)
        {
            var rifa = await dbContext.Rifas.
                Include(r => r.Premios).
                FirstOrDefaultAsync(rifaBD => rifaBD.Id == id);
            if (rifa == null)
            {
                return NotFound();
            }
            return mapper.Map<GetRifaDTO>(rifa);
        }

        [HttpGet("{idRifa:int}/MostrarNumerosDisponibles")]
        public async Task<ActionResult<List<int>>> GetNumeros(int idRifa)
        {
            var personasEnRifaDB = await dbContext.PersonasRifas.Where(x => x.RifaId == idRifa).ToListAsync();
            var numeroDisponible = new List<int>();
            for (int i = 1; i <= 54; i++)
            {
                numeroDisponible.Add(i);
            }
            foreach (var persona in personasEnRifaDB)
            {
                foreach (var numero in numeroDisponible)
                {
                    if (numero == persona.NumLoteria)
                    {
                        numeroDisponible.Remove(numero);
                        break;
                    }
                }
            }
            return numeroDisponible;
        }

        [HttpPost("CreacionDeRifa")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Administrador")]
        public async Task<IActionResult> Post([FromBody] RifaCreacionDTO rifaCreacionDtoO)
        {
            var existeRifaMismoNombre = await dbContext.Rifas.AnyAsync(x => x.Nombre == rifaCreacionDtoO.Nombre);
            if (existeRifaMismoNombre)
            {
                return BadRequest("Ya existe una rifa con el mismo nombre.");
            }
            var rifa = mapper.Map<Rifa>(rifaCreacionDtoO);
            dbContext.Add(rifa);
            await dbContext.SaveChangesAsync();
            return Ok();
        }


        [HttpDelete("EliminarRifa")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Administrador")]
        public async Task<ActionResult> Delete(int id)
        {
            var exist = await dbContext.Rifas.AnyAsync(x => x.Id == id);
            if (!exist)
            {
                return NotFound("El recurso no fue encontrado.");
            }
            dbContext.Remove(new Rifa { Id = id });
            await dbContext.SaveChangesAsync();
            return Ok();
        }
    }
}