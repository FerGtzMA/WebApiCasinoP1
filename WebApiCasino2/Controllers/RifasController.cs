using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiCasino2.Entidades;
using WebApiCasino2.DTOs;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;

namespace WebApiCasino2.Controllers
{
    [ApiController]
    [Route("api/rifas")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class RifasController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IMapper mapper;
        private readonly UserManager<IdentityUser> userManager;

        public RifasController(ApplicationDbContext context, IMapper mapper, UserManager<IdentityUser> userManager)
        {
            this.dbContext = context;
            this.mapper = mapper;
            this.userManager = userManager;
        }

        [HttpGet("/listadoRifas")]
        [AllowAnonymous]
        public async Task<ActionResult<List<Rifa>>> GetAll()
        {
            return await dbContext.Rifa.ToListAsync();
        }

        [HttpGet("{id:int}", Name = "obtenerRifa")]
        public async Task<ActionResult<RifaDTOConPersonas>> GetById(int id)
        {
            var rifa = await dbContext.Rifa
                .Include(rifaDB => rifaDB.PersonaRifa)
                .ThenInclude(personaRifaDB => personaRifaDB.Persona)
                .Include(rifaDB => rifaDB.Numeros)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (rifa == null)
            {
                return NotFound();
            }

            rifa.PersonaRifa = rifa.PersonaRifa.OrderBy(x => x.Oredn).ToList();

            return mapper.Map<RifaDTOConPersonas>(rifa);
        }

        [HttpPost]
        public async Task<ActionResult> Post(RifaCreacionDTO rifaCreacionDTO)
        {

            if (rifaCreacionDTO.PersonasIds == null)
            {
                return BadRequest("No se puede crear una rifa sin participantes.");
            }

            var personasIds = await dbContext.Personas
                .Where(personaBD => rifaCreacionDTO.PersonasIds.Contains(personaBD.Id)).Select(x => x.Id).ToListAsync();

            if (rifaCreacionDTO.PersonasIds.Count != personasIds.Count)
            {
                return BadRequest("No existe uno de los participantes enviados");
            }

            var rifa = mapper.Map<Rifa>(rifaCreacionDTO);

            OrdenarPorClientes(rifa);

            dbContext.Add(rifa);
            await dbContext.SaveChangesAsync();

            var rifaDTO = mapper.Map<RifaDTO>(rifa);

            return CreatedAtRoute("obtenerRifa", new { id = rifa.Id }, rifaDTO);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, RifaCreacionDTO rifaCreacionDTO)
        {
            var rifaDB = await dbContext.Rifa
                .Include(x => x.PersonaRifa)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (rifaDB == null)
            {
                return NotFound();
            }

            rifaDB = mapper.Map(rifaCreacionDTO, rifaDB);

            OrdenarPorClientes(rifaDB);

            await dbContext.SaveChangesAsync();
            return NoContent();
        }


        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var exist = await dbContext.Rifa.AnyAsync(x => x.Id == id);
            if (!exist)
            {
                return NotFound("El Recurso no fue encontrado.");
            }

            dbContext.Remove(new Rifa { Id = id });
            await dbContext.SaveChangesAsync();
            return Ok();
        }

        [HttpPatch("{id:int}")]
        public async Task<ActionResult> Patch(int id, JsonPatchDocument<RifaPatchDTO> patchDocument)
        {
            if (patchDocument == null) { return BadRequest(); }

            var rifaDB = await dbContext.Rifa.FirstOrDefaultAsync(x => x.Id == id);

            if (rifaDB == null) { return NotFound(); }

            var rifaDTO = mapper.Map<RifaPatchDTO>(rifaDB);

            patchDocument.ApplyTo(rifaDTO);

            var isValid = TryValidateModel(rifaDTO);

            if (!isValid)
            {
                return BadRequest(ModelState);
            }

            mapper.Map(rifaDTO, rifaDB);

            await dbContext.SaveChangesAsync();
            return NoContent();
        }


        private void OrdenarPorClientes(Rifa rifa)
        {
            if (rifa.PersonaRifa != null)
            {
                for (int i = 0; i < rifa.PersonaRifa.Count; i++)
                {
                    rifa.PersonaRifa[i].Oredn = i;
                }
            }
        }
    }
}
