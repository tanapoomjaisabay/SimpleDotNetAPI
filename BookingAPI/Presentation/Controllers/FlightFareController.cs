using BookingAPI.Application.DTOs;
using BookingAPI.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BookingAPI.Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FlightFareController : ControllerBase
{
    private readonly IFlightFareService _flightFareService;
    private readonly ILogger<FlightFareController> _logger;

    public FlightFareController(
        IFlightFareService flightFareService,
        ILogger<FlightFareController> logger)
    {
        _flightFareService = flightFareService;
        _logger = logger;
    }

    /// <summary>
    /// Search for flight fares based on criteria
    /// </summary>
    /// <param name="request">Flight search criteria</param>
    /// <returns>List of available flight fares</returns>
    [HttpPost("search")]
    [ProducesResponseType(typeof(IEnumerable<FlightFareResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> SearchFlightFares([FromBody] FlightSearchRequestDto request)
    {
        _logger.LogInformation("Received flight fare search request");

        if (!ModelState.IsValid)
        {
            _logger.LogWarning("Invalid model state for flight search request");
            return BadRequest(ModelState);
        }

        try
        {
            var result = await _flightFareService.SearchFlightFaresAsync(request);
            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to process flight fare search request");
            return StatusCode(500, "An error occurred while processing your request");
        }
    }

    /// <summary>
    /// Health check endpoint for flight fare service
    /// </summary>
    [HttpGet("health")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult Health()
    {
        return Ok(new { status = "healthy", service = "FlightFareService" });
    }
}
