using Microsoft.EntityFrameworkCore;
using Task.DataBase;
using Task.Models;

namespace Task.Services;

public class ContactsService : IContactsService
{
    private readonly ContactsDbContext _db;

    public ContactsService(ContactsDbContext db)
    {
        _db = db;
    }

    public async Task<List<Contacts>> GetAllAsync()
    {
        return await _db.Contacts.ToListAsync();
    }

    public async Task<Contacts> CreateAsync(Contacts contact)
    {
        _db.Contacts.Add(contact);
        await _db.SaveChangesAsync();
        return contact;
    }

    public async Task<bool> UpdateAsync(Contacts contact)
    {
        var exist = await _db.Contacts.FindAsync(contact.Id);
        if (exist == null) return false;

        exist.Name = contact.Name;
        exist.MobilePhone = contact.MobilePhone;
        exist.JobTitle = contact.JobTitle;
        exist.BirthDate = contact.BirthDate;

        await _db.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var contact = await _db.Contacts.FindAsync(id);
        if (contact == null) return false;

        _db.Contacts.Remove(contact);
        await _db.SaveChangesAsync();
        return true;
    }
}