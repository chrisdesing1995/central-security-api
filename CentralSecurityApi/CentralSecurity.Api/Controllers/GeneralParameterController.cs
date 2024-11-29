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
    public class GeneralParameterController: ControllerBase
    {
        private IGeneralParameterService _generalParameterService;
        public GeneralParameterController(IGeneralParameterService generalParameterService)
        {
            _generalParameterService = generalParameterService;
        }


        [HttpGet("GetAll")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(ResponseResult<IEnumerable<UserOutput>>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var responseResult = await _generalParameterService.GetAllGeneralParameterAsync();

                var outputResult = new ResponseResult<IEnumerable<GeneralParameterOutput>>
                {
                    Result = responseResult,
                    Status = responseResult != null ? true : false,
                    Message = responseResult != null ? "Consulta exitosa" : "Error al obtener parametros generales",
                };

                return BuildResponse(outputResult);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
            
        }

        [HttpGet("GetById/{id}")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(ResponseResult<GeneralParameterOutput>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetById(Guid id)
        {
            try
            {
                var responseResult = await _generalParameterService.GetGeneralParameterByIdAsync(id);

                var outputResult = new ResponseResult<GeneralParameterOutput>
                {
                    Result = responseResult,
                    Status = responseResult != null ? true : false,
                    Message = responseResult != null ? "Consulta exitosa" : "Error al obtener parametro generales por Id",
                };

                return BuildResponse(outputResult);
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
            
        }

        [HttpGet("GetByCode/{code}")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(ResponseResult<IEnumerable<GeneralParameterDetailOutput>>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetByCode(string code)
        {
            try
            {
                var responseResult = await _generalParameterService.GetGeneralParameterByCodeAsync(code);

                var outputResult = new ResponseResult<IEnumerable<GeneralParameterDetailOutput>>
                {
                    Result = responseResult,
                    Status = responseResult != null ? true : false,
                    Message = responseResult != null ? "Consulta exitosa" : "Error al obtener parametro generales por codigo",
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
        public async Task<IActionResult> Create(GeneralParameterInput input)
        {
            try
            {
                var responseResult = await _generalParameterService.CreateGeneralParameterAsync(input);
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
        public async Task<IActionResult> Update(GeneralParameterInput input)
        {
            try
            {
                var responseResult = await _generalParameterService.UpdateGeneralParameterAsync(input);
                var outputResult = new ResponseResult<ResultSp>
                {
                    Result = responseResult,
                    Status = responseResult != null,
                    Message = responseResult != null ? responseResult.Message : "Error al crear usuario"
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
