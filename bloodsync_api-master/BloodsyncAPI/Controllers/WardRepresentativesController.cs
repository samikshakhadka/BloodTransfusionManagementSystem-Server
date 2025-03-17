using AutoMapper;
using BloodsyncAPI.DTOs.WardRepresentativesDTO;
using BloodsyncAPI.Models;
using BloodsyncAPI.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace BloodsyncAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WardRepresentativesController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IGenericRepos _genericRepos;
        private readonly ISpecificRepos _specificRepos;

        public WardRepresentativesController(IMapper mapper, IGenericRepos genericRepos, ISpecificRepos specificRepos)
        {
            _mapper = mapper;
            _genericRepos = genericRepos;
            _specificRepos = specificRepos;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<WardRepresentativesReadDTO>>> GetBloodGroup()
        {
            var wardRepresentatives = await _specificRepos.GetAllWardRepresentatives();
            return _mapper.Map<List<WardRepresentativesReadDTO>>(wardRepresentatives);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<WardRepresentativesReadDTO>> GetWardRepresentativeById(Guid id)
        {
            var wardRep = await _specificRepos.GetWardRepById(id);

            if (wardRep == null)
            {
                return NotFound();
            }
            return _mapper.Map<WardRepresentativesReadDTO>(wardRep);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateWardRep(Guid id, WardRepresentativesUpdateDTO wardRepresentativesUpdateDTO)
        {
            var wardRep = await _genericRepos.GetOneById<WardRepresentatives>(wardRep => wardRep.WardRepID == id && wardRep.DateDeleted == null);

            if (id != wardRepresentativesUpdateDTO.WardRepID || wardRep == null)
            {
                return BadRequest();
            }
            _mapper.Map(wardRepresentativesUpdateDTO, wardRep);
            wardRep.DateModified = DateTime.Now;
            await _genericRepos.Update<WardRepresentatives>(id, wardRep);
            var bloodGroupReadDTO = _mapper.Map<WardRepresentativesReadDTO>(wardRep);
            return Ok(wardRep);
        }
        [HttpPost]
        public async Task<ActionResult<WardRepresentatives>> PostWardRepresentative(WardRepresentativesCreateDTO wardRepresentativesCreateDTO)
        {

            var wardRepresentative = _mapper.Map<WardRepresentatives>(wardRepresentativesCreateDTO);
            wardRepresentative.DateCreated = DateTime.Now;
            await _genericRepos.Create(wardRepresentative);
            var bloodGroupRead = _mapper.Map<WardRepresentativesReadDTO>(wardRepresentative);
            return CreatedAtAction("GetWardRepresentativeById", new { id = wardRepresentative.WardRepID }, bloodGroupRead);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletWardRepresentative(Guid id)
        {
            var wardRepresentative = await _genericRepos.GetOneById<WardRepresentatives>(wardRep => wardRep.WardRepID == id && wardRep.DateDeleted == null);
            if (wardRepresentative == null)
            {
                return NotFound();
            }
            wardRepresentative.DateDeleted = DateTime.Now;
            await _genericRepos.Update<WardRepresentatives>(id, wardRepresentative);
            return NoContent();
        }

    }
}
