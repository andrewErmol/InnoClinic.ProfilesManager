using AutoMapper;
using ProfilesManager.Contracts.Models;
using ProfilesManager.Presentation.RequestEntity;

namespace ProfilesManager.API
{
    public class MappingProfileForRequest : Profile
    {
        public MappingProfileForRequest()
        {
            CreateMap<DoctorForRequest, Doctor>();
            CreateMap<PatientForRequest, Patient>();
            CreateMap<ReceptionistForRequest, Receptionist>();
            CreateMap<SpecializationForRequest, Specialization>();
        }
    }
}
