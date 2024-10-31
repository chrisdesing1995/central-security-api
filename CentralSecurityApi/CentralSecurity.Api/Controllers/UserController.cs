using CentralSecurity.Api.Models.Input;
using CentralSecurity.Api.Models.Output;
using CentralSecurity.Api.Services;
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
    public class UserController: ControllerBase
    {
        private IUserService _userService;

        public UserController(IUserService userService) { 
            _userService = userService;
        }

        [HttpGet("GetAll")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(ResponseResult<IEnumerable<UserOutput>>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAll()
        {
            var responseResult = await _userService.GetAllUserAsync();

            var outputResult = new ResponseResult<IEnumerable<UserOutput>>
            {
                Result = responseResult,
                Status = responseResult != null ? true : false,
                Message = responseResult != null ? "Consulta exitosa" : "Error al obtener usuarios",
            };

            return Ok(outputResult);
        }


        [HttpGet("GetById/{id}")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(ResponseResult<UserOutput>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetById(Guid id)
        {
            var responseResult = await _userService.GetUserByIdAsync(id);

            var outputResult = new ResponseResult<UserOutput>
            {
                Result = responseResult,
                Status = responseResult != null ? true : false,
                Message = responseResult != null ? "Consulta exitosa" : "Error al obtener usuario por Id",
            };

            return Ok(outputResult);
        }

        [HttpPost("Create")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(IActionResult))]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create(UserInput input)
        {
            var responseResult = await _userService.CreateUserAsync(input);
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
        public async Task<IActionResult> Update(UserInput input)
        {
            var responseResult = await _userService.UpdateUserAsync(input);
            return Ok(new
            {
                Result = responseResult.Data,
                responseResult.Status,
                responseResult.Message
            });

        }
    }
}
