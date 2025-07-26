# ⚽ FootballManager API

A .NET 8 RESTful Web API for managing football teams, players, matches, and player statistics — designed using **Clean Architecture**, Entity Framework Core, and full test coverage via xUnit.

---

## 🚀 Task Overview

This application is built as part of the `.NET Football Team Management API Assessment`. It includes:

✅ Clean & layered architecture  
✅ Proper use of EF Core and relationships  
✅ Validations, error handling & meaningful status codes  
✅ Swagger documentation  
✅ Comprehensive unit testing using xUnit & Moq

---

## 🧱 Project Structure

```
FootballManager/
├── FootballManager.API/           # Web API project (Controllers, DI config, Swagger)
├── FootballManager.Core/          # Entities, DTOs, Interfaces
├── FootballManager.Infrastructure/ # EF Core, DbContext, Repositories, Migrations
├── FootballManager.Services/      # Business logic layer
└── FootballManager.Tests/         # xUnit + Moq unit tests
```

---

## 🔧 Tech Stack

- **.NET 8**
- **Entity Framework Core**
- **SQL Server** (default, can be switched)
- **xUnit & Moq**
- **Swagger (OpenAPI)**
- **Clean Architecture**

---

## 🏁 Getting Started

### 1. Clone the repository

```bash
git clone https://github.com/your-username/FootballManager.git
cd FootballManager
```

### 2. Open the solution

Open `FootballManager.sln` in **Visual Studio 2022+**.

---

## ⚙️ Run the Application

### 1. Restore & Build

From Visual Studio:
- Open solution `FootballManager.sln`
- Build the solution (`Ctrl + Shift + B`)

Or via terminal:

```bash
dotnet restore
dotnet build
```

---

### 2. Update the Database

Make sure the connection string in `FootballManager.API/appsettings.json` is correct:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=HWARY\SQLEXPRESS;Database=FootballManagerDb;Trusted_Connection=True;"
}
```

Then apply migrations:

```bash
dotnet ef database update --project FootballManager.Infrastructure --startup-project FootballManager.API
```

---

### 3. Run the API

From Visual Studio:
- Set `FootballManager.API` as startup project
- Press `F5` or `Ctrl + F5`

From terminal:

```bash
cd FootballManager.API
dotnet run
```

Swagger will be available at:

```
https://localhost:5001/swagger
```

---

## 📌 API Endpoints Summary

### 🟡 Teams

| Verb | Endpoint                  | Description            |
|------|---------------------------|------------------------|
| GET  | /api/teams                | Get all teams          |
| GET  | /api/teams/{name}         | Get team by name       |
| POST | /api/teams                | Create new team        |
| PUT  | /api/teams/{id}           | Update team info       |
| DELETE | /api/teams/{id}         | Delete a team          |

---

### 🟢 Players

| Verb | Endpoint                        | Description               |
|------|----------------------------------|---------------------------|
| GET  | /api/players                    | Get all players           |
| GET  | /api/players/{id}              | Get player by ID          |
| GET  | /api/teams/{teamId}/players    | Get players by team       |
| POST | /api/players                   | Create new player         |
| PUT  | /api/players/{id}             | Update player             |
| DELETE | /api/players/{id}           | Delete player             |

---

### 🔵 Matches

| Verb | Endpoint                         | Description                  |
|------|----------------------------------|------------------------------|
| GET  | /api/matches                     | Get all matches              |
| GET  | /api/teams/{teamId}/matches      | Get matches for a team       |
| POST | /api/matches                     | Create a new match           |

---

### 🔴 Statistics

| Verb | Endpoint                                  | Description                        |
|------|-------------------------------------------|------------------------------------|
| GET  | /api/players/{id}/stats                   | Player statistics                  |
| GET  | /api/teams/{teamId}/stats                 | Aggregate team stats               |
| POST | /api/matches/{matchId}/stats              | Add stats for a match              |

---

## ✅ Validation Rules

- Team name must be unique
- Player position must be one of:
  `Goalkeeper`, `Defender`, `Midfielder`, `Forward`
- Scores and stats (goals, minutes) must be non-negative
- Proper validation for nulls, formats, and constraints

---

## 🧪 Testing

This project includes **unit tests** for:

- Controllers
- Services
- Business logic
- Error handling

To run all tests:

```bash
dotnet test
```

Tests are located in:

```
FootballManager.Tests/
```

---

## 📌 Assumptions Made

- A match can exist even if one of the teams is deleted (FK set to `null`)
- Player statistics are only tied to existing matches and players
- No authentication is implemented for simplicity

---

## ✨ Bonus Ideas (Not Implemented)

- JWT Authentication
- Pagination on list endpoints
- Global exception handling middleware
- Caching (e.g., for `GetAllTeams`)
- Logging (via Serilog or built-in)

---

## 📄 License

This project is licensed under the MIT License.

---

> Built with ❤️ by **Mohamed Elhawary**
