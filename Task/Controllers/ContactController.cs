using Microsoft.AspNetCore.Mvc;
using Task.Models;
using Task.Services;

namespace Task.Controllers;

[ApiController]
[Route("Contacts")]
public class ContactController : Controller
{
    private readonly IContactsService _service;

    public ContactController(IContactsService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _service.GetAllAsync());
    }

    [HttpPost("Create")]
    public async Task<IActionResult> Create([FromBody] Contacts contact)
    {
        var result = await _service.CreateAsync(contact);
        return Ok(result);
    }

    [HttpPost("Update")]
    public async Task<IActionResult> Update([FromBody] Contacts contact)
    {
        var ok = await _service.UpdateAsync(contact);
        return ok ? Ok() : NotFound();
    }

    [HttpDelete("Delete")]
    public async Task<IActionResult> Delete(int id)
    {
        var ok = await _service.DeleteAsync(id);
        return ok ? Ok() : NotFound();
    }
}