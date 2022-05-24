using AutoMapper;
using WebApiCasino2.DTOs;
using WebApiCasino2.Entidades;

namespace WebApiCasino2.Utilidades
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<PersonaCreacionDTO, Persona>();
            CreateMap<RifaPersonaCreacionDTO, PersonaRifa>();
            CreateMap<PremioCreacionDTO, Premio>();
            CreateMap<RifaCreacionDTO, Rifa>();

            CreateMap<Rifa, GetRifaDTO>().ForMember(premioDTO => premioDTO.Premios, options => options.MapFrom(MapearListaPremios));
            CreateMap<PremioPatchDTO, Premio>().ReverseMap();
        }
        private List<Premio> MapearListaPremios(Rifa rifa, GetRifaDTO getRifaDTO)
        {
            var result = new List<Premio>();
            if (rifa.Premios == null)
            {
                return result;
            }
            foreach (var premios in rifa.Premios)
            {
                result.Add(new Premio()
                {
                    Id = premios.Id,
                    Nombre = premios.Nombre,
                    Entregado = premios.Entregado,
                    Orden = premios.Orden,
                    RifaId = premios.RifaId
                });
            }
            return result;
        }
    }
}