using System.Security.Authentication;
using Microsoft.AspNetCore.Mvc;

using Project_19.Models;

namespace Project_19.Controllers;

/// <summary>
/// Api-контроллер работы с данными коллекции элементов типа <see cref="Contact"/>
/// </summary>
public class ContactsController : PhonebookController
{
	/// <inheritdoc cref="IContacts"/>
	private readonly IContacts _contacts;

	/// <summary>
	/// Конструктор
	/// </summary>
	/// <param name="contacts">Коллекция контактов</param>
	public ContactsController(IContacts contacts) 
		: base("SignIn", "Identity") => _contacts = contacts;

	#region [Get]

	/// <summary>
	/// <inheritdoc cref="IContacts.GetAsync()"/>
	/// </summary>
	/// <returns>~Index (коллекция элементов <see cref="Contact"/>)</returns>
	[HttpGet]
	public async Task<IActionResult> Index()
	{
		try { return View(await _contacts.GetAsync()); }

		catch (Exception) { return StatusCode(502); }
	}

	/// <summary>
	/// Запрашивает страницу создания <see cref="Contact"/>
	/// </summary>
	/// <returns>~View (новый <see cref="Contact"/>)</returns>
	[HttpGet]
	public IActionResult Create() 
		=> User.Identity is { IsAuthenticated: true } ? View() : NotFound();

	/// <summary>
	/// <inheritdoc cref="IContacts.GetAsync(int)"/>
	/// </summary>
	/// <returns>~View (<see cref="Contact"/>) , NotFound</returns>
	[HttpGet]
	public async Task<IActionResult> Details(int? id)
	{
		if (User.Identity is not { IsAuthenticated: true }) return NotFound();

		if (id is null) return NotFound();

		try
		{
			var contact = await _contacts.AddToken(Token).GetAsync((int)id);
			return contact is null? NotFound() : View(contact);
		}

		catch (AuthenticationException) { return Authorize(Url.Action("Details", id)); }

		catch (Exception) { return StatusCode(502); }
	}

	/// <summary>
	/// Запрашивает страницу редактирования <see cref="Contact"/>
	/// </summary>
	/// <param name="id">Идентификатор</param>
	/// <returns>~View (<see cref="Contact"/>) , NotFound</returns>
	[HttpGet]
	public async Task<IActionResult> Edit(int? id)
	{
		if (!User.IsInRole("admin")) return NotFound();

		if (id is null) return NotFound();

		try
		{
			var contact = await _contacts.AddToken(Token).FindAsync((int)id);
			return contact is null ? NotFound() : View(contact);
		}

		catch (AuthenticationException) { return Authorize(Url.Action("Edit", id)); }

		catch (Exception) { return StatusCode(502); }
	}

	/// <summary>
	/// Запрашивает страницу удаления <see cref="Contact"/>
	/// </summary>
	/// <param name="id">Идентификатор</param>
	/// <returns>~View (<see cref="Contact"/>) , NotFound</returns>
	[HttpGet]
	public async Task<IActionResult> Delete(int? id)
	{
		if (!User.IsInRole("admin")) return NotFound();

		if (id is null) return NotFound();

		try
		{
			var contact = await _contacts.AddToken(Token).GetAsync((int)id);
			return contact is null ? NotFound() : View(contact);
		}

		catch (AuthenticationException) { return Authorize(Url.Action("Delete", id)); }

		catch (Exception) { return StatusCode(502); }
	}

	#endregion

	#region [Post]

	/// <summary>
	/// <inheritdoc cref="IContacts.AddAsync"/>
	/// </summary>
	/// <param name="contact">Тело контакта</param>
	/// <returns>~View (<see cref="Contact"/>) если ошибка , ~Index (коллекция элементов <see cref="Contact"/>)</returns>
	[HttpPost]
	[ValidateAntiForgeryToken]
	public async Task<IActionResult> Create([Bind
	("Id,LastName,FirstName,Patronymic,MobileNumber," +
	 "Address,Description,CreatedBy")] Contact contact)
	{
		if (!ModelState.IsValid) return View(contact);

		try { await _contacts.AddToken(Token).AddAsync(contact); return RedirectToAction(nameof(Index)); }

		catch (AuthenticationException) { return Authorize(Url.Action("Create", contact)); }

		catch (Exception) { return StatusCode(502); }
	}

	/// <summary>
	/// Обновляет существующий контакт
	/// </summary>
	/// <param name="id">Идентификатор</param>
	/// <param name="contact">Тело контакта</param>
	/// <returns>~View (<see cref="Contact"/>) если ошибка , ~Index (коллекция элементов <see cref="Contact"/>) , NotFound , InternalServerError</returns>
	[HttpPost]
	[ValidateAntiForgeryToken]
	public async Task<IActionResult> Edit(int id, [Bind
	("Id,LastName,FirstName,Patronymic,MobileNumber," +
	 "Address,Description,CreatedBy,LastModifiedBy")] Contact contact)
	{
		if (!User.IsInRole("admin")) return NotFound();

		if (id != contact.Id) return NotFound();

		if (!ModelState.IsValid) return View(contact);

		try { await _contacts.AddToken(Token).UpdateAsync(contact); return RedirectToAction(nameof(Index)); }

		catch (AuthenticationException) { return Authorize(Url.Action("Edit", new { id, contact })); }

		catch (KeyNotFoundException) { return NotFound(); }

		catch (Exception) { return StatusCode(502); }
	}

	/// <summary>
	/// Удаляет существующий контакт
	/// </summary>
	/// <param name="id">Идентификатор</param>
	/// <returns>~Index (коллекция элементов <see cref="Contact"/>) , NotFound</returns>
	[HttpPost]
	[ValidateAntiForgeryToken]
	[ActionName(nameof(Delete))]
	public async Task<IActionResult> DeleteConfirmed(int id)
	{
		if (!User.IsInRole("admin")) return NotFound();

		try { await _contacts.AddToken(Token).RemoveAsync(id); return RedirectToAction(nameof(Index)); }

		catch (AuthenticationException) { return Authorize(Url.Action("Delete", id)); }

		catch (InvalidOperationException) { return StatusCode(500); }

		catch (KeyNotFoundException) { return NotFound(); }

		catch (Exception) { return StatusCode(502); }
	}

	#endregion
}
