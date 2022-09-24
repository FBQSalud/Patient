using AutoMapper;
using FBQ.Salud_Domain.Commands;
using FBQ.Salud_Domain.Dtos;
using FBQ.Salud_Domain.Entities;


namespace FBQ.Salud_Application.Services
{
    public interface IHistoriaClinicaServices
    {
        List<HistoriaClinica> GetAll();
        HistoriaClinica GetHistoriaClinicaById(int id);
        void Update(HistoriaClinica historiaClinica);
        void Delete(HistoriaClinica historiaClinica);
        void Add(HistoriaClinica historiaClinica);
        HistoriaClinica CreateHistoriaClinica(HistoriaClinicaDTO historiaClinica);
    }
    public class HistoriaClinicaServices : IHistoriaClinicaServices
    {
        IHistoriaClinicaRepository _historiaClinicaRepository;
        private readonly IMapper _mapper;

        public HistoriaClinicaServices(IHistoriaClinicaRepository historiaClinicaRepository, 
            IMapper mapper)
        {
            _historiaClinicaRepository = historiaClinicaRepository;
            _mapper = mapper;
        }
        public List<HistoriaClinica> GetAll()
        {
            return _historiaClinicaRepository.GetAll();
        }
        public HistoriaClinica GetHistoriaClinicaById(int id)
        {
            return _historiaClinicaRepository.GetHistoriaClinicaById(id);
        }
       
        public HistoriaClinica CreateHistoriaClinica(HistoriaClinicaDTO historiaClinica)
        {
            var historiaClinicaMapped = _mapper.Map<HistoriaClinica>(historiaClinica);
            _historiaClinicaRepository.Add(historiaClinicaMapped);
            return historiaClinicaMapped;
        }

        public void Add(HistoriaClinica historiaClinica)
        {
            var historia = _mapper.Map<HistoriaClinica>(historiaClinica);
            _historiaClinicaRepository.Add(historiaClinica);
        }

        public void Update(HistoriaClinica historiaClinica)
        {
            _historiaClinicaRepository.Update(historiaClinica);
        }

        public void Delete(HistoriaClinica historiaClinica)
        {
            _historiaClinicaRepository.Delete(historiaClinica);
        }      
    }
}
