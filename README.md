# Project 19 : Phonebook Web

<img align="right" width="100" height="100" src="https://github.com/rozhkovsvyat/Project19.API/assets/71471748/56c66f5c-b87e-45c3-bca7-1f9f2cde5d21">
<img align="right" width="100" height="100" src="https://github.com/rozhkovsvyat/Project19.API/assets/71471748/cdfe90f7-88d8-4d52-a921-8e0a1ec87a26">

**#net7.0.10-aspnetcore**

Web-клиент телефонной книги на базе [API](https://github.com/rozhkovsvyat/Project19.API)

> :link: [Использует общие библиотеки](https://github.com/rozhkovsvyat/Project19.Libs)
> 
> :link: [Использует Bootstrap](https://getbootstrap.com)

---

### SERVICES

* **PhonebookApi** -- поставщик контактов и идентификация / [Api.ApiContacts](https://www.nuget.org/packages/RozhkovSvyat.Project19.Services.Api.ApiContacts) + [Api.ApiIdentity](https://www.nuget.org/packages/RozhkovSvyat.Project19.Services.Api.ApiIdentity)
* **PhonebookApiTokenClaims** -- сервис полномочий пользователя / [JwtBearer](https://www.nuget.org/packages/Microsoft.AspNetCore.Authentication.JwtBearer) + [Session](https://www.nuget.org/packages/Microsoft.AspNetCore.Session)
* **SocialBar** -- панель социальных сетей / [UrlButtonService.SocialBar](https://www.nuget.org/packages/RozhkovSvyat.Project19.Services.UrlButtonService.SocialBar) + [FontAwesome](https://fontawesome.com)

---

### CONTROLLERS
  
* **ContactsController** ../contacts/ -- управление книгой контактов

> :memo: **<sub>_type_</sub>Method<sup>(args)**</sup>*<sup>-auth/</sup>**<sup>-аdmin</sup>
>
> <sub>_g_</sub>**Index**</sub><sup>( )</sup> / <sub>_g_</sub>**Create**<sup>( )</sup>* / <sub>_p_</sub>**Create**<sup>(contact)</sup>* / <sub>_g_</sub>**Details**<sup>(int)</sup>* / <sub>_g_</sub>**Edit**<sup>(int)</sup>** / <sub>_p_</sub>**Edit**<sup>(int,contact)</sup>** / <sub>_g_</sub>**Delete**<sup>(int)</sup>** / <sub>_p_</sub>**Delete**<sup>(int)</sup>**

* **IdentityController** ../identity/ -- управление авторизацией пользователей

> <sub>_p_</sub>**LogOut**<sup>( )</sup> / <sub>_g_</sub>**SignIn**<sup>( )</sup> / <sub>_p_</sub>**SignIn**<sup>(signinform)</sup> / <sub>_g_</sub>**Create**<sup>( )</sup> / <sub>_p_</sub>**Create**<sup>(accform)</sup> / <sub>_g_</sub>**ChangePassword**<sup>( )</sup>* / <sub>_p_</sub>**ChangePassword**<sup>(passform)</sup>*

* **IdentityRoleController** ../identityrole/ -- управление пользовательскими ролями

> <sub>_g_</sub>**Index**<sup>( )</sup>** / <sub>_g_</sub>**Create**<sup>( )</sup>** / <sub>_p_</sub>**Create**<sup>(roleform)</sup>** / <sub>_g_</sub>**Details**<sup>(str)</sup>**  / <sub>_g_</sub>**Edit**<sup>(str)</sup>** / <sub>_p_</sub>**Edit**<sup>(str,role)</sup>** / <sub>_g_</sub>**Delete**<sup>(str)</sup>** / <sub>_p_</sub>**Delete**<sup>(str)</sup>**

* **IdentityAccountController** ../identityaccount/ -- управление пользовательскими аккаунтами

> <sub>_g_</sub>**Index**</sub><sup>( )</sup>** / <sub>_g_</sub>**Create**<sup>( )</sup>** / <sub>_p_</sub>**Create**<sup>(accform)</sup>** / <sub>_g_</sub>**Details**<sup>(str)</sup>** / <sub>_g_</sub>**Edit**<sup>(str)</sup>** / <sub>_p_</sub>**Edit**<sup>(str,acc)</sup>** / <sub>_g_</sub>**Delete**<sup>(str)</sup>** / <sub>_p_</sub>**Delete**<sup>(str)</sup>** / <sub>_g_</sub>**ChangePassword**<sup>(str)</sup>** / <sub>_p_</sub>**ChangePassword**<sup>(passform)</sup>** / <sub>_g_</sub>**ShowRoles**<sup>(str)</sup>** / <sub>_g_</sub>**AssignRole**<sup>(str)</sup>** / <sub>_p_</sub>**AssignRole**<sup>(str,rolesform)</sup>** / <sub>_g_</sub>**RemoveRole**<sup>(str)</sup>** / <sub>_p_</sub>**RemoveRole**<sup>(str,rolesform)</sup>**

* **PhonebookController** -- абстрактный контроллер, реализующий переход на страницу авторизации

---

💣 **404** notfound
💣 **500** exception
💣 **502** apinotavailable
