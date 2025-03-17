using System.ComponentModel.DataAnnotations.Schema;

namespace BloodsyncAPI.DTOs.UserTypesDTO
{
    public class UserTypeUpdateDTO
    {

        public Guid UserTypeId { get; set; }
        public string UserTypeName { get; set; }

        public Guid ParentTypeId { get; set; }



    }
}
