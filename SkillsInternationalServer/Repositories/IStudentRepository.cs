namespace SkillsInternationalServer.Repositories;

using System.Collections.Generic;
using System.Threading.Tasks;
using SkillsInternationalServer.Models;

public interface IStudentRepository
{
    Task<Student?> GetStudentByRegNo(int regNo);
    Task<Student> CreateStudent(Student student);
    Task<Student?> UpdateStudent(Student student);
    Task<bool> DeleteStudentByRegNo(int regNo);

    Task<List<int>?> GetAllStudentIds();
    Task<IEnumerable<Student>> GetAllStudents(int page , int pageSize);
    Task<int> GetStudentsCount();

}
