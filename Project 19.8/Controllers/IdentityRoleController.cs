using System.Security.Authentication;
using Microsoft.AspNetCore.Mvc;

using Project_19.Models;

namespace Project_19.Controllers;

/// <summary>
/// Контроллер администратора (управление <see cref="Role"/>)
/// </summary>
public class IdentityRoleController : PhonebookController
{
	/// <inheritdoc cref="IIdentity"/>
	private readonly IIdentity _identity;

	/// <summary>
	/// Конструктор
	/// </summary>
	/// <param name="identity">Идентификация</param>
	public IdentityRoleController(IIdentity identity) 
		: base("SignIn", "Identity") => _identity = identity;

	#region [Get]

	/// <summary>
	/// <inheritdoc cref="IIdentity.GetRolesAsync()"/>
	/// </summary>
	/// <returns>~Index (коллекция элементов <see cref="Role"/>)</returns>
	[HttpGet]
	public async Task<IActionResult> Index()
	{
		if (!User.IsInRole("admin")) return NotFound();

		try { return View(await _identity.AddToken(Token).GetRolesAsync()); }

		catch (AuthenticationException) { return Authorize(Url.Action("Index")); }

		catch (Exception) { return StatusCode(502); }
	}

	/// <summary>
	/// <inheritdoc cref="IIdentity.GetRoleByIdAsync"/>
	/// </summary>
	/// <returns>~View (<see cref="Role"/>) , NotFound</returns>
	[HttpGet]
	public async Task<IActionResult> Details(string? id)
	{
		if (!User.IsInRole("admin")) return NotFound();

		if (string.IsNullOrWhiteSpace(id)) return NotFound();

		try
		{
			var role = await _identity.AddToken(Token).GetRoleByIdAsync(id);
			return role is null ? NotFound() : View(role);
		}

		catch (AuthenticationException)
		{
			return Authorize(Url.Action
				("Details", new { id }));
		}

		catch (Exception) { return StatusCode(502); }
	}

	/// <summary>
	/// Запрашивает страницу создания <see cref="Role"/>
	/// </summary>
	/// <returns>~View (<see cref="CreateRoleForm"/>)</returns>
	[HttpGet]
	public IActionResult Create() 
		=> User.IsInRole("admin") ? View(_identity.CreateRoleForm) : NotFound();

	/// <summary>
	/// Запрашивает страницу редактирования <see cref="Role"/>
	/// </summary>
	/// <param name="id">Идентификатор</param>
	/// <returns>~View (<see cref="Role"/>) , NotFound</returns>
	[HttpGet]
	public async Task<IActionResult> Edit(string? id)
	{
		if (!User.IsInRole("admin")) return NotFound();

		if (string.IsNullOrWhiteSpace(id)) return NotFound();

		try
		{
			var role = await _identity.AddToken(Token).GetRoleByIdAsync(id);
			if (role is null || role.Name is "admin") return NotFound();
			return View(role);
		}

		catch (AuthenticationException)
		{
			return Authorize(Url.Action
				("Edit", new { id }));
		}

		catch (Exception) { return StatusCode(502); }
	}

	/// <summary>
	/// Запрашивает страницу удаления <see cref="Role"/>
	/// </summary>
	/// <param name="id">Идентификатор</param>
	/// <returns>~View (<see cref="Role"/>) , NotFound</returns>
	[HttpGet]
	public async Task<IActionResult> Delete(string? id)
	{
		if (!User.IsInRole("admin")) return NotFound();

		if (string.IsNullOrWhiteSpace(id)) return NotFound();

		try
		{
			var role = await _identity.AddToken(Token).GetRoleByIdAsync(id);
			if (role is null || role.Name is "admin") return NotFound();
			return View(role);
		}

		catch (AuthenticationException)
		{
			return Authorize(Url.Action
				("Delete", new { id }));
		}

		catch (Exception) { return StatusCode(502); }
	}

	#endregion

	#region [Post]

	/// <summary>
	/// <inheritdoc cref="IIdentity.AddRoleAsync"/>
	/// </summary>
	/// <param name="form">Форма создания <see cref="Role"/></param>
	/// <returns>~View (<see cref="CreateRoleForm"/>) если ошибка , ~Index (коллекция элементов <see cref="Role"/>)</returns>
	[HttpPost, ValidateAntiForgeryToken]
	public async Task<IActionResult> Create(CreateRoleForm form)
	{
		if (!User.IsInRole("admin")) return NotFound();

		if (!ModelState.IsValid) return View(form);

		try
		{
			await _identity.AddToken(Token).AddRoleAsync(form);
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
	/// Обновляет существующую <see cref="Role"/>
	/// </summary>
	/// <param name="id">Идентификатор</param>
	/// <param name="role">Тело роли</param>
	/// <returns>~View (<see cref="Account"/>) если ошибка , ~Index (коллекция элементов <see cref="Role"/>) , NotFound</returns>
	[HttpPost, ValidateAntiForgeryToken]
	public async Task<IActionResult> Edit(string id, [Bind
	("Id,Name")] Role role)
	{
		if (!User.IsInRole("admin")) return NotFound();

		if (id != role.Id || role.Name is "admin") return NotFound();

		if (!ModelState.IsValid) return View(role);

		try
		{
			await _identity.AddToken(Token).UpdateRoleAsync(role);
			return RedirectToAction(nameof(Index));
		}

		catch (InvalidOperationException e)
		{
			foreach (var error in e.Message.Deserialize())
				ModelState.AddModelError(string.Empty, error);
			return View(role);
		}

		catch (AuthenticationException)
		{
			return Authorize(Url.Action("Edit", new { id, role }));
		}

		catch (KeyNotFoundException) { return NotFound(); }

		catch (Exception) { return StatusCode(502); }
	}

	/// <summary>
	/// Удаляет существующую <see cref="Role"/>
	/// </summary>
	/// <param name="id">Идентификатор</param>
	/// <returns>~Index (коллекция элементов <see cref="Role"/>) , NotFound</returns>
	[HttpPost, ActionName(nameof(Delete)), ValidateAntiForgeryToken]
	public async Task<IActionResult> DeleteConfirmed(string id)
	{
		if (!User.IsInRole("admin")) return NotFound();

		try
		{
			var role = await _identity.AddToken(Token).GetRoleByIdAsync(id);

			if (role is null || role.Name is "admin") return NotFound();

			await _identity.RemoveRoleByIdAsync(id);

			return RedirectToAction(nameof(Index));
		}

		catch (AuthenticationException) { return Authorize(Url.Action("Delete", new { id })); }

		catch (InvalidOperationException) { return StatusCode(500); }

		catch (KeyNotFoundException) { return NotFound(); }

		catch (Exception) { return StatusCode(502); }
	}

	#endregion
}
