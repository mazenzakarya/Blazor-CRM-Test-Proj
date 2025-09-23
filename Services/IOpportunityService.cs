using Blazor_CRM_Test_Proj.Models;

namespace Blazor_CRM_Test_Proj.Services;

public interface IOpportunityService
{
    Task<IEnumerable<Opportunity>> GetAllOpportunitiesAsync();
    Task<Opportunity?> GetOpportunityByIdAsync(int id);
    Task<Opportunity> CreateOpportunityAsync(Opportunity opportunity);
    Task<Opportunity> UpdateOpportunityAsync(Opportunity opportunity);
    Task DeleteOpportunityAsync(int id);
    Task<IEnumerable<Opportunity>> GetOpportunitiesByCustomerIdAsync(int customerId);
    Task<IEnumerable<Opportunity>> GetOpportunitiesByStatusAsync(string status);
    Task<IEnumerable<Opportunity>> GetOpportunitiesByValueRangeAsync(decimal minValue, decimal maxValue);
    Task<decimal> GetTotalOpportunityValueAsync();
    Task<decimal> GetTotalOpportunityValueByStatusAsync(string status);
    Task<bool> OpportunityExistsAsync(int id);
    Task<int> GetOpportunityCountAsync();
}

