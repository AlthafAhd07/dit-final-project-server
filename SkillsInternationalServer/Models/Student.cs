using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace SkillsInternationalServer.Models
{
    public class Student
    {
        [Key][NotNull]
        public int RegNo { get; set; }

        public string FirstName { get; set; } = "";

        public string LastName { get; set; } = "";

        public string Dob { get; set; } = "";

        public string Gender { get; set; } = "";


        // Relationships
        public int ContactId { get; set; }
        public Contact Contact { get; set; }

        public int ParentId { get; set; }
        public Parent Parent { get; set; }
    }
}
