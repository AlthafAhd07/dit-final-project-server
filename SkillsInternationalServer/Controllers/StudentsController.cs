using Microsoft.AspNetCore.Mvc;

using SkillsInternationalServer.Models;
using SkillsInternationalServer.Utilities;
using SkillsInternationalServer.Repositories;
using Microsoft.AspNetCore.Authorization;

namespace SkillsInternationalServer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = "BasicAuthentication")]
    public class StudentsController : ControllerBase
    {

        private readonly IStudentRepository _repository;

        public StudentsController(IStudentRepository repository)
        {
            _repository = repository;
        }


        public class AllStudentsResponseData
        {
            public IEnumerable<Student>? Students { get; set; }
            public PaginationResult? Pagination { get; set; }
        }

        public class StudentIdsResponseData
        {
            public List<int>? StudentIds { get; set; }
        }

        public class SingleStudentResponseData
        {
            public Student? Student { get; set; }
        }
        public class CreateStudentResponseData
        {
            public Student? Student { get; set; }
        }



        [HttpPost()]
        public async Task<ApiResponse<CreateStudentResponseData>> CreateStudent([FromBody] Student student)
        {
            try
            {
                Student newStudent = await _repository.CreateStudent(student);

                return new ApiResponse<CreateStudentResponseData>
                {
                    success = true,
                    message = "Student created successfully!",
                    data = new CreateStudentResponseData
                    {
                        Student = newStudent ?? null,
                    }

                };

            }

            catch (Exception ex)
            {
                return new ApiResponse<CreateStudentResponseData>
                {
                    success = false,
                    message = null,
                    error = new Error { code = "SYS100", message = "An error occured. Please try again later." }

                };
            }
        }



        [HttpPut("{regNo:int}")]
        public async Task<ApiResponse<CreateStudentResponseData>> UpdateStudent(int regNo ,[FromBody] Student student)
        {
            try
            {

                student.RegNo = regNo;

                Student? updatedStudent = await _repository.UpdateStudent(student);


                if(updatedStudent == null)
                {
                    throw new Exception();
                }

                return new ApiResponse<CreateStudentResponseData>
                {
                    success = true,
                    message = "Student updated successfully!",
                    data = new CreateStudentResponseData
                    {
                        Student = updatedStudent ?? null,
                    }

                };

            }

            catch (Exception ex)
            {
                return new ApiResponse<CreateStudentResponseData>
                {
                    success = false,
                    message = null,
                    error = new Error { code = "SYS100", message = "An error occured. Please try again later." }

                };
            }
        }





        [HttpDelete("{regNo:int}")]
        public async Task<ApiResponse<int?>> DeleteStudent(int regNo)
        {
            try
            {
               bool isDeleted = await _repository.DeleteStudentByRegNo(regNo);

                if (!isDeleted)
                {
                    throw new Exception();
                }

                return new ApiResponse<int?>
                {
                    success = true,
                    message = "Student deleted successfully!",
                };

            }

            catch (Exception ex)
            {
                return new ApiResponse<int?>
                {
                    success = false,
                    message = null,
                    error = new Error { code = "SYS100", message = "An error occured. Please try again later." }
                };
            }
        }



        [HttpGet("{regNo:int}")]
        public async Task<ApiResponse<SingleStudentResponseData>> GetSingleStudent(int regNo)
        {


            try
            {
                Student? student = await _repository.GetStudentByRegNo(regNo);

                return new ApiResponse<SingleStudentResponseData>
                {
                    success = true,
                    data = new SingleStudentResponseData
                    {
                        Student = student ?? null,
                    }

                };

            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return new ApiResponse<SingleStudentResponseData>
                {
                    success = false,
                    message = null,
                    error = new Error { code = "SYS100", message = "An error occured. Please try again later." }

                };
            }
        }

        [HttpGet]
        public async Task<ApiResponse<AllStudentsResponseData>> GetAllStudents(int page = 1, int pageSize = 10)
        {
            try
            {
                IEnumerable<Student> students = await _repository.GetAllStudents(page, pageSize);
                int studentsCount = await _repository.GetStudentsCount();

                PaginationResult paginationResult = Pagination.GetPaginationMetadata(page, pageSize, studentsCount);

                return new ApiResponse<AllStudentsResponseData>
                {
                    success = true,
                    data = new AllStudentsResponseData
                    {
                        Students = students ?? Enumerable.Empty<Student>(), // Handle potential null
                        Pagination = paginationResult
                    }

                };
            }
            catch (Exception ex)
            {
                return new ApiResponse<AllStudentsResponseData>
                {
                    success = false,
                    message = null,
                    error = new Error { code = "SYS100", message = "An error occured. Please try again later." }
                };
            }
        }


        [HttpGet("ids")]
        public async Task<ApiResponse<StudentIdsResponseData>> GetAllStudentIds() {
            try
            {
                List<int>? studentIds = await _repository.GetAllStudentIds();

                return new ApiResponse<StudentIdsResponseData>
                {
                    success = true,
                    data = new StudentIdsResponseData
                    {
                        StudentIds = studentIds,

                    }

                };
            }
            catch (Exception ex)
            {
                return new ApiResponse<StudentIdsResponseData>
                {
                    success = false,
                    message = null,
                    error = new Error { code = "SYS100", message = "An error occured. Please try again later." }
                };
            }
        }
    }



}
