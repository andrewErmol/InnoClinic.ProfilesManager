using AutoMapper;
using ProfilesManager.Contracts.Models;
using ProfilesManager.Contracts.RequestEntity;

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
