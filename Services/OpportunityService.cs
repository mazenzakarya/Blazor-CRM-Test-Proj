using Blazor_CRM_Test_Proj.Data.Repositories;
using Blazor_CRM_Test_Proj.Models;

namespace Blazor_CRM_Test_Proj.Services;

public class OpportunityService : IOpportunityService
{
    private readonly IRepository<Opportunity> _opportunityRepository;
    private readonly ICustomerRepository _customerRepository;

    public OpportunityService(IRepository<Opportunity> opportunityRepository, ICustomerRepository customerRepository)
    {
        _opportunityRepository = opportunityRepository;
        _customerRepository = customerRepository;
    }

    public async Task<IEnumerable<Opportunity>> GetAllOpportunitiesAsync()
    {
        return await _opportunityRepository.GetAllAsync();
    }

    public async Task<Opportunity?> GetOpportunityByIdAsync(int id)
    {
        return await _opportunityRepository.GetByIdAsync(id);
    }

    public async Task<Opportunity> CreateOpportunityAsync(Opportunity opportunity)
    {
        // Business logic validation
        if (string.IsNullOrWhiteSpace(opportunity.Title))
            throw new ArgumentException("Title is required");

        if (opportunity.CustomerId <= 0)
            throw new ArgumentException("Customer ID is required");

        if (opportunity.EstimatedValue < 0)
            throw new ArgumentException("Estimated value cannot be negative");

        // Verify customer exists
        var customer = await _customerRepository.GetByIdAsync(opportunity.CustomerId);
        if (customer == null)
            throw new ArgumentException("Customer not found");

        opportunity.CreatedDate = DateTime.UtcNow;

        return await _opportunityRepository.AddAsync(opportunity);
    }

    public async Task<Opportunity> UpdateOpportunityAsync(Opportunity opportunity)
    {
        // Business logic validation
        if (string.IsNullOrWhiteSpace(opportunity.Title))
            throw new ArgumentException("Title is required");

        if (opportunity.EstimatedValue < 0)
            throw new ArgumentException("Estimated value cannot be negative");

        var existingOpportunity = await _opportunityRepository.GetByIdAsync(opportunity.Id);
        if (existingOpportunity == null)
            throw new ArgumentException("Opportunity not found");

        // If status is "Closed Won" or "Closed Lost", set actual close date
        if ((opportunity.Status == "Closed Won" || opportunity.Status == "Closed Lost") && 
            opportunity.ActualCloseDate == null)
        {
            opportunity.ActualCloseDate = DateTime.UtcNow;
        }

        await _opportunityRepository.UpdateAsync(opportunity);
        return opportunity;
    }

    public async Task DeleteOpportunityAsync(int id)
    {
        var opportunity = await _opportunityRepository.GetByIdAsync(id);
        if (opportunity == null)
            throw new ArgumentException("Opportunity not found");

        await _opportunityRepository.DeleteAsync(id);
    }

    public async Task<IEnumerable<Opportunity>> GetOpportunitiesByCustomerIdAsync(int customerId)
    {
        return await _opportunityRepository.FindAsync(o => o.CustomerId == customerId);
    }

    public async Task<IEnumerable<Opportunity>> GetOpportunitiesByStatusAsync(string status)
    {
        return await _opportunityRepository.FindAsync(o => o.Status == status);
    }

    public async Task<IEnumerable<Opportunity>> GetOpportunitiesByValueRangeAsync(decimal minValue, decimal maxValue)
    {
        return await _opportunityRepository.FindAsync(o => o.EstimatedValue >= minValue && o.EstimatedValue <= maxValue);
    }

    public async Task<decimal> GetTotalOpportunityValueAsync()
    {
        var opportunities = await _opportunityRepository.GetAllAsync();
        return opportunities.Sum(o => o.EstimatedValue);
    }

    public async Task<decimal> GetTotalOpportunityValueByStatusAsync(string status)
    {
        var opportunities = await GetOpportunitiesByStatusAsync(status);
        return opportunities.Sum(o => o.EstimatedValue);
    }

    public async Task<bool> OpportunityExistsAsync(int id)
    {
        return await _opportunityRepository.ExistsAsync(id);
    }

    public async Task<int> GetOpportunityCountAsync()
    {
        return await _opportunityRepository.CountAsync();
    }
}

