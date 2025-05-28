# Employee Management API

A simple RESTful API for managing employees, built with .NET 8 and Dapper.

## Features

- CRUD operations for employees
- Clean architecture (Controller → Service → Repository)
- Uses SQL Server and Dapper
- Swagger UI for testing

## Getting Started

1. Clone the repo
2. Update the connection string in `appsettings.json`
3. Run the app:

```bash
dotnet run
````

4. Access Swagger at: `https://localhost:5001/swagger`

## Endpoints

* `GET /api/employees`
* `GET /api/employees/{id}`
* `POST /api/employees`
* `PUT /api/employees/{id}`
* `DELETE /api/employees/{id}`
