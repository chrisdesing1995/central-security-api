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
            try
            {
                var responseResult = await _userService.GetAllUserAsync();

                var outputResult = new ResponseResult<IEnumerable<UserOutput>>
                {
                    Result = responseResult,
                    Status = responseResult != null ? true : false,
                    Message = responseResult != null ? "Consulta exitosa" : "Error al obtener usuarios",
                };

                return BuildResponse(outputResult);
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
           
        }


        [HttpGet("GetById/{id}")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(ResponseResult<UserOutput>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetById(Guid id)
        {
            try
            {
                var responseResult = await _userService.GetUserByIdAsync(id);

                var outputResult = new ResponseResult<UserOutput>
                {
                    Result = responseResult,
                    Status = responseResult != null ? true : false,
                    Message = responseResult != null ? "Consulta exitosa" : "Error al obtener usuario por Id",
                };

                return BuildResponse(outputResult);

            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
            
        }

        [HttpPost("Create")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(IActionResult))]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create(UserInput input)
        {
            try
            {
                var responseResult = await _userService.CreateUserAsync(input);
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


        [HttpPut("Update")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(IActionResult))]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update(UserInput input)
        {

            try
            {
                var responseResult = await _userService.UpdateUserAsync(input);
                var outputResult = new ResponseResult<ResultSp>
                {
                    Result = responseResult,
                    Status = responseResult != null,
                    Message = responseResult != null ? responseResult.Message : "Error al actualizar usuario"
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
