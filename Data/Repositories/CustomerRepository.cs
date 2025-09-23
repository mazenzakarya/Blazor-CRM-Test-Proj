using Microsoft.EntityFrameworkCore;
using Blazor_CRM_Test_Proj.Models;

namespace Blazor_CRM_Test_Proj.Data.Repositories;

public class CustomerRepository : Repository<Customer>, ICustomerRepository
{
    public CustomerRepository(CrmDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Customer>> GetActiveCustomersAsync()
    {
        return await _dbSet.Where(c => c.IsActive).ToListAsync();
    }

    public async Task<IEnumerable<Customer>> SearchCustomersAsync(string searchTerm)
    {
        var term = searchTerm.ToLower();
        return await _dbSet.Where(c => 
            c.FirstName.ToLower().Contains(term) ||
            c.LastName.ToLower().Contains(term) ||
            c.Email.ToLower().Contains(term) ||
            (c.Company != null && c.Company.ToLower().Contains(term))
        ).ToListAsync();
    }

    public async Task<Customer?> GetCustomerWithContactsAsync(int id)
    {
        return await _dbSet
            .Include(c => c.Contacts)
            .FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<Customer?> GetCustomerWithOpportunitiesAsync(int id)
    {
        return await _dbSet
            .Include(c => c.Opportunities)
            .FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<Customer?> GetCustomerWithAllDetailsAsync(int id)
    {
        return await _dbSet
            .Include(c => c.Contacts)
            .Include(c => c.Opportunities)
            .FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<IEnumerable<Customer>> GetCustomersByCompanyAsync(string company)
    {
        return await _dbSet
            .Where(c => c.Company != null && c.Company.Contains(company))
            .ToListAsync();
    }

    public async Task<DateTime?> GetLastContactDateAsync(int customerId)
    {
        var customer = await _dbSet
            .Include(c => c.Contacts)
            .FirstOrDefaultAsync(c => c.Id == customerId);
        
        return customer?.Contacts
            .OrderByDescending(contact => contact.ContactDate)
            .FirstOrDefault()?.ContactDate;
    }

    public async Task UpdateLastContactDateAsync(int customerId)
    {
        var lastContactDate = await GetLastContactDateAsync(customerId);
        var customer = await GetByIdAsync(customerId);
        
        if (customer != null)
        {
            customer.LastContactDate = lastContactDate;
            await UpdateAsync(customer);
        }
    }
}

