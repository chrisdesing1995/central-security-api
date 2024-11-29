using CentralSecurity.Api.Models.Input;
using CentralSecurity.Api.Models.Output;
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
    public class RoleController: ControllerBase
    {
        private IRoleService _roleService;

        public RoleController(IRoleService roleService) { 
            _roleService = roleService;
        }

        [HttpGet("GetAll")]
        [SwaggerResponse(StatusCodes.Status200OK,Type= typeof(ResponseResult<IEnumerable<RoleOutput>>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var responseResult = await _roleService.GetAllRoleAsync();

                var outputResult = new ResponseResult<IEnumerable<RoleOutput>>
                {
                    Result = responseResult,
                    Status = responseResult != null ? true : false,
                    Message = responseResult != null ? "Consulta exitosa" : "Error al obtener roles",
                };

                return BuildResponse(outputResult);
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
            
        }


        [HttpGet("GetById/{id}")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(ResponseResult<RoleOutput>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetById(Guid id)
        {
            try
            {
                var responseResult = await _roleService.GetRoleByIdAsync(id);

                var outputResult = new ResponseResult<RoleOutput>
                {
                    Result = responseResult,
                    Status = responseResult != null ? true : false,
                    Message = responseResult != null ? "Consulta exitosa" : "Error al obtener rol por Id",
                };
                return BuildResponse(outputResult);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }

        [HttpPost("Create")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(IActionResult))]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create(RoleInput input)
        {
            try
            {
                var responseResult = await _roleService.CreateRoleAsync(input);
                var outputResult = new ResponseResult<ResultSp>
                {
                    Result = responseResult,
                    Status = responseResult != null,
                    Message = responseResult != null ? responseResult.Message : "Error al crear Rol"
                };
                return BuildResponse(outputResult);
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
            
        }


        [HttpPut("Update")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(IActionResult))]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update(RoleInput input)
        {
            try
            {
                var responseResult = await _roleService.UpdateRoleAsync(input);
                var outputResult = new ResponseResult<ResultSp>
                {
                    Result = responseResult,
                    Status = responseResult != null,
                    Message = responseResult != null ? responseResult.Message : "Error al actualizar Rol"
                };
                return BuildResponse(outputResult);
            }
            catch (Exception ex)
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
