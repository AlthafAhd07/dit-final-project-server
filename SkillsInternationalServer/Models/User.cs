
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace SkillsInternationalServer.Models
{
    public class User
    {
        [Key]
        [NotNull]
        public int Id { get; set; }
        public string Username { get; set; } = "";
        public string Password { get; set; } = "";
    }

}
