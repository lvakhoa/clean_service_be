# ✅ Development Guidelines Added Successfully!

## What Was Done

### 1. Created `appsettings.Development.json`
**Location:** `/CleanService/appsettings.Development.json`

This file is based on `appsettings.example.json` and includes placeholder values for all configuration settings needed for local development:

- **Database**: MySQL connection string (localhost)
- **Redis**: Local Redis configuration
- **OAuth/Clerk**: Authentication provider settings
- **Firebase**: Service account configuration
- **Payment Gateways**: PayOS, VnPay, ZaloPay (sandbox URLs)
- **Cloudinary**: Media storage configuration
- **Email**: Resend API for email services
- **Application URLs**: Frontend, backend, and app URLs
- **JWT Secret**: Secret key for token generation

**Important:** This file is already in `.gitignore` and won't be committed to version control.

### 2. Added Comprehensive Development Guidelines to README

Added a new **"Development Guidelines"** section covering:

#### Environment Setup
- Step-by-step configuration instructions
- Database setup (local MySQL or Docker)
- Redis setup (local or Docker)
- Detailed explanation of each configuration section

#### Code Style and Standards
- **Naming Conventions**: PascalCase, camelCase, UPPER_SNAKE_CASE
- **Entity Framework Guidelines**:
    - Always use specifications
    - Use async/await (never `.Result` or `.Wait()`)
    - Handle timestamps automatically
- **Exception Handling**: Use custom exceptions, let global handler catch them
- **Controller Best Practices**: Thin controllers, async actions, proper status codes
- **Background Jobs**: Logging, cancellation handling, scoped services

#### Git Workflow
- **Branch Naming**: `feat/`, `fix/`, `hotfix/`, `docs/`
- **Commit Messages**: `{type}: #{issue_id} {subject}`
- **Pull Request Guidelines**: Clear process from branch creation to review

#### Testing
- Unit test commands
- Manual API testing with Swagger
- Tools: Postman, Insomnia, cURL

#### Debugging
- Visual Studio/Rider breakpoints
- Log checking for background jobs

#### Common Issues and Solutions
- **DateTime always shows same value** ✅ Fixed with our previous work!
- **Exception not caught by handler** ✅ Fixed with our previous work!
- **Database connection failed**
- **Redis connection failed**

#### Best Practices
- **Performance**: Caching, pagination, indexes, AsNoTracking(), avoid N+1
- **Security**: No sensitive data in commits, input validation, HTTPS

#### Before Submitting PR Checklist
- [ ] Code compiles
- [ ] Tests pass
- [ ] Follows conventions
- [ ] Documentation updated
- [ ] No sensitive data
- [ ] Commit messages follow guidelines
- [ ] Clear PR description

### 3. Updated Table of Contents
Added link to the new Development Guidelines section in the README table of contents.

---

## Next Steps for Developers

### First-Time Setup

1. **Copy the configuration file:**
   ```bash
   cp CleanService/appsettings.example.json CleanService/appsettings.Development.json
   ```

2. **Update with your actual values:**
    - MySQL password
    - Clerk OAuth credentials
    - Firebase service account
    - Payment gateway credentials (or use sandbox)
    - Cloudinary URL
    - Resend API key
    - JWT secret key (minimum 32 characters)

3. **Start services:**
   ```bash
   # Option 1: Docker
   docker-compose up mysql redis

   # Option 2: Local services
   sudo service mysql start
   redis-server
   ```

4. **Run migrations:**
   ```bash
   dotnet ef database update
   ```

5. **Run the application:**
   ```bash
   dotnet run
   ```

6. **Access Swagger:**
   Open `http://localhost:5011/swagger`

---

## Key Guidelines to Remember

### ✅ DO's
- Use `async/await` for all async operations
- Use custom exceptions (BadRequestException, NotFoundException, etc.)
- Use specifications for database queries
- Use `DateTime.UtcNow` for timestamps
- Follow naming conventions
- Write clear commit messages
- Add tests for new features
- Update documentation

### ❌ DON'Ts
- Don't use `.Result` or `.Wait()` on async methods
- Don't use `ContinueWith` - use `await` instead
- Don't use `HasDefaultValue(DateTime.Now)` in EF config
- Don't commit sensitive data
- Don't access DbContext directly - use repositories
- Don't put business logic in controllers

---

## Files Summary

| File | Status | Purpose |
|------|--------|---------|
| `appsettings.Development.json` | ✅ Created | Local development configuration |
| `README.md` | ✅ Updated | Added comprehensive development guidelines |
| `.gitignore` | ✅ Already configured | Excludes appsettings.Development.json |

---

## Security Note 🔒

The `appsettings.Development.json` file contains placeholder values only. When you update it with real credentials:
- ✅ It will NOT be committed (already in .gitignore)
- ✅ Each developer maintains their own local copy
- ✅ Production uses environment variables or secure vaults

Happy coding! 🚀
