# Project 19 : Phonebook
#aspnetcore7.0.10 

Реализован простой сайт, выводящий информацию телефонной книжки

Обновления в рамках модуля 20.8:
- Изменена верстка страниц: используется Grid Layout и стили Bootstrap, подключена библиотека изображений FontAwesome
- Добавлен футер (привязан к низу страницы), область контента сделана адаптивной (скроллится при изменении размера страницы)
- С использованием DI реализована служба панели кнопок-ссылок на социальные сети (панель интегрирована в футер страницы)
- База данных перенесена из MSSQLLocalDb в PostgreSQL (VDS)
  
Закладывалось, но не было реализовано:
- Развертывание сайта на VDS с использованием Kubernetes (разобрался, но на стороне сервера возникли проблемы с Kubernetes - решается тех.поддержкой)
- Реализация запросов PUT и DELETE: не удалось согласовать использование ajax-скриптов с контроллером и браузером.
  P.S. - По сути, оно того и не стоит (ибо сейчас норма использовать GET и POST), но из интереса хотелось попробовать. Пример ниже:
  
  <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.7.0/jquery.min.js"></script>
  ...
  <script>
        $("#submit").click(function (e) {
        e.preventDefault();
        var data = $('#edit').serialize();
        $.ajax({
            type: "PUT",
                url: "@Url.Action("Edit", new {id = Model.Id})",
            data: data,
            success: function (response) {
                window.location.href = response.redirectToUrl;
            }
        });
    })
    </script>
