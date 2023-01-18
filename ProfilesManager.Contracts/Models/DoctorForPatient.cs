﻿using ProfilesManager.Domain.Entities;

namespace ProfilesManager.Contracts.Models
{
    public class DoctorForPatient
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string Status { get; set; }
        public Guid SpecializationId { get; set; }
        public string SpecializationName { get; set; }
        public Guid OfficeId { get; set; }
        public DateTime CareerStartYear { get; set; }
        public string Address { get; set; }
    }
}
