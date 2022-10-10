using Microsoft.AspNetCore.Mvc;
using Serilog;
using WhatDoesTheWulfSay.Application.Common.Interfaces;
using WhatDoesTheWulfSay.Application.Models.ReviewModels;
using WhatDoesTheWulfSay.Application.ReviewModels;

namespace WhatDoesTheWulfSay.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {

        private readonly IReviewService reviewService;

        public ReviewController(IReviewService reviewService)
        {
            this.reviewService = reviewService;
        }

        /// <summary>
        /// Get All Reviews
        /// </summary>
        /// <param name="productId">Product Id</param>
        /// <returns>Returns a list of All Reviews</returns>
        [HttpGet("{productId}", Name = "Get")]
        [ProducesResponseType(typeof(ReviewViewModel), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get(Guid productId)
        {
            try
            {
                return Ok(await reviewService.GetAllAsync(productId));
            }
            catch (Exception ex)
            {
                Log.Error($"Error: message: {ex.Message} ");

                return StatusCode(StatusCodes.Status500InternalServerError, new { exception_message = ex.Message });
            }
        }

        /// <summary>
        /// Get All Reviews
        /// </summary>
        /// <param name="productId">Product Id</param>
        /// <returns>Returns a list of All Reviews</returns>
        [HttpGet("summary/{productId}")]
        [ProducesResponseType(typeof(ReviewViewModel), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetSummary(Guid productId)
        {
            try
            {
                return Ok(await reviewService.GetSummaryAsync(productId));
            }
            catch (Exception ex)
            {
                Log.Error($"Error: message: {ex.Message} ");

                return StatusCode(StatusCodes.Status500InternalServerError, new { exception_message = ex.Message });
            }
        }

        /// <summary>
        /// Post Review
        /// </summary>
        /// <returns>Returns Review </returns>
        [HttpPost]
        [ProducesResponseType(typeof(ReviewViewModel), StatusCodes.Status200OK)]
        public async Task<IActionResult> Insert(ReviewInsertCommand command)
        {
            try
            {
                return Ok(await reviewService.InsertAsync(command));
            }
            catch (Exception ex)
            {
                Log.Error($"Error: message: {ex.Message} ");

                return StatusCode(StatusCodes.Status500InternalServerError, new { exception_message = ex.Message });
            }
        }
    }
}

