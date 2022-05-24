using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiCasino2.DTOs;
using WebApiCasino2.Entidades;

namespace WebApiCasino2.Controllers
{
    [ApiController]
    //Esto quiere decir que es un controlador dependiente, y en este caso depende de la rifa.
    //Debemos especificar qué numero de lotería quiero yo.
    [Route("rifas/{rifaId:int}/NumerosDeLoteria")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class NumsLoteriaController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IMapper mapper;
        private readonly UserManager<IdentityUser> userManager;

        public NumsLoteriaController(ApplicationDbContext dbContext, IMapper mapper,
            UserManager<IdentityUser> userManager)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
            this.userManager = userManager;
        }

        [HttpGet]
        public async Task<ActionResult<List<NumsLoteriaDTO>>> Get(int rifaId)
        {
            var existeRifa = await dbContext.NumerosL.AnyAsync(rifaDB => rifaDB.Id == rifaId);
            if (!existeRifa)
            {
                return NotFound();
            }

            var numerosL = await dbContext.NumerosL.Where(numerosDB => numerosDB.RifaId == rifaId).ToListAsync();

            return mapper.Map<List<NumsLoteriaDTO>>(numerosL);
        }

        [HttpGet("{id:int}", Name = "obtenerNumeros")]
        public async Task<ActionResult<NumsLoteriaDTO>> GetById(int id)
        {
            var numero = await dbContext.NumerosL.FirstOrDefaultAsync(numeroDB => numeroDB.Id == id);

            if (numero == null)
            {
                return NotFound();
            }

            return mapper.Map<NumsLoteriaDTO>(numero);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> Post(int rifaId, NumsLoteriaCreacionDTO numeroCreacionDTO)
        {
            //mediante los claims podemos acceder a la información del usaurio que esta logueado.
            //Es la información que obtenemos desde el token.
            var emailClaim = HttpContext.User.Claims.Where(claim => claim.Type == "email").FirstOrDefault();

            var email = emailClaim.Value;
            //Obtenemos el usuario desde el correo.
            var usuario = await userManager.FindByEmailAsync(email);
            var usuarioId = usuario.Id;

            var existeRifa = await dbContext.NumerosL.AnyAsync(rifaDB => rifaDB.Id == rifaId);
            if (!existeRifa)
            {
                return NotFound();
            }

            var numero = mapper.Map<NumsLoteria>(numeroCreacionDTO);
            numero.RifaId = rifaId;
            numero.UsuarioId = usuarioId;
            dbContext.Add(numero);
            await dbContext.SaveChangesAsync();

            var numeroDTO = mapper.Map<NumsLoteriaDTO>(numero);

            //Perimte generar una ruta de acceso al registro que acabo de crear.
            //Se pasa el nombre de la ruta, un objeto con su dependencia, el objeto que acabamos de crear. 
            return CreatedAtRoute("obtenerNumero", new { id = numero.Id, rifaId = rifaId }, numeroDTO);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int rifaId, int id, NumsLoteriaCreacionDTO numeroCreacionDTO)
        {
            var existeRifa = await dbContext.Rifa.AnyAsync(rifaDB => rifaDB.Id == rifaId);
            if (!existeRifa)
            {
                return NotFound();
            }

            var existeNumero = await dbContext.NumerosL.AnyAsync(numeroDB => numeroDB.Id == id);
            if (!existeNumero)
            {
                return NotFound();
            }

            var numero = mapper.Map<NumsLoteria>(numeroCreacionDTO);
            numero.Id = id;
            numero.RifaId = rifaId;

            dbContext.Update(numero);
            await dbContext.SaveChangesAsync();
            return NoContent();
        }
    }
}