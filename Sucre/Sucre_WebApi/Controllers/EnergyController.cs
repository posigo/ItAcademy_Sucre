using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sucre_Services.Interfaces;

namespace Sucre_WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnergyController : ControllerBase
    {
        private readonly IEnergyService _energyService;

        public EnergyController(IEnergyService energyService) 
        { 
            _energyService = energyService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetEnergyById(int id)
        {
            var energy = await _energyService.GetEnergyTypeByIdAsync(id);
            return Ok(energy);
        }

        [HttpGet()]
        public async Task<IActionResult> GetEnergies()
        {
            var energies = await _energyService.GetListEnergyTypesAsync();
            return Ok(energies);
        }

        [HttpPost]
        public async Task<IActionResult> CreateEnergy()
        {
            return Ok();
        }
                
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEnergy(int id)
        {
            var result = await _energyService.DeleteEnergyTypeByIdAsync(id);
            return Ok(result);
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> UdateEnergy()
        {
            return Ok();
        }

    }
}
