using System.ComponentModel.DataAnnotations.Schema;

namespace BloodsyncAPI.DTOs.PriorityDTO
{
    public class PriorityReadDTO
    {
        public Guid PriorityId { get; set; }
        public string PriorityLevelName { get; set; }
        public DateTime DateCreated { get; set; }

       
    }
}
