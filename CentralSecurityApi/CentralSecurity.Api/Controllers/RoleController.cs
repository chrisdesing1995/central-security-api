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
            var responseResult = await _roleService.GetAllRoleAsync();

            var outputResult = new ResponseResult<IEnumerable<RoleOutput>>
            {
                Result = responseResult,
                Status = responseResult != null ? true : false,
                Message = responseResult != null ? "Consulta exitosa" : "Error al obtener roles",
            };

            return Ok(outputResult);
        }


        [HttpGet("GetById/{id}")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(ResponseResult<RoleOutput>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetById(Guid id)
        {
            var responseResult = await _roleService.GetRoleByIdAsync(id);

            var outputResult = new ResponseResult<RoleOutput>
            {
                Result = responseResult,
                Status = responseResult != null ? true : false,
                Message = responseResult != null ? "Consulta exitosa" : "Error al obtener rol por Id",
            };

            return Ok(outputResult);
        }

        [HttpPost("Create")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(IActionResult))]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create(RoleInput input)
        {
            var responseResult = await _roleService.CreateRoleAsync(input);
            return Ok(new {
                Result = responseResult.Data,
                responseResult.Status,
                responseResult.Message
            });
        }


        [HttpPut("Update")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(IActionResult))]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update(RoleInput input)
        {
            var responseResult = await _roleService.UpdateRoleAsync(input);
            return Ok(new
            {
                Result = responseResult.Data,
                responseResult.Status,
                responseResult.Message
            });

        }

    }
}
