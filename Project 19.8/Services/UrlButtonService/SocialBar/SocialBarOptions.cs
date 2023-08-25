namespace Project_19._8.Services
{
	/// <summary>
	/// Содержит опции службы <see cref="SocialBar"/>
	/// </summary>
	public class SocialBarOptions : IUrlButtonService<UrlButton, UrlScript>
	{
		#region IUrlButtonService

		public IEnumerable<UrlScript> Scripts { get; set; } = Enumerable.Empty<UrlScript>();
		public IEnumerable<UrlButton> Buttons { get; set; } = Enumerable.Empty<UrlButton>();

		#endregion
	}
}
