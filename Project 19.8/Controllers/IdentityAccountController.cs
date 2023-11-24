using System.Security.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

using Project_19.Models;

namespace Project_19.Controllers;

/// <summary>
/// Контроллер администратора (управление <see cref="Account"/>)
/// </summary>
public class IdentityAccountController : PhonebookController
{
	/// <inheritdoc cref="IIdentity"/>
	private readonly IIdentity _identity;

	/// <summary>
	/// Конструктор
	/// </summary>
	/// <param name="identity">Идентификация</param>
	public IdentityAccountController(IIdentity identity) 
		: base("SignIn", "Identity") => _identity = identity;

	#region [Get]

	/// <summary>
	/// <inheritdoc cref="IIdentity.GetAsync"/>
	/// </summary>
	/// <returns>~Index (коллекция элементов <see cref="Account"/>)</returns>
	[HttpGet]
	public async Task<IActionResult> Index()
	{
		if (!User.IsInRole("admin")) return NotFound();

		try { return View(await _identity.AddToken(Token).GetAsync()); }

		catch (AuthenticationException) { return Authorize(Url.Action("Index")); }

		catch (Exception) { return StatusCode(502); }
	}

	/// <summary>
	/// <inheritdoc cref="IIdentity.GetByIdAsync"/>
	/// </summary>
	/// <returns>~View (<see cref="Account"/>) , NotFound</returns>
	[HttpGet]
	public async Task<IActionResult> Details(string? id)
	{
		if (!User.IsInRole("admin")) return NotFound();

		if (string.IsNullOrWhiteSpace(id)) return NotFound();

		try
		{
			var account = await _identity.AddToken(Token).GetByIdAsync(id);
			return account is null ? NotFound() : View(account);
		}

		catch (AuthenticationException)
		{
			return Authorize(Url.Action
				("Details", new { id }));
		}

		catch (Exception) { return StatusCode(502); }
	}

	/// <summary>
	/// Запрашивает страницу создания <see cref="Account"/>
	/// </summary>
	/// <returns>~View (<see cref="CreateAccountForm"/>)</returns>
	[HttpGet]
	public IActionResult Create() 
		=> User.IsInRole("admin") ? View(_identity.CreateAccountForm) : NotFound();

	/// <summary>
	/// Запрашивает страницу редактирования <see cref="Account"/>
	/// </summary>
	/// <param name="id">Идентификатор</param>
	/// <returns>~View (<see cref="Account"/>) , NotFound</returns>
	[HttpGet]
	public async Task<IActionResult> Edit(string? id)
	{
		if (!User.IsInRole("admin")) return NotFound();

		if (string.IsNullOrWhiteSpace(id)) return NotFound();

		try
		{
			var account = await _identity.AddToken(Token).GetByIdAsync(id);
			return account is null ? NotFound() : View(account);
		}

		catch (AuthenticationException)
		{
			return Authorize(Url.Action
				("Edit", new { id }));
		}

		catch (Exception) { return StatusCode(502); }
	}

	/// <summary>
	/// Запрашивает страницу удаления <see cref="Account"/>
	/// </summary>
	/// <param name="id">Идентификатор</param>
	/// <returns>~View (<see cref="Account"/>) , NotFound</returns>
	[HttpGet]
	public async Task<IActionResult> Delete(string? id)
	{
		if (!User.IsInRole("admin")) return NotFound();

		if (string.IsNullOrWhiteSpace(id) || id == User.FindFirst
			    (ClaimTypes.NameIdentifier)?.Value) return NotFound();

		try
		{
			var account = await _identity.AddToken(Token).GetByIdAsync(id);
			return account is null ? NotFound() : View(account);
		}

		catch (AuthenticationException)
		{
			return Authorize(Url.Action
				("Delete", new { id }));
		}

		catch (Exception) { return StatusCode(502); }
	}

	/// <summary>
	/// Запрашивает форму <see cref="ChangePasswordForm"/>
	/// </summary>
	/// <returns>~View (<see cref="ChangePasswordForm"/>) , NotFound</returns>
	[HttpGet]
	public async Task<IActionResult> ChangePassword(string? id)
	{
		if (!User.IsInRole("admin")) return NotFound();

		if (string.IsNullOrWhiteSpace(id)) return NotFound();

		try
		{
			var account = await _identity.AddToken(Token).GetByIdAsync(id);
			return account is null ? NotFound() : View(_identity
				.ChangePasswordForm.ForLogin(account.Login));
		}

		catch (AuthenticationException)
		{
			return Authorize(Url.Action
				("ChangePassword", new { id }));
		}

		catch (Exception) { return StatusCode(502); }
	}

	/// <summary>
	/// Запрашивает страницу назначения <see cref="Role"/> для <see cref="Account"/>
	/// </summary>
	/// <param name="id">Идентификатор</param>
	/// <returns>~View (<see cref="ManageRolesForm"/>) , NotFound</returns>
	[HttpGet]
	public async Task<IActionResult> AssignRole(string? id)
	{
		if (!User.IsInRole("admin")) return NotFound();

		if (string.IsNullOrWhiteSpace(id)) return NotFound();

		try
		{
			if (await _identity.AddToken(Token).GetByIdAsync(id) is not { } account) return NotFound();

			var roles = await _identity.GetAvailableRolesAsync(account.Login);

			return View(_identity.ManageRolesForm.ForLogin(account.Login).ForRoles(roles));
		}

		catch (AuthenticationException)
		{
			return Authorize(Url.Action
				("AssignRole", new { id }));
		}

		catch (KeyNotFoundException) { return NotFound(); }

		catch (Exception) { return StatusCode(502); }
	}

	/// <summary>
	/// Запрашивает страницу снятия <see cref="Role"/> для <see cref="Account"/>
	/// </summary>
	/// <param name="id">Идентификатор</param>
	/// <returns>~View (<see cref="ManageRolesForm"/>) , NotFound</returns>
	[HttpGet]
	public async Task<IActionResult> RemoveRole(string? id)
	{
		if (!User.IsInRole("admin")) return NotFound();

		if (string.IsNullOrWhiteSpace(id)) return NotFound();

		try
		{
			if (await _identity.AddToken(Token).GetByIdAsync(id) is not { } account) return NotFound();

			var roles = await _identity.GetRolesAsync(account.Login);

			if (account.Id == User.FindFirst(ClaimTypes.NameIdentifier)?.Value)
				roles = roles.Where(r => r.Name != "admin").Select(r => r);

			return View(_identity.ManageRolesForm.ForLogin(account.Login).ForRoles(roles));
		}

		catch (AuthenticationException)
		{
			return Authorize(Url.Action
				("RemoveRole", new { id }));
		}

		catch (KeyNotFoundException) { return NotFound(); }

		catch (Exception) { return StatusCode(502); }
	}

	/// <summary>
	/// <inheritdoc cref="IIdentity.GetRolesAsync(string)"/>
	/// </summary>
	/// <param name="id">Идентификатор</param>
	/// <returns>~View (коллекция элементов <see cref="Role"/>), NotFound</returns>
	[HttpGet]
	public async Task<IActionResult> ShowRoles(string? id)
	{
		if (!User.IsInRole("admin")) return NotFound();

		if (string.IsNullOrWhiteSpace(id)) return NotFound();

		try
		{
			if (await _identity.AddToken(Token).GetByIdAsync(id) is not { } account) return NotFound();

			ViewBag.Login = account.Login;

			return View(await _identity.GetRolesAsync(account.Login));
		}

		catch (AuthenticationException)
		{
			return Authorize(Url.Action
				("ShowRoles", new { id }));
		}

		catch (KeyNotFoundException) { return NotFound(); }

		catch (Exception) { return StatusCode(502); }
	}

	#endregion

	#region [Post]

	/// <summary>
	/// <inheritdoc cref="IIdentity.AddAsync(CreateAccountForm)"/>
	/// </summary>
	/// <param name="form">Форма создания <see cref="Account"/></param>
	/// <returns>~View (<see cref="CreateAccountForm"/>) если ошибка , ~Index (коллекция элементов <see cref="Account"/>)</returns>
	[HttpPost, ValidateAntiForgeryToken]
	public async Task<IActionResult> Create(CreateAccountForm form)
	{
		if (!ModelState.IsValid) return View(form);

		try
		{
			await _identity.AddAsync(form);
			return RedirectToAction(nameof(Index));
		}

		catch (InvalidOperationException e)
		{
			foreach (var error in e.Message.Deserialize())
				ModelState.AddModelError(string.Empty, error);
			return View(form);
		}

		catch (AuthenticationException)
		{
			return Authorize(Url.Action
				("Create", form));
		}

		catch (Exception) { return StatusCode(502); }
	}

	/// <summary>
	/// Обновляет существующий <see cref="Account"/>
	/// </summary>
	/// <param name="id">Идентификатор</param>
	/// <param name="account">Тело аккаунта</param>
	/// <returns>~View (<see cref="Account"/>) если ошибка , ~Index (коллекция элементов <see cref="Account"/>) , NotFound</returns>
	[HttpPost, ValidateAntiForgeryToken]
	public async Task<IActionResult> Edit(string id, [Bind
		("Id,Login,Email")] Account account)
	{
		if (id != account.Id) return NotFound();

		if (!ModelState.IsValid) return View(account);

		try
		{
			await _identity.AddToken(Token).UpdateAsync(account);
			return RedirectToAction(nameof(Index));
		}

		catch (InvalidOperationException e)
		{
			foreach (var error in e.Message.Deserialize())
				ModelState.AddModelError(string.Empty, error);
			return View(account);
		}

		catch (AuthenticationException)
		{
			return Authorize(Url.Action
				("Edit", new { id, account }));
		}

		catch (KeyNotFoundException) { return NotFound(); }

		catch (Exception) { return StatusCode(502); }
	}

	/// <summary>
	/// Удаляет существующий <see cref="Account"/>
	/// </summary>
	/// <param name="id">Идентификатор</param>
	/// <returns>~Index (коллекция элементов <see cref="Account"/>) , NotFound</returns>
	[HttpPost, ActionName(nameof(Delete)), ValidateAntiForgeryToken]
	public async Task<IActionResult> DeleteConfirmed(string id)
	{
		if (id == User.FindFirst(ClaimTypes.NameIdentifier)?.Value) return NotFound();

		try
		{
			await _identity.RemoveByIdAsync(id);
			return RedirectToAction(nameof(Index));
		}
		
		catch (AuthenticationException) { return Authorize(Url.Action("Delete", new { id })); }

		catch (InvalidOperationException) { return StatusCode(500); }

		catch (KeyNotFoundException) { return NotFound(); }

		catch (Exception) { return StatusCode(502); }
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

		catch (AuthenticationException)
		{
			return Authorize(Url.Action
				("ChangePassword", form));
		}

		catch (KeyNotFoundException) { return NotFound(); }

		catch (Exception) { return StatusCode(502); }

		return View(form);
	}

	/// <summary>
	/// Назначает <see cref="Role"/> указанному <see cref="Account"/>
	/// </summary>
	/// <param name="login">Логин</param>
	/// <param name="form">Форма <see cref="ManageRolesForm"/></param>
	/// <returns>~View (<see cref="ManageRolesForm"/>) если ошибка , ~Index (коллекция элементов <see cref="Account"/>) , NotFound</returns>
	[HttpPost, ValidateAntiForgeryToken]
	public async Task<IActionResult> AssignRole(string login, [Bind
		("Login,Roles,RoleName")] ManageRolesForm form)
	{
		if (login != form.Login) return NotFound();

		if (!ModelState.IsValid) return View(form);

		try
		{
			await _identity.AddToken(Token).AddToRoleAsync(login, form.RoleName);
			return RedirectToAction(nameof(Index));
		}

		catch (InvalidOperationException e)
		{
			foreach (var error in e.Message.Deserialize())
				ModelState.AddModelError(string.Empty, error);
			return View(form);
		}

		catch (AuthenticationException)
		{
			return Authorize(Url.Action
				("AssignRole", new { login, form }));
		}

		catch (KeyNotFoundException) { return NotFound(); }

		catch (Exception) { return StatusCode(502); }
	}

	/// <summary>
	/// Снимает <see cref="Role"/> у указанного <see cref="Account"/>
	/// </summary>
	/// <param name="login">Логин</param>
	/// <param name="form">Форма <see cref="ManageRolesForm"/></param>
	/// <returns>~View (<see cref="ManageRolesForm"/>) если ошибка , ~Index (коллекция элементов <see cref="Account"/>) , NotFound</returns>
	[HttpPost, ValidateAntiForgeryToken]
	public async Task<IActionResult> RemoveRole(string login, [Bind
		("Login,Roles,RoleName")] ManageRolesForm form)
	{
		if (login != form.Login) return NotFound();

		if (!ModelState.IsValid) return View(form);

		try
		{
			await _identity.AddToken(Token).RemoveFromRoleAsync(login, form.RoleName);
			return RedirectToAction(nameof(Index));
		}

		catch (InvalidOperationException e)
		{
			foreach (var error in e.Message.Deserialize())
				ModelState.AddModelError(string.Empty, error);
			return View(form);
		}

		catch (AuthenticationException)
		{
			return Authorize(Url.Action
				("RemoveRole", new { login, form }));
		}

		catch (KeyNotFoundException) { return NotFound(); }

		catch (Exception) { return StatusCode(502); }
	}

	#endregion
}
