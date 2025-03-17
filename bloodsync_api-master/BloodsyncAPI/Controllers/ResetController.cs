using AutoMapper;
using BloodsyncAPI.DTOs.LoginDTOs;
using BloodsyncAPI.Helpers;
using BloodsyncAPI.Models;
using BloodsyncAPI.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace BloodsyncAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResetController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ISpecificRepos _specificRepos;

        public ResetController(IMapper mapper, ISpecificRepos specificRepos)
        {
            _mapper = mapper;
            _specificRepos = specificRepos;
        }
        [HttpPost]
        public async Task<ActionResult<Donor>> GetLogin()
        {
            const string TokenEmail = "email";
            Request.Headers.TryGetValue(TokenEmail, out var userEmail);
            var user = await _specificRepos.GetUserByEmail(userEmail);
            if (user == null)
            {
                return NotFound("Email doesn't exist");
            }else
            {
                //await _specificRepos.SendEmailAsync(userEmail, "Test", "Hello World!");
                var loginData = _mapper.Map<LoginDisplayDTO>(user);
                return Ok(loginData);
            }
            
        }
    }
}
