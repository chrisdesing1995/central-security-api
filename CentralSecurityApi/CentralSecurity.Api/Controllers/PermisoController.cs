using CentralSecurity.Api.Models.Input;
using CentralSecurity.Api.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace CentralSecurity.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class PermisoController: ControllerBase
    {
        private IPermisoService _permisoService;

        public PermisoController(IPermisoService permisoService)
        {
            this._permisoService = permisoService;
        }

        [HttpPost("Create")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(IActionResult))]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create(RoleMenuInput input)
        {
            var responseResult = await _permisoService.CreatePermisoAsync(input);
            return Ok(new
            {
                Result = responseResult.Data,
                responseResult.Status,
                responseResult.Message
            });
        }


    }
}
