namespace Project_19._8.Services
{
	/// <summary>
	/// Содержит свойства кнопки-ссылки
	/// </summary>
	public class UrlButton : IUrlButton
	{
		#region IUrlButton

		public string Title { get; set; } = string.Empty;
		public string Color { get; set; } = string.Empty;
		public string Icss { get; set; } = string.Empty;
		public string Url { get; set; } = string.Empty;

		#endregion
	}
}
