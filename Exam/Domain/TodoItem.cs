using System;
using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class TodoItem
    {
        public int TodoItemId { get; set; }
        
        [MaxLength(128)] public string Heading { get; set; } = null!;
        
        public string Description { get; set; } = null!;
        
        public EPriority EPriority { get; set; }
        
        public DateTime DateCreated { get; set; }
        
        public DateTime DateDue { get; set; }
        
        public DateTime? DateDone { get; set; }
        
        public int CategoryId { get; set; }
        public Category Category { get; set; } = null!;
    }
}