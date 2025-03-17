using Microsoft.AspNetCore.Mvc;
using BloodsyncAPI.Models;
using AutoMapper;
using BloodsyncAPI.DTOs.HospitalsDTO;
using BloodsyncAPI.Repositories;
using BloodSyncAPI.Helpers;

namespace BloodsyncAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowThisUserType("SuperAdmin","HospitalAdmin", "RedCrossAdmin")]
    public class HospitalsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IGenericRepos _genericRepos;
        private readonly ISpecificRepos _specificRepos;

        public HospitalsController(IMapper mapper, IGenericRepos genericRepos, ISpecificRepos specificRepos)
        {
            _mapper = mapper;
            _genericRepos = genericRepos; 
            _specificRepos = specificRepos;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Hospital>>> GetHospital()
        {
            var hospitals = await _genericRepos.GetAll<Hospital>(data => data.DateDeleted == null);
            var records = _mapper.Map<List<HospitalReadDTO>>(hospitals);
            return Ok(records);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Hospital>> GetHospital(Guid id)
        {

            var hospital = await _genericRepos.GetOneById<Hospital>(hospital => hospital.HospitalId == id && hospital.DateDeleted == null);

            if (hospital == null)
            {
                return NotFound();
            }
            var records = _mapper.Map<HospitalReadDTO>(hospital);   
            return Ok(records);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutHospital(Guid id, HospitalUpdateDTO hospitalUpdateDTO)
        {
            var hospital = await _genericRepos.GetOneById<Hospital>(hospital => hospital.HospitalId == id && hospital.DateDeleted == null);

            if (id != hospitalUpdateDTO.HospitalId)
            {
                return BadRequest();
            }
            if (hospital == null)
            {
                throw new Exception($"Hospital {id} is not found.");
            }
            hospital.DateModified = DateTime.Now;

            _mapper.Map(hospitalUpdateDTO, hospital);
            await _genericRepos.Update<Hospital>(id, hospital);

            var hospitalReadDTO = _mapper.Map<HospitalReadDTO>(hospital);
            return Ok(hospitalReadDTO);
        }

        [HttpPost]
        public async Task<ActionResult<Hospital>> PostHospital(HospitalCreateDTO hospitalCreateDTO)
        {
          var hospital = _mapper.Map<Hospital>(hospitalCreateDTO);
            hospital.DateCreated = DateTime.Now;
            await _genericRepos.Create<Hospital>(hospital);

            var hospitalReadDTO = _mapper.Map<HospitalReadDTO>(hospital);
            return CreatedAtAction("GetHospital", new { id = hospital.HospitalId }, hospitalReadDTO);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHospital(Guid id)
        {
            var hospital = await _genericRepos.GetOneById<Hospital>(hospital => hospital.HospitalId == id && hospital.DateDeleted == null);
            if (hospital == null)
            {
                return NotFound();
            }
            hospital.DateDeleted = DateTime.Now;

            await _genericRepos.Update<Hospital>(id, hospital);
            return NoContent();
        }
    }
}
