﻿namespace ProfilesManager.Presentation.RequestEntity
{
    public class ReceptionistForRequest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public Guid OfficeId { get; set; }
        public Guid AccountId { get; set; }
    }
}
