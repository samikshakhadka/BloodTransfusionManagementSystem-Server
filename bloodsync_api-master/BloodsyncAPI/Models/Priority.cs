using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BloodsyncAPI.Models
{
    public class Priority
    {
        [Key]
        public Guid PriorityId { get; set; }
        public string PriorityLevelName { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
        public DateTime? DateDeleted { get; set; }

       
    }
}
