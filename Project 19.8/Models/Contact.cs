namespace Project_19._8.Models;

/// <summary>
/// Содержит свойства контакта
/// </summary>
public class Contact
{
    /// <summary>
    /// Идентификатор
    /// </summary>
    public int ID { get; set; }
    /// <summary>
    /// Фамилия
    /// </summary>
    public string LastName { get; set; }
    /// <summary>
    /// Имя
    /// </summary>
    public string FirstName { get; set; }
    /// <summary>
    /// Отчество
    /// </summary>
    public string Patronymic { get; set; }
    /// <summary>
    /// Мобильный телефон
    /// </summary>
    public string MobileNumber { get; set; }
    /// <summary>
    /// Адрес
    /// </summary>
    public string Address { get; set; }
    /// <summary>
    /// Описание
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    /// Пустой конструктор
    /// </summary>
    public Contact() : this(string.Empty, string.Empty, string.Empty, 
        string.Empty, string.Empty, string.Empty) { }

    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="lastName">Фамилия</param>
    /// <param name="firstName">Имя</param>
    /// <param name="patronymic">Отчество</param>
    /// <param name="mobileNumber">Мобильный телефон</param>
    /// <param name="address"><Адрес/param>
    /// <param name="description">Описание</param>
    public Contact(string lastName, string firstName, string patronymic, 
        string mobileNumber, string address, string description)
    {
        LastName = lastName;
        FirstName = firstName;
        Patronymic = patronymic;
        MobileNumber = mobileNumber;
        Address = address;
        Description = description;
    }
}
