using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiCasino2.DTOs;
using WebApiCasino2.Entidades;
using WebApiCasino2.Utilidades;

namespace WebApiCasino2.Controllers
{
    [ApiController]
    [Route("api/sorteo")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Administrador")]
    public class PersonasRifasController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IMapper mapper;

        public PersonasRifasController(ApplicationDbContext context, IMapper mapper)
        {
            this.dbContext = context;
            this.mapper = mapper;
        }

        [HttpGet("{rifaId:int}/DarGanador")]
        public async Task<ActionResult<Object>> Ganador(int rifaId)
        {
            var rifa = await dbContext.Rifas.FirstOrDefaultAsync(x => x.Id == rifaId);
            if (rifa == null)
            {
                return BadRequest("El id de la rifa no ha sido ingresada correctamente.");
            }
            var personasEnRifa = await dbContext.PersonasRifas.Where(x => x.RifaId == rifaId && x.Gana == false).ToListAsync();
            if (personasEnRifa.Count == 0)
            {
                return BadRequest("No se encontraron personas en la rifa.");
            }
            Random random = new Random(); //Para genearar un ganador aleatorio
            var ganador = personasEnRifa.OrderBy(x => random.Next()).Take(1).FirstOrDefault();
            ganador.Gana = true;
            dbContext.PersonasRifas.Update(ganador);
            await dbContext.SaveChangesAsync();
            var datosDePersona = await dbContext.Personas.Where(x => x.Id == ganador.PersonaId).FirstOrDefaultAsync();
            var list = NumerosDeLoteria.baraja;
            var ticketDeGanador = new
            {
                nombre = datosDePersona.Nombre,
                numero = ganador.NumLoteria,
                Carta = list.ElementAt(ganador.NumLoteria - 1)
            };
            return ticketDeGanador;
        }

        [HttpPost("IngresarPersonaARifa")]
        [AllowAnonymous]
        public async Task<ActionResult> Post(RifaPersonaCreacionDTO rifaPersonaCreacionDto)
        {
            var existeMismoNumero = await dbContext.PersonasRifas.AnyAsync(x => x.NumLoteria == rifaPersonaCreacionDto.NumLoteria && x.RifaId == rifaPersonaCreacionDto.RifaId);
            if (existeMismoNumero)
            {
                return BadRequest($"Ya existe una persona con este número de lotería.");
            }
            var rifaPersona = mapper.Map<PersonaRifa>(rifaPersonaCreacionDto);
            rifaPersona.Gana = false;
            dbContext.Add(rifaPersona);
            await dbContext.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var exist = await dbContext.PersonasRifas.AnyAsync(x => x.Id == id);
            if (!exist)
            {
                return NotFound("El recurso no fue encontrado");
            }
            dbContext.Remove(new PersonaRifa() { Id = id });
            await dbContext.SaveChangesAsync();
            return Ok();
        }
    }
}
