using Microsoft.EntityFrameworkCore;

namespace Project_19._8.Models;

/// <summary>
/// Контекст контактов
/// </summary>
public class ContactsContext : DbContext
{
    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="options">Опции контекста</param>
    public ContactsContext(DbContextOptions<ContactsContext> options)
        : base(options) { }

    /// <summary>
    /// Сущности контекста
    /// </summary>
    public DbSet<Contact> Contacts { get; set; } = default!;
}
