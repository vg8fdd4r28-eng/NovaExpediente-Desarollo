# Technology Stack - NovaExpediente

## Backend Technologies

### Core Framework
- **.NET Standard 2.1**: Domain and Application layers
- **ASP.NET Core 5.0**: Web API (WebUI project)
- **C# 8.0+**: Primary programming language

### Database & ORM
- **SQL Server**: Primary database
- **Entity Framework Core 5.0.9**: ORM and data access
- **Stored Procedures**: Complex queries and business logic
- **EF Core Migrations**: Database schema versioning

### Architecture & Patterns
- **MediatR 9.0.0**: CQRS implementation and request handling
- **AutoMapper 10.1.1**: Object-to-object mapping
- **FluentValidation 10.3.1**: Input validation

### Authentication & Authorization
- **IdentityServer4**: OAuth2 + OpenID Connect
- **ASP.NET Core Identity**: User management
- **JWT Tokens**: API authentication
- **Role-based Authorization**: 10+ roles with permission policies

### External Services Integration
- **CloudinaryDotNet 1.19.0**: Image storage and management
- **RestSharp 108.0.1**: HTTP client for external APIs
- **OneSignal**: Push notifications (REST API)
- **SendGrid**: Email notifications (REST API)

### Data Processing
- **CsvHelper 27.1.1**: CSV file parsing and generation
- **Newtonsoft.Json 13.0.1**: JSON serialization
- **DocumentFormat.OpenXml**: Word document generation (CV templates)

### Logging & Monitoring
- **Microsoft.Extensions.Logging.Abstractions 5.0.0**: Logging abstraction
- **Custom logging service**: Database logging

### Testing
- **xUnit**: Test framework
- **Integration Tests**: Application layer testing

## Frontend Technologies

### Primary Frontend (WebUI-Vue)

#### Core Framework
- **Vue.js 2.6**: Progressive JavaScript framework
- **Vue CLI**: Build tooling and development server

#### UI Framework & Components
- **CoreUI for Vue.js**: Admin dashboard template
- **PrimeVue**: Rich UI component library
  - DataTable with advanced features
  - Calendar and date pickers
  - Dialog and modal components
  - Form controls and validation
  - Charts and visualizations

#### State Management & Routing
- **Vuex**: Centralized state management
- **Vue Router**: Client-side routing

#### HTTP & Authentication
- **Axios**: HTTP client for API calls
- **oidc-client**: OAuth2/OpenID Connect client

#### Build Tools
- **Webpack**: Module bundler (via Vue CLI)
- **Babel**: JavaScript transpiler
- **PostCSS**: CSS processing
- **ESLint**: Code linting

#### Testing
- **Jest**: JavaScript testing framework

### Alternative Frontend (WebUI-React)
- **React**: Component-based UI library
- **TypeScript**: Type-safe JavaScript
- **Create React App**: Build configuration

## Development Tools

### IDE & Version Control
- **Visual Studio 2022**: Primary IDE
- **Visual Studio Code**: Alternative editor
- **Azure DevOps**: Source control (Team Foundation Version Control)
- **Git**: Version control system

### Package Management
- **.NET CLI**: NuGet package management
- **npm**: Node.js package management
- **yarn**: Alternative package manager (Vue project)

### Database Tools
- **SQL Server Management Studio**: Database administration
- **EF Core CLI**: Migration management
- **SQL Server Integration Services (SSIS)**: ETL processes

## External Service Dependencies

### Cloud Services
- **Cloudinary**: Image hosting and transformation
  - Cloud name: novacomp
  - API-based upload and management

### Communication Services
- **SendGrid**: Transactional email service
  - Template-based emails
  - Dynamic content injection
  - 10+ email templates configured

- **OneSignal**: Push notification service
  - Mobile app notifications
  - Segmented messaging

### Integration APIs
- **SAP Business One (Payroll)**: 
  - Base URL: https://www.scgsb1c.com:4108/SCGP1API/odata/
  - OData API for vacation balance sync
  - Employee data synchronization

- **AWS Lambda Services**:
  - CV Generation API
  - Chat/Conversation API
  - Reports/IA API

### Identity Provider
- **IdentityServer4**: 
  - Centralized authentication
  - API resource protection
  - Client application management

## Configuration Management

### Application Settings
- **appsettings.json**: Base configuration
- **appsettings.Development.json**: Development overrides
- **Environment Variables**: Sensitive data (production)

### Key Configuration Sections
```json
{
  "ConnectionStrings": {
    "NovaExpedienteDatabase": "SQL Server connection"
  },
  "GeneracionCV": {
    "BaseUrlApiCV": "CV generation service URL",
    "RutaPlantillas": "Template directory",
    "Plantilla-*": "8 CV template files"
  },
  "Nomina": {
    "BaseUrlApiNomina": "Payroll API URL",
    "UserNameLogin": "API credentials",
    "CompanyDBLogin": "Database name"
  },
  "Cloudinary": {
    "CloudName": "Account name",
    "APIKey": "API key",
    "APISecret": "API secret"
  },
  "OneSignal": {
    "REST_API_KEY": "API key",
    "APP_ID": "Application ID"
  },
  "GeneracionCorreo": {
    "apiKey": "SendGrid API key",
    "IdPlantilla*": "Email template IDs"
  },
  "NovaExpedienteApiConfiguration": {
    "ApiBaseUrl": "API base URL",
    "IdentityServerBaseUrl": "Auth server URL",
    "OidcApiName": "API resource name"
  },
  "ConfiguracionPermisos": {
    "Role": ["Permission1", "Permission2"]
  }
}
```

## Development Commands

### Backend (.NET)

#### Build & Run
```bash
# Restore dependencies
dotnet restore

# Build solution
dotnet build

# Run API
dotnet run --project WebUI/WebUI.csproj

# Run with specific environment
dotnet run --project WebUI/WebUI.csproj --environment Development
```

#### Database Migrations
```bash
# Add new migration
dotnet ef migrations add MigrationName --project Persistence --startup-project WebUI

# Update database
dotnet ef database update --project Persistence --startup-project WebUI

# Generate SQL script
dotnet ef migrations script --project Persistence --startup-project WebUI
```

#### Testing
```bash
# Run all tests
dotnet test

# Run specific test project
dotnet test Tests/NovaExpediente.Application.Integration.Tests
```

### Frontend (Vue.js)

#### Setup
```bash
cd WebUI-Vue/clientapp

# Install dependencies
npm install
# or
yarn install
```

#### Development
```bash
# Run development server
npm run serve
# or
yarn serve

# Build for production
npm run build
# or
yarn build

# Run tests
npm run test
# or
yarn test

# Lint code
npm run lint
# or
yarn lint
```

## Environment Setup

### Prerequisites
1. **Visual Studio 2022** (or Visual Studio 2019)
   - ASP.NET and web development workload
   - .NET Core cross-platform development

2. **SQL Server 2019** (or SQL Server 2017+)
   - Developer or Express edition
   - SQL Server Management Studio

3. **Node.js 14+** and **npm 6+**
   - For Vue.js frontend development

4. **.NET 5.0 SDK**
   - Required for building and running the API

5. **Git** for version control

### Optional Tools
- **Postman** or **Swagger UI**: API testing
- **Azure Data Studio**: Cross-platform database tool
- **Docker**: Containerization (if needed)

## Deployment

### API Deployment
- **Target**: Azure App Service or IIS
- **Configuration**: Transform appsettings for environment
- **Database**: Run migrations on target database
- **Identity Server**: Configure redirect URIs

### Frontend Deployment
- **Build**: `npm run build` generates static files
- **Target**: Azure Static Web Apps, Azure Blob Storage, or CDN
- **Configuration**: Update API base URLs in environment files

### Database Deployment
- **Migrations**: Apply via EF Core CLI or SQL scripts
- **Stored Procedures**: Deploy via SSMS or Azure DevOps pipeline
- **Data**: Use SSIS packages for initial data load

## Performance Considerations

### Backend Optimizations
- **Stored Procedures**: Complex queries executed in database
- **Async/Await**: Non-blocking I/O operations
- **Response Compression**: Gzip compression enabled
- **Caching**: Consider adding Redis for frequently accessed data

### Frontend Optimizations
- **Lazy Loading**: Route-based code splitting
- **Component Optimization**: Vue.js reactivity optimizations
- **API Batching**: Combine related API calls
- **Image Optimization**: Cloudinary transformations

## Security Measures

### API Security
- **HTTPS**: Required in production
- **CORS**: Configured allowed origins
- **Authentication**: OAuth2 + OpenID Connect
- **Authorization**: Role and permission-based
- **Input Validation**: FluentValidation on all commands
- **SQL Injection**: Parameterized queries and EF Core
- **XSS Protection**: Output encoding

### Data Security
- **Audit Trail**: All changes tracked with user and timestamp
- **Soft Delete**: Preserve data integrity
- **Encryption**: Sensitive data encrypted at rest
- **Connection Strings**: Stored securely (Azure Key Vault in production)

## Monitoring & Logging

### Application Logging
- **Database Logging**: Custom logging service writes to database
- **Log Levels**: Configurable via appsettings.json
- **Structured Logging**: Consider adding Serilog

### Health Monitoring
- **Health Checks**: Endpoint for monitoring service health
- **Application Insights**: Consider adding for production monitoring
- **Database Monitoring**: SQL Server performance metrics

## Version Information

### Current Versions
- **.NET**: 5.0
- **Entity Framework Core**: 5.0.9
- **Vue.js**: 2.6
- **MediatR**: 9.0.0
- **AutoMapper**: 10.1.1
- **FluentValidation**: 10.3.1

### Upgrade Considerations
- **Vue 3**: Consider migration path for Vue.js 3.x
- **.NET 6/7/8**: Plan for .NET upgrade
- **EF Core**: Keep in sync with .NET version
