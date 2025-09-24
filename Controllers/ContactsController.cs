using Microsoft.AspNetCore.Mvc;
using Blazor_CRM_Test_Proj.Models;
using Blazor_CRM_Test_Proj.Services;

namespace Blazor_CRM_Test_Proj.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ContactsController : ControllerBase
{
    private readonly IContactService _contactService;

    public ContactsController(IContactService contactService)
    {
        _contactService = contactService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Contact>>> GetContacts()
    {
        var contacts = await _contactService.GetAllContactsAsync();
        return Ok(contacts);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Contact>> GetContact(int id)
    {
        var contact = await _contactService.GetContactByIdAsync(id);
        if (contact == null)
            return NotFound();

        return Ok(contact);
    }

    [HttpGet("customer/{customerId}")]
    public async Task<ActionResult<IEnumerable<Contact>>> GetContactsByCustomer(int customerId)
    {
        var contacts = await _contactService.GetContactsByCustomerIdAsync(customerId);
        return Ok(contacts);
    }

    [HttpGet("type/{contactType}")]
    public async Task<ActionResult<IEnumerable<Contact>>> GetContactsByType(string contactType)
    {
        var contacts = await _contactService.GetContactsByTypeAsync(contactType);
        return Ok(contacts);
    }

    [HttpGet("followup")]
    public async Task<ActionResult<IEnumerable<Contact>>> GetFollowUpRequiredContacts()
    {
        var contacts = await _contactService.GetFollowUpRequiredContactsAsync();
        return Ok(contacts);
    }

    [HttpPost]
    public async Task<ActionResult<Contact>> CreateContact(Contact contact)
    {
        try
        {
            var createdContact = await _contactService.CreateContactAsync(contact);
            return CreatedAtAction(nameof(GetContact), new { id = createdContact.Id }, createdContact);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateContact(int id, Contact contact)
    {
        if (id != contact.Id)
            return BadRequest("ID mismatch");

        try
        {
            await _contactService.UpdateContactAsync(contact);
            return NoContent();
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteContact(int id)
    {
        try
        {
            await _contactService.DeleteContactAsync(id);
            return NoContent();
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }
}

