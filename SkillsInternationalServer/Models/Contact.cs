using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace SkillsInternationalServer.Models
{
    public class Contact
    {
        [Key]
        [NotNull]
        public int Id { get; set; }
    
        public string Address { get; set; } = "";
        public string Email { get; set; } = "";
        public string MobilePhone { get; set; } = "";
        public string HomePhone { get; set; } = "";
  
}
}
