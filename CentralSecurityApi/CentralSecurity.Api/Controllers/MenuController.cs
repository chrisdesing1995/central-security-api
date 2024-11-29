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
    public class MenuController: ControllerBase
    {
        private IMenuService _menuService;

        public MenuController(IMenuService menuService)
        {
            _menuService = menuService;
        }

        [HttpGet("GetAll")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(ResponseResult<IEnumerable<MenuOutput>>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var responseResult = await _menuService.GetAllMenuAsync();
                var outputResult = new ResponseResult<IEnumerable<MenuOutput>>
                {
                    Result = responseResult,
                    Status = responseResult != null,
                    Message = responseResult != null ? "Consulta exitosa" : "Error al obtener lista de menu"
                };
                return BuildResponse(outputResult);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }


        [HttpGet("GetById/{id}")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(ResponseResult<MenuOutput>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetById(Guid id)
        {
            try
            {
                var responseResult = await _menuService.GetMenuByIdAsync(id);
                var outputResult = new ResponseResult<MenuOutput>
                {
                    Result = responseResult,
                    Status = responseResult != null,
                    Message = responseResult != null ? "Consulta exitosa" : "Error al obtener menu por id"
                };
                return BuildResponse(outputResult);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }

        [HttpGet("GetByUserId/{userId}")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(ResponseResult<IEnumerable<MenuOutput>>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetByUserId(Guid userId)
        {
            try
            {
                var responseResult = await _menuService.GetMenuByUserAsync(userId);
                var outputResult = new ResponseResult<IEnumerable<MenuOutput>>
                {
                    Result = responseResult,
                    Status = responseResult != null,
                    Message = responseResult != null ? "Consulta exitosa" : "Error al obtener menu por usuario"
                };
                return BuildResponse(outputResult);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }

        [HttpGet("GetByRolId/{rolId}")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(ResponseResult<IEnumerable<MenuOutput>>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetByRolId(Guid rolId)
        {
            try
            {
                var responseResult = await _menuService.GetMenuByRolAsync(rolId);
                var outputResult = new ResponseResult<IEnumerable<MenuOutput>>
                {
                    Result = responseResult,
                    Status = responseResult != null,
                    Message = responseResult != null ? "Consulta exitosa" : "Error al obtener menu por rol"
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
        public async Task<IActionResult> Create(MenuInput input)
        {
            try
            {
                var responseResult = await _menuService.CreateMenuAsync(input);
                var outputResult = new ResponseResult<ResultSp>
                {
                    Result = responseResult,
                    Status = responseResult != null,
                    Message = responseResult != null ? responseResult.Message : "Error al crear item del menu"
                };
                return BuildResponse(outputResult);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }


        [HttpPut("Update")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(IActionResult))]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update(MenuInput input)
        {
            try
            {
                var responseResult = await _menuService.UpdateMenuAsync(input);
                var outputResult = new ResponseResult<ResultSp>
                {
                    Result = responseResult,
                    Status = responseResult != null,
                    Message = responseResult != null ? responseResult.Message : "Error al actualizar item del menu"

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
