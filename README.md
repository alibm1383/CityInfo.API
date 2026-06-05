
# 🌍 CityInfo REST API

A RESTful Web API built with ASP.NET Core for managing city data. The project is structured around common backend development practices such as layered architecture, CRUD operations, Entity Framework Core, DTOs, AutoMapper, validation, and dependency injection.

## ✨ Features

- Create, read, update, and delete city data
- Built with ASP.NET Core Web API
- Layered architecture with Controllers, Services, and Repositories
- Entity Framework Core with SQLite
- DTOs for cleaner request and response models
- AutoMapper for object mapping
- JSON Patch support for partial updates
- Data validation using Data Annotations
- Dependency Injection for better structure and maintainability

## 🛠 Technologies

- C#
- ASP.NET Core Web API
- Entity Framework Core
- SQLite
- AutoMapper
- JSON Patch
- LINQ

## 📁 Project Structure

```text
CityInfo.API
├── Controllers
├── Services
├── Repositories
├── Entities
├── Models
├── DbContexts
├── Profiles
├── Migrations
├── appsettings.json
└── Program.cs
```

## 🚀 Getting Started

### Prerequisites

Make sure the following tools are installed before running the project:

- .NET SDK
- Visual Studio or VS Code

### Installation

1. Clone the project

2. Restore the required packages

    ```bash
    dotnet restore
    ```

3. Apply the database migrations

    ```bash
    dotnet ef database update
    ```

4. Run the project

    ```bash
    dotnet run
    ```

## 🔗 API Overview

The API includes endpoints for:

- Getting all cities
- Getting a city by ID
- Creating a new city
- Updating a city
- Partially updating a city with JSON Patch
- Deleting a city

## 🧱 Architecture

The project uses a layered structure to keep responsibilities separated and the codebase easier to maintain:

- **Controllers** handle HTTP requests and responses
- **Services** contain the core business logic
- **Repositories** are responsible for data access
- **DTOs** help separate API models from database entities

## ✅ Validation

Data Annotations are used to validate incoming data and ensure that only valid input is accepted.

Examples include:

- Required fields
- Maximum length limits
- Basic format validation

## 🔄 Mapping

AutoMapper is used to map data between entities and DTOs, helping keep the code clean and preventing direct exposure of database models in API responses.

## 🗃 Database

The project uses SQLite with Entity Framework Core.

> Database files such as `.db` or `.sqlite` are usually excluded from source control.
