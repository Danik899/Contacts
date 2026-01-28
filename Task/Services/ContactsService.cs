using Microsoft.EntityFrameworkCore;
using Task.DataBase;
using Task.Models;

namespace Task.Services;

public class ContactsService : IContactsService
{
    private readonly ContactsDbContext _db;
    private readonly ILogger<ContactsService> _logger;
    
    public ContactsService(ContactsDbContext db, ILogger<ContactsService> logger)
    {
        _db = db;
        _logger = logger;
    }

    public async Task<List<Contacts>> GetAllAsync()
    {
        try
        {
            return await _db.Contacts.ToListAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError("Error getting contacts.");
            return new List<Contacts>();
        }
    }

    //C- создание контакта
    public async Task<Contacts> CreateAsync(Contacts contact)
    {
        try
        {
            _db.Contacts.Add(contact);
            await _db.SaveChangesAsync();
            return contact;
        }
        catch (Exception e)
        {
            _logger.LogError("Error while creating contact");
            return null;
        }
    }

    //U - обновить контакт
    public async Task<bool> UpdateAsync(Contacts contact)
    {
        try
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
        catch (Exception e)
        {
            _logger.LogError("Error while update contact by id {id}", contact.Id);
            return false;
        }
        
    }

    //D-удалить контакт
    public async Task<bool> DeleteAsync(int id)
    {
        try
        {
            var contact = await _db.Contacts.FindAsync(id);
            if (contact == null) return false;

            _db.Contacts.Remove(contact);
            await _db.SaveChangesAsync();
            return true;
        }
        catch (Exception e)
        {
            _logger.LogError("Error while delete contact by id {id}",id);
            return false;
        }
    }
}