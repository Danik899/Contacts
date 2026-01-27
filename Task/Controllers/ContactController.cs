using Microsoft.AspNetCore.Mvc;
using Task.DataBase;
using Task.Models;

namespace Task.Controllers;


[ApiController]
[Route("Contacts")]
public class ContactController : Controller
{
    private readonly ContactsDbContext _contactsDbContext;

    public ContactController(ContactsDbContext contactsDbContext)
    {
        _contactsDbContext = contactsDbContext;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        return Json(_contactsDbContext.Contacts.ToList());
    }

    [HttpPost("Create")]
    public IActionResult Create([FromBody] Contacts contacts)
    {
        _contactsDbContext.Add(contacts);
        _contactsDbContext.SaveChanges();
        return Ok(contacts);
    }

    [HttpPost("Delete")]
    public IActionResult Delete(int id)
    {
        var contacts = _contactsDbContext.Contacts.Find(id);
        if (contacts != null)
        {
            _contactsDbContext.Contacts.Remove(contacts);
            _contactsDbContext.SaveChanges();
        }
        return Ok();
    }

    [HttpPost("Update")]
    public IActionResult Update([FromBody] Contacts contacts)
    {
        var exist = _contactsDbContext.Contacts.Find(contacts.Id);
        if (exist == null)
        {
            return NotFound();
        }
        exist.Name = contacts.Name;
        exist.MobilePhone = contacts.MobilePhone;
        exist.JobTitle = contacts.JobTitle;
        exist.BirthDate = contacts.BirthDate;

        _contactsDbContext.SaveChanges();
        return Ok();
    }
}