using Microsoft.AspNetCore.Mvc;
using Blazor_CRM_Test_Proj.Models;
using Blazor_CRM_Test_Proj.Services;

namespace Blazor_CRM_Test_Proj.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OpportunitiesController : ControllerBase
{
    private readonly IOpportunityService _opportunityService;

    public OpportunitiesController(IOpportunityService opportunityService)
    {
        _opportunityService = opportunityService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Opportunity>>> GetOpportunities()
    {
        var opportunities = await _opportunityService.GetAllOpportunitiesAsync();
        return Ok(opportunities);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Opportunity>> GetOpportunity(int id)
    {
        var opportunity = await _opportunityService.GetOpportunityByIdAsync(id);
        if (opportunity == null)
            return NotFound();

        return Ok(opportunity);
    }

    [HttpGet("customer/{customerId}")]
    public async Task<ActionResult<IEnumerable<Opportunity>>> GetOpportunitiesByCustomer(int customerId)
    {
        var opportunities = await _opportunityService.GetOpportunitiesByCustomerIdAsync(customerId);
        return Ok(opportunities);
    }

    [HttpGet("status/{status}")]
    public async Task<ActionResult<IEnumerable<Opportunity>>> GetOpportunitiesByStatus(string status)
    {
        var opportunities = await _opportunityService.GetOpportunitiesByStatusAsync(status);
        return Ok(opportunities);
    }

    [HttpGet("value-range")]
    public async Task<ActionResult<IEnumerable<Opportunity>>> GetOpportunitiesByValueRange(
        [FromQuery] decimal minValue, 
        [FromQuery] decimal maxValue)
    {
        var opportunities = await _opportunityService.GetOpportunitiesByValueRangeAsync(minValue, maxValue);
        return Ok(opportunities);
    }

    [HttpGet("total-value")]
    public async Task<ActionResult<decimal>> GetTotalOpportunityValue()
    {
        var totalValue = await _opportunityService.GetTotalOpportunityValueAsync();
        return Ok(totalValue);
    }

    [HttpGet("total-value/{status}")]
    public async Task<ActionResult<decimal>> GetTotalOpportunityValueByStatus(string status)
    {
        var totalValue = await _opportunityService.GetTotalOpportunityValueByStatusAsync(status);
        return Ok(totalValue);
    }

    [HttpPost]
    public async Task<ActionResult<Opportunity>> CreateOpportunity(Opportunity opportunity)
    {
        try
        {
            var createdOpportunity = await _opportunityService.CreateOpportunityAsync(opportunity);
            return CreatedAtAction(nameof(GetOpportunity), new { id = createdOpportunity.Id }, createdOpportunity);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateOpportunity(int id, Opportunity opportunity)
    {
        if (id != opportunity.Id)
            return BadRequest("ID mismatch");

        try
        {
            await _opportunityService.UpdateOpportunityAsync(opportunity);
            return NoContent();
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteOpportunity(int id)
    {
        try
        {
            await _opportunityService.DeleteOpportunityAsync(id);
            return NoContent();
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }
}

