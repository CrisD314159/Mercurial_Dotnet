# MercurialBackendDotnet

<p align="center">
  <img alt="VS Code in action" src="https://res.cloudinary.com/dw43hgf5p/image/upload/v1744040055/mercurialPhotos/cnpqltg5vjmhbnufiudm.png" width="200">
</p>

MercurialBackendDotnet is a .NET backend API for managing users, assignments, subjects, topics, checklists, and notifications. It is designed to support a productivity or educational platform with robust authentication, authorization, and notification features.

This backend is the main backend for Mercurial UI Application, you can fint the repository at (https://github.com/CrisD314159/mercurial_ui)

You can find the app deployment at (https://mercurial-app.vercel.app)

**Author:** [Crisdev](https://crisdev-pi.vercel.app)

## Features

- **User Management:** Registration, authentication (JWT), and profile management.
- **Assignments:** CRUD operations, state management (TODO, IN_PROGRESS, DONE), and association with subjects and topics.
- **Subjects & Topics:** Organization of assignments by subjects and topics.
- **Checklists:** Add, update, remove, and mark checklist items as done/undone for assignments.
- **Notifications:** Email and push notifications for reminders and verification.
- **Role-based Authorization:** Secured endpoints using JWT Bearer authentication.
- **Database:** PostgreSQL with Entity Framework Core migrations.
- **Email Integration:** Google SMTP API support for sending emails.

## Project Structure

```
Controllers/         # API controllers for each resource
DB/                  # Entity Framework Core DbContext
Dto/                 # Data Transfer Objects (InputDTO, OutputDTO)
Exceptions/          # Custom exception classes
Migrations/          # EF Core database migrations
Model/               # Entity models
Services/            # Business logic and interfaces
Templates/           # Email HTML templates
Utils/               # Utility classes (e.g., EmailUtil)
Validations/         # FluentValidation validators
```

## Getting Started

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download)
- [PostgreSQL](https://www.postgresql.org/)

### Configuration

Set up the `.env` file with the following keys:

- `Gmail__Token` - Gmail app password for SMTP
- `Gmail__Mail` - Gmail address for sending emails
- `Jwt__Key` - JWT signing key
- `Jwt__RefreshKey` - JWT refresh token key
- `Jwt__Audience` - JWT audience
- `Jwt__Issuer` - JWT issuer
- `ConnectionStrings__DBConnection` - PostgreSQL connection string

### Database Migration

Apply migrations to set up the database:

```sh
dotnet ef database update
```

### Running the Application

```sh
dotnet run
```

The API will be available at `https://localhost:5001` or `http://localhost:5000`.

### Docker

To build and run with Docker:

```sh
docker build -t mercurial-backend .
docker run -p 5000:80 --env-file .env mercurial-backend
```

## API Overview

- **Authentication:** JWT-based, endpoints secured with `[Authorize]`
- **Assignments:** `/Assignment` (CRUD, mark as done/in progress/todo)
- **Subjects:** `/Subject` (CRUD)
- **Topics:** `/Topic` (CRUD)
- **Checklists:** `/CheckList` (CRUD, add/remove/mark nodes)
- **Notifications:** `/PushNotification` (schedule, send)

## Technologies Used

- ASP.NET Core Web API
- Entity Framework Core (PostgreSQL)
- FluentValidation
- Hangfire (background jobs)
- JWT Authentication
- Google SMTP
- Docker

