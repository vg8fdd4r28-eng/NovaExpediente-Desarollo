# Explicación Completa del Repositorio NovaExpediente

## 📋 Descripción General
**NovaExpediente** es un sistema de gestión de expedientes digitales para NOVACOMP, una empresa de servicios tecnológicos. El sistema permite administrar información de colaboradores, proyectos, certificaciones, vacaciones, nómina, y más.

---

## 🏗️ Arquitectura del Proyecto

El proyecto sigue la **Arquitectura Limpia (Clean Architecture)** con el patrón **CQRS (Command Query Responsibility Segregation)** usando **MediatR**.

### Estructura de Capas:

```
NovaExpediente/
├── Core/                    # Lógica de negocio central
│   ├── Domain/             # Entidades del dominio
│   ├── Application/        # Casos de uso (Commands/Queries)
│   └── Common/             # Interfaces compartidas
├── Infrastructure/          # Implementaciones externas
│   ├── Persistence/        # Acceso a datos (Entity Framework)
│   └── Infrastructure/     # Servicios externos (Email, Cloud, etc.)
└── Presentation/           # Interfaces de usuario
    ├── WebUI/             # API REST (ASP.NET Core)
    ├── WebUI-Vue/         # Frontend Vue.js
    └── WebUI-React/       # Frontend React (alternativo)
```

---

## 📁 Descripción de Proyectos

### 1. **Domain** (Capa de Dominio)
**Propósito**: Define las entidades del negocio y reglas de dominio.

**Contenido Principal**:
- **Entities/**: Más de 90 entidades del negocio
  - `Colaborador.cs`: Información de empleados (datos personales, laborales, vacaciones)
  - `Proyecto.cs`: Proyectos de la empresa
  - `Certificacion.cs`, `Curso.cs`: Formación y certificaciones
  - `Vacacion.cs`, `Incapacidad.cs`, `Permiso.cs`: Gestión de ausencias
  - `Noticia.cs`, `Convenio.cs`, `Sorteo.cs`: Contenido para app móvil
  - `UnoAUno.cs`: Reuniones 1 a 1 entre colaboradores y managers
  - `SolicitudMasterData.cs`: Sistema de solicitudes y aprobaciones

- **Common/**: Clases base
  - `AuditableEntity.cs`: Campos de auditoría (usuario creación/modificación, fechas)
  - `ValueObject.cs`: Objetos de valor inmutables

**Características**:
- Sin dependencias externas (puro C#)
- Entidades con relaciones (Foreign Keys)
- Herencia de AuditableEntity para tracking de cambios

---

### 2. **Application** (Capa de Aplicación)
**Propósito**: Contiene la lógica de aplicación y casos de uso.

**Estructura por Módulo** (más de 80 módulos):
```
Application/
├── Colaboradores/
│   ├── Commands/          # Crear, actualizar, eliminar colaboradores
│   └── Queries/           # Consultar colaboradores, CVs, reportes
├── Vacaciones/
│   ├── Commands/          # Solicitar, aprobar vacaciones
│   └── Queries/           # Consultar saldo, historial
├── MasterData/            # Sistema de solicitudes genéricas
├── Noticias/              # Gestión de noticias para app móvil
├── Common/
│   ├── Behaviours/        # Pipeline behaviors (validación, performance)
│   ├── Interfaces/        # INovaExpedienteDbContext, servicios
│   └── Mappings/          # AutoMapper profiles
└── DependencyInjection.cs # Configuración de servicios
```

**Patrón CQRS**:
- **Commands**: Operaciones que modifican estado (Create, Update, Delete)
- **Queries**: Operaciones de solo lectura (Get, GetList)
- **MediatR**: Mediador para desacoplar handlers

**Ejemplo de Módulos**:
- **Colaboradores**: CRUD de empleados, consultas avanzadas, generación de CVs
- **CurriculumVitae**: Generación de CVs en múltiples formatos (Word)
- **Vacaciones**: Solicitud, aprobación, cálculo de saldos
- **Incapacidades**: Gestión de incapacidades médicas
- **Certificaciones**: Tracking de certificaciones profesionales
- **Proyectos**: Gestión de proyectos y asignación de colaboradores
- **AplicacionMovil**: Endpoints para app móvil (noticias, convenios, sorteos)

---

### 3. **Persistence** (Capa de Persistencia)
**Propósito**: Implementa el acceso a datos usando Entity Framework Core.

**Archivos Principales**:
- `NovaExpedienteDbContext.cs`: Contexto principal de EF Core
  - 100+ DbSets (tablas)
  - Métodos personalizados para stored procedures
  - Auditoría automática en SaveChangesAsync
  - Métodos especiales:
    - `ConsultaCriterios()`: Búsqueda avanzada de colaboradores
    - `ConsultaCV()`: Generación de JSON para CVs
    - `PersistirSolicitud()`: Ejecuta SP según tipo de solicitud
    - `CargarVacaciones()`, `CargarIncapacidad()`: Carga masiva desde Excel
    - Métodos para app móvil (noticias, convenios, actividades)

- **Configurations/**: Configuraciones de entidades (Fluent API)
- **Migrations/**: Migraciones de base de datos
- `DependencyInjection.cs`: Registro de servicios de persistencia

**Características**:
- SQL Server como base de datos
- Uso extensivo de stored procedures
- Auditoría automática (usuario y fecha de creación/modificación)
- Soporte para operaciones complejas (reportes, dashboards)

---

### 4. **Infrastructure** (Capa de Infraestructura)
**Propósito**: Implementa servicios externos y de infraestructura.

**Servicios Implementados**:
- **Identity/**: Autenticación y autorización
  - `ApplicationDbContext.cs`: Contexto de Identity
  - `UserManagerService.cs`: Gestión de usuarios
  
- **CloudService/**: Integración con Cloudinary (almacenamiento de imágenes)
- **Curriculum/**: Generación de CVs en Word (múltiples plantillas)
- **Nomina/**: Integración con sistema de nómina externo
- **OneSignal/**: Notificaciones push para app móvil
- **Security/**: Servicios de seguridad y encriptación
- **NotificationService.cs**: Envío de notificaciones

---

### 5. **WebUI** (API REST)
**Propósito**: API REST construida con ASP.NET Core.

**Características**:
- **Autenticación**: OAuth2 + OpenID Connect (IdentityServer)
- **Autorización**: Políticas basadas en roles y permisos
- **Swagger**: Documentación interactiva de API
- **CORS**: Configurado para múltiples orígenes
- **Compresión**: Gzip para respuestas JSON
- **Health Checks**: Endpoint /health para monitoreo

**Controllers** (más de 90):
```
Controllers/
├── ColaboradorController.cs       # CRUD de colaboradores
├── VacacionesController.cs        # Gestión de vacaciones
├── CurriculumVitaeController.cs   # Generación de CVs
├── MasterDataController.cs        # Sistema de solicitudes
├── NoticiaController.cs           # Noticias para app móvil
├── ConvenioController.cs          # Convenios empresariales
├── SorteoController.cs            # Sorteos para colaboradores
├── UnoAUnoController.cs           # Reuniones 1 a 1
├── ReportesController.cs          # Reportes y dashboards
└── ...
```

**Configuración**:
- `Startup.cs`: Configuración de servicios y middleware
- `appsettings.json`: Configuración de conexiones, servicios externos
- **Templates/**: Plantillas de Word para CVs (8 plantillas diferentes)

---

### 6. **WebUI-Vue** (Frontend Principal)
**Propósito**: Aplicación web SPA construida con Vue.js 2.

**Stack Tecnológico**:
- **Vue.js 2.6**: Framework principal
- **CoreUI**: Template de administración
- **PrimeVue**: Componentes UI (tablas, formularios, calendarios)
- **Vuex**: Gestión de estado
- **Vue Router**: Navegación
- **Axios**: Cliente HTTP
- **OIDC Client**: Autenticación OAuth2

**Características**:
- **Gestión de Colaboradores**: CRUD completo, búsqueda avanzada
- **Generación de CVs**: Interfaz para crear CVs personalizados
- **Dashboard**: Métricas y KPIs de RRHH
- **Gestión de Vacaciones**: Solicitud, aprobación, calendario
- **Sistema de Solicitudes**: Workflow de aprobaciones
- **Reportes**: Exportación a Excel, gráficos
- **Chat**: Sistema de mensajería interna
- **Gestión de Contenido**: Noticias, convenios, sorteos para app móvil

**Estructura**:
```
clientapp/
├── src/
│   ├── views/           # Páginas/vistas
│   ├── components/      # Componentes reutilizables
│   ├── store/          # Vuex store
│   ├── router/         # Configuración de rutas
│   └── services/       # Servicios API
├── public/             # Archivos estáticos
└── package.json        # Dependencias
```

---

### 7. **WebUI-React** (Frontend Alternativo)
**Propósito**: Implementación alternativa en React (menos desarrollado).

---

### 8. **Common**
**Propósito**: Interfaces y tipos compartidos entre capas.

**Contenido**:
- `IDateTime.cs`: Abstracción para obtener fecha/hora actual (testeable)

---

### 9. **Tests**
**Propósito**: Pruebas de integración.

**Contenido**:
- `NovaExpediente.Application.Integration.Tests/`: Tests de casos de uso
- `TestBase.cs`: Clase base para tests
- `Testing.cs`: Helpers para testing

---

## 🔑 Funcionalidades Principales

### 1. **Gestión de Colaboradores**
- CRUD completo de empleados
- Información personal, laboral, contacto
- Foto de perfil
- Asignación de Delivery Manager
- Tracking de estado (activo/inactivo)

### 2. **Expediente Digital**
- Formación académica (grados, centros de estudio)
- Certificaciones profesionales (con fechas de vencimiento)
- Cursos realizados e impartidos
- Idiomas (con niveles de dominio)
- Habilidades blandas
- Experiencia técnica:
  - Lenguajes de programación
  - Bases de datos
  - Tecnologías y frameworks
  - Herramientas de calidad
  - Infraestructura
  - Cloud (AWS, Azure, GCP)
  - ERP, BPM, Bus de servicios
- Experiencia en proyectos (con contactos de referencia)

### 3. **Generación de CVs**
- 8 plantillas diferentes en Word
- Personalización por empresa (Novacomp, Innovative, ITQS, NovaShore, NovaTesting)
- Versiones en español e inglés
- Generación automática desde datos del expediente

### 4. **Gestión de Vacaciones**
- Solicitud de vacaciones
- Workflow de aprobación (colaborador → DM → RRHH)
- Cálculo automático de saldos
- Validación de días disponibles
- Consideración de feriados
- Integración con nómina
- Reportes y bitácoras

### 5. **Gestión de Incapacidades**
- Registro de incapacidades médicas
- Carga masiva desde Excel
- Tracking de días de incapacidad
- Reportes

### 6. **Sistema de Solicitudes (MasterData)**
- Framework genérico para cualquier tipo de solicitud
- Workflow configurable por tipo de solicitud
- Estados y transiciones
- Comentarios y tracking
- Notificaciones
- Persistencia automática mediante stored procedures

### 7. **Aplicación Móvil (Backend)**
- **Noticias**: Publicación de noticias corporativas (con categorías, imágenes)
- **Convenios**: Descuentos y beneficios para colaboradores
- **Sorteos**: Rifas y sorteos internos
- **Actividades**: Calendario de eventos corporativos
- **Denuncias**: Canal de denuncias anónimas
- **Datos Personales**: Actualización de información personal
- **Vacunación**: Registro de vacunas COVID-19

### 8. **Reuniones 1 a 1**
- Programación de reuniones entre colaborador y manager
- Categorías de temas
- Estados de seguimiento
- Detalles y notas de reuniones

### 9. **Reportes y Dashboards**
- Dashboard de actualización de expedientes (semáforo: verde/amarillo/rojo)
- Dashboard de actualización de CVs
- Uso de la aplicación (estadísticas)
- Reporte de certificaciones
- Reporte de cursos
- Reporte de formación académica
- Exportación a Excel

### 10. **Búsqueda Avanzada**
- Búsqueda de colaboradores por múltiples criterios
- Ranking de colaboradores según criterios
- Consultas complejas mediante stored procedures

---

## 🔐 Seguridad

### Autenticación
- **OAuth2 + OpenID Connect** con IdentityServer
- Tokens JWT para API
- Refresh tokens
- Single Sign-On (SSO)

### Autorización
- Roles de usuario (Admin, RRHH, Delivery Manager, Colaborador)
- Políticas de autorización
- Permisos granulares por funcionalidad
- Validación en API y frontend

### Auditoría
- Tracking automático de creación/modificación
- Usuario y fecha en todas las entidades
- Bitácora de acciones (visitas, operaciones)
- Logging de errores

---

## 🗄️ Base de Datos

### Tecnología
- **SQL Server**
- **Entity Framework Core** (Code First)
- **Migraciones** para versionado de esquema

### Esquema
- **Schema**: `NE` (NovaExpediente)
- **100+ tablas**
- **Stored Procedures** para operaciones complejas
- **Funciones** para cálculos
- **Vistas** para reportes

### Tablas Principales
- `NE.COLABORADOR`: Datos de empleados
- `NE.FORMACIONACADEMICA`: Educación
- `NE.CERTIFICACIONCOLABORADOR`: Certificaciones
- `NE.EXPERIENCIAPROYECTO`: Experiencia laboral
- `NE.VACACION`: Vacaciones
- `NE.INCAPACIDAD`: Incapacidades
- `NE.SOLICITUDMASTERDATA`: Solicitudes genéricas
- `NE.NOTICIA`, `NE.CONVENIO`, `NE.SORTEO`: Contenido app móvil
- `NE.UNOAUNO`: Reuniones 1 a 1

---

## 🛠️ Tecnologías Utilizadas

### Backend
- **ASP.NET Core 3.1+**: Framework web
- **C# 8.0+**: Lenguaje
- **Entity Framework Core**: ORM
- **MediatR**: Patrón mediador (CQRS)
- **AutoMapper**: Mapeo de objetos
- **FluentValidation**: Validación de modelos
- **IdentityServer4**: Autenticación OAuth2
- **Swagger/OpenAPI**: Documentación de API
- **Cloudinary**: Almacenamiento de imágenes
- **OneSignal**: Notificaciones push

### Frontend (Vue)
- **Vue.js 2.6**: Framework SPA
- **Vuex**: Gestión de estado
- **Vue Router**: Enrutamiento
- **CoreUI**: Template de administración
- **PrimeVue**: Componentes UI
- **Axios**: Cliente HTTP
- **OIDC Client**: Autenticación
- **Chart.js**: Gráficos
- **Moment.js**: Manejo de fechas
- **XLSX**: Exportación a Excel

### Base de Datos
- **SQL Server**: Base de datos relacional

### DevOps
- **Azure DevOps**: Control de versiones (TFS)
- **Git**: Control de versiones local

---

## 📊 Patrones de Diseño

### 1. **Clean Architecture**
- Separación de capas (Domain, Application, Infrastructure, Presentation)
- Dependencias hacia el centro (Domain no depende de nada)
- Inversión de dependencias (interfaces en Application)

### 2. **CQRS (Command Query Responsibility Segregation)**
- Separación de comandos (escritura) y queries (lectura)
- Handlers específicos para cada operación
- Optimización independiente de lectura/escritura

### 3. **Repository Pattern**
- Abstracción del acceso a datos
- DbContext como repositorio genérico
- Métodos personalizados para operaciones complejas

### 4. **Dependency Injection**
- Inyección de dependencias en todos los niveles
- Configuración en `DependencyInjection.cs` de cada capa
- Uso de interfaces para desacoplamiento

### 5. **Pipeline Behavior**
- Validación automática antes de ejecutar comandos
- Logging de performance
- Manejo centralizado de excepciones

---

## 🚀 Flujo de una Petición

### Ejemplo: Crear un Colaborador

1. **Frontend (Vue)**:
   ```javascript
   // Usuario llena formulario y hace clic en "Guardar"
   axios.post('/api/Colaborador/Create', colaboradorData)
   ```

2. **API (WebUI)**:
   ```csharp
   // ColaboradorController.cs
   [HttpPost]
   public async Task<ActionResult<int>> Create(CreateColaboradorCommand command)
   {
       return await Mediator.Send(command); // Envía comando a MediatR
   }
   ```

3. **Application Layer**:
   ```csharp
   // CreateColaboradorCommandHandler.cs
   public async Task<int> Handle(CreateColaboradorCommand request, ...)
   {
       // 1. Validación (FluentValidation - automática)
       // 2. Mapeo (AutoMapper)
       var colaborador = _mapper.Map<Colaborador>(request);
       
       // 3. Lógica de negocio
       _context.Colaboradores.Add(colaborador);
       await _context.SaveChangesAsync(); // Auditoría automática
       
       return colaborador.IDCOLABORADOR;
   }
   ```

4. **Persistence Layer**:
   ```csharp
   // NovaExpedienteDbContext.cs
   public override async Task<int> SaveChangesAsync(...)
   {
       // Auditoría automática
       foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
       {
           if (entry.State == EntityState.Added)
           {
               entry.Entity.IDUSUARIOCREA = _currentUserService.UserId;
               entry.Entity.FECHACREACION = _dateTime.Now;
           }
       }
       return await base.SaveChangesAsync();
   }
   ```

5. **Base de Datos**:
   - INSERT en tabla `NE.COLABORADOR`
   - Retorna ID generado

6. **Respuesta**:
   - API retorna ID del colaborador creado
   - Frontend muestra mensaje de éxito

---

## 📝 Convenciones de Código

### Nomenclatura
- **Entidades**: PascalCase (ej: `Colaborador`)
- **Propiedades**: MAYÚSCULAS (ej: `IDCOLABORADOR`, `NOMBRE`)
- **Comandos**: `{Verbo}{Entidad}Command` (ej: `CreateColaboradorCommand`)
- **Queries**: `Get{Entidad}{Criterio}Query` (ej: `GetColaboradorByIdQuery`)
- **Handlers**: `{Comando/Query}Handler`
- **Controllers**: `{Entidad}Controller`

### Organización
- Un archivo por clase
- Carpetas por módulo/entidad
- Separación Commands/Queries

---

## 🔧 Configuración y Despliegue

### Requisitos
- **.NET Core 3.1+** SDK
- **SQL Server 2016+**
- **Node.js 12+** (para frontend Vue)
- **Visual Studio 2019+** o **VS Code**

### Configuración
1. **Base de Datos**:
   - Crear base de datos en SQL Server
   - Actualizar connection string en `appsettings.json`
   - Ejecutar migraciones: `dotnet ef database update`

2. **Backend**:
   - Configurar IdentityServer (URL, secrets)
   - Configurar servicios externos (Cloudinary, OneSignal)
   - Compilar: `dotnet build`
   - Ejecutar: `dotnet run --project WebUI`

3. **Frontend**:
   - Instalar dependencias: `npm install`
   - Configurar API URL en `.env`
   - Ejecutar: `npm run serve` (desarrollo)
   - Build: `npm run build` (producción)

### Despliegue
- **Backend**: Azure App Service / IIS
- **Frontend**: Azure Static Web Apps / CDN
- **Base de Datos**: Azure SQL Database / SQL Server on-premises

---

## 📈 Métricas del Proyecto

- **Líneas de código**: ~100,000+
- **Entidades**: 90+
- **Controllers**: 90+
- **Casos de uso (Commands/Queries)**: 300+
- **Stored Procedures**: 50+
- **Componentes Vue**: 200+
- **Años de desarrollo**: 3+ (desde 2018)

---

## 🎯 Casos de Uso Principales

### Para Colaboradores
- Ver y actualizar su expediente digital
- Solicitar vacaciones
- Ver saldo de vacaciones
- Actualizar datos personales
- Ver noticias y convenios (app móvil)
- Participar en sorteos

### Para Delivery Managers
- Ver expedientes de su equipo
- Aprobar/rechazar solicitudes de vacaciones
- Programar reuniones 1 a 1
- Ver dashboard de actualización de expedientes
- Reasignar colaboradores

### Para RRHH
- Gestión completa de colaboradores
- Aprobación final de vacaciones
- Carga masiva de incapacidades
- Generación de CVs
- Gestión de contenido (noticias, convenios)
- Reportes y estadísticas
- Administración de solicitudes

### Para Administradores
- Configuración de catálogos (puestos, tecnologías, etc.)
- Gestión de usuarios y permisos
- Configuración de workflows
- Monitoreo del sistema

---

## 🔄 Integraciones

### Sistemas Externos
- **IdentityServer**: Autenticación centralizada
- **Cloudinary**: Almacenamiento de imágenes
- **OneSignal**: Notificaciones push
- **Sistema de Nómina**: Consulta de datos laborales
- **Email**: Envío de notificaciones

### APIs Expuestas
- REST API completa (100+ endpoints)
- Swagger UI para documentación
- Autenticación OAuth2
- Versionado de API

---

## 🐛 Manejo de Errores

### Backend
- Middleware de excepciones personalizado
- Logging estructurado
- Respuestas HTTP estandarizadas
- Validación de modelos (FluentValidation)

### Frontend
- Interceptores de Axios para errores HTTP
- Toast notifications para mensajes
- Manejo de errores de autenticación
- Validación de formularios

---

## 📚 Documentación

### Disponible
- Swagger UI (documentación interactiva de API)
- Comentarios en código
- README en proyectos individuales

### Recomendado Crear
- Manual de usuario
- Guía de desarrollo
- Diagramas de arquitectura
- Documentación de base de datos

---

## 🚧 Áreas de Mejora Potenciales

1. **Testing**: Aumentar cobertura de pruebas unitarias e integración
2. **Performance**: Implementar caching (Redis)
3. **Escalabilidad**: Migrar a microservicios si crece
4. **Documentación**: Crear documentación técnica completa
5. **Modernización**: Actualizar a .NET 6/7/8
6. **Frontend**: Migrar a Vue 3 o considerar alternativas modernas
7. **CI/CD**: Implementar pipelines automatizados
8. **Monitoreo**: Agregar Application Insights o similar
9. **Seguridad**: Auditoría de seguridad completa
10. **Refactoring**: Reducir complejidad de stored procedures

---

## 👥 Roles y Permisos

### Roles Principales
- **Administrador**: Acceso total
- **RRHH**: Gestión de colaboradores y procesos de RRHH
- **Delivery Manager**: Gestión de su equipo
- **Colaborador**: Acceso a su expediente y funciones básicas

### Permisos Granulares
- Lectura/escritura por módulo
- Aprobación de solicitudes
- Generación de reportes
- Administración de catálogos

---

## 🎓 Conceptos Clave para Entender el Sistema

1. **Colaborador**: Empleado de la empresa
2. **Expediente Digital**: Conjunto de información profesional del colaborador
3. **Delivery Manager (DM)**: Manager responsable de un equipo
4. **Solicitud MasterData**: Sistema genérico de solicitudes con workflow
5. **CV (Curriculum Vitae)**: Documento generado automáticamente
6. **Uno a Uno**: Reunión individual entre colaborador y manager
7. **Nómina**: Sistema externo de gestión de pagos
8. **Corporación**: Grupo de empresas (Novacomp, Innovative, ITQS, etc.)

---

## 📞 Contacto y Soporte

- **Repositorio**: Azure DevOps (https://dev.azure.com/novainiciativas)
- **Empresa**: NOVACOMP
- **Proyecto**: Nova Expediente Digital

---

## 📄 Licencia

Propietario: NOVACOMP
Uso interno exclusivo

---

## 🎉 Conclusión

**NovaExpediente** es un sistema empresarial robusto y completo para la gestión de recursos humanos, construido con tecnologías modernas y siguiendo las mejores prácticas de arquitectura de software. El sistema cubre todo el ciclo de vida del colaborador, desde su ingreso hasta la gestión diaria de vacaciones, certificaciones y desarrollo profesional.

La arquitectura limpia y el uso de patrones como CQRS facilitan el mantenimiento y la evolución del sistema. La separación en capas permite que cada parte del sistema pueda evolucionar independientemente, y el uso de interfaces facilita el testing y la sustitución de implementaciones.

El sistema está diseñado para escalar y adaptarse a las necesidades cambiantes de la organización, con un enfoque en la experiencia del usuario tanto en la web como en dispositivos móviles.
