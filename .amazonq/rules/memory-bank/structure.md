# Project Structure - NovaExpediente

## Architecture Overview
NovaExpediente follows **Clean Architecture** principles with **CQRS (Command Query Responsibility Segregation)** pattern using **MediatR** for request handling.

## Solution Structure

```
NovaExpediente.sln
├── Core/                           # Business logic layer
│   ├── Domain/                     # Domain entities and business rules
│   ├── Application/                # Use cases (Commands/Queries)
│   └── Common/                     # Shared interfaces
├── Infrastructure/                 # External implementations
│   ├── Persistence/                # Data access (Entity Framework Core)
│   └── Infrastructure/             # External services
└── Presentation/                   # User interfaces
    ├── WebUI/                      # REST API (ASP.NET Core)
    ├── WebUI-Vue/                  # Vue.js frontend (primary)
    └── WebUI-React/                # React frontend (alternative)
```

## Layer Dependencies
- **Domain**: No dependencies (pure C# entities)
- **Application**: Depends on Domain, Common
- **Persistence**: Depends on Domain, Application
- **Infrastructure**: Depends on Domain, Application
- **WebUI**: Depends on all layers

## Core Layer

### Domain Project
**Purpose**: Contains business entities and domain logic

**Key Components**:
- **Entities/** (90+ domain entities):
  - `Colaborador.cs`: Employee master data
  - `Proyecto.cs`: Project information
  - `Certificacion.cs`, `Curso.cs`: Training and certifications
  - `Vacacion.cs`, `Incapacidad.cs`, `Permiso.cs`: Leave management
  - `SolicitudMasterData.cs`: Generic request workflow
  - `Noticia.cs`, `Convenio.cs`, `Sorteo.cs`: Mobile app content
  - `UnoAUno.cs`: One-on-one meetings
  - `CurriculumVitae.cs`: CV generation tracking
  - `ExperienciaProyecto.cs`: Project experience
  - `FormacionAcademica.cs`: Academic background
  - `CertificacionColaborador.cs`: Employee certifications
  - `IdiomaColaborador.cs`: Language skills
  - `HabilidadColaborador.cs`: Soft skills
  - Technical experience entities (languages, databases, clouds, etc.)

- **Common/**:
  - `AuditableEntity.cs`: Base class with audit fields (CreatedBy, Created, LastModifiedBy, LastModified)
  - `ValueObject.cs`: Base for immutable value objects

**Technology**: .NET Standard 2.1, no external dependencies

### Application Project
**Purpose**: Contains application logic and use cases

**Structure** (80+ feature modules):
```
Application/
├── Colaboradores/                  # Employee management
│   ├── Commands/                   # Create, Update, Delete
│   └── Queries/                    # Get, GetList, Search
├── CurriculumVitae/                # CV generation
├── Vacaciones/                     # Vacation management
├── Incapacidades/                  # Sick leave
├── Permisos/                       # Permits
├── MasterData/                     # Generic request workflow
├── Certificaciones/                # Certifications
├── Proyectos/                      # Projects
├── AplicacionMovil/                # Mobile app content
│   ├── Noticias/                   # News
│   ├── Convenios/                  # Benefits
│   ├── Sorteos/                    # Raffles
│   └── Eventos/                    # Events
├── UnosAUnos/                      # 1:1 meetings
├── Nomina/                         # Payroll integration
├── Common/
│   ├── Behaviours/                 # MediatR pipeline behaviors
│   ├── Interfaces/                 # INovaExpedienteDbContext, services
│   ├── Mappings/                   # AutoMapper profiles
│   └── Models/                     # DTOs and ViewModels
└── DependencyInjection.cs          # Service registration
```

**CQRS Pattern**:
- **Commands**: Modify state (CreateXCommand, UpdateXCommand, DeleteXCommand)
- **Queries**: Read-only (GetXQuery, GetXListQuery)
- **Handlers**: One handler per command/query
- **Validators**: FluentValidation for input validation

**Key Technologies**:
- MediatR 9.0
- AutoMapper 10.1
- FluentValidation 10.3
- Entity Framework Core 5.0

### Common Project
**Purpose**: Shared interfaces and abstractions

**Contents**:
- `IDateTime.cs`: Abstraction for current date/time (testable)

## Infrastructure Layer

### Persistence Project
**Purpose**: Data access implementation using Entity Framework Core

**Key Components**:
- `NovaExpedienteDbContext.cs`: Main EF Core context
  - 100+ DbSet properties (one per entity)
  - Custom methods for stored procedures:
    - `ConsultaCriterios()`: Advanced employee search
    - `ConsultaCV()`: CV data retrieval
    - `PersistirSolicitud()`: Execute request workflow SPs
    - `CargarVacaciones()`, `CargarIncapacidad()`: Bulk imports
    - Mobile app data methods
  - Automatic audit field population in SaveChangesAsync
  - Soft delete support

- **Configurations/**: Fluent API entity configurations
  - `ColaboradorConfiguration.cs`
  - `FormacionAcademicaConfiguration.cs`
  - `GradoAcademicoConfiguration.cs`
  - `ClasificadorConfiguration.cs`

- **Migrations/**: EF Core migrations
  - Initial migration: 20200929165541_CreacionInicial
  - Subsequent schema changes

**Database**: SQL Server with extensive use of stored procedures

### Infrastructure Project
**Purpose**: External service implementations

**Services**:
- **Identity/**:
  - `ApplicationDbContext.cs`: ASP.NET Core Identity context
  - `UserManagerService.cs`: User management
  - `ApplicationUser.cs`: Custom user entity

- **CloudService/**:
  - `CloudinaryService.cs`: Image upload/management

- **Curriculum/**:
  - `CurriculumService.cs`: CV generation (Word documents)
  - `CurriculumService2.cs`: Alternative CV generation

- **Nomina/**:
  - `NominaService.cs`: Payroll system integration (SAP B1)

- **OneSignal/**:
  - `OneSignalService.cs`: Push notifications

- **Security/**:
  - `SecurityService.cs`: Encryption and security utilities

- **Informes/**:
  - `InformeService.cs`: Report generation

- `NotificationService.cs`: Email notifications (SendGrid)

## Presentation Layer

### WebUI Project (REST API)
**Purpose**: ASP.NET Core Web API

**Structure**:
```
WebUI/
├── Controllers/                    # 90+ API controllers
│   ├── ColaboradorController.cs
│   ├── VacacionesController.cs
│   ├── CurriculumVitaeController.cs
│   ├── MasterDataController.cs
│   ├── NoticiaController.cs
│   └── ...
├── Configuration/
│   ├── Authorization/              # Policy-based authorization
│   └── NovaExpedienteApiConfiguration.cs
├── Services/
│   ├── CurrentUserService.cs       # Current user context
│   └── RolesPermissions.cs         # Role-permission mapping
├── Templates/                      # CV Word templates (8 files)
├── Common/
│   └── CustomExceptionHandlerMiddleware.cs
├── Startup.cs                      # Service configuration
├── Program.cs                      # Application entry point
└── appsettings.json                # Configuration
```

**Features**:
- OAuth2 + OpenID Connect authentication
- Role-based authorization (10+ roles)
- Swagger/OpenAPI documentation
- CORS support for multiple origins
- Response compression (Gzip)
- Health checks endpoint
- Custom exception handling middleware

**Roles**:
- Expediente.Admin
- Expediente.SuperUsuario
- Expediente.RRHH
- Expediente.DM (Delivery Manager)
- Expediente.BDM (Business Development Manager)
- Expediente.Colaborador
- Expediente.Financiero
- Expediente.Licitaciones
- Expediente.AdminRH
- Expediente.AdminAPP
- Expediente.AdminPayroll
- Expediente.Reportes

### WebUI-Vue Project (Primary Frontend)
**Purpose**: Vue.js 2 SPA with CoreUI template

**Structure**:
```
WebUI-Vue/
└── clientapp/
    ├── src/
    │   ├── views/                  # Page components
    │   ├── components/             # Reusable components
    │   ├── store/                  # Vuex state management
    │   ├── router/                 # Vue Router configuration
    │   └── services/               # API client services
    ├── public/                     # Static assets
    └── package.json                # Dependencies
```

**Technology Stack**:
- Vue.js 2.6
- CoreUI (admin template)
- PrimeVue (UI components)
- Vuex (state management)
- Vue Router (navigation)
- Axios (HTTP client)
- OIDC Client (authentication)

**Features**:
- Employee management interface
- CV generation wizard
- Dashboard with metrics
- Vacation calendar and requests
- Request workflow management
- Mobile app content management
- Reporting and exports
- Internal chat system

### WebUI-React Project
**Purpose**: Alternative React implementation (less developed)

## Testing

### NovaExpediente.Application.Integration.Tests
**Purpose**: Integration tests for application layer

**Structure**:
```
Tests/NovaExpediente.Application.Integration.Tests/
├── Colaboradores/                  # Employee tests
├── CertificacionesColaborador/     # Certification tests
├── EspecialidadConsultor/          # Specialty tests
├── ExperienciasAmbientesDesarrollo/
├── ExperienciasTecnologias/
├── GradosAcademicos/
├── IdiomasColaborador/
├── TestBase.cs                     # Base test class
└── Testing.cs                      # Test helpers
```

## Architectural Patterns

### Clean Architecture Benefits
- **Independence**: Business logic independent of frameworks and UI
- **Testability**: Core logic can be tested without external dependencies
- **Flexibility**: Easy to swap implementations (e.g., database, UI framework)
- **Maintainability**: Clear separation of concerns

### CQRS Benefits
- **Separation**: Read and write operations separated
- **Optimization**: Queries optimized differently from commands
- **Scalability**: Can scale read and write sides independently
- **Clarity**: Clear intent of each operation

### Key Design Decisions
1. **MediatR**: Decouples request handling from controllers
2. **AutoMapper**: Separates domain entities from DTOs
3. **FluentValidation**: Declarative validation rules
4. **Repository Pattern**: Abstracted through DbContext interface
5. **Audit Trail**: Automatic tracking via AuditableEntity base class
6. **Stored Procedures**: Complex queries and operations in database
7. **Multi-tenancy**: Support for multiple companies in corporation
