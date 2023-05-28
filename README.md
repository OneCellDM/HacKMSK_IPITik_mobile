# HacKMSK_IPITik_mobile

#DESCRIPTION 
Мобильное приложение создано на Xamarin.forms (С# NetStandart 2.1) 

Поддерживаемые платформы:
Мнимальная: Android 7.0  
Целевая :Android 12.0

Проверено на Android 7.0 (Nox player emulator)
Проверено на Android 12.0 (Samsung galaxy A71)

Приложение служит для отправки видео  на сервер обработчик и получение результата обработки

# BACKEND AND MOBILE CONNECTION
Так как у нас не получилось воспользоваться сервисом Cloud и нету выделенной VDS с доменом, для автомной работы проекта, то всё наше творчество может существовать только в предалах локальной сети, сейчас адреса прописаны хардкодом чтобы поменять, перейдите в директорию мобильного проекта по пути: services/network/service.cs поменяйте ip нa
line - 25
line - 26

Перед зазапуском требуется рабочий сервер: https://github.com/Jexelus/HacKMSK_IPITik_web

# THIS README ONLY FOR WEBSITE AND BACKEND 
mobile project - https://github.com/OneCellDM/HacKMSK_IPITik_mobile
