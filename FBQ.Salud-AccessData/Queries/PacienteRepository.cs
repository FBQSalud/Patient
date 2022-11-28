using FBQ.Salud_AccessData.Commands;
using FBQ.Salud_Domain.Commands;
using FBQ.Salud_Domain.Entities;


namespace FBQ.Salud_AccessData.Queries
{
    public class PacienteRepository:IPacienteRepository
    {
        private readonly FbqSaludDbContext _context;

        public PacienteRepository(FbqSaludDbContext context)
        {
            _context = context;
        }
        public void Add(Paciente paciente)
        {
            _context.Pacientes.Add(paciente);
            _context.SaveChanges();
        }

        public void Delete(Paciente paciente)
        {
            _context.Remove(paciente);
            _context.SaveChanges();
        }

        public List<Paciente> GetAll( string? nombre)
        {
            
           
                var pacientes = (from o in _context.Pacientes where o.Estado==false select o)
                                        .OrderBy(o => o.Edad).ToList();
                return pacientes;
            
        }
        public List<Paciente> GetListPacientesByNombre()
        {
            var pacientes = _context.Pacientes.Where(x =>x.Estado==false).ToList();

            return pacientes;
        }
        public Paciente GetPacienteByDNI(string dni)
        {
            return _context.Pacientes.FirstOrDefault(x => x.DNI == dni);
        }

        public Paciente GetPacienteById(int id)
        {
            return _context.Pacientes.FirstOrDefault(x => x.PacienteId == id);
        }

        public void Update(Paciente paciente)
        {
            _context.Update(paciente);
            _context.SaveChanges();
        }
    }
}
