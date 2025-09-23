using Blazor_CRM_Test_Proj.Models;

namespace Blazor_CRM_Test_Proj.Services;

public interface IContactService
{
    Task<IEnumerable<Contact>> GetAllContactsAsync();
    Task<Contact?> GetContactByIdAsync(int id);
    Task<Contact> CreateContactAsync(Contact contact);
    Task<Contact> UpdateContactAsync(Contact contact);
    Task DeleteContactAsync(int id);
    Task<IEnumerable<Contact>> GetContactsByCustomerIdAsync(int customerId);
    Task<IEnumerable<Contact>> GetContactsByTypeAsync(string contactType);
    Task<IEnumerable<Contact>> GetFollowUpRequiredContactsAsync();
    Task<bool> ContactExistsAsync(int id);
    Task<int> GetContactCountAsync();
}

