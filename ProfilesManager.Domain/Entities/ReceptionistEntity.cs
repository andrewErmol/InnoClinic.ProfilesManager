using System.ComponentModel.DataAnnotations.Schema;

namespace ProfilesManager.Domain.Entities
{
    [Table("Receptionists")]
    public class ReceptionistEntity
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public Guid OfficeId { get; set; }
        public Guid AccountId { get; set; }
    }
}
