using AutoMapper;
using BloodsyncAPI.DTOs.LoginDTOs;
using BloodsyncAPI.Helpers;
using BloodsyncAPI.Models;
using BloodsyncAPI.Repositories;
using Microsoft.AspNetCore.Mvc;

// namespace BloodsyncAPI.Controllers
// {
//     [Route("api/[controller]")]
//     [ApiController]
//     public class LoginController : ControllerBase
//     {
//         private readonly IMapper _mapper;
//         private readonly ISpecificRepos _specificRepos;
//         public LoginController(IMapper mapper, ISpecificRepos specificRepos)
//         {
//             _mapper = mapper;
//             _specificRepos = specificRepos;
//         }
//         [HttpPost]
//         public async Task<ActionResult<Donor>> GetLogin()
//         {
//            const string TokenEmail = "email";
//            const string TokenPassword = "password";
//            Request.Headers.TryGetValue(TokenEmail, out var userEmail);
//            Request.Headers.TryGetValue(TokenPassword, out var userPassword);
//            var user = await _specificRepos.GetUserByEmail(userEmail);
//            if (user == null)
//            {
//                return NotFound("Email doesn't exist");
//            }
//            bool confirmResult = PasswordHasher.Confirm(userPassword, user.Password, PasswordHasher.Supported_HA.SHA256);
//            if (confirmResult)
//            {
//                var loginData = _mapper.Map<LoginDisplayDTO>(user);
//                return Ok(loginData);

//            }
//            else
//            {
//                return BadRequest("Invalid Password");
//            }
//         }

//         // [HttpPost]
//         // public async Task<ActionResult<Donor>> GetLogin()
//         // {
//         //     try  // Add try-catch to see the actual error
//         //     {
//         //         const string TokenEmail = "email";
//         //         const string TokenPassword = "password";

//         //         // Get header values and convert to string
//         //         Request.Headers.TryGetValue(TokenEmail, out var emailValues);
//         //         Request.Headers.TryGetValue(TokenPassword, out var passwordValues);

//         //         string userEmail = emailValues.FirstOrDefault() ?? "";
//         //         string userPassword = passwordValues.FirstOrDefault() ?? "";

//         //         // Debug prints
//         //         Console.WriteLine($"Email from header: {userEmail}");
//         //         Console.WriteLine($"Password from header: {userPassword}");

//         //         var user = await _specificRepos.GetUserByEmail(userEmail);
//         //         if (user == null)
//         //         {
//         //             return NotFound("Email doesn't exist");
//         //         }

//         //         // Debug print
//         //         Console.WriteLine($"DB Password: {user.Password}");

//         //         if (userPassword == user.Password)
//         //         {
//         //             var loginData = _mapper.Map<LoginDisplayDTO>(user);
//         //             return Ok(loginData);
//         //         }
//         //         else
//         //         {
//         //             return BadRequest("Invalid Password");
//         //         }
//         //     }
//         //     catch (Exception ex)
//         //     {
//         //         // This will show us the actual error
//         //         Console.WriteLine($"Error in login: {ex.Message}");
//         //         Console.WriteLine($"Stack trace: {ex.StackTrace}");
//         //         return StatusCode(500, $"Login error: {ex.Message}");
//         //     }
//         // }
//     }
// }


//--------------------
// [Route("api/[controller]")]
// [ApiController]
// public class LoginController : ControllerBase
// {
//     private readonly IMapper _mapper;
//     private readonly ISpecificRepos _specificRepos;

//     public LoginController(IMapper mapper, ISpecificRepos specificRepos)
//     {
//         _mapper = mapper;
//         _specificRepos = specificRepos;
//     }

//     [HttpPost]
//     public async Task<ActionResult<LoginDisplayDTO>> GetLogin([FromBody] LoginReadDTO loginInput)
//     {
//         try
//         {
//             if (string.IsNullOrEmpty(loginInput?.Email) || string.IsNullOrEmpty(loginInput?.Password))
//             {
//                 return BadRequest("Email and password are required");
//             }

//             var user = await _specificRepos.GetUserByEmail(loginInput.Email);
//             if (user == null)
//             {
//                 return NotFound("Email doesn't exist");
//             }

//             bool confirmResult = PasswordHasher.Confirm(
//                 loginInput.Password, 
//                 user.Password, 
//                 PasswordHasher.Supported_HA.SHA256
//             );

//             if (confirmResult)
//             {
//                 var loginData = _mapper.Map<LoginDisplayDTO>(user);
//                 return Ok(loginData);
//             }

//             return BadRequest("Invalid Password");
//         }
//         catch (Exception ex)
//         {
//             // Log the exception here
//             return StatusCode(500, "Something went wrong! We are looking into resolving this.");
//         }
//     }
// }



[Route("api/[controller]")]
[ApiController]
public class LoginController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly ISpecificRepos _specificRepos;

    public LoginController(IMapper mapper, ISpecificRepos specificRepos)
    {
        _mapper = mapper;
        _specificRepos = specificRepos;
    }

    public class LoginRequest
    {
        
        public string Email { get; set; }
        
    
        public string Password { get; set; }
    }

    [HttpPost]
    public async Task<ActionResult<LoginDisplayDTO>> GetLogin([FromBody] LoginRequest request)
    {
        try
        {
            if (request == null || string.IsNullOrEmpty(request.Email) || string.IsNullOrEmpty(request.Password))
            {
                return BadRequest("Email and password are required");
            }

            var user = await _specificRepos.GetUserByEmail(request.Email);
            if (user == null)
            {
                return NotFound("Email doesn't exist");
            }

            bool confirmResult = PasswordHasher.Confirm(
                request.Password, 
                user.Password, 
                PasswordHasher.Supported_HA.SHA256
            );

            if (confirmResult)
            {
                var loginData = _mapper.Map<LoginDisplayDTO>(user);
                return Ok(loginData);
            }

            return BadRequest("Invalid Password");
        }
        catch (Exception ex)
        {
            // Add logging here
            Console.WriteLine($"Login error: {ex.Message}");
            Console.WriteLine($"Stack trace: {ex.StackTrace}");
            
            return StatusCode(500, "Something went wrong! We are looking into resolving this.");
        }
    }
}