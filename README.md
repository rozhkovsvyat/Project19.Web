# Project 19 : Phonebook Web

<img align="right" width="100" height="100" src="https://github.com/rozhkovsvyat/Project19.API/assets/71471748/56c66f5c-b87e-45c3-bca7-1f9f2cde5d21">
<img align="right" width="100" height="100" src="https://github.com/rozhkovsvyat/Project19.API/assets/71471748/cdfe90f7-88d8-4d52-a921-8e0a1ec87a26">

**#aspnetcore7.0.10**

Web-клиент проекта Phonebook на базе [API](https://github.com/rozhkovsvyat/Project19.API/tree/master)

> :link: [Использует общие библиотеки](https://github.com/rozhkovsvyat/Project19.Libs)
> 
> :link: [Использует Bootstrap](https://getbootstrap.com/)

Предоставляет разграниченный доступ к телефонной книге:
* **Администратор** -- полный доступ к книге, администрирование пользователей
* **Пользователь** -- детальный просмотр записей, добавление новых записей, смена пароля
* **Анонимус** -- только просмотр записей

---

### SERVICES

* **PhonebookApi** -- сервисы поставщика контактов и идентификации / [Api.ApiContacts](https://www.nuget.org/packages/RozhkovSvyat.Project19.Services.Api.ApiContacts/) + [Api.ApiIdentity](https://www.nuget.org/packages/RozhkovSvyat.Project19.Services.Api.ApiIdentity/)
* **PhonebookApiTokenClaims** -- сервис пользователя токена / [JwtBearer](https://www.nuget.org/packages/Microsoft.AspNetCore.Authentication.JwtBearer) + [Session](https://www.nuget.org/packages/Microsoft.AspNetCore.Session/)
> :bulb: Предоставляет пользователю полномочия согласно токену сессии
* **SocialBar** -- сервис панели социальных сетей / [UrlButtonService.SocialBar](https://www.nuget.org/packages/RozhkovSvyat.Project19.Services.UrlButtonService.SocialBar/) + [FontAwesome](https://fontawesome.com/)

---

### LAYOUT

* **HeaderViewComponent** -- шапка с выпадающим навигационным меню и кнопкой возврата домой
* **Title**, **Body**, **Scripts** -- текущая страница
* **Footer** -- панель социальных сетей с кнопкой загрузки [WPF-клиента](https://github.com/rozhkovsvyat/Project19.WPF)
---

### CONTROLLERS
  
* **ContactsController** ../Contacts/ -- управление контактами

> :bulb: **<sub>_type_</sub>Method<sup>(args)**</sup>*<sup>-auth/</sup>**<sup>-аdmin</sup>
>
> <sub>_g_</sub>**Index**</sub><sup>( )</sup> / <sub>_g_</sub>**Create**<sup>( )</sup>* / <sub>_p_</sub>**Create**<sup>(contact)</sup>* / <sub>_g_</sub>**Details**<sup>(int)</sup>* / <sub>_g_</sub>**Edit**<sup>(int)</sup>** / <sub>_p_</sub>**Edit**<sup>(int,contact)</sup>** / <sub>_g_</sub>**Delete**<sup>(int)</sup>** / <sub>_p_</sub>**Delete**<sup>(int)</sup>**

* **IdentityController** ../Identity/ -- управление авторизацией пользователей

> <sub>_p_</sub>**LogOut**<sup>( )</sup> / <sub>_g_</sub>**SignIn**<sup>( )</sup> / <sub>_p_</sub>**SignIn**<sup>(signinform)</sup> / <sub>_g_</sub>**Create**<sup>( )</sup> / <sub>_p_</sub>**Create**<sup>(accform)</sup> / <sub>_g_</sub>**ChangePassword**<sup>( )</sup>* / <sub>_p_</sub>**ChangePassword**<sup>(passform)</sup>*

* **IdentityRoleController** ../IdentityRole/ -- управление пользовательскими ролями

> <sub>_g_</sub>**Index**<sup>( )</sup>** / <sub>_g_</sub>**Create**<sup>( )</sup>** / <sub>_p_</sub>**Create**<sup>(roleform)</sup>** / <sub>_g_</sub>**Details**<sup>(str)</sup>**  / <sub>_g_</sub>**Edit**<sup>(str)</sup>** / <sub>_p_</sub>**Edit**<sup>(str,role)</sup>** / <sub>_g_</sub>**Delete**<sup>(str)</sup>** / <sub>_p_</sub>**Delete**<sup>(str)</sup>**

* **IdentityAccountController** ../IdentityAccount/ -- управление пользовательскими аккаунтами

> <sub>_g_</sub>**Index**</sub><sup>( )</sup>** / <sub>_g_</sub>**Create**<sup>( )</sup>** / <sub>_p_</sub>**Create**<sup>(accform)</sup>** / <sub>_g_</sub>**Details**<sup>(str)</sup>** / <sub>_g_</sub>**Edit**<sup>(str)</sup>** / <sub>_p_</sub>**Edit**<sup>(str,acc)</sup>** / <sub>_g_</sub>**Delete**<sup>(str)</sup>** / <sub>_p_</sub>**Delete**<sup>(str)</sup>** / <sub>_g_</sub>**ChangePassword**<sup>(str)</sup>** / <sub>_p_</sub>**ChangePassword**<sup>(passform)</sup>** / <sub>_g_</sub>**ShowRoles**<sup>(str)</sup>** / <sub>_g_</sub>**AssignRole**<sup>(str)</sup>** / <sub>_p_</sub>**AssignRole**<sup>(str,rolesform)</sup>** / <sub>_g_</sub>**RemoveRole**<sup>(str)</sup>** / <sub>_p_</sub>**RemoveRole**<sup>(str,rolesform)</sup>**

* **PhonebookController** -- абстрактный контроллер, реализующий переход на страницу авторизации
