namespace Project_19._8.Services
{
	/// <summary>
	/// Содержит свойства ссылки на скрипт
	/// </summary>
	public class UrlScript : IUrlScript
	{
		#region IUrlScript

		public string CrossOrigin { get; set; } = string.Empty;
		public string Url { get; set; } = string.Empty;

		#endregion
	}
}
