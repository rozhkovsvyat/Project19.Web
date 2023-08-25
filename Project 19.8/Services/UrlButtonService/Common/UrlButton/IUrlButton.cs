namespace Project_19._8.Services
{
    /// <summary>
    /// Предоставляет свойства кнопки-ссылки
    /// </summary>
    public interface IUrlButton
    {
        /// <summary>
        /// Имя ссылки
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// Цвет кнопки
        /// </summary>
        public string Color { get; set; }
		/// <summary>
		/// Класс иконки
		/// </summary>
		public string Icss { get; set; }
		/// <summary>
		/// Адрес ссылки
		/// </summary>
		public string Url { get; set; }
	}
}
