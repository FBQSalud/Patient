
using FBQ.Salud_Domain.Dtos;
using FBQ.Salud_Domain.Entities;

namespace FBQ.Salud_Domain.Commands
{
    public interface ITurnosRepository
    {      
        List<Turno> GetAll();
        Turno GetTurnoById(int id);
        void Update(Turno turno);
        void Delete(Turno turno);
        void Add(Turno turno);
    }
}
