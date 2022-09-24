
using FBQ.Salud_AccessData.Commands;
using FBQ.Salud_Domain.Commands;
using FBQ.Salud_Domain.Entities;

namespace FBQ.Salud_AccessData.Queries
{
    public class HistoriaClinicaRepository:IHistoriaClinicaRepository
    {
        private readonly FbqSaludDbContext _context;

        public HistoriaClinicaRepository(FbqSaludDbContext context)
        {
            _context = context;
        }

        public void Add(HistoriaClinica historiaClinica)
        {
            _context.HistoriasClinicas.Add(historiaClinica);
            _context.SaveChanges();
        }

        public void Delete(HistoriaClinica historiaClinica)
        {
            _context.Remove(historiaClinica);
            _context.SaveChanges();
        }

        public List<HistoriaClinica> GetAll()
        {
            return _context.HistoriasClinicas.ToList();
        }

        public HistoriaClinica GetHistoriaClinicaById(int id)
        {
            return _context.HistoriasClinicas.FirstOrDefault(x => x.HistoriaClinicaId== id);
        }

        public void Update(HistoriaClinica historiaClinica)
        {
            _context.Update(historiaClinica);
            _context.SaveChanges();
        }
    }
}
