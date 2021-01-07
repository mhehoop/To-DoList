using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class Category
    {
        public int CategoryId { get; set; }
        
        [MaxLength(128)] public string Name { get; set; } = null!;
        
        public ICollection<TodoItem>? TodoItems { get; set; } //can have category without todoitem
    }
    
}