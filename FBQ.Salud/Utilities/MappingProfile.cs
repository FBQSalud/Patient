using AutoMapper;
using FBQ.Salud_Domain.Entities;
using FBQ.Salud_Domain.Dtos;

namespace FBQ.Salud_Presentation.Utilities
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Turno, TurnoDTO>().ReverseMap();
            CreateMap<Paciente, PacienteDto>().ReverseMap();
        }
    }
}
   

