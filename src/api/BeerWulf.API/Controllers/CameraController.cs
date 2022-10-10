using Microsoft.AspNetCore.Mvc;
using EverybodyCodes.Application.Camera;
using Serilog;
using EverybodyCodes.Application.Common.Interfaces;

namespace EverybodyCodes.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CameraController : ControllerBase
    {

        private readonly ICameraService cameraService;

        public CameraController(ICameraService cameraService)
        {
            this.cameraService = cameraService;
        }

        /// <summary>
        /// Get Cameras
        /// </summary>
        /// <returns>Returns a list of All Cameras</returns>
        [HttpGet]
        [ProducesResponseType(typeof(CameraViewModel), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get()
        {
            try
            {
                return Ok(await cameraService.GetAll());
            }
            catch (Exception ex)
            {
                Log.Error($"Error: message: {ex.Message} ");

                return StatusCode(StatusCodes.Status500InternalServerError, new { exception_message = ex.Message });
            }
        }

        /// <summary>
        /// Get Cameras by name
        /// </summary>
        /// <param name="keyword">Camera keyword</param>
        /// <returns>Returns a Camera by its name</returns>
        [HttpGet("{keyword}", Name = "Get")]
        [ProducesResponseType(typeof(CameraViewModel), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get(string keyword)
        {
            try
            {
                return Ok(await cameraService.GetByName(keyword));
            }
            catch (Exception ex)
            {
                Log.Error($"Error: message: {ex.Message} ");

                return StatusCode(StatusCodes.Status500InternalServerError, new { exception_message = ex.Message });
            }
        }
    }
}

