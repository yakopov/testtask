# Registration Wizard (Test Task)

This project implements a registration wizard consisting of two steps. The backend is built using C# Web API (CQRS) with Entity Framework Core and SQLite, while the frontend is a single-page application (SPA) developed with Angular.

![UI Preview](https://github.com/user-attachments/assets/c5a910fe-1d02-4461-a8d0-672b14b9a09d)

### Features

1. **Step 1 (User Information)**:
   - Fields: Email, Password, Confirm Password, Agree to Terms.
   - Validation: Password must contain at least one letter and one digit. Confirm Password must match the Password.
   - Validation errors are displayed under corresponding fields.

2. **Step 2 (Location Information)**:
   - Fields: Country (dropdown), Province (dependent on country selection).
   - AJAX is used to load the list of provinces based on the selected country.

3. **Step 3 (Success Screen)**:
   - Displays user ID and a success message upon successful registration.

### API Endpoints
- **GET /api/v1/countries**: Retrieves a list of countries.
- **GET /api/v1/countries/{countryId}/provinces**: Retrieves provinces based on country selection.
- **GET /api/v1/users/check-email**: Validates email availability for registration.
- **POST /api/v1/users**: Registers a new user.

### Architecture
- The application follows the **CQRS** pattern, with separate commands and queries for business logic and data access layers, ensuring flexibility and scalability.
- **EF Core + SQLite** are used for database interactions.

### Solution Structure

The solution is organized into several projects:

1. **Akoyur.TestTask.SPAWebApp**: Angular SPA frontend for the registration wizard.
2. **Akoyur.TestTask.WebAPI**: REST API backend project.
3. **Akoyur.TestTask.Abstractions.Database**: Contains auxiliary entities related to database objects and functionality.
4. **Akoyur.TestTask.Abstractions**: Includes common interfaces and base entities.
5. **Akoyur.TestTask.ApiModels**: Models for API requests and responses, including request validators.
6. **Akoyur.TestTask.Application.UnitTests**: Contains unit tests for UseCase commands and queries.
7. **Akoyur.TestTask.Application**: Core application logic, implementing CQRS commands and queries.
8. **Akoyur.TestTask.Configuration**: Application configuration files.
9. **Akoyur.TestTask.Database**: Handles database access, implemented using **EF Core** and **SQLite**.
10. **Akoyur.TestTask.Dto**: Defines Data Transfer Object (DTO) models.
11. **Akoyur.TestTask.Entities**: Defines the database entities.
12. **Akoyur.TestTask.Enumerations**: Includes enumerations and constants.
13. **Akoyur.TestTask.Extensions**: Contains extension methods for additional functionality.
14. **Akoyur.TestTask.Helpers**: Utility helper classes.
15. **Akoyur.TestTask.Infrastructure.Database**: Implements basic database functionality using **CQRS**.
16. **Akoyur.TestTask.Mappings**: Contains model mappings.

### How to Run

1. Open the solution in Visual Studio.
2. Press **Ctrl+F5** to run the **Akoyur.TestTask.WebAPI** project.
3. Press **Ctrl+F5** to run the **Akoyur.TestTask.SPAWebApp** project.

---

### Thank You!

Thank you for taking the time to review this project. I hope this solution meets your expectations and demonstrates a flexible, scalable approach to building a registration wizard with modern technologies.
