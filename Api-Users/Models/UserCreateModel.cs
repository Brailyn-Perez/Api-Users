using System.ComponentModel.DataAnnotations;

namespace Api_Users.Models
{
    public class UserCreateModel
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}
