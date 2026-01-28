using Microsoft.AspNetCore.Mvc;
using Task.Models;
using Task.Services;

namespace Task.Controllers;

[ApiController]
[Route("Contacts")]
public class ContactController : Controller
{
    private readonly IContactsService _service;
    private readonly ILogger<ContactController> _logger;
    public ContactController(IContactsService service,ILogger<ContactController> logger)
    {
        _service = service;
        _logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        try
        {
            return Ok(await _service.GetAllAsync());
        }
        catch (Exception e)
        {
            _logger.LogError("Error while get all contacts");
            return StatusCode(500);
        }
    }

    [HttpPost("Create")]
    public async Task<IActionResult> Create([FromBody] Contacts contact)
    {
        try
        {
            var result = await _service.CreateAsync(contact);
            return Ok(result);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Create failed for {@Contact}", contact);
            return StatusCode(500, "Internal server error");        
        }
    }

    [HttpPost("Update")]
    public async Task<IActionResult> Update([FromBody] Contacts contact)
    {
        try
        {
            var ok = await _service.UpdateAsync(contact);
            return ok ? Ok() : NotFound();
        }
        catch (Exception e)
        {
            _logger.LogError(e,"Update failed for id {id}", contact.Id);
            return StatusCode(500);
        }
    }

    [HttpDelete("Delete")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            var ok = await _service.DeleteAsync(id);
            return ok ? Ok() : NotFound();
        }
        catch (Exception e)
        {
            _logger.LogError(e,"Failed to delete contact by id {id}",id);
            return StatusCode(500);
        }
    }
}