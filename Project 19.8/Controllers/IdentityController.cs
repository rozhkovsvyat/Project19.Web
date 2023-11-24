using System.Security.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

using Project_19.Models;

namespace Project_19.Controllers;

/// <summary>
/// Контроллер работы с авторизацией
/// </summary>
public class IdentityController : PhonebookController
{
	/// <inheritdoc cref="IIdentity"/>
	private readonly IIdentity _identity;

	/// <inheritdoc cref="PhonebookController.Token"/>
	protected new string? Token
	{
		get => base.Token;
		private set
		{
			if (string.IsNullOrEmpty(value)) return;
			HttpContext.Session.SetString(nameof(HttpContext), value);
		}
	}

	/// <summary>
	/// Конструктор
	/// </summary>
	/// <param name="identity">Идентификация</param>
	public IdentityController(IIdentity identity) 
		: base("SignIn", "Identity") => _identity = identity;

	#region [Get]

	/// <summary>
	/// Запрашивает форму <see cref="SignInForm"/>
	/// </summary>
	/// <returns>~View (<see cref="SignInForm"/>)</returns>
	[HttpGet]
	public IActionResult SignIn(string? returnUrl) 
		=> View(_identity.SignInForm.ForReturnUrl(returnUrl));

	/// <summary>
	/// Запрашивает форму <see cref="CreateAccountForm"/>
	/// </summary>
	/// <returns>~View (<see cref="CreateAccountForm"/>)</returns>  
	[HttpGet]
	public IActionResult Create(string? returnUrl)
		=> View(_identity.CreateAccountForm.ForReturnUrl(returnUrl));

	/// <summary>
	/// Запрашивает форму <see cref="ChangePasswordForm"/>
	/// </summary>
	/// <returns>~View (<see cref="ChangePasswordForm"/>) , NotFound</returns>
	[HttpGet]
	public async Task<IActionResult> ChangePassword()
	{
		if (!User.IsInRole("admin")) 
			return string.IsNullOrEmpty(User.Identity?.Name) ? NotFound() 
				: View(_identity.ChangePasswordForm.ForLogin(User.Identity.Name));

		try
		{
			if (User.FindFirst(ClaimTypes.NameIdentifier)?.Value is not { } id) return NotFound();

			if (await _identity.AddToken(Token).GetByIdAsync(id) is not { } account) return NotFound();

			return View(_identity.ChangePasswordForm.ForLogin(account.Login));
		}

		catch (AuthenticationException) { return Authorize(Url.Action("ChangePassword")); }

		catch (Exception) { return StatusCode(502); }
	}

	#endregion

	#region [Post]

	/// <summary>
	/// <inheritdoc cref="IIdentity.AddAsync"/>
	/// </summary>
	/// <param name="form">Форма аккаунта</param>
	/// <returns>~View (<see cref="CreateAccountForm"/>) если ошибка создания , View (<see cref="SignInForm"/>) если ошибка входа , ~Index (коллекция элементов <see cref="Contact"/>)</returns>
	[HttpPost, ValidateAntiForgeryToken]
	public async Task<IActionResult> Create(CreateAccountForm form)
	{
		if (!ModelState.IsValid) return View(form);

		try
		{
			await _identity.AddAsync(form);
				return await SignIn(_identity.SignInForm
					.ForLogin(form.Login).ForPassword(form.Password)
					.ForReturnUrl(form.ReturnUrl));
		}

		catch (InvalidOperationException e)
		{
			foreach (var error in e.Message.Deserialize())
				ModelState.AddModelError(string.Empty, error);
		}

		catch (Exception) { return StatusCode(502); }

		return View(form);
	}

	/// <summary>
	/// <inheritdoc cref="IIdentity.SignInAsync"/>
	/// </summary>
	/// <param name="form">Форма аккаунта</param>
	/// <returns>~View (<see cref="SignInForm"/>) если ошибка , ~Index (коллекция элементов <see cref="Contact"/>)</returns>
	[HttpPost, ValidateAntiForgeryToken]
	public async Task<IActionResult> SignIn(SignInForm form)
	{
		if (!ModelState.IsValid) return View(form);

		try
		{
			Token = await _identity.SignInAsync(form);
			return Url.IsLocalUrl(form.ReturnUrl) ? Redirect(form.ReturnUrl)
				: RedirectToAction("Index", "Contacts");
		}

		catch (InvalidOperationException e)
		{
			foreach (var error in e.Message.Deserialize())
				ModelState.AddModelError(string.Empty, error);
		}

		catch (Exception) { return StatusCode(502); }

		return View(form);
	}

	/// <summary>
	/// <inheritdoc cref="IIdentity.UpdatePasswordAsync"/>
	/// </summary>
	/// <param name="form">Форма аккаунта</param>
	/// <returns>~View (<see cref="ChangePasswordForm"/>)</returns>
	[HttpPost, ValidateAntiForgeryToken]
	public async Task<IActionResult> ChangePassword(ChangePasswordForm form)
	{
		if (!ModelState.IsValid) return View(form);

		try
		{
			await _identity.AddToken(Token).UpdatePasswordAsync(form);
			ViewBag.SuccessMessage = "Password changed.";
		}

		catch (InvalidOperationException e)
		{
			foreach (var error in e.Message.Deserialize())
				ModelState.AddModelError(string.Empty, error);
		}

		catch (AuthenticationException) { return Authorize(Url.Action("ChangePassword", form)); }

		catch (KeyNotFoundException) { return NotFound(); }

		catch (Exception) { return StatusCode(502); }

		return View(form);
	}

	/// <summary>
	/// <inheritdoc cref="IIdentity.SignOutAsync"/>
	/// </summary>
	/// <returns>~Index (коллекция элементов <see cref="Contact"/>)</returns>
	[HttpPost, IgnoreAntiforgeryToken]
	public async Task<IActionResult> LogOut()
	{
		try
		{
			await _identity.SignOutAsync();
			return ClearSession().RedirectToAction("Index", "Contacts");
		}

		catch (Exception) { return StatusCode(502); }
	}

	#endregion
}
