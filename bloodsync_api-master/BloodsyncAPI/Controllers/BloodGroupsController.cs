using Microsoft.AspNetCore.Mvc;
using BloodsyncAPI.Models;
using BloodsyncAPI.DTOs.BloodGroupsDTO;
using AutoMapper;
using BloodsyncAPI.Repositories;

namespace BloodsyncAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BloodGroupsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IGenericRepos _genericRepos;
        public BloodGroupsController(IMapper mapper, IGenericRepos genericRepos)
        {
            _mapper = mapper;
            _genericRepos = genericRepos;
        }
        [HttpGet]
        public async Task<ActionResult<List<BloodGroupReadDTO>>> GetBloodGroup()
        {
            var bloodGroups =  await _genericRepos.GetAll<BloodGroup>(data => data.DateDeleted == null);
            return _mapper.Map<List<BloodGroupReadDTO>>(bloodGroups);
        }
         
        [HttpGet("{id}")]
        public async Task<ActionResult<BloodGroupReadDTO>> GetBloodGroup(Guid id)
        {
            var bloodGroup = await _genericRepos.GetOneById<BloodGroup>(bloodGroup=>bloodGroup.BloodGroupId  == id && bloodGroup.DateDeleted == null);

            if (bloodGroup == null || bloodGroup.DateDeleted != null)
            {
                return NotFound();
            }
            return _mapper.Map<BloodGroupReadDTO>(bloodGroup);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBloodGroup(Guid id, BloodGroupUpdateDTO bloodGroupUpdateDTO)
        {
            var bloodGroup = await _genericRepos.GetById<BloodGroup>(id);

            if (id != bloodGroupUpdateDTO.BloodGroupId || bloodGroup == null)
            {
                return BadRequest();
            }   
            _mapper.Map(bloodGroupUpdateDTO, bloodGroup);
            bloodGroup.DateModified = DateTime.Now;
            await _genericRepos.Update<BloodGroup>(id, bloodGroup);
            var bloodGroupReadDTO = _mapper.Map<BloodGroupReadDTO>(bloodGroup);
            return Ok(bloodGroupReadDTO);
        }
        [HttpPost]
        public async Task<ActionResult<BloodGroup>> PostBloodGroup(BloodGroupCreateDTO bloodGroupCreateDTO)
        {

            var bloodGroup = _mapper.Map<BloodGroup>(bloodGroupCreateDTO);
            bloodGroup.DateCreated = DateTime.Now;
            await _genericRepos.Create<BloodGroup>(bloodGroup);
            var bloodGroupRead = _mapper.Map<BloodGroupReadDTO>(bloodGroup);
            return CreatedAtAction("GetBloodGroup", new { id = bloodGroup.BloodGroupId }, bloodGroupRead);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBloodGroup(Guid id)
        {
            var bloodGroup = await _genericRepos.GetById<BloodGroup>(id);
            if (bloodGroup == null)
            {
                return NotFound();
            }
            bloodGroup.DateDeleted = DateTime.Now;
            await _genericRepos.Update<BloodGroup>(id, bloodGroup);
            return NoContent();
        }
    }
}