
using Microsoft.AspNetCore.Mvc;
using BloodsyncAPI.Models;
using AutoMapper;
using BloodsyncAPI.DTOs.UserTypesDTO;
using BloodsyncAPI.Repositories;

namespace BloodsyncAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserTypesController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IGenericRepos _genericRepos;
        private readonly ISpecificRepos _specificRepos;

        public UserTypesController( IMapper mapper, IGenericRepos genericRepos, ISpecificRepos specificRepos)
        {
            _mapper = mapper;
            _genericRepos = genericRepos;
            _specificRepos = specificRepos;
        }

        // GET: api/UserTypes
        [HttpGet]
        public async Task<ActionResult<List<UserTypeReadDTO>>> GetUserType()
        {
            var usertypes= await _genericRepos.GetAll<UserType>(data => data.DateDeleted == null);
            var result = _mapper.Map<List<UserTypeReadDTO>>(usertypes);
            return Ok(result);            
        }

        // GET: api/UserTypes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserTypeReadDTO>> GetUserType(Guid id)
        {
            var userType = await _genericRepos.GetOneById<UserType>(userType => userType.UserTypeId == id && userType.DateDeleted == null);

            if (userType == null || userType.DateDeleted !=null)
            {
                return NotFound();
            }
            var result = _mapper.Map<UserTypeReadDTO>(userType);
            return Ok(result);
            
        }

        // PUT: api/UserTypes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUserType(Guid id, UserTypeUpdateDTO userTypeUpdateDTOs)
        {
            var userType = await _genericRepos.GetOneById<UserType>(userType => userType.UserTypeId == id && userType.DateDeleted == null);
            if (id != userTypeUpdateDTOs.UserTypeId || userType == null)
            {
                return BadRequest();
            }
            if (userType == null)
            {
                throw new Exception($"UserType {id} is not found.");
            }

            _mapper.Map(userTypeUpdateDTOs, userType);
            userType.DateModified = DateTime.Now;
            await _genericRepos.Update<UserType>(id, userType);
            var userTypeReadDTO = _mapper.Map<UserTypeReadDTO>(userType);
            return Ok(userTypeReadDTO);
        }

        // POST: api/UserTypes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<UserType>> PostUserType(UserTypeCreateDTO userTypeCreateDTOs)
        {
            var userType = _mapper.Map<UserType>(userTypeCreateDTOs);
            userType.DateCreated = DateTime.Now;
            await _genericRepos.Create<UserType>(userType);
            var userTypeRead = _mapper.Map<UserTypeReadDTO>(userType);
            return CreatedAtAction("GetUserType", new { id = userType.UserTypeId }, userType);
        }

        // DELETE: api/UserTypes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserType(Guid id)
        {
            var userType = await _genericRepos.GetOneById<UserType>(userType => userType.UserTypeId == id && userType.DateDeleted == null);
            if (userType == null)
            {
                 return NotFound();
            }
            userType.DateDeleted = DateTime.Now;
            await _genericRepos.Update<UserType>(id, userType);
            return NoContent();
        }
    }
}
