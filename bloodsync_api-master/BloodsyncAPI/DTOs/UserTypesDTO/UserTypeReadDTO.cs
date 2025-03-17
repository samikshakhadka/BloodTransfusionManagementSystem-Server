using System.ComponentModel.DataAnnotations.Schema;

namespace BloodsyncAPI.DTOs.UserTypesDTO
{
    public class UserTypeReadDTO
    {
        public Guid UserTypeId { get; set; }

        public string UserTypeName { get; set; }

        public Guid ParentTypeId { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime DateCreated { get; set; }

        public DateTime? DateModified { get; set; }

        public DateTime? DateDeleted { get; set; }

    }
}
