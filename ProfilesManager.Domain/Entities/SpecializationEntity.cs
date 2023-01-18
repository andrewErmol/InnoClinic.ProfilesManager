using System.ComponentModel.DataAnnotations.Schema;

namespace ProfilesManager.Domain.Entities
{
    [Table("Specializations")]
    public class SpecializationEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
