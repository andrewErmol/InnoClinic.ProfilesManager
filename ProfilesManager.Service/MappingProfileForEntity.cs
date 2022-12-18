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
            CreateMap<DoctorEntity, Doctor>();
            CreateMap<DoctorEntity, DoctorForPatient>();

            CreateMap<Patient, PatientEntity>();
            CreateMap<PatientEntity, Patient>();

            CreateMap<Specialization, SpecializationEntity>();
            CreateMap<SpecializationEntity, Specialization>();

            CreateMap<Receptionist, ReceptionistEntity>();
            CreateMap<ReceptionistEntity, Receptionist>();
        }
    }
}
