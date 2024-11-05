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
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(ResponseResult<IEnumerable<UserOutput>>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAll()
        {
            var responseResult = await _menuService.GetAllMenuAsync();

            var outputResult = new ResponseResult<IEnumerable<MenuOutput>>
            {
                Result = responseResult,
                Status = responseResult != null ? true : false,
                Message = responseResult != null ? "Consulta exitosa" : "Error al obtener usuarios",
            };

            return Ok(outputResult);
        }

        [HttpGet("GetById/{id}")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(ResponseResult<MenuOutput>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetById(Guid id)
        {
            var responseResult = await _menuService.GetMenuByIdAsync(id);

            var outputResult = new ResponseResult<MenuOutput>
            {
                Result = responseResult,
                Status = responseResult != null ? true : false,
                Message = responseResult != null ? "Consulta exitosa" : "Error al obtener menu por Id",
            };

            return Ok(outputResult);
        }

        [HttpGet("GetByUserId/{userId}")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(ResponseResult<IEnumerable<MenuOutput>>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetByUserId(Guid userId)
        {
            var responseResult = await _menuService.GetMenuByUserAsync(userId);

            var outputResult = new ResponseResult<IEnumerable<MenuOutput>>
            {
                Result = responseResult,
                Status = responseResult != null ? true : false,
                Message = responseResult != null ? "Consulta exitosa" : "Error al obtener menu por usuario",
            };

            return Ok(outputResult);
        }

        [HttpPost("Create")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(IActionResult))]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create(MenuInput input)
        {
            var responseResult = await _menuService.CreateMenuAsync(input);
            return Ok(new
            {
                Result = responseResult.Data,
                responseResult.Status,
                responseResult.Message
            });
        }


        [HttpPut("Update")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(IActionResult))]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update(MenuInput input)
        {
            var responseResult = await _menuService.UpdateMenuAsync(input);
            return Ok(new
            {
                Result = responseResult.Data,
                responseResult.Status,
                responseResult.Message
            });

        }
    }
}
