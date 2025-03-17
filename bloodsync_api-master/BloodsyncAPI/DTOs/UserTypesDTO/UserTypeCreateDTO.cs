using System.ComponentModel.DataAnnotations.Schema;

namespace BloodsyncAPI.DTOs.UserTypesDTO
{
    public class UserTypeCreateDTO
    {
       

        public string UserTypeName { get; set; }

        public Guid ParentTypeId { get; set; }



       

       


    }
}
