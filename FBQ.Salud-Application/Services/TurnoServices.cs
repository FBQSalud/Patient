using AutoMapper;
using FBQ.Salud_Domain.Commands;
using FBQ.Salud_Domain.Dtos;
using FBQ.Salud_Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FBQ.Salud_Application.Services
{
    public interface ITurnoServices
    {
        List<Turno> GetAll();
        Turno GetTurnoById(int id);
        void Update(Turno turno);
        void Delete(Turno turno);
        Turno CreateTurno(TurnoDTO turno);
    }
    public class TurnoServices : ITurnoServices
    {
        private ITurnosRepository _turnosRepository;
        private readonly IMapper _mapper;

        public TurnoServices(ITurnosRepository turnosRepository,
            IMapper mapper)
        {
            _turnosRepository = turnosRepository;
            _mapper = mapper;   
        }

        public List<Turno> GetAll()
        {
            return _turnosRepository.GetAll();
        }
        public Turno GetTurnoById(int id)
        {
            return _turnosRepository.GetTurnoById(id);
        }

        public Turno CreateTurno(TurnoDTO turno)
        {
            var turnoMapped = _mapper.Map<Turno>(turno);
            _turnosRepository.Add(turnoMapped);

            return turnoMapped;
        }

        public void Update(Turno turno)
        {
            _turnosRepository.Update(turno);
        }

        public void Delete(Turno turno)
        {
            _turnosRepository.Delete(turno);
        }
    }
}
