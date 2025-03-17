using System.ComponentModel.DataAnnotations.Schema;

namespace BloodsyncAPI.DTOs.PriorityDTO
{
    public class PriorityUpdateDTO
    {
        public Guid PriorityId { get; set; }
        public string PriorityLevelName { get; set; }
    }
}
