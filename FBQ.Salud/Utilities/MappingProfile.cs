using AutoMapper;
<<<<<<< .merge_file_a17532
using FBQ.Salud_Domain.Dtos;
using FBQ.Salud_Domain.Entities;

namespace FBQ.Salud.Presentation.Utilities
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            CreateMap<Paciente, PacienteDto>().ReverseMap();

        }
    }
}
=======
using FBQ.Salud_Domain.Entities;
using FBQ.Salud_Domain.Dtos;

namespace FBQ.Salud_Presentation.Utilities
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Turno, TurnoDTO>().ReverseMap();
        }
    }
}
   

>>>>>>> .merge_file_a14240
