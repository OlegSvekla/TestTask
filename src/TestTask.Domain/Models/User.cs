using System.Text.Json.Serialization;
using TestTask.Domain.Enums;

namespace TestTask.Domain.Models
{
    public class User
    {
        public int Id { get; set; } 

        public string Email { get; set; }

        public UserStatus Status { get; set; }

        [JsonIgnore]
        public virtual List<Order> Orders { get; set; }
    }
}
