﻿using FBQ.Salud_Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FBQ.Salud_Domain.Commands
{
    public interface IDiagnosticoRepository
    {
        Diagnostico GetDiagnosticoByCodigo(string codigo);
        List<Diagnostico> GetAll();
    }
}
