using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace SkillsInternationalServer.Models
{
    public class Parent
    {
        [Key]
        [NotNull]
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public string Nic { get; set; } = "";
        public string ContactNumber { get; set; } = "";


    }
}
