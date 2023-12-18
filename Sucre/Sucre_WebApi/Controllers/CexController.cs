using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sucre_Services.Interfaces;

namespace Sucre_WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CexController : ControllerBase
    {
        private readonly ICexService _cexService;

        public CexController(ICexService cexService) 
        { 
            _cexService=cexService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCexById(int id)
        {
            var cex = await _cexService.GetCexByIdAsync(id);
            return Ok(cex);
        }

        [HttpGet()]
        public async Task<IActionResult> GetCexs()
        {
            var cexs = await _cexService.GetListCexsAsync();
            return Ok(cexs);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCex()
        {
            return Ok();
        }
                
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCex(int id)
        {
            var result = await _cexService.DeleteCexByIdAsync(id);
            return Ok(result);
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> UdateEnergy()
        {
            return Ok();
        }

    }
}
