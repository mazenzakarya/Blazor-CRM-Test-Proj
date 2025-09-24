A simple Customer Relationship Management (CRM) application built with Blazor.

## Features

- Customer Management
- Contact Management
- Opportunity Tracking
- JWT Authentication

## Technologies

- .NET 8.0
- Blazor Server & WebAssembly
- Entity Framework Core
- SQL Server

## Getting Started

1. Clone the repository
2. Update connection string in `appsettings.json`
3. Run migrations: `dotnet ef database update`
4. Run the application: `dotnet run`

## Project Structure

```
├── Models/          # Data models
├── Data/           # Database context
├── Services/       # Business logic
├── Controllers/    # API endpoints
└── Components/     # Blazor UI
```

## API Endpoints

- `/api/customers` - Customer management
- `/api/contacts` - Contact management
- `/api/opportunities` - Opportunity tracking
- `/api/auth/login` - Authentication


---

**Author**: [Mazen Zakarya](https://github.com/mazenzakarya)
