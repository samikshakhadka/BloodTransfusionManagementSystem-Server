using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BloodsyncAPI.Data;
using BloodsyncAPI.Models;
using AutoMapper;
using BloodsyncAPI.DTOs.PriorityDTO;
using BloodsyncAPI.DTOs.PatientWaitlistsDTO;
using BloodsyncAPI.Repositories;
using BloodsyncAPI.DTOs.DonorsDTO;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Drawing;
using BloodsyncAPI.DTOs.HospitalsDTO;
using BloodsyncAPI.DTOs.InventorysDTO;

namespace BloodsyncAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientWaitlistsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ISpecificRepos _specificRepos;
        private readonly IGenericRepos _genericRepos;

        public PatientWaitlistsController(IMapper mapper, ISpecificRepos specificRepos, IGenericRepos genericRepos)
        {
            _mapper = mapper;
            _specificRepos = specificRepos;
            _genericRepos = genericRepos;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PatientWaitListReadDTO>>> GetAllPatient()
        {

            var patient = await _specificRepos.GetAllPatient();
            return _mapper.Map<List<PatientWaitListReadDTO>>(patient);

        }
        [HttpGet("{id}")]
        public async Task<ActionResult<PatientWaitlist>> GetAllPatient(Guid id)
        {
            var wait = await _specificRepos.GetPatientById(id);
            if (wait == null)
            {
                return NotFound();
            }
            var patientReadDTO = _mapper.Map<PatientWaitListReadDTO>(wait);
            return Ok(patientReadDTO);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> PutPatientWaitlist(Guid id, PatientWaitListUpdateDTO patientWaitlistDTO)
        {
            var info = await _genericRepos.GetOneById<PatientWaitlist>(info => info.PatientId == id && info.DateDeleted == null);
            if (info == null)
            {
                return NotFound($" PatientWaitlist {id} is not found.");
            }
            info.DateModified = DateTime.Now;

            _mapper.Map(patientWaitlistDTO, info);
            await _genericRepos.Update(id, info);
            var priorReadDTO = _mapper.Map<PatientWaitListReadDTO>(info);
            return Ok(priorReadDTO);
        }
        [HttpPost]
        public async Task<ActionResult<PatientWaitlist>> PostPatientWaitlist(PatientWaitListCreateDTO patientWaitlistCreateDTO)
        {
            var patient = _mapper.Map<PatientWaitlist>(patientWaitlistCreateDTO);
            patient.DateCreated = DateTime.Now;
            await _genericRepos.Create<PatientWaitlist>(patient);
            var records = _mapper.Map<PatientWaitListReadDTO>(patient);
            return CreatedAtAction("GetAllPatient", new {id = patient.PatientId}, records);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePatientWaitlist(Guid id)
        {

            var waitlist = await _genericRepos.GetOneById<PatientWaitlist>
                (waitlist => waitlist.PatientId == id && waitlist.DateDeleted == null);
            if (waitlist == null)
            {
                return NotFound();
            }
            waitlist.DateDeleted = DateTime.Now;
            await _genericRepos.Update<PatientWaitlist>(id, waitlist);
            return NoContent();
        }
       
    }
}
