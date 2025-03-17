using Microsoft.AspNetCore.Mvc;
using BloodsyncAPI.Models;
using AutoMapper;
using BloodsyncAPI.DTOs.UsersDTO;
using BloodsyncAPI.Repositories;
using System.Transactions;
using BloodsyncAPI.Helpers;
using BloodSyncAPI.Helpers;

namespace BloodsyncAPI.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ISpecificRepos _specificRepos;
        private readonly IGenericRepos _genericRepos;

        public UsersController( IMapper mapper, ISpecificRepos specificRepos, IGenericRepos genericRepos)
        {
            
            _mapper = mapper;
            _specificRepos = specificRepos;
            _genericRepos = genericRepos;
        }

       
        
        [HttpGet]
        [AllowThisUserType("SuperAdmin")]
        public async Task<ActionResult<IEnumerable<UsersReadDTO>>> GetUser()
        {
            var users = await _specificRepos.GetAllUsers();
            var userReadDTOs = _mapper.ProjectTo<UsersReadDTO>(users);
            return Ok(userReadDTOs);

        }

        [HttpGet("{id}")]
        [AllowThisUserType("SuperAdmin", "HospitalAdmin", "NagarpalikaAdmin", "RedCrossAdmin")]
        public async Task<ActionResult<User>> GetUser(Guid id)
        {          
            var user = await _specificRepos.GetUserById(id);
            if (user == null) 
              {
                return NotFound();
              }
            var userReadDTO = _mapper.Map<UsersReadDTO>(user);
            return Ok(userReadDTO);
        }


        [HttpPut("{id}")]
        [AllowThisUserType("SuperAdmin", "HospitalAdmin", "NagarpalikaAdmin", "RedCrossAdmin")]
        public async Task<IActionResult> PutUser(Guid id, UsersUpdateDTO userUpdateDTOs)
        {
            var user = await _genericRepos.GetById<User>(id);
            if (user == null)
            {
                return NotFound();
            }
            _mapper.Map(userUpdateDTOs, user);
            string hashedPassword = PasswordHasher.ComputeHash(user.Password, PasswordHasher.Supported_HA.SHA256, null);
            user.Password = hashedPassword;
            await _genericRepos.Update(id, user);
            var userReadDTO = _mapper.Map<UsersReadDTO>(user);
            return Ok(userReadDTO);
        }


        //super admin page
        [HttpPut("resetpassword/{id}")]
        [Route("~/api/resetpassword/{id}")]
        [AllowThisUserType("SuperAdmin")]
        public async Task<IActionResult> GetUserPassword(Guid id, PasswordChangeDTO passwordChangeDTO)
        {
            var user = await _genericRepos.GetById<User>(id);
            if (user == null)
            {
                return NotFound();
            }
            _mapper.Map(passwordChangeDTO, user);
            string hashedPassword = PasswordHasher.ComputeHash(user.Password, PasswordHasher.Supported_HA.SHA256, null);
            user.Password = hashedPassword;
            user.DateModified = DateTime.Now;
            await _genericRepos.Update(id, user);
            var userReadDTO = _mapper.Map<UsersReadDTO>(user);
            return Ok(userReadDTO);
        }


        //forgetpassword
        [HttpPut("forgetpassword/{id}")]
        [Route("~/api/forgetpassword/{id}")]
        [AllowThisUserType("SuperAdmin", "HospitalAdmin", "NagarpalikaAdmin", "RedCrossAdmin")]

        public async Task<IActionResult> ResetPassword(Guid id, PasswordResetDTO passwordResetDTO)
        {
            var user = await _genericRepos.GetById<User>(id);
            if (user == null)
            {
                return NotFound();
            }
            _mapper.Map(passwordResetDTO, user);
            string hashedPassword = PasswordHasher.ComputeHash(user.Password, PasswordHasher.Supported_HA.SHA256, null);
            user.Password = hashedPassword;
            user.DateModified = DateTime.Now;
            await _genericRepos.Update(id, user);
            var userReadDTO = _mapper.Map<UsersReadDTO>(user);
            return Ok(userReadDTO);
        }


        // [HttpPost]
        // [AllowThisUserType("SuperAdmin")]
        // public async Task<ActionResult<User>> PostUser(UserDonorCreateDTO userDonorCreateDTOs)
        // {
        //     var user = _mapper.Map<User>(userDonorCreateDTOs);
        //     user.DateCreated = DateTime.Now;
        //     await _genericRepos.Create(user);
        //     var userReadDTO = _mapper.Map<UsersReadDTO>(user);
        //     return CreatedAtAction("GetUser", new { id = user.UserId }, userReadDTO);
        // }


        [HttpPost]
        [AllowThisUserType("SuperAdmin")]
        public async Task<ActionResult<User>> PostUser(UserDonorCreateDTO userDonorCreateDTOs)
        {
            try 
            {
                var user = _mapper.Map<User>(userDonorCreateDTOs);
                user.DateCreated = DateTime.Now;
                await _genericRepos.Create(user);
                var userReadDTO = _mapper.Map<UsersReadDTO>(user);
                return CreatedAtAction("GetUser", new { id = user.UserId }, userReadDTO);
            }
            catch (Exception ex)
            {
                // Get all nested exceptions
                var messages = new List<string>();
                var currentEx = ex;
                while (currentEx != null)
                {
                    messages.Add($"Error: {currentEx.Message}");
                    currentEx = currentEx.InnerException;
                }
                
                return StatusCode(500, new { 
                    errors = messages,
                    stackTrace = ex.StackTrace 
                });
            }
        }
            
        // [HttpPost]
        // [AllowThisUserType("SuperAdmin")]
        // public async Task<ActionResult<User>> PostUser(UserDonorCreateDTO userDonorCreateDTOs)
        // {
        //     try 
        //     {
        //         var user = _mapper.Map<User>(userDonorCreateDTOs);
                
        //         // Hash the password matching the forget password logic
        //         string hashedPassword = PasswordHasher.ComputeHash(user.Password, 
        //             PasswordHasher.Supported_HA.SHA256, 
        //             null);
        //         user.Password = hashedPassword;
                
        //         user.DateCreated = DateTime.Now;
        //         await _genericRepos.Create(user);
        //         var userReadDTO = _mapper.Map<UsersReadDTO>(user);
        //         return CreatedAtAction("GetUser", new { id = user.UserId }, userReadDTO);
        //     }
        //     catch (Exception ex)
        //     {
        //         // Get all nested exceptions
        //         var messages = new List<string>();
        //         var currentEx = ex;
        //         while (currentEx != null)
        //         {
        //             messages.Add($"Error: {currentEx.Message}");
        //             currentEx = currentEx.InnerException;
        //         }
                
        //         return StatusCode(500, new { 
        //             errors = messages,
        //             stackTrace = ex.StackTrace 
        //         });
        //     }
        // }

        [HttpDelete("{id}")]
        [AllowThisUserType("SuperAdmin")]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
                var user = await _genericRepos.GetById<User>(id);
                if (user == null)
                {
                    return NotFound();
                }
                user.DateDeleted = DateTime.Now;
                await _genericRepos.Update<User>(id, user);
                return NoContent();
        }


       


        //Special Function
        [HttpDelete]
        [Route("~/api/deletedonor/{id}")]
        [AllowThisUserType("SuperAdmin")]
        public async Task<IActionResult> DeleteUserDonor(Guid id)
        {
            using var transactionScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
            try
            {
                var user = await _genericRepos.GetById<User>(id);
                if (user == null)
                {
                    return NotFound();
                }
                var donor = await _genericRepos.GetById<Donor>(user.DonorId);
                if(donor == null)
                {
                    return NotFound();
                }
                
                user.DateDeleted = DateTime.Now;
                donor.DateDeleted = DateTime.Now;
                await _genericRepos.Update<User>(id, user);
                await _genericRepos.Update<Donor>(donor.DonorId, donor);
                transactionScope.Complete();
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }

        }
    }
}
