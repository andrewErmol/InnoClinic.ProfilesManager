using System.ComponentModel.DataAnnotations.Schema;

namespace ProfilesManager.Domain.Entities
{
    [Table("Dostors")]
    public class DoctorEntity
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public DoctorStatus Status { get; set; }
        public Guid AccountId { get; set; }
        public Guid SpecializationId { get; set; }
        public Guid OfficeId { get; set; }
        public DateTime CareerStartYear { get; set; }
        public virtual SpecializationEntity Specialization { get; set; }
        public string Address { get; set; }
    }
}
