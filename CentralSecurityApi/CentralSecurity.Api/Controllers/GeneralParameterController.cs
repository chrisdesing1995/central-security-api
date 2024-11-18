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
            var responseResult = await _generalParameterService.GetAllGeneralParameterAsync();

            var outputResult = new ResponseResult<IEnumerable<GeneralParameterOutput>>
            {
                Result = responseResult,
                Status = responseResult != null ? true : false,
                Message = responseResult != null ? "Consulta exitosa" : "Error al obtener parametros generales",
            };

            return Ok(outputResult);
        }

        [HttpGet("GetById/{id}")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(ResponseResult<GeneralParameterOutput>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetById(Guid id)
        {
            var responseResult = await _generalParameterService.GetGeneralParameterByIdAsync(id);

            var outputResult = new ResponseResult<GeneralParameterOutput>
            {
                Result = responseResult,
                Status = responseResult != null ? true : false,
                Message = responseResult != null ? "Consulta exitosa" : "Error al obtener parametro generales por Id",
            };

            return Ok(outputResult);
        }

        [HttpGet("GetByCode/{code}")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(ResponseResult<IEnumerable<GeneralParameterOutput>>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetByCode(string code)
        {
            var responseResult = await _generalParameterService.GetGeneralParameterByCodeAsync(code);

            var outputResult = new ResponseResult<IEnumerable<GeneralParameterOutput>>
            {
                Result = responseResult,
                Status = responseResult != null ? true : false,
                Message = responseResult != null ? "Consulta exitosa" : "Error al obtener parametro generales por codigo",
            };

            return Ok(outputResult);
        }

        [HttpPost("Create")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(IActionResult))]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create(GeneralParameterInput input)
        {
            var responseResult = await _generalParameterService.CreateGeneralParameterAsync(input);
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
        public async Task<IActionResult> Update(GeneralParameterInput input)
        {
            var responseResult = await _generalParameterService.UpdateGeneralParameterAsync(input);
            return Ok(new
            {
                Result = responseResult.Data,
                responseResult.Status,
                responseResult.Message
            });

        }
    }
}
