using FBQ.Salud_Domain.Dtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FBQ.Salud_Application.Validation
{
    public class PacienteValidation: AbstractValidator<PacienteDto>
    {
        public PacienteValidation()
        {
            RuleFor(c => c.Sexo).Cascade(CascadeMode.Stop)
                .NotNull().WithMessage("{PropertyName} no puede ser nulo")
                .NotEmpty().WithMessage("{PropertyName} no puede ser vacio")
                .MaximumLength(15).WithMessage("{PropertyName} valor demasiado largo ");
            RuleFor(c => c.Telefono).Cascade(CascadeMode.Stop)
                .NotNull().WithMessage("{PropertyName} no puede ser nulo")
                .NotEmpty().WithMessage("{PropertyName} no puede ser vacio")
                .Matches(@"^\d{8}$").WithMessage("{PropertyName} debe cumplir el formato 0000000000");
            RuleFor(c => c.DNI).Cascade(CascadeMode.Stop)
                .GreaterThan("0").WithMessage("{PropertyName} no puede ser valor negativo")
                .Length(8).WithMessage("{PropertyName} debe ingresar 8 caracteres")
                .NotNull().WithMessage("{PropertyName} no puede ser nulo")
                .NotEmpty().WithMessage("{PropertyName} no puede ser vacio");
            RuleFor(c => c.DirecionNumero).Cascade(CascadeMode.Stop)
                .NotNull().WithMessage("{PropertyName} no puede ser nulo")
                .NotEmpty().WithMessage("{PropertyName} no puede ser vacio")
                .MaximumLength(4).WithMessage("{PropertyName} valor demasiado largo ");
        }
    }
}
