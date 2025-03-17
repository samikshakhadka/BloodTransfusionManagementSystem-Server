using System.ComponentModel.DataAnnotations.Schema;

namespace BloodsyncAPI.Models
{
    public class UserType
    {
        public Guid UserTypeId { get; set; }

        public string UserTypeName { get; set; }

        public Guid ParentTypeId { get; set; }
        public DateTime DateCreated { get; set; }

        public DateTime? DateModified { get; set; }

        public DateTime? DateDeleted { get; set;}

    }
}
