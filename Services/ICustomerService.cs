using Blazor_CRM_Test_Proj.Models;

namespace Blazor_CRM_Test_Proj.Services;

public interface ICustomerService
{
    Task<IEnumerable<Customer>> GetAllCustomersAsync();
    Task<Customer?> GetCustomerByIdAsync(int id);
    Task<Customer> CreateCustomerAsync(Customer customer);
    Task<Customer> UpdateCustomerAsync(Customer customer);
    Task DeleteCustomerAsync(int id);
    Task<IEnumerable<Customer>> SearchCustomersAsync(string searchTerm);
    Task<Customer?> GetCustomerWithDetailsAsync(int id);
    Task<IEnumerable<Customer>> GetActiveCustomersAsync();
    Task<bool> CustomerExistsAsync(int id);
    Task<int> GetCustomerCountAsync();
    Task<IEnumerable<Customer>> GetCustomersByCompanyAsync(string company);
}

