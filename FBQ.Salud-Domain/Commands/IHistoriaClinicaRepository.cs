﻿using FBQ.Salud_Domain.Entities;

namespace FBQ.Salud_Domain.Commands
{
    public interface IHistoriaClinicaRepository
    {
        List<HistoriaClinica> GetAll();
        HistoriaClinica GetHistoriaClinicaById(int id);
        void Update(HistoriaClinica historiaClinica);
        void Delete(HistoriaClinica historiaClinica);
        void Add(HistoriaClinica historiaClinica);
    }
}
