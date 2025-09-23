using Blazor_CRM_Test_Proj.Data.Repositories;
using Blazor_CRM_Test_Proj.Models;
using Microsoft.EntityFrameworkCore;

namespace Blazor_CRM_Test_Proj.Services;

public class ContactService : IContactService
{
    private readonly IRepository<Contact> _contactRepository;
    private readonly ICustomerRepository _customerRepository;

    public ContactService(IRepository<Contact> contactRepository, ICustomerRepository customerRepository)
    {
        _contactRepository = contactRepository;
        _customerRepository = customerRepository;
    }

    public async Task<IEnumerable<Contact>> GetAllContactsAsync()
    {
        return await _contactRepository.GetAllAsync();
    }

    public async Task<Contact?> GetContactByIdAsync(int id)
    {
        return await _contactRepository.GetByIdAsync(id);
    }

    public async Task<Contact> CreateContactAsync(Contact contact)
    {
        // Business logic validation
        if (string.IsNullOrWhiteSpace(contact.Subject))
            throw new ArgumentException("Subject is required");

        if (string.IsNullOrWhiteSpace(contact.Message))
            throw new ArgumentException("Message is required");

        if (contact.CustomerId <= 0)
            throw new ArgumentException("Customer ID is required");

        // Verify customer exists
        var customer = await _customerRepository.GetByIdAsync(contact.CustomerId);
        if (customer == null)
            throw new ArgumentException("Customer not found");

        contact.ContactDate = DateTime.UtcNow;

        var createdContact = await _contactRepository.AddAsync(contact);
        
        // Update customer's last contact date
        await _customerRepository.UpdateLastContactDateAsync(contact.CustomerId);

        return createdContact;
    }

    public async Task<Contact> UpdateContactAsync(Contact contact)
    {
        // Business logic validation
        if (string.IsNullOrWhiteSpace(contact.Subject))
            throw new ArgumentException("Subject is required");

        if (string.IsNullOrWhiteSpace(contact.Message))
            throw new ArgumentException("Message is required");

        var existingContact = await _contactRepository.GetByIdAsync(contact.Id);
        if (existingContact == null)
            throw new ArgumentException("Contact not found");

        await _contactRepository.UpdateAsync(contact);
        return contact;
    }

    public async Task DeleteContactAsync(int id)
    {
        var contact = await _contactRepository.GetByIdAsync(id);
        if (contact == null)
            throw new ArgumentException("Contact not found");

        await _contactRepository.DeleteAsync(id);
    }

    public async Task<IEnumerable<Contact>> GetContactsByCustomerIdAsync(int customerId)
    {
        return await _contactRepository.FindAsync(c => c.CustomerId == customerId);
    }

    public async Task<IEnumerable<Contact>> GetContactsByTypeAsync(string contactType)
    {
        return await _contactRepository.FindAsync(c => c.ContactType == contactType);
    }

    public async Task<IEnumerable<Contact>> GetFollowUpRequiredContactsAsync()
    {
        return await _contactRepository.FindAsync(c => c.IsFollowUpRequired);
    }

    public async Task<bool> ContactExistsAsync(int id)
    {
        return await _contactRepository.ExistsAsync(id);
    }

    public async Task<int> GetContactCountAsync()
    {
        return await _contactRepository.CountAsync();
    }
}

