using AutoMapper;
using ProfilesManager.Contracts.Models;
using ProfilesManager.Domain.Entities;

namespace ProfilesManager.Service
{
    public class MappingProfileForEntity : Profile
    {
        public MappingProfileForEntity()
        {
            CreateMap<Doctor, DoctorEntity>();
            CreateMap<DoctorEntity, Doctor>()
                .ForMember(d => d.SpecializationName, opt => opt.MapFrom(s => s.Specialization.Name));
            CreateMap<DoctorEntity, DoctorForPatient>()
                .ForMember(d => d.SpecializationName, opt => opt.MapFrom(s => s.Specialization.Name));

            CreateMap<Patient, PatientEntity>();
            CreateMap<PatientEntity, Patient>();

            CreateMap<Specialization, SpecializationEntity>();
            CreateMap<SpecializationEntity, Specialization>();

            CreateMap<Receptionist, ReceptionistEntity>();
            CreateMap<ReceptionistEntity, Receptionist>();
        }
    }
}
