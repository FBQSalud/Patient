
using FBQ.Salud_AccessData.Commands;
using FBQ.Salud_Domain.Commands;
using FBQ.Salud_Domain.Dtos;
using FBQ.Salud_Domain.Entities;

namespace FBQ.Salud_AccessData.Queries
{
    public class HistoriaClinicaRepository : IHistoriaClinicaRepository
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

        public List<HistoriaClinica> GetAll(bool fecha, int? pacienteId)
        {
            if (fecha)
            {
                var historias = (from u in _context.HistoriasClinicas select u).OrderByDescending(o => o.FechaApertura.Day).ToList();
                return historias;
            }
            else
            {
                var historias = (from o in _context.HistoriasClinicas select o)
                                        .OrderBy(o => o.FechaApertura).ToList();
                return historias;
            }
        }
        public List<HistoriaClinica> GetListHistoriaClinicaByPacienteId(int? pacienteId)
        {
            var historias = _context.HistoriasClinicas.Where(x=> x.PacienteId == pacienteId).ToList();

            return historias;
        }

        public HistoriaClinica GetHistoriaClinicaById(int id)
        {
            return _context.HistoriasClinicas.FirstOrDefault(x => x.HistoriaClinicaId == id);
        }
        public void Update(HistoriaClinica historiaClinica)
        {
            _context.Update(historiaClinica);
            _context.SaveChanges();
        }
    }
}
