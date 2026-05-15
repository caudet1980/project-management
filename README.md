# Project Manager

A full-stack project management app built with React and .NET 8.

**[Live Demo](#)** <!-- Replace with your deployed URL -->

## Features

- User authentication (register / login) with JWT
- Create and delete projects with a due date
- Create and delete tasks per project
- Form validation with error feedback
- Toast notifications
- French / English language toggle

## Tech Stack

**Frontend**
- React 18, React Router
- Axios with JWT interceptors
- Custom CSS with design tokens

**Backend**
- ASP.NET Core 8, Entity Framework Core
- SQLite
- JWT authentication, BCrypt password hashing
- Swagger / OpenAPI

## Getting Started

### Prerequisites
- Node.js 18+
- .NET 8 SDK

### Frontend

```bash
npm install
npm run dev
```

### Backend

```bash
cd backend
dotnet restore
dotnet ef database update
dotnet run
```

The API will be available at `http://localhost:5290` and Swagger at `http://localhost:5290/swagger`.

## Project Structure

```
├── src/                  # React frontend
│   ├── api/              # Axios API client
│   ├── components/       # UI components
│   ├── context/          # Auth and Language contexts
│   ├── locales/          # FR / EN translation files
│   └── pages/            # Login page
└── backend/
    ├── Controllers/       # API endpoints
    ├── DTOs/              # Request / response models
    ├── Entities/          # Database entities
    ├── Migrations/        # EF Core migrations
    └── Services/          # Business logic
```
