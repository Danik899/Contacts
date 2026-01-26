using Microsoft.EntityFrameworkCore;

namespace Task.Models;

public class Contacts
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int MobilePhone { get; set; }
    public string JobTitle { get; set; }
    public DateOnly BirthDate { get; set; }
}