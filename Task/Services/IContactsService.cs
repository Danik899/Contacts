using Task.Models;
namespace Task.Services;

public interface IContactsService
{
    Task<List<Contacts>> GetAllAsync();
    Task<Contacts> CreateAsync(Contacts contact);
    Task<bool> UpdateAsync(Contacts contact);
    Task<bool> DeleteAsync(int id);
}
