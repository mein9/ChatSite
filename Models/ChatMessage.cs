using System;
using System.ComponentModel.DataAnnotations;

namespace AspNetCoreTodo.Models
{
    public class ChatMessage
    {
        [Key]
        public int Id { get; set; }
        public string User { get; set; } = string.Empty; // Initialize with default value
        public string Message { get; set; } = string.Empty; // Initialize with default value
        public DateTime Timestamp { get; set; }
    }
}