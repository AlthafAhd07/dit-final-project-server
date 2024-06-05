namespace SkillsInternationalServer.Repositories;

using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using SkillsInternationalServer.Data;
using SkillsInternationalServer.Models;
using SkillsInternationalServer.Utilities;

public class StudentRepository : IStudentRepository
{
    private readonly AppDbContext context;

    public StudentRepository(AppDbContext _context)
    {
        context = _context;
    }




    public async Task<Student?> GetStudentByRegNo(int regNo)
    {
        return await context.Students
            .Include(s => s.Contact)
            .Include(s => s.Parent)
            .FirstOrDefaultAsync(x => x.RegNo == regNo);

    }

    public async Task<Student> CreateStudent(Student student)
    {
        context.Contacts.Add(student.Contact);
        await context.SaveChangesAsync(); 

        context.Parents.Add(student.Parent);
        await context.SaveChangesAsync();  

        student.ContactId = student.Contact.Id;
        student.ParentId = student.Parent.Id;

        student.RegNo = 0;
        context.Students.Add(student);

        await context.SaveChangesAsync();

        return student;
    }

    public async Task<Student?> UpdateStudent(Student student)
    {
        var dbStudent = await context.Students
                                     .Include(s => s.Contact)
                                     .Include(s => s.Parent)
                                     .FirstOrDefaultAsync(s => s.RegNo == student.RegNo);

        if (dbStudent == null)
        {
            return null;
        }

        // Update Student Properties
        dbStudent.FirstName = student.FirstName;
        dbStudent.LastName = student.LastName;
        dbStudent.Dob = student.Dob;
        dbStudent.Gender = student.Gender;

        // Update Contact Properties
        dbStudent.Contact.Address = student.Contact.Address;
        dbStudent.Contact.Email = student.Contact.Email;
        dbStudent.Contact.MobilePhone = student.Contact.MobilePhone;
        dbStudent.Contact.HomePhone = student.Contact.HomePhone;

        // Update Parent Properties
        dbStudent.Parent.Name = student.Parent.Name;
        dbStudent.Parent.Nic = student.Parent.Nic;
        dbStudent.Parent.ContactNumber = student.Parent.ContactNumber;

        await context.SaveChangesAsync();

        return dbStudent;
    }



    public async Task<bool> DeleteStudentByRegNo(int regNo)
    {
        var student = await context.Students
                            .Include(s => s.Contact)
                            .Include(s => s.Parent)
                            .FirstOrDefaultAsync(s => s.RegNo == regNo);

        if (student == null) return false;

        context.Contacts.Remove(student.Contact);
        context.Parents.Remove(student.Parent);
        context.Students.Remove(student);

        await context.SaveChangesAsync();
        return true;
    }


    public async Task<List<int>?> GetAllStudentIds()
    {
        return await context.Students.Select(s => s.RegNo).ToListAsync();
    }

    public async Task<IEnumerable<Student>> GetAllStudents(int page = 1, int pageSize = 10)
    {
        var (limit, offset) = Pagination.GetPagingParameters(page, pageSize);

        return await context.Students
                    .Include(s => s.Contact)
                    .Include(s => s.Parent)
                    .Skip(offset)
                    .Take(limit)
                    .ToListAsync();
    }


    public async Task<int> GetStudentsCount()
    {
        return await context.Students.CountAsync();
    }

}
