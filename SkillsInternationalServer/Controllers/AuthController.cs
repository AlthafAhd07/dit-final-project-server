using Microsoft.AspNetCore.Mvc;

using SkillsInternationalServer.Models;
using SkillsInternationalServer.Utilities;
using SkillsInternationalServer.Repositories;
using SkillsInternationalServer.Services;

namespace SkillsInternationalServer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {

        private readonly IAuthRepository _repository;
        private readonly IAuthService _authService;

        public AuthController(IAuthRepository repository, IAuthService authService)
        {
            _repository = repository;
            _authService = authService; // Inject the service
        }



    public class LoginResponseData
        {
            public User? User { get; set; }
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] User _user)
        {
            try
            {
                User? user = await _authService.LoginUser(_user.Username, _user.Password);


                if (user == null)
                {
                    ApiResponse<LoginResponseData> resp = new ApiResponse<LoginResponseData>
                    {
                        success = false,
                        message = null,
                        error = new Utilities.Error { code = "USER1000", message = "Invalid credentials." }
                    };

                    return StatusCode(401, resp);
                }


                ApiResponse<LoginResponseData> response = new ApiResponse<LoginResponseData>
                {
                    success = true,
                    message = "Loggedin successfully!",
                    data = new LoginResponseData
                    {
                        User = user
                    }
                };

                return StatusCode(200, response);

            }

            catch (Exception ex)
            {

                ApiResponse<LoginResponseData> response = new ApiResponse<LoginResponseData>
                {
                    success = false,
                    message = null,
                    error = new Utilities.Error { code = "SYS100", message = "An error occured. Please try again later." }
                };

                return StatusCode(500, response);
            }
        }

    }
}
