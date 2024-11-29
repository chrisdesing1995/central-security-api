using CentralSecurity.Api.Models.Input;
using CentralSecurity.Api.Services.Interfaces;
using CentralSecurity.Domain.Common;
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
            try
            {
                var responseResult = await _permisoService.CreatePermisoAsync(input);
                var outputResult = new ResponseResult<ResultSp>
                {
                    Result = responseResult,
                    Status = responseResult != null,
                    Message = responseResult != null ? responseResult.Message : "Error al crear usuario"
                };
                return BuildResponse(outputResult);
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }
        private IActionResult BuildResponse<T>(ResponseResult<T> responseResult)
        {
            if (responseResult == null || !responseResult.Status)
            {
                return NotFound(new { message = responseResult?.Message ?? "Ocurrió un error." });
            }
            return Ok(new
            {
                responseResult.Result,
                responseResult.Status,
                responseResult.Message
            });
        }
    }
}
