using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Challenge_todo_list.Models
{
    public partial class Task
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public DateTime? DueDate { get; set; }
        public int PriorityId { get; set; }
        
        public bool IsCompleted { get; set; }
        public DateTime CreatedAt { get; set; }

        public virtual Priority Priority { get; set; } = null!;
    }
}
