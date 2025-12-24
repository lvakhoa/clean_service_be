# Cleaning Service - Backend Service 🚀

[![ASP.NET Core](https://img.shields.io/badge/ASP.NET%20Core-8.0-purple?style=flat&logo=.net)](https://dotnet.microsoft.com/)
[![Entity Framework Core](https://img.shields.io/badge/EF%20Core-8.0-blue?style=flat&logo=.net)](https://docs.microsoft.com/en-us/ef/core/)
[![MySQL](https://img.shields.io/badge/MySQL-8.0-blue?style=flat&logo=mysql)](https://www.postgresql.org/)
[![Redis](https://img.shields.io/badge/Redis-7.4-DC382D?style=flat&logo=redis)](https://redis.io/lp/get-started1/?utm_campaign=gg_s_core_apac_en_brand_acq_21161918358&utm_source=google&utm_medium=cpc&utm_content=redis_exact&utm_term=&gad_source=1&gclid=Cj0KCQjw1um-BhDtARIsABjU5x7v5WI-b4S18STdeVoPS4yH44hwk2CepCYQpNwAzzDPSNax0WW2ws4aAtyLEALw_wcB)
[![Docker](https://img.shields.io/badge/Docker-Latest-blue?style=flat&logo=docker)](https://www.docker.com/)
[![License](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT)

**The Cleaning Service Backend** is a robust and scalable system designed to manage bookings, service scheduling, payments, and user data for a professional cleaning service platform. Built with a modern technology stack, it ensures efficient data handling, secure authentication, and seamless integration with the frontend. This repository contains all backend components, including API endpoints, database models, and business logic, to power the cleaning service application.



## 📑 Table of Contents
- [Related Repositories](#-related-repositories)
- [Features](#-features)
- [Technology Stack](#%EF%B8%8F-technology-stack)
- [Getting Started](#-getting-started)
- [Development Guidelines](#-development-guidelines)
- [Project Structure](#-project-structure)
- [Database Configuration](#-database-configuration)
- [Deployment](#-deployment)
- [How to push](#-how-to-push)
- [License](#-license)

## 📚 Related Repositories

- **Frontend Application**: [Cleaning Service - Frontend](https://github.com/lvakhoa/clean_service_fe)

## 🌟 Features

### Core Features
- User authentication and authorization
- Customer and Employee management
- Interactive booking system
- Feedback and rating system
- Social media integration
- File upload handling
- Contract manage
- Supports bank payments
- Accelerate data querying with Redis

### Module-Specific Features
- **Auth**: Handles user authentication, authorization.
- **Booking**: Cleaning service booking system
- **Contract**: Manages contract for each invoice
- **Mail**: Manages user verification and related communication
- **Manage**: Cleaning service information details
- **Payment**: Bank payments service
- **Scheduler**: Supports managing order scheduling
- **ServiceCategory**: Service Category relate features
- **ServiceType**: Service Type relate features
- **Storage**: Media file management

## 🛠️ Technology Stack

### Core Framework
- **ASP.NET Core 8.0**
- **Entity Framework Core 8.0**
- **MySQL 8.0**
- **Redis 7.4**
- **AutoMapper** for object mapping

### External Services
- **Cloudinary**: Media storage
- **Clerk**: Authentication and Authorization services

### Development Tools
- **Docker & Docker Compose**
- **Git** for version control

## 🚀 Getting Started

### Prerequisites
- .NET 8.0 SDK
- MySQL 8.0+
- Redis 7.4
- Docker (optional)
- IDE (Visual Studio 2022 or JetBrains Rider recommended)

### Local Development Setup

1. Clone the repository
```bash
git clone https://github.com/lvakhoa/clean_service_be.git
cd clean_service_be
```

2. Configure your environment
```bash
cp appsettings.Development.example.json appsettings.Development.json
# Update the configuration values in appsettings.Development.json
```

3. Install dependencies
```bash
dotnet restore
```

4. Set up the database
```bash
# Create migration
dotnet ef migrations add InitialCreate

# Update database
dotnet ef database update
```

5. Run the application
```bash
dotnet run
```

### Docker Setup

1. Build and run using Docker Compose
```bash
docker-compose up --build
#Or else run by the "Start" button from IDE you are using
```

## 👨‍💻 Development Guidelines

### Environment Setup

#### 1. Configure Development Environment
Create your development configuration file:
```bash
cp CleanService/appsettings.example.json CleanService/appsettings.Development.json
```

Update the following required settings in `appsettings.Development.json`:

**Database Configuration:**
```json
"ConnectionStrings": {
  "DefaultConnection": "Server=localhost;Database=clean_service_db;User=root;Password=your_password;"
}
```

**Redis Configuration:**
```json
"Redis": {
  "Host": "localhost",
  "Port": "6379",
  "Password": "",
  "User": "default"
}
```

**OAuth/Clerk Configuration:**
```json
"OAuthProvider": {
  "Domain": "your-clerk-domain.clerk.accounts.dev",
  "ClientId": "your-oauth-client-id",
  "ClientSecret": "your-oauth-client-secret"
}
```

**Firebase Configuration:**
- Get your Firebase service account JSON from Firebase Console
- Copy the values to the `FirebaseConfig` section

**Payment Gateway Configuration:**
- **PayOS**: For local payments
- **VnPay**: Vietnamese payment gateway (use sandbox URL for development)
- **ZaloPay**: ZaloPay integration (use sandbox URL for development)

**Cloudinary Configuration:**
```json
"CLOUDINARY_URL": "cloudinary://api_key:api_secret@cloud_name"
```

**Email Configuration:**
```json
"Resend": {
  "ApiKey": "re_your_resend_api_key"
},
"Mail": {
  "From": "noreply@yourdomain.com"
}
```

**Application URLs:**
```json
"WEB_URL": "http://localhost:3000",
"APP_URL": "myapp://",
"API_URL": "http://localhost:5011"
```

**Secret Key for JWT:**
```json
"SECRET_KEY": "your-secret-key-for-jwt-minimum-32-characters-long"
```

#### 2. Database Setup

**Local MySQL Setup:**
```bash
# Start MySQL service
sudo service mysql start  # Linux
brew services start mysql  # macOS

# Create database
mysql -u root -p
CREATE DATABASE clean_service_db;
exit;

# Run migrations
dotnet ef database update
```

**Docker MySQL Setup:**
```bash
# MySQL is configured in docker-compose.yaml
# Just run docker-compose
docker-compose up mysql redis
```

#### 3. Redis Setup

**Local Redis:**
```bash
# Install Redis
sudo apt-get install redis-server  # Linux
brew install redis                  # macOS

# Start Redis
redis-server
```

**Docker Redis:**
```bash
# Included in docker-compose.yaml
docker-compose up redis
```

### Code Style and Standards

#### Naming Conventions
- **Classes**: PascalCase (e.g., `BookingService`, `UserController`)
- **Methods**: PascalCase (e.g., `CreateBooking`, `GetUserById`)
- **Variables**: camelCase (e.g., `userId`, `bookingDetails`)
- **Private fields**: _camelCase (e.g., `_unitOfWork`, `_logger`)
- **Constants**: UPPER_SNAKE_CASE (e.g., `MAX_RETRY_COUNT`)

#### Entity Framework Guidelines
- Always use specifications for queries
- Use async/await for database operations
- Never use `.Result` or `.Wait()` - always use `await`
- Handle CreatedAt/UpdatedAt automatically in DbContext
- Use transactions for complex operations

**Example - Correct Way:**
```csharp
// ✅ CORRECT
public async Task<Booking> GetBookingAsync(Guid id)
{
    var spec = BookingSpecification.GetBookingByIdSpec(id);
    return await _unitOfWork.Repository<Bookings, PartialBookings>()
        .GetFirstOrThrowAsync(spec);
}

// ❌ WRONG
public Booking GetBooking(Guid id)
{
    return _context.Bookings.FirstOrDefault(b => b.Id == id); // Don't use DbContext directly
}
```

#### Exception Handling
- Use custom exceptions (`BadRequestException`, `NotFoundException`, etc.)
- Let the global exception handler catch and format errors
- Don't wrap exceptions unnecessarily

**Example:**
```csharp
// ✅ CORRECT
if (user == null)
{
    throw new NotFoundException("User not found");
}

// ❌ WRONG
try
{
    // ... some code
}
catch (Exception ex)
{
    return new ErrorResponse(ex.Message); // Let exception handler do this
}
```

#### Controller Best Practices
- Keep controllers thin - business logic goes in services
- Use async actions
- Return proper status codes
- Use ApiController attribute and ApiSuccessResult/ApiErrorResult

**Example:**
```csharp
[HttpPost]
public async Task<IActionResult> CreateBooking([FromBody] CreateBookingDto dto)
{
    var result = await _bookingService.CreateBookingAsync(dto);
    return Ok(new ApiSuccessResult<BookingResponseDto>(
        StatusCodes.Status201Created,
        "Booking created successfully",
        result
    ));
}
```

#### Background Jobs
- Use BackgroundService for long-running tasks
- Log important events and errors
- Handle cancellation tokens properly
- Use scoped services correctly

### Git Workflow

#### Branch Naming
- Feature: `feat/issue-id-short-description`
- Bug fix: `fix/issue-id-short-description`
- Hotfix: `hotfix/issue-id-short-description`
- Documentation: `docs/short-description`

**Examples:**
```bash
feat/123-add-booking-cancellation
fix/456-datetime-timezone-issue
hotfix/789-payment-callback-error
docs/update-api-documentation
```

#### Commit Messages
Follow the format: `{type}: #{issue_id} {subject}`

**Types:**
- `feat`: New feature
- `fix`: Bug fix
- `docs`: Documentation changes
- `style`: Code style changes (formatting, semicolons, etc.)
- `refactor`: Code refactoring
- `perf`: Performance improvements
- `test`: Adding tests
- `build`: Build system changes
- `ci`: CI/CD changes
- `chore`: Other changes

**Examples:**
```bash
git commit -m "feat: #123 add booking cancellation job"
git commit -m "fix: #456 correct CreatedAt timezone issue"
git commit -m "docs: update development guidelines"
```

#### Pull Request Guidelines
1. Create a branch from `main`
2. Make your changes
3. Run tests and ensure they pass
4. Update documentation if needed
5. Create PR with clear description
6. Link related issues
7. Request review from team members

### Testing

#### Unit Tests
```bash
# Run all tests
dotnet test

# Run specific test
dotnet test --filter "FullyQualifiedName~BookingServiceTests"

# Run with coverage
dotnet test --collect:"XPlat Code Coverage"
```

#### Manual API Testing
Use the Swagger UI available at: `http://localhost:5011/swagger`

Or use tools like:
- Postman
- Insomnia
- cURL

### Debugging

#### Visual Studio / Rider
1. Set breakpoints in your code
2. Press F5 to start debugging
3. Use the Debug Console to inspect variables

#### Logs
Check application logs in:
```bash
# Console output
dotnet run

# Check background job logs
# Look for BookingCancellationJob messages
```

### Common Issues and Solutions

#### Issue: DateTime always shows same value
**Solution:**
- Don't use `HasDefaultValue(DateTime.Now)` in EF configuration
- Handle CreatedAt/UpdatedAt in SaveChanges override
- Use `DateTime.UtcNow` instead of `DateTime.Now`

#### Issue: Exception not caught by exception handler
**Solution:**
- Never use `.Result` or `.Wait()` on async methods
- Use `async/await` properly
- Don't use `ContinueWith` - use `await` instead

#### Issue: Database connection failed
**Solution:**
- Check MySQL service is running
- Verify connection string in appsettings.Development.json
- Ensure database exists: `CREATE DATABASE clean_service_db;`

#### Issue: Redis connection failed
**Solution:**
- Check Redis service is running: `redis-cli ping`
- Verify Redis configuration in appsettings.Development.json
- For local development, password can be empty

### Performance Best Practices

1. **Use Redis caching** for frequently accessed data
2. **Use pagination** for list endpoints
3. **Add database indexes** for frequently queried columns
4. **Use .AsNoTracking()** for read-only queries
5. **Avoid N+1 queries** - use Include() or AutoInclude
6. **Use background jobs** for long-running tasks

### Security Best Practices

1. **Never commit sensitive data** - use appsettings.Development.json (gitignored)
2. **Use environment variables** for production secrets
3. **Validate all user inputs** using FluentValidation
4. **Use parameterized queries** (EF Core does this by default)
5. **Implement proper authorization** using policies and roles
6. **Use HTTPS** in production

### Before Submitting PR

- [ ] Code compiles without errors
- [ ] All tests pass
- [ ] Code follows naming conventions
- [ ] Added/updated documentation
- [ ] No sensitive data in commits
- [ ] Commit messages follow guidelines
- [ ] PR description is clear and complete


## 🏗️ Project Structure

```
clean_service_be/
├── CleanService/
│   ├── Properties/
│   ├── Src/
│   │   ├── Common/
│   │   ├── Configs/
│   │   ├── Constant/
│   │   ├── Database/
│   │   ├── Exceptions/
│   │   ├── Filters/
│   │   ├── Infrastructures/
│   │   ├── Middlewares/
│   │   ├── Model/
│   │   ├── Modules/
│   │   ├── Utils/
│   ├── CleanService.csproj
│   ├── CleanService.csproj.DotSettings
│   └── Program.cs
├── .config/
├── .husky/
└── Dockerfile
```

### Module Structure
Each feature module follows this structure:
```
Module/
├── Controller.cs        # API endpoints
├── Module.cs            # Configuration and dependency
├── Mapper/              # AutoMapper profiles
└── Services/            # Business logic implementation
```

## 💾 Database Configuration

### Connection String Format
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Database=foodspot;Username=your_username;Password=your_password"
  }
}
```

### Migration Commands
```bash
# Add new migration
dotnet ef migrations add MigrationName

# Update database
dotnet ef database update

# Remove last migration
dotnet ef migrations remove
```

## 🚢 Deployment

### Production Setup
1. Update appsettings.json with production values
2. Set environment variables for sensitive data
3. Build the Docker image
```bash
update the docker build command
```

### Environment Variables
Required environment variables:
```
#View the .env.example
```

## ❔ **How to push**

- Role commit
  `{type}: #{issue_id} {subject}`
  - type: build | chore | ci | docs | feat | fix | perf | refactor | revert | style | test
  - subject: 'Write a short, imperative tense description of the change'
- Automatic: check lint and format pre-commit

- Example:

```bash
git commit -m "{type}: #{issue_id} {subject}"
```

Description
|**Types**| **Description** |
|:---| :--- |
|feat| A new feature|
|fix| A bug fix|
|docs| Documentation only changes|
|style| Changes that do not affect the meaning of the code (white-space, formatting, missing semi-colons, etc) |
|refactor| A code change that neither fixes a bug nor adds a feature |
|perf| A code change that improves performance |
|test| Adding missing tests or correcting existing tests |
|build| Changes that affect the build system or external dependencies (example scopes: gulp, broccoli, npm) |
|ci| 'Changes to our CI configuration files and scripts (example scopes: Travis, Circle, BrowserStack, SauceLabs) |
|chore| Other changes that don't modify src or test files |
|revert| Reverts a previous commit |

### Coding Guidelines
- Follow C# coding conventions
- Use meaningful names for methods and variables
- Add XML comments for public APIs
- Include unit tests for new features
- Update documentation as needed

## 📝 License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.
