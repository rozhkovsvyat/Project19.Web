using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Project_19.Controllers;

/// <summary>
/// Базовый абстрактный контроллер Phonebook
/// </summary>
public abstract class PhonebookController : Controller
{
	/// <summary>
	/// Токен сессии
	/// </summary>
	/// <returns></returns>
	protected virtual string? Token => HttpContext.Session.GetString(nameof(HttpContext));

	/// <summary>
	/// Имя контроллера авторизации
	/// </summary>
	private readonly string _authController;
	/// <summary>
	/// Имя действия авторизации
	/// </summary>
	private readonly string _authAction;

	/// <summary>
	/// Конструктор
	/// </summary>
	/// <param name="authAction">Название действия авторизации</param>
	/// /// <param name="authController">Название контроллера авторизации</param>
	protected PhonebookController(string authAction, string authController)
	{
		_authController = authController;
		_authAction = authAction;
	}

	/// <summary>
	/// Перенаправляет на страницу авторизации
	/// </summary>
	/// <param name="returnUrl">Адрес страницы возврата</param>
	protected virtual IActionResult Authorize(string? returnUrl)
		=> ClearSession().RedirectToAction(_authAction, _authController, new { returnUrl });

	/// <summary>
	/// Завершает сессию
	/// </summary>
	protected virtual PhonebookController ClearSession()
	{
		HttpContext.Session.Clear();
		HttpContext.User = new ClaimsPrincipal();
		return this;
	}
}
