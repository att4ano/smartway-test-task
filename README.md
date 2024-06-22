# smartway-test-task
## Web-Сервис сотрудников, сделанный на платформе .Net Core

### Инструкция для запуска сервиса

Из папки с проектом запустить:

```
docker-compose build && docker-compose up
```
С документацией можно ознакомится здесь: [http://localhost:8080/swagger/index.html](http://localhost:8080/swagger/index.html)

### Постановка задачи

**Сервис должен уметь:**

1. Добавлять сотрудников, в ответ должен приходить Id добавленного сотрудника.
2. Удалять сотрудников по Id.
3. Выводить список сотрудников для указанной компании. Все доступные поля.
4. Выводить список сотрудников для указанного отдела компании. Все доступные
поля.
5. Изменять сотрудника по его Id. Изменения должно быть только тех полей,
которые указаны в запросе.

**Модель сотрудника:**
```
{
  Id int
  Name string
  Surname string
  Phone string
  CompanyId int
  Passport {
    Type string
    Number string
  }
  Department {
    Name string
    Phone string
  }
}
```
Все методы должны быть реализованы в виде HTTP запросов в формате JSON. 

БД: любая.

ORM: Dapper

### Схема базы данных сервиса

![image](https://github.com/att4ano/smartway-test-task/assets/113085241/4f7e1c03-c341-4b21-8476-7e679933dd78)



