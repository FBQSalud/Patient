<<<<<<< .merge_file_a13476
﻿using FBQ.Salud_Domain.Entities;
=======
﻿
using FBQ.Salud_Domain.Entities;
>>>>>>> .merge_file_a17368
using Microsoft.EntityFrameworkCore;

namespace FBQ.Salud_AccessData.Commands
{
    public class FbqSaludDbContext : DbContext
    {
        public FbqSaludDbContext() { }
<<<<<<< .merge_file_a13476
        public FbqSaludDbContext(DbContextOptions<FbqSaludDbContext> options) : base(options) { }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("");
        }
        public virtual DbSet<Paciente> Pacientes { get; set; }
=======

        public FbqSaludDbContext(DbContextOptions<FbqSaludDbContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=localhost;Database=DbPatientApi;Trusted_Connection=True;MultipleActiveResultSets=true");
        }

        public virtual DbSet<Turno> Turnos { get; set; }
>>>>>>> .merge_file_a17368
    }
}
