namespace Project_19._8.Services
{
	/// <summary>
	/// Предоставляет свойства ссылки на скрипт
	/// </summary>
	public interface IUrlScript
	{
		/// <summary>
		/// Аттрибут обработки запроса стороннего источника
		/// </summary>
		public string CrossOrigin { get; set; }
		/// <summary>
		/// Адрес ссылки
		/// </summary>
		public string Url { get; set; }
	}
}
