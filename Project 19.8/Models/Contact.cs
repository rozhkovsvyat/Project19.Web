namespace Project_19._8.Models;

/// <summary>
/// Содержит свойства контакта
/// </summary>
public class Contact
{
    /// <summary>
    /// Идентификатор
    /// </summary>
    public int Id { get; set; }
    /// <summary>
    /// Фамилия
    /// </summary>
    public string LastName { get; set; } = string.Empty;
    /// <summary>
    /// Имя
    /// </summary>
    public string FirstName { get; set; } = string.Empty;
	/// <summary>
	/// Отчество
	/// </summary>
	public string Patronymic { get; set; } = string.Empty;
	/// <summary>
	/// Мобильный телефон
	/// </summary>
	public string MobileNumber { get; set; } = string.Empty;
	/// <summary>
	/// Адрес
	/// </summary>
	public string Address { get; set; } = string.Empty;
	/// <summary>
	/// Описание
	/// </summary>
	public string Description { get; set; } = string.Empty;
}
