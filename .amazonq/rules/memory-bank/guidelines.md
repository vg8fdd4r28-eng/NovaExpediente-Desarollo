# Development Guidelines - NovaExpediente

## Code Quality Standards

### Naming Conventions

#### C# Naming Standards
- **Classes**: PascalCase (e.g., `CurriculumService`, `EnviaSolicitudMobileCommand`)
- **Interfaces**: PascalCase with 'I' prefix (e.g., `INovaExpedienteDbContext`, `ICurriculumService`, `INotificationService`)
- **Methods**: PascalCase (e.g., `GenerarCV`, `Handle`, `ConsultaConversaciones`)
- **Private Fields**: camelCase with underscore prefix (e.g., `_context`, `_configuration`, `_mapper`)
- **Properties**: PascalCase (e.g., `IdColaborador`, `FechaInicio`, `NombreProyecto`)
- **Local Variables**: camelCase (e.g., `result`, `tipoSolicitud`, `esValido`)
- **Constants**: UPPERCASE with underscores (e.g., `LLAVE_RAIZ`)
- **Enums**: PascalCase for type and values (e.g., `Plantilla.NovacompEspanol`)

#### Database Naming Standards
- **Database Fields**: UPPERCASE (e.g., `IDCOLABORADOR`, `NOMBRECLIENTE`, `FECHAINICIO`)
- **Table Names**: PascalCase in code, mapped to database names
- **Stored Procedures**: PascalCase method names (e.g., `ConsultaCriterios`, `PersistirSolicitud`)

### File Organization

#### Project Structure
- **Commands**: Organized by feature in `Commands/` folders
  - Each command in its own subfolder (e.g., `EnviaSolicitudMobile/EnviaSolicitudMobileCommand.cs`)
  - Handler nested as inner class or separate file
  - Validator in same folder if needed

- **Queries**: Organized by feature in `Queries/` folders
  - Each query in its own subfolder (e.g., `GetProyectoClienteValido/`)
  - Query, Handler, ViewModel, and DTO in same folder

- **Services**: Organized by domain in Infrastructure layer
  - One service per file (e.g., `CurriculumService.cs`, `InformeService.cs`)
  - Interface defined in Application layer

### Code Formatting

#### Indentation and Spacing
- Use 4 spaces for indentation (not tabs)
- Opening braces on same line for methods and classes
- Single blank line between methods
- No blank lines at start or end of code blocks

#### Using Statements
- Organize using statements at top of file
- Group by namespace (System, Microsoft, third-party, project)
- Remove unused using statements
- Example order:
```csharp
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using MediatR;
using NovaExpediente.Application.Common.Interfaces;
using NovaExpediente.Domain.Entities;
```

#### Comments
- Use Spanish for business logic comments
- Minimal inline comments - prefer self-documenting code
- XML documentation comments for public APIs (optional)
- Comment complex business rules and algorithms

## Architectural Patterns

### CQRS Implementation

#### Command Pattern
Commands modify state and return simple types (int, bool, void):

```csharp
public class EnviaSolicitudMobileCommand : IRequest<int>
{
    // Command properties (input data)
    public int idColaborador { get; set; }
    public string tipoSolicitud { get; set; }
    public DateTime fechaInicio { get; set; }
    
    // Nested handler class
    public class UpsertCategoryCommandHandler : IRequestHandler<EnviaSolicitudMobileCommand, int>
    {
        private readonly INovaExpedienteDbContext _context;
        private readonly IMapper _mapper;
        
        public UpsertCategoryCommandHandler(INovaExpedienteDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        
        public async Task<int> Handle(EnviaSolicitudMobileCommand request, CancellationToken cancellationToken)
        {
            // Implementation
            return result;
        }
    }
}
```

#### Query Pattern
Queries return data without modifying state:

```csharp
public class GetProyectoClienteValidoQueryHandler : IRequestHandler<GetProyectoClienteValidoQuery, ProyectoClienteValidoVm>
{
    private readonly INovaExpedienteDbContext _context;
    
    public GetProyectoClienteValidoQueryHandler(INovaExpedienteDbContext context)
    {
        _context = context;
    }
    
    public async Task<ProyectoClienteValidoVm> Handle(GetProyectoClienteValidoQuery request, CancellationToken cancellationToken)
    {
        var proyectos = await _context.ExperienciaProyectos
            .Where(e => e.IDCOLABORADOR == request.IdColaborador)
            .Select(e => new ProyectoClienteValidoDto { /* ... */ })
            .ToListAsync(cancellationToken);
            
        return new ProyectoClienteValidoVm { /* ... */ };
    }
}
```

### Dependency Injection

#### Constructor Injection
Always use constructor injection for dependencies:

```csharp
public class CurriculumService : ICurriculumService
{
    private readonly IConfiguration Configuration;
    private readonly IWebHostEnvironment _Environment;
    
    public CurriculumService(IConfiguration configuration, IWebHostEnvironment environment)
    {
        Configuration = configuration;
        _Environment = environment;
    }
}
```

#### Common Dependencies
- `INovaExpedienteDbContext`: Database access
- `IMapper`: AutoMapper for object mapping
- `IConfiguration`: Application configuration
- `INotificationService`: Email notifications
- `INominaService`: Payroll integration
- `ISecurityService`: Security operations
- `IFireForgetCommandHandler`: Background tasks

### Service Layer Pattern

#### Service Implementation
Services implement interfaces defined in Application layer:

```csharp
public class InformeService : IInformeService
{
    private readonly IConfiguration _configuration;
    
    public InformeService(IConfiguration configuration) => _configuration = configuration;
    
    private HttpClient InitHttpClient()
    {
        var baseAddress = _configuration
            ?.GetSection("InformesIA")
            ?.GetSection("BaseUrlApiConversacion")
            ?.Value;
            
        var client = new HttpClient();
        client.BaseAddress = new Uri(baseAddress);
        return client;
    }
    
    public async Task<string> ConsultaConversaciones(string userId, int currentPage)
    {
        // Implementation
    }
}
```

## Data Access Patterns

### Entity Framework Core Usage

#### Async Operations
Always use async methods with CancellationToken:

```csharp
var proyectos = await _context.ExperienciaProyectos
    .Where(e => e.IDCOLABORADOR == request.IdColaborador)
    .ToListAsync(cancellationToken);
```

#### Projection with Select
Use Select to project only needed fields:

```csharp
.Select(e => new ProyectoClienteValidoDto
{
    IdExperienciaProyecto = e.IDEXPERIENCIAPROYECTO,
    FechaInicio = e.FECHAINICIO.Value,
    FechaFin = e.FECHAFINAL
})
```

#### AutoMapper Integration
Use ProjectTo for efficient queries:

```csharp
var colaborador = await _context.Colaboradores
    .Where(c => c.IDCOLABORADOR == request.idColaborador)
    .ProjectTo<ColaboradorVacacionesDto>(_mapper.ConfigurationProvider)
    .ToListAsync(cancellationToken);
```

#### Null Handling
Check for null and empty strings in queries:

```csharp
.Where(e => !string.IsNullOrEmpty(request.NombreCliente) &&
            (e.NOMBRECLIENTE.Trim() == request.NombreCliente.Trim() ||
             e.Cliente.NOMBRE.Trim() == request.NombreCliente.Trim()))
```

### Stored Procedures
Use stored procedures for complex operations:
- Call via DbContext custom methods
- Return results as entities or DTOs
- Handle transactions in stored procedures

## Error Handling

### Exception Handling Pattern

#### Try-Catch-Finally
Use try-catch-finally for resource cleanup:

```csharp
try
{
    // Main logic
    if (!ValidarParametros(JsonCV, TipoCV))
    {
        result["Resultado"] = -1;
        result["Mensaje"] = "Error message";
        return result;
    }
    // Process
}
catch (Exception ex)
{
    result["Resultado"] = -1;
    result["Mensaje"] = "Error:" + ": " + ex.ToString();
}
finally
{
    // Cleanup
    if ((int)result["Resultado"] == 1 && listaDocumentos != null)
    {
        result["Archivo"] = EmpacarDocumentos(listaDocumentos);
        EliminarDocumentos(listaDocumentos);
    }
}
```

#### Validation Before Processing
Validate inputs early and return/throw:

```csharp
if (JsonCV.Trim().Equals(string.Empty))
{
    return false;
}

if (!Enum.IsDefined(typeof(Plantilla), TipoCV))
{
    return false;
}
```

#### Custom Error Messages
Provide meaningful error messages in Spanish:

```csharp
throw new Exception("No existe el tipo de solicitud");
```

### HTTP Error Handling
Handle HTTP errors gracefully:

```csharp
result = response.IsSuccessStatusCode
    ? await response.Content.ReadAsStringAsync()
    : await HandleErrorResponse(response);

private static async Task<string> HandleErrorResponse(HttpResponseMessage response)
{
    var errorMessage = "An error occurred while processing the request.";
    try
    {
        var errorContent = await response.Content.ReadAsStringAsync();
        if (!string.IsNullOrEmpty(errorContent))
        {
            var errorObject = JsonConvert.DeserializeObject<dynamic>(errorContent);
            errorMessage = errorObject?.message ?? errorMessage;
        }
    }
    catch
    {
        // Log error or handle parsing exception
    }
    return $"Error: {errorMessage}";
}
```

## Configuration Management

### Reading Configuration
Use IConfiguration with null-conditional operators:

```csharp
var baseAddress = _configuration
    ?.GetSection("InformesIA")
    ?.GetSection("BaseUrlApiConversacion")
    ?.Value;

var pageSize = _configuration.GetSection("InformesIA").GetValue<int>("pageSize");
```

### Configuration Sections
Organize configuration by feature:
- `GeneracionCV`: CV generation settings
- `Nomina`: Payroll integration
- `Cloudinary`: Image storage
- `OneSignal`: Push notifications
- `GeneracionCorreo`: Email templates
- `InformesIA`: Reports API

### String Formatting with Configuration
Use string.Format for dynamic paths:

```csharp
var formattedRutaApi = string.Format(
    rutaApi ?? throw new InvalidOperationException(), 
    userId, 
    currentPage, 
    pageSize
);
```

## Common Patterns

### Dictionary Usage
Use Dictionary for flexible return types:

```csharp
Dictionary<string, object> result = new Dictionary<string, object>() 
{ 
    {"Resultado", 1},
    {"Mensaje", ""},
    {"Archivo", null}
};
```

### Switch Statements
Use switch for type-based logic:

```csharp
switch (request.tipoSolicitud)
{
    case "Vacaciones":
        tipoSolicitudNombre = "Solicitud de vacaciones";
        break;
    case "Permisos":
        tipoSolicitudNombre = "Solicitud de permiso";
        break;
    default:
        break;
}
```

### Enum Usage
Define enums for fixed sets of values:

```csharp
enum Plantilla
{
    NovacompEspanol = 1,
    InnovativeEspanol = 2,
    InnovativeIngles = 3,
    ITQSEspanol = 4,
    ITQSIngles = 5,
    NovaShore = 6,
    NovaTestingEspanol = 7,
    NovaTestingIngles = 8
}
```

### File Operations

#### Path Combination
Use Path.Combine for cross-platform paths:

```csharp
rutaPlantilla = Path.Combine(
    _Environment.ContentRootPath, 
    baseAddress, 
    ObtenerNombreArchivoPlantilla((Plantilla)TipoCV, Configuration)
);
```

#### File Naming
Include timestamps and identifiers in generated files:

```csharp
string nuevoDocumento = Path.Combine(
    rutaDocumentos, 
    string.Format("CV_{0}_{1}_{2}.docx",
        datos["NombreProfesional"].ToString(),
        ((Plantilla)TipoCV).ToString(),
        DateTime.Now.ToString("ddMMyyyy"))
);
```

### JSON Handling

#### Serialization
Use Newtonsoft.Json for serialization:

```csharp
command.DocSolicitud = JsonConvert.SerializeObject(docJson, Formatting.Indented);

var jsonContent = JsonConvert.SerializeObject(conversacionRequest);
var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
```

#### Deserialization
Handle dynamic JSON objects:

```csharp
var errorObject = JsonConvert.DeserializeObject<dynamic>(errorContent);
errorMessage = errorObject?.message ?? errorMessage;
```

### Date Handling

#### Date Formatting
Format dates consistently:

```csharp
fechaInicio = request.fechaInicio.ToString("yyyy-MM-dd")
```

#### Nullable Date Handling
Handle nullable dates with HasValue:

```csharp
FechaInicio = e.FECHAINICIO.Value,
FechaFin = e.FECHAFINAL  // nullable

span1.Item2.HasValue ? span1.Item2.Value : DateTime.MaxValue
```

### String Operations

#### Trimming
Always trim strings when comparing:

```csharp
e.NOMBRECLIENTE.Trim() == request.NombreCliente.Trim()
```

#### Null/Empty Checks
Use string.IsNullOrEmpty:

```csharp
if (!string.IsNullOrEmpty(request.NombreCliente))
```

#### String Building
Use StringBuilder for concatenation in loops:

```csharp
StringBuilder outStringBuilder = new StringBuilder();
foreach (var item in items)
{
    outStringBuilder.Append(item);
}
text.Text = outStringBuilder.ToString();
```

## HTTP Client Patterns

### HttpClient Initialization
Create HttpClient with proper configuration:

```csharp
private HttpClient InitHttpClient()
{
    var handler = new HttpClientHandler();
    handler.ClientCertificateOptions = ClientCertificateOption.Manual;
    handler.ServerCertificateCustomValidationCallback = 
        (httpRequestMessage, cert, certChain, policyErrors) => true;
    
    var client = new HttpClient(handler);
    client.BaseAddress = new Uri(baseAddress);
    client.DefaultRequestHeaders.Accept.Clear();
    client.DefaultRequestHeaders.Accept.Add(
        new Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json")
    );
    
    return client;
}
```

### HTTP Requests
Use async methods for HTTP operations:

```csharp
// GET
var response = await client.GetAsync(formattedRutaApi);

// POST
var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
var response = await client.PostAsync(rutaApi, content);

// Read response
result = await response.Content.ReadAsStringAsync();
```

## Validation Patterns

### FluentValidation
Use FluentValidation for command validation:

```csharp
WorkflowCommandValidator workflowValidator = new WorkflowCommandValidator(_context);
var resultadoValidacionWorkFlow = workflowValidator.Validate(command);

if (!resultadoValidacionWorkFlow.IsValid)
{
    throw new Exception(resultadoValidacionWorkFlow.Errors[0].ToString());
}
```

### Manual Validation
Implement validation methods for complex logic:

```csharp
private bool ValidarParametros(string JsonCV, int TipoCV)
{
    if (JsonCV.Trim().Equals(string.Empty))
    {
        return false;
    }
    if (!Enum.IsDefined(typeof(Plantilla), TipoCV))
    {
        return false;
    }
    return true;
}
```

## Business Logic Patterns

### Workflow Pattern
Delegate to workflow handlers for complex processes:

```csharp
WorkflowCommandHandler workflow = new WorkflowCommandHandler(
    _context, 
    _fireForgetCommand, 
    _securityService, 
    _notificationService, 
    _nominaService, 
    _mapper
);

WorkflowCommand command = new WorkflowCommand()
{
    IdSolicitud = null,
    IdTipoSolicitud = tipoSolicitud.IDTIPOSOLICITUD,
    CodigoEstado = "RE",
    Accion = "Enviar",
    DocSolicitud = JsonConvert.SerializeObject(docJson, Formatting.Indented)
};

result = await workflow.Handle(command, cancellationToken);
```

### Private Helper Classes
Use private nested classes for internal DTOs:

```csharp
private class DocSolicitud
{
    public int idColaborador { get; set; }
    public string fechaInicio { get; set; }
    public decimal saldoVacaciones { get; set; }
}

private class ConversacionRequest
{
    public string userId { get; set; }
}
```

### Complex Business Logic
Extract complex logic into private methods:

```csharp
private bool AreSpansOverlapping(
    Tuple<DateTime, DateTime?> span1, 
    Tuple<DateTime, DateTime?> span2, 
    bool includeEndPoints)
{
    // Complex date overlap logic
    if (includeEndPoints)
    {
        return ((span1.Item1 <= span2.Item1 && span1.Item2 >= span2.Item1) ||
                (span1.Item1 <= span2.Item2 && span1.Item2 >= span2.Item2));
    }
    // More logic...
}
```

## Testing Patterns

### Integration Tests
- Test handlers directly with real DbContext
- Use TestBase for common setup
- Mock external services
- Use CancellationToken.None for tests

## Performance Considerations

### Async/Await
Always use async/await for I/O operations:
- Database queries
- HTTP requests
- File operations

### Query Optimization
- Use Select to project only needed fields
- Use Where before ToList
- Avoid N+1 queries with Include
- Use ProjectTo for AutoMapper projections

### Resource Management
- Use using statements for IDisposable
- Clean up temporary files
- Dispose HttpClient properly (or use IHttpClientFactory)

## Security Practices

### Input Validation
- Validate all user inputs
- Use FluentValidation for commands
- Check for null and empty strings
- Trim strings before comparison

### SQL Injection Prevention
- Use EF Core parameterized queries
- Never concatenate SQL strings
- Use stored procedures for complex queries

### Authentication & Authorization
- Check user permissions in handlers
- Use role-based authorization
- Validate user context from ICurrentUserService

## Documentation Standards

### Code Comments
- Comment complex business logic in Spanish
- Explain "why" not "what"
- Document assumptions and constraints
- Keep comments up to date

### API Documentation
- Use Swagger/OpenAPI for API documentation
- Document request/response models
- Include example requests
- Document error responses
