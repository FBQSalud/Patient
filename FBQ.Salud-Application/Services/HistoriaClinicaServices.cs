using AutoMapper;
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
        IHistoriaClinicaServices _historiaClinicaServices;
        private readonly IMapper _mapper;

        public HistoriaClinicaServices(IHistoriaClinicaServices historiaClinicaServices, 
            IMapper mapper)
        {
            _historiaClinicaServices = historiaClinicaServices;
            _mapper = mapper;
        }
        public List<HistoriaClinica> GetAll()
        {
            return _historiaClinicaServices.GetAll();
        }
        public HistoriaClinica GetHistoriaClinicaById(int id)
        {
            return _historiaClinicaServices.GetHistoriaClinicaById(id);
        }
       
        public HistoriaClinica CreateHistoriaClinica(HistoriaClinicaDTO historiaClinica)
        {
            var historiaClinicaMapped = _mapper.Map<HistoriaClinica>(historiaClinica);
            _historiaClinicaServices.Add(historiaClinicaMapped);
            return historiaClinicaMapped;
        }

        public void Add(HistoriaClinica historiaClinica)
        {
            var historia = _mapper.Map<HistoriaClinica>(historiaClinica);
            _historiaClinicaServices.Add(historiaClinica);
        }

        public void Update(HistoriaClinica historiaClinica)
        {
            _historiaClinicaServices.Update(historiaClinica);
        }

        public void Delete(HistoriaClinica historiaClinica)
        {
            _historiaClinicaServices.Delete(historiaClinica);
        }      
    }
}
