using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiCasino2.DTOs;
using WebApiCasino2.Entidades;

namespace WebApiCasino2.Controllers
{
    [ApiController]
    [Route("api/personas")]
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

        [HttpGet]
        public async Task<ActionResult<List<Persona>>> Get()
        {
            return await dbContext.Personas.ToListAsync();
        }

        /*
        [HttpGet]
        public async Task<ActionResult<List<GetPersonaDTO>>> Get()
        {
            var personas = await dbContext.Personas.ToListAsync();
            return mapper.Map<List<GetPersonaDTO>>(personas);
        }
        */

        [HttpGet("{nombre}")]
        public async Task<ActionResult<List<GetPersonaDTO>>> Get([FromRoute] string nombre)
        {
            var personas = await dbContext.Personas.Where(personaBD => personaBD.Nombre.Contains(nombre)).ToListAsync();
            
            EscribirGet(nombre);

            return mapper.Map<List<GetPersonaDTO>>(personas);

        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] PersonaDTO personaDto)
        {
            var existePersonaMismoNombre = await dbContext.Personas.AnyAsync(x => x.Nombre == personaDto.Nombre);

            if (existePersonaMismoNombre)
            {
                return BadRequest($"Ya existe un autor con el nombre {personaDto.Nombre}");
            }

            var persona = mapper.Map<Persona>(personaDto);

            EscribirPost("-->" + (persona.Nombre).ToString());

            dbContext.Add(persona);
            await dbContext.SaveChangesAsync();

            var personaDTO = mapper.Map<GetPersonaDTO>(persona);

            return CreatedAtRoute("obtenerpersona", new { id = persona.Id }, personaDTO);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(PersonaDTO personaCreacionDTO, int id)
        {
            var exist = await dbContext.Personas.AnyAsync(x => x.Id == id);
            if (!exist)
            {
                return NotFound();
            }

            var persona = mapper.Map<Persona>(personaCreacionDTO);
            persona.Id = id;

            dbContext.Update(persona);
            await dbContext.SaveChangesAsync();
            return NoContent();
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




        //---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        //MÉTODOS PARA LOS PUNTOS 1 Y 2.
        //Estos dos se crean, si no están creados, y se guardan en la carpeta wwwroot
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
