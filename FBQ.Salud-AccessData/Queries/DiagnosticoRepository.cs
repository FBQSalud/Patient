using FBQ.Salud_AccessData.Commands;
using FBQ.Salud_Domain.Commands;
using FBQ.Salud_Domain.Entities;

namespace FBQ.Salud_AccessData.Queries
{
    public class DiagnosticoRepository : IDiagnosticoRepository
    {
        private readonly FbqSaludDbContext _context;
        public DiagnosticoRepository(FbqSaludDbContext context)
        {
            _context= context;
        }

        public List<Diagnostico> GetAll()
        {
            return _context.Diagnostico.ToList();
        }

        public Diagnostico GetDiagnosticoByCodigo(string codigo)
        {
            return _context.Diagnostico.FirstOrDefault(x => x.Codigo == codigo);
        }
    }
}
