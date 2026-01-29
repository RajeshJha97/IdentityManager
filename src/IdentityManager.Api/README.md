# IdentityManager.Api

Lightweight ASP.NET Core minimal API for user identity management using ASP.NET Core Identity and Entity Framework Core.

## Overview
IdentityManager.Api provides basic user registration and validation built with .NET 8 and C# 12. It exposes minimal API endpoints backed by an AuthHandler that handles validation (FluentValidation) and user creation (Identity + EF Core).

## Key features
- User registration endpoint: POST /api/user/register
- ASP.NET Core Identity integration (ApplicationUser)
- EF Core with SQL Server (AppDbContext)
- FluentValidation validators for request models
- Minimal API style with single-file endpoint mapping

## Tech stack
- .NET 8
- C# 12
- ASP.NET Core Minimal APIs
- Microsoft.AspNetCore.Identity
- Entity Framework Core (SQL Server)
- FluentValidation

## Getting started

Prerequisites
- .NET 8 SDK installed
- SQL Server instance accessible
- Visual Studio 2022 (recommended)

1. Open the solution in Visual Studio 2022.
2. Configure the connection string in `appsettings.json` under `ConnectionStrings:IdentityConnection`.
3. Add EF migrations and update the database:
   - In the __Package Manager Console__: `Add-Migration Initial` then `Update-Database`
   - Or via CLI: `dotnet ef migrations add Initial` and `dotnet ef database update`
4. Set the web project as startup, then run (__F5__ or __Debug > Start Debugging__).

## API

Register user
- POST /api/user/register
- Content-Type: application/json
- Request body example:
- Successful response: 200 OK
  - Body: { "message": "User registered successfully", "user": { ...ApplicationUser... } }
- Validation errors: 400 Bad Request with HttpValidationProblemDetails (error details keyed by field)

## Validation & Errors
Requests are validated using FluentValidation. Validation failures and Identity creation errors return structured problem details (HttpValidationProblemDetails) to make client-side handling straightforward.

## Extending
- Add login/auth token issuance (JWT) in ConfigureAuth and AuthHandler.
- Implement email confirmation and password reset flows via Identity.
- Add integration and unit tests for handlers and validators.
- Harden password rules and Identity options in Program.cs.

## Troubleshooting
- Ensure the connection string is correct and the database is reachable.
- If migrations fail, confirm EF tools are installed: `dotnet tool install --global dotnet-ef` or use the Package Manager Console.
- Check the application logs for validation and Identity errors.

## Contributing
Pull requests welcome — keep changes small and focused. Run tests and include migration updates as needed.