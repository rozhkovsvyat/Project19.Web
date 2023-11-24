using Microsoft.AspNetCore.Mvc;

namespace Project_19.Components
{
	public class HeaderViewComponent : ViewComponent
	{
		public IViewComponentResult Invoke() => View();
	}
}
