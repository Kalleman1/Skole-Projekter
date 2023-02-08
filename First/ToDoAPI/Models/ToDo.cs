using System.ComponentModel.DataAnnotations;

namespace ToDoAPI.Models
{
    public class ToDo
    {
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
    }
}
