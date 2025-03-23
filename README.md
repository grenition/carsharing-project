# F-API
This project is a **modular monolith** template built with **ASP.NET Core**, aimed at large-scale applications that may later evolve into a **microservices architecture**.

## Structure
```
src/
├── API/               # AppHost - Entry point, API Gateway, composition root
├── Modules/           # Feature-based modules
│   ├── Users/
│   │   ├── API/
│   │   ├── Application/
│   │   ├── Domain/
│   │   └── Infrastructure/
│   └── ...            # Other modules (e.g., Orders, Billing)
└── Shared/            # Shared framework: config, base abstractions, middleware, etc.
```
`API/AppHost` – acts as the API Gateway and composition root for the application. Contains `Program.cs` entry application file.

`Modules` – isolated business features organized using Clean Architecture, each with its own API, Application, Infrastructure, and Domain layers.

`Shared/SharedFramework` – contains reusable components, cross-cutting concerns, and configuration logic shared across all modules.

## Modules
### Authentication module

The `Users.Auth` module handles user identity and access management. It supports:

- **User registration** with **email confirmation**
- **Authentication** using **JWT tokens**
- **Two-factor authentication (2FA)** with a temporary short-lived JWT and numeric code
- **Enabling/disabling 2FA** through user settings

#### API Endpoints:

- `POST   /api/users/auth/login` – Standard email/password login  
- `POST   /api/users/auth/login/two-factor` – Verifies 2FA code after login  
- `POST   /api/users/auth/register` – Registers a new user  
- `POST   /api/users/auth/confirm-email` – Confirms user email  
- `GET    /api/users/settings/two-factor` – Retrieves 2FA status  
- `PUT    /api/users/settings/two-factor` – Enables or disables 2FA  

<img width="1444" alt="image" src="https://github.com/user-attachments/assets/590b228c-8634-4e5d-b179-3ac1e6c6270a" />

This module is fully isolated and designed for clean integration with other application modules.

## Deployment
To run the project locally or deploy it in a containerized environment:

1. Create or edit the .env file in src/API/AppHost/ and set the required environment variables.
```
JWT_KEY=
TWO_FA_KEY=
SMTP_USER_ADDRESS=
SMTP_USER_PASSWORD=
```
2. Start the application. Spin up all services via Docker:
```
docker-compose up --build
```
This will launch the API Gateway, modules, database, and other required services defined in docker-compose.yml.
