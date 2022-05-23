using AutoMapper;
using WebApiCasino2.DTOs;
using WebApiCasino2.Entidades;

namespace WebApiCasino2.Utilidades
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<PersonaDTO, Persona>();
            CreateMap<Persona, GetPersonaDTO>();
            CreateMap<Persona, PersonaDTOConRifas>()
                .ForMember(personaDTO => personaDTO.Rifas, opciones => opciones.MapFrom(MapPersonaDTORifas));
            CreateMap<RifaCreacionDTO, Rifa>()
                .ForMember(rifa => rifa.PersonaRifa, opciones => opciones.MapFrom(MapPersonaRifa));
            CreateMap<Rifa, RifaDTO>();
            CreateMap<Rifa, RifaDTOConPersonas>()
                .ForMember(rifaDTO => rifaDTO.Personas, opciones => opciones.MapFrom(MapRifaDTOPersonas));
            CreateMap<RifaPatchDTO, Rifa>().ReverseMap();
            CreateMap<NumsLoteriaCreacionDTO, NumsLoteria>();
            CreateMap<NumsLoteria, NumsLoteriaDTO>();
        }

        private List<RifaDTO> MapPersonaDTORifas(Persona persona, GetPersonaDTO getPersonaDTO)
        {
            var result = new List<RifaDTO>();

            if (persona.PersonaRifa == null) { return result; }

            foreach (var personaRifa in persona.PersonaRifa)
            {
                result.Add(new RifaDTO()
                {
                    Id = personaRifa.RifaId,
                    Nombre = personaRifa.Rifa.Nombre
                });
            }

            return result;
        }

        private List<GetPersonaDTO> MapRifaDTOPersonas(Rifa rifa, RifaDTO rifaDTO)
        {
            var result = new List<GetPersonaDTO>();

            if (rifa.PersonaRifa == null)
            {
                return result;
            }

            foreach (var personarifa in rifa.PersonaRifa)
            {
                result.Add(new GetPersonaDTO()
                {
                    Id = personarifa.PersonaId,
                    Nombre = personarifa.Persona.Nombre
                });
            }

            return result;
        }

        private List<PersonaRifa> MapPersonaRifa(RifaCreacionDTO rifaCreacionDTO, Rifa rifa)
        {
            var resultado = new List<PersonaRifa>();

            if (rifaCreacionDTO.PersonasIds == null) { return resultado; }
            foreach (var personaId in rifaCreacionDTO.PersonasIds)
            {
                resultado.Add(new PersonaRifa() { PersonaId = personaId });
            }
            return resultado;
        }
    }
}