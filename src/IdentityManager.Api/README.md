# IdentityManager.Api

Lightweight ASP.NET Core minimal API for user identity management using ASP.NET Core Identity and Entity Framework Core.

## Overview
IdentityManager.Api provides endpoints for user registration, login, email confirmation and account removal. It uses ASP.NET Core Identity (ApplicationUser) with EF Core (SQL Server) and FluentValidation for request validation. Built with .NET 8 and C# 12 using Minimal APIs.

## Key features
- Register, login, email confirmation and remove user endpoints
- ASP.NET Core Identity integration (ApplicationUser)
- EF Core with SQL Server (AppDbContext)
- FluentValidation validators discovered via assembly scan
- Minimal API style with single-file endpoint mapping (ConfigureAuth)

## Endpoints
- POST /api/user/register
  - Request (application/json):
    ```json
    {
      "name": "Doe",
      "email": "user@example.com",
      "password": "P@ssw0rd!",
      "confirmPassword": "P@ssw0rd!"      
    }
    ```
  - Success: 200 OK with message and created user metadata (id, email, userName, firstName, lastName)
  - Validation errors: 400 Bad Request (HttpValidationProblemDetails)

- POST /api/user/login
  - Request:
    ```json
    {
      "email": "user@example.com",
      "password": "P@ssw0rd!"
    }
    ```
  - Success: 200 OK (handler currently returns sign-in result or problem details)
  - Failed login: structured problem details or 400/401 depending on handler logic

- POST /api/user/emailconfirmation
  - Request:
    ```json
    {
      "email": "user@example.com",
      "token": "base64-or-url-encoded-token"
    }
    ```
  - Success: 200 OK when confirmation token is valid
  - Errors: 400 Bad Request with problem details

- DELETE /api/user/remove
  - Request (application/json):
    ```json
    {
      "email": "user@example.com"
    }
    ```
  - Success: 200 OK on successful removal or structured error otherwise

Notes:
- Endpoints are registered in src/IdentityManager.Api/Endpoints/ConfigureAuth.cs via MapAuthEndpoints.
- Handler implementations live in src/IdentityManager.Api/Handlers/AuthHandler.cs.

## Configuration highlights (Program.cs)
- DbContext: Connection string key `ConnectionStrings:IdentityConnection` (SQL Server).
- Identity:
  - ApplicationUser + IdentityRole with EF stores and default token providers.
  - Email confirmation required: SignIn.RequireConfirmedEmail = true
  - Password non-alphanumeric not required by default: Password.RequireNonAlphanumeric = false
  - Lockout policy: MaxFailedAccessAttempts = 3, DefaultLockoutTimeSpan = 2 hours
  - Email tokens use the default provider (TokenOptions.DefaultEmailProvider)
- FluentValidation scanners:
  - Validators are registered via AddValidatorsFromAssembly(typeof(Program).Assembly, includeInternalTypes: true)

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

Environment tips
- For local secrets prefer __dotnet user-secrets__ or environment variables for connection strings and any future JWT keys.
- Ensure EF tools are available: `dotnet tool install --global dotnet-ef` (or use the Package Manager Console).

## Validation & Errors
Requests are validated using FluentValidation. Validation failures and Identity errors return structured problem details (HttpValidationProblemDetails) to aid client-side handling. Check application logs for more details.

## Extending
- Add JWT issuance on login and authentication middleware (AuthHandler and ConfigureAuth are the integration points).
- Implement email sending for confirmation tokens (Identity currently requires confirmed email).
- Add password reset flows and additional Identity options as needed.
- Add integration and unit tests (xUnit/NUnit + WebApplicationFactory for Minimal APIs).

## Troubleshooting
- Ensure the connection string is correct and the database is reachable.
- If migrations fail, confirm EF tools are installed or run migrations from __Package Manager Console__.
- If you get 400 responses, inspect the response body for HttpValidationProblemDetails to see field-level errors.
- Check the application logs (console or configured sinks) for validation and Identity errors.

## Contributing
Pull requests welcome — keep changes small and focused. Run tests and include migration updates as needed. Follow the existing coding style and add tests for new behavior.

## Notes / TODO
- Consider adding Swagger UI in non-development environments behind authentication.
- Add rate limiting, logging improvements and request throttling before production use.
- Consider dockerizing the API and the database for easier local development.