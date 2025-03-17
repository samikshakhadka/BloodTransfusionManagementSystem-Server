using Microsoft.AspNetCore.Mvc;
using BloodsyncAPI.Models;
using AutoMapper;
using BloodsyncAPI.Repositories;
using BloodsyncAPI.DTOs.InventorysDTO;
using BloodSyncAPI.Helpers;

namespace BloodsyncAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [BlockThisUserType("NagarpalikaAdmin")]
    public class InventorysController : ControllerBase
    {
        public const string NagarpalikaAdmin = "UserType:NagarpalikaAdmin";
        private readonly IMapper _mapper;
        private readonly IGenericRepos _genericRepos;
        private readonly ISpecificRepos _specificRepos;
        private readonly IConfiguration _configuration;
        public InventorysController(IMapper mapper, IGenericRepos genericRepos, ISpecificRepos specificRepos, IConfiguration configuration)
        {
            _mapper = mapper;
            _genericRepos = genericRepos;
            _specificRepos = specificRepos;
            _configuration = configuration;
        }

        [HttpGet]
        public async Task<ActionResult<List<InventoryReadDTO>>> GetInventory()
        {
            //const string TokenUserId = "userTypeId";
            //Request.Headers.TryGetValue(TokenUserId, out var userTypeId);
            //var nagarpalikaAdminId = _configuration.GetValue<string>(NagarpalikaAdmin);

            //if(nagarpalikaAdminId == userTypeId) {
            //    return BadRequest("You are not allowed to access");
            //}
            var inventory = await _specificRepos.GetAllInventory();
            var inventoryReadDTO = _mapper.Map<List<InventoryReadDTO>>(inventory);
            return Ok(inventoryReadDTO);
        }

        [HttpGet("{id}")]

        public async Task<ActionResult<InventoryReadDTO>> GetInventory(Guid id)
        {
            //const string TokenUserId = "userTypeId";
            //Request.Headers.TryGetValue(TokenUserId, out var userTypeId);
            //var nagarpalikaAdminId = _configuration.GetValue<string>(NagarpalikaAdmin);
            var inventory = await _specificRepos.GetInventoryById(id);
            //if (nagarpalikaAdminId == userTypeId)
            //{
            //    return BadRequest("You are not allowed to access");
            //}
            if (inventory == null)
            {
                return NotFound();
            }

            var inventoryReadDTO = _mapper.Map<InventoryReadDTO>(inventory);
            return Ok(inventoryReadDTO);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutInventory (Guid id, InventoryUpdateDTO inventoryUpdateDTO)
        {
            var inventory = await _genericRepos.GetOneById<Inventory>(inventory => inventory.InventoryId == id && inventory.DateDeleted == null);

            if (id != inventoryUpdateDTO.InventoryId)
            {
                return BadRequest();
            }
            if (inventory == null)
            {
                throw new Exception($"Inventory {id} is not found.");
            }
            inventory.DateModified = DateTime.Now;   
            _mapper.Map(inventoryUpdateDTO, inventory);
            await _genericRepos.Update<Inventory>(id, inventory);

            var inventoryReadDTO = _mapper.Map<InventoryReadDTO>(inventory);
            return Ok(inventoryReadDTO);
        }

        [HttpPost]

        public async Task<ActionResult<Inventory>> PostInventory(InventoryCreateDTO inventoryCreateDTO)
        {

            var inventory = _mapper.Map<Inventory>(inventoryCreateDTO);
            inventory.DateCreated = DateTime.Now;
            await _genericRepos.Create<Inventory>(inventory);

            var inventoryReadDTO = _mapper.Map<InventoryReadDTO>(inventory);
            return CreatedAtAction("GetInventory", new { id =  inventory.InventoryId }, inventoryReadDTO);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInventory(Guid id)
        {
            var inventory = await _genericRepos.GetOneById<Inventory>(inventory => inventory.InventoryId == id && inventory.DateDeleted == null);
            if (inventory == null)
            {
                return NotFound();
            }
            inventory.DateDeleted = DateTime.Now;
            await _genericRepos.Update<Inventory>(id, inventory);

            return NoContent();
        }

        [HttpGet("inventorybyhospital/{id}")]
        [Route("~/api/inventorybyhospital/{id}")]
        public async Task<ActionResult<List<InventoryReadDTO>>> GetInventoryByHospitalId(Guid id)
        {
            var inventory = await _specificRepos.GetAllInventoryByHospitalId(id);
            var inventoryReadDTO = _mapper.Map<List<InventoryReadDTO>>(inventory);
            return Ok(inventoryReadDTO);
        }
    }
}
