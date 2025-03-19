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
