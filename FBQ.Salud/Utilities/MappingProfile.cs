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
            CreateMap<HistoriaClinica,HistoriaClinicaDTO>().ReverseMap();
            CreateMap<Turno, TurnoDTOForUpdate>().ReverseMap();
            CreateMap<Diagnostico, DiagnosticoDTO>().ReverseMap();
            CreateMap<Turno, TurnoDTOForCreated>().ReverseMap();
            CreateMap<HistoriaClinica, HistoriaClinicaDTOForUpdate>().ReverseMap();
        }
    }
}
   

