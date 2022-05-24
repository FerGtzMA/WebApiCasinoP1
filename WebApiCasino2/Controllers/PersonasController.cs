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
    [Route("api/personas")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Administrador")]
    public class PersonasController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IWebHostEnvironment env;
        private readonly IMapper mapper;

        public PersonasController(ApplicationDbContext context, IWebHostEnvironment env, IMapper mapper)
        {
            this.dbContext = context;
            this.env = env;
            this.mapper = mapper;
        }

        [HttpGet("VerPersonas")]
        public async Task<ActionResult<List<Persona>>> Get()
        {
            return await dbContext.Personas.ToListAsync();
        }

        [HttpPost("CracionDePersona")]
        public async Task<ActionResult> Post(PersonaCreacionDTO personaDto)
        {
            var parti = mapper.Map<Persona>(personaDto);
            dbContext.Add(parti);
            await dbContext.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var exist = await dbContext.Personas.AnyAsync(x => x.Id == id);
            if (!exist)
            {
                return NotFound("El Recurso no fue encontrado.");
            }

            dbContext.Remove(new Persona()
            {
                Id = id
            });
            await dbContext.SaveChangesAsync();
            return Ok();
        }


        //-----------------------------------------------------------------------------------------
        private void EscribirGet(string msg)
        {
            //así se hace la ruta con el enviroment
            var ruta = $@"{env.ContentRootPath}\wwwroot\registroConsultado.txt";
            using (StreamWriter writer = new StreamWriter(ruta, append: true)) { writer.WriteLine(msg + "        " + DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss")); writer.Close(); }
        }
        private void EscribirPost(string msg)
        {
            var ruta = $@"{env.ContentRootPath}\wwwroot\nuevosRegistros.txt";
            using (StreamWriter writer = new StreamWriter(ruta, append: true)) { writer.WriteLine(msg + "        " + DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss")); writer.Close(); }
        }
    }
}
