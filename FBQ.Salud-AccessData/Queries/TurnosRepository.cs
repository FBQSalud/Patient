
using FBQ.Salud_AccessData.Commands;
using FBQ.Salud_Domain.Commands;
using FBQ.Salud_Domain.Entities;

namespace FBQ.Salud_AccessData.Queries
{
    public class TurnosRepository : ITurnosRepository
    {
        private readonly FbqSaludDbContext _context;

        public TurnosRepository(FbqSaludDbContext context)
        {
            _context = context;
        }

        public List<Turno> GetAll()
        {
            return _context.Turnos.ToList();
        }

        public Turno GetTurnoById(int id)
        {
            return _context.Turnos.FirstOrDefault(x => x.TurnoId == id);
        }

        public void Add(Turno turno)
        {
            _context.Turnos.Add(turno);
            _context.SaveChanges();
        }

        public void Update(Turno turno)
        {
            _context.Update(turno);
            _context.SaveChanges();
        }

        public void Delete(Turno turno)
        {
            _context.Turnos.Remove(turno);
            _context.SaveChanges();
        }
    }
}
