using Blazor_CRM_Test_Proj.Data.Repositories;
using Blazor_CRM_Test_Proj.Models;

namespace Blazor_CRM_Test_Proj.Services;

public class CustomerService : ICustomerService
{
    private readonly ICustomerRepository _customerRepository;

    public CustomerService(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }

    public async Task<IEnumerable<Customer>> GetAllCustomersAsync()
    {
        return await _customerRepository.GetAllAsync();
    }

    public async Task<Customer?> GetCustomerByIdAsync(int id)
    {
        return await _customerRepository.GetByIdAsync(id);
    }

    public async Task<Customer> CreateCustomerAsync(Customer customer)
    {
        // Business logic validation
        if (string.IsNullOrWhiteSpace(customer.Email))
            throw new ArgumentException("Email is required");

        if (string.IsNullOrWhiteSpace(customer.FirstName))
            throw new ArgumentException("First name is required");

        if (string.IsNullOrWhiteSpace(customer.LastName))
            throw new ArgumentException("Last name is required");

        customer.CreatedDate = DateTime.UtcNow;
        customer.IsActive = true;

        return await _customerRepository.AddAsync(customer);
    }

    public async Task<Customer> UpdateCustomerAsync(Customer customer)
    {
        // Business logic validation
        if (string.IsNullOrWhiteSpace(customer.Email))
            throw new ArgumentException("Email is required");

        if (string.IsNullOrWhiteSpace(customer.FirstName))
            throw new ArgumentException("First name is required");

        if (string.IsNullOrWhiteSpace(customer.LastName))
            throw new ArgumentException("Last name is required");

        var existingCustomer = await _customerRepository.GetByIdAsync(customer.Id);
        if (existingCustomer == null)
            throw new ArgumentException("Customer not found");

        await _customerRepository.UpdateAsync(customer);
        return customer;
    }

    public async Task DeleteCustomerAsync(int id)
    {
        var customer = await _customerRepository.GetByIdAsync(id);
        if (customer == null)
            throw new ArgumentException("Customer not found");

        // Soft delete - mark as inactive instead of hard delete
        customer.IsActive = false;
        await _customerRepository.UpdateAsync(customer);
    }

    public async Task<IEnumerable<Customer>> SearchCustomersAsync(string searchTerm)
    {
        if (string.IsNullOrWhiteSpace(searchTerm))
            return await GetAllCustomersAsync();

        return await _customerRepository.SearchCustomersAsync(searchTerm);
    }

    public async Task<Customer?> GetCustomerWithDetailsAsync(int id)
    {
        return await _customerRepository.GetCustomerWithAllDetailsAsync(id);
    }

    public async Task<IEnumerable<Customer>> GetActiveCustomersAsync()
    {
        return await _customerRepository.GetActiveCustomersAsync();
    }

    public async Task<bool> CustomerExistsAsync(int id)
    {
        return await _customerRepository.ExistsAsync(id);
    }

    public async Task<int> GetCustomerCountAsync()
    {
        return await _customerRepository.CountAsync();
    }

    public async Task<IEnumerable<Customer>> GetCustomersByCompanyAsync(string company)
    {
        if (string.IsNullOrWhiteSpace(company))
            return await GetAllCustomersAsync();

        return await _customerRepository.GetCustomersByCompanyAsync(company);
    }
}

