using Microsoft.AspNetCore.Mvc;
using BloodsyncAPI.Models;
using BloodsyncAPI.DTOs.DonorsDTO;
using AutoMapper;
using BloodsyncAPI.Repositories;
using BloodsyncAPI.DTOs.UsersDTO;
using System.Transactions;
using BloodsyncAPI.Helpers;
using BloodSyncAPI.Helpers;
using BloodsyncAPI.DTOs;

namespace BloodsyncAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DonorsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ISpecificRepos _specificRepos;
        private readonly IGenericRepos _genericRepos;
        public DonorsController(IMapper mapper, ISpecificRepos specificRepos, IGenericRepos genericRepos)
        {
            _mapper = mapper;
            _specificRepos = specificRepos;
            _genericRepos = genericRepos;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DonorResponseDTO>>> GetAllDonors([FromQuery] List<Filter> filters, [FromQuery] string? filterQuery, [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 100)
        {
            var donors = await _specificRepos.GetAllDonors(filters, pageNumber, pageSize);
            var responseDTO = new DonorResponseDTO
            {
                Count = donors.count,
                Data = _mapper.Map<List<DonorReadDTO>>(donors.Donors)
            };
            return Ok(responseDTO);
        }

        [HttpGet("{id}")]

        public async Task<ActionResult<Donor>> GetDonor(Guid id)
        {
            var donor = await _specificRepos.GetDonorById(id);

            if (donor == null)
            {
                return NotFound();
            }
            var donorReadDTO = _mapper.Map<DonorReadDTO>(donor);
            return Ok(donorReadDTO);
        }

        [HttpPost]
        [BlockThisUserType("NagarpalikaAdmin")]
        public async Task<ActionResult<Donor>> CreateDonor(DonorCreateDTO donorCreateDTO)
        {
            var donor = _mapper.Map<Donor>(donorCreateDTO);
            donor.DateCreated = DateTime.Now;
            await _genericRepos.Create<Donor>(donor);
            var donorReadDTO = _mapper.Map<DonorReadDTO>(donor);

            return CreatedAtAction("GetDonor", new { id = donor.DonorId }, donorReadDTO);
        }

        [HttpDelete("{id}")]
        [BlockThisUserType("NagarpalikaAdmin")]
        public async Task<IActionResult> DeleteDonor(Guid id)
        {
            try
            {
                var donor = await _genericRepos.GetById<Donor>(id);
                if (donor == null)
                {
                    return NotFound();
                }
                donor.DateDeleted = DateTime.Now;
                await _genericRepos.Update<Donor>(id, donor);
                await _specificRepos.DeleteRelatedUserByDonorId(id);
                return NoContent();

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPut("{id}")]
        [BlockThisUserType("NagarpalikaAdmin")]
        public async Task<ActionResult> UpdateDonor(Guid id, DonorUpdateDTO updatedDonorDTO)
        {

            var donor = await _genericRepos.GetById<Donor>(id);
            if (donor == null)
            {
                return NotFound();
            }
            _mapper.Map(updatedDonorDTO, donor);
            await _genericRepos.Update(id, donor);
            var updatedReadDTO = _mapper.Map<DonorReadDTO>(donor);
            return Ok(updatedReadDTO);
        }

        // donor user methods
        [HttpPost]
        [Route("~/api/userdonor")]
        [BlockThisUserType("NagarpalikaAdmin")]
        public async Task<ActionResult<User>> PostUserDonor(UserDonorCreateDTO userDonorCreateDTOs)
        {
            using var transactionScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
            var donor = _mapper.Map<Donor>(userDonorCreateDTOs);
            donor.DateCreated = DateTime.Now;
            var user = _mapper.Map<User>(userDonorCreateDTOs);
            user.DateCreated = DateTime.Now;
            string hashedPassword = PasswordHasher.ComputeHash(user.Password, PasswordHasher.Supported_HA.SHA256, null);
            user.Password = hashedPassword;
            if (!await _specificRepos.IsEmailUnique(user.Email))
            {
                return BadRequest("Email already exists");
            }

            try
            {
                user.IsVerified = "0";
                await _genericRepos.Create(donor);

                user.DonorId = donor.DonorId;
                await _genericRepos.Create(user);
                var donorReadDTO = _mapper.Map<DonorReadDTO>(donor);
                transactionScope.Complete();
                return CreatedAtAction("GetDonor", new { id = donor.DonorId }, donorReadDTO);

            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }

        }

        [HttpPut("userdonor/{id}")]
        [Route("~/api/userdonor/{id}")]
        [BlockThisUserType("NagarpalikaAdmin")]
        public async Task<IActionResult> UpdateUserDonor(Guid id, UserDonorUpdateDTO userDonorUpdateDTO)
        {
            using var transactionScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
            var donor = await _genericRepos.GetById<Donor>(id);
            var user = await _specificRepos.GetUserByDonorID(id);
            if (user == null || donor == null)
            {
                return NotFound();
            }
            _mapper.Map(userDonorUpdateDTO, user);
            _mapper.Map(userDonorUpdateDTO, donor);
            try
            {
                user.DateModified = DateTime.Now;
                donor.DateModified = DateTime.Now;
                await _genericRepos.Update(id, donor);
                await _genericRepos.Update(user.UserId, user);
                transactionScope.Complete();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
            var donorReadDTO = _mapper.Map<DonorReadDTO>(donor);
            return Ok(donorReadDTO);
        }

        [HttpGet("filtereddonors/{bloodgroup}/{municipality}/{wardno}")]
        [Route("~/api/filtereddonors/{bloodgroup}/{municipality}/{wardno}")]
        public async Task<ActionResult<IEnumerable<DonorResponseDTO>>> GetFilteredDonors(string bloodGroup, string municipality, int wardNo)
        {
            var donors = await _specificRepos.GetFilteredDonors(bloodGroup, municipality, wardNo);
            return Ok(donors);
        }

        [HttpGet("/totalcount/{hospitalId}")]
        [Route("~/api/totalcount/{hospitalId}")]
        public async Task<ActionResult<IEnumerable<InfoCountDTO>>> GetDonorByHospitalId(Guid hospitalId)
        {
            var info = _specificRepos.GetAllDonorsByHospitalId(hospitalId);
            var infoCountDTO = new InfoCountDTO()
            {
                InventoryCount = info.Result.InventoryCount,
                DonorCount = info.Result.DonorCount,
                PatientCount = info.Result.PatientCount
            };
            return Ok(infoCountDTO);

        }

    }
}
