Последовательность действий для работы с пакетом:
1. Добавить на сцену префаб из пакета
2. Добавить на сцену скрипт AvatarCreator и перенести туда WebView.
3. В сторонних скриптах есть возможность вызывать следующие события:

3а. OnUrlLoaded - пробует загрузить созданный аватар из PlayerPrefs. Если его нет - возвращает null, иначе возвращает url.

3б. OnAvatarImported - импортирует персонажа по существующему url. Вызывать, когда OnUrlLoaded вернул не null.

3в. OnAvatarCreated - создает персонажа заново. 

Созданный аватар является GameObject`ом.
Класс имеет 2 свойства - Avatar и Url. Используйте их для получения GameObject и его Url.
