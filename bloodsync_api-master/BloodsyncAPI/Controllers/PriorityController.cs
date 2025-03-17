using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BloodsyncAPI.Data;
using BloodsyncAPI.Models;
using System.Reflection.Metadata.Ecma335;
using AutoMapper;
using BloodsyncAPI.DTOs.HospitalsDTO;
using BloodsyncAPI.DTOs.PriorityDTO;
using BloodsyncAPI.Helpers;
using BloodsyncAPI.Repositories;
using Microsoft.EntityFrameworkCore;
using BloodsyncAPI.DTOs.InventorysDTO;
using BloodsyncAPI.DTOs.BloodGroupsDTO;

namespace BloodsyncAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PriorityController : ControllerBase
    {
        
        private readonly IMapper _mapper;
        private readonly IGenericRepos _genericRepos;
        public PriorityController( IMapper mapper, IGenericRepos genericRepos)
        {
            
            _mapper = mapper;
            _genericRepos = genericRepos;
        }


        [HttpGet]
        public async Task<ActionResult<List<PriorityReadDTO>>> GetPriorityList()
        {

            var infos = await _genericRepos.GetAll<Priority>(data => data.DateDeleted == null);
            var retunInfos = _mapper.Map<List<PriorityReadDTO>>(infos);
            return Ok(retunInfos);
        }

        // GET: api/Priority/1
        [HttpGet("{id}")]
        public async Task<ActionResult<PriorityReadDTO>> GetPriority(Guid id)
        {

            var priority = await _genericRepos.GetOneById<Priority>(priority => priority.PriorityId == id && priority.DateDeleted == null);
            if (priority == null || priority.DateDeleted !=null)
            {
                return NotFound();
            }
           return _mapper.Map<PriorityReadDTO>(priority);
        }
        



        // Put: api/priority/1
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPriority(Guid id, PriorityUpdateDTO priorityUpdateDTO)
        {
            var priority = await _genericRepos.GetOneById<Priority>(priority => priority.PriorityId == id && priority.DateDeleted == null );
            if (id != priorityUpdateDTO.PriorityId)
            {
                return BadRequest();
            }
            if (priority == null)
            {
                throw new Exception($"priority {id} is not found");
            }

            priority.DateModified = DateTime.UtcNow;
            _mapper.Map(priorityUpdateDTO, priority);
            priority = await _genericRepos.Update<Priority>(id, priority);
            var priorReadDTO = _mapper.Map<PriorityUpdateDTO>(priority);
            return Ok(priorReadDTO);
        }
        // POST: api/Priority

        [HttpPost]
        public async Task<ActionResult<Priority>> PostPatientWaitlist(PriorityCreateDTO priorityCreateDTO)
        {
            
           var prioCreateDTO = _mapper.Map<Priority>(priorityCreateDTO);

            prioCreateDTO.DateCreated= DateTime.Now;
            await _genericRepos.Create<Priority>(prioCreateDTO);

            var priority = _mapper.Map<Priority>(priorityCreateDTO);
            return Ok(priority);
        }
       
        // DELETE: api/Priority/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePriority(Guid id)
        {
            
            var priority = await _genericRepos.GetOneById<Priority>(priority => priority.PriorityId == id && priority.DateDeleted == null); ;
            if (priority == null)
            {
                return NotFound();
            }
            priority.DateDeleted = DateTime.Now;
            await _genericRepos.Update<Priority>(id, priority);


            return NoContent();
        }
    }
}






