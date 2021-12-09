using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;
using System;
using System.Threading.Tasks;
using PropertyAPI.Services;
using PropertyAPI.DTOs;
using PropertyAPI.DTO;

namespace PropertyAPI.Controllers
{
    [Route("api/[controller]")]
    [OpenApiTag("Fs", Description = "Endpoint to handle all Fs related operations.")]
    [ApiController]
    public class PropertyController : Controller
    {
        private readonly IPropertyService _iPropertyService;
        public PropertyController(IPropertyService iPropertyService)
        {
            _iPropertyService = iPropertyService ?? throw new ArgumentNullException(nameof(iPropertyService));
        }

        [HttpGet("getproperties")]
        public async Task<IActionResult> GetProperties()
        {
            var sampleContents = await _iPropertyService.GetProperties();

            if (sampleContents == null)
            {
                var response = new Response { IsSuccess = false, Comments = "No property data found." };
                return NotFound(response);
            }

            return Ok(sampleContents);
        }

        [HttpPost("saveproperty")]
        public async Task<IActionResult> SaveRecord([FromBody] PropertyContent newRecord)
        {
            Response response = null;

            var result= await _iPropertyService.SaveRecord(newRecord);

            if (result.IsSuccess)
                return Ok(result);
            
            return BadRequest(result);
        }
    }
}
