using Blazor_CRM_Test_Proj.Models;

namespace Blazor_CRM_Test_Proj.Data.Repositories;

public interface ICustomerRepository : IRepository<Customer>
{
    Task<IEnumerable<Customer>> GetActiveCustomersAsync();
    Task<IEnumerable<Customer>> SearchCustomersAsync(string searchTerm);
    Task<Customer?> GetCustomerWithContactsAsync(int id);
    Task<Customer?> GetCustomerWithOpportunitiesAsync(int id);
    Task<Customer?> GetCustomerWithAllDetailsAsync(int id);
    Task<IEnumerable<Customer>> GetCustomersByCompanyAsync(string company);
    Task<DateTime?> GetLastContactDateAsync(int customerId);
    Task UpdateLastContactDateAsync(int customerId);
}

