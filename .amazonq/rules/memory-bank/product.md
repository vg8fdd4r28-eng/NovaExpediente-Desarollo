# Product Overview - NovaExpediente

## Purpose
NovaExpediente is a comprehensive digital employee records management system (expediente digital) for NOVACOMP, a technology services company. The system centralizes and manages all employee-related information including personal data, projects, certifications, vacations, payroll integration, and mobile app content.

## Value Proposition
- **Centralized HR Management**: Single source of truth for all employee information across multiple companies in the NOVACOMP corporation
- **Automated CV Generation**: Creates professional CVs in multiple formats (8 different templates) for business development and client proposals
- **Workflow Automation**: Streamlines approval processes for vacations, leaves, permits, and other HR requests
- **Mobile Integration**: Provides content management for employee mobile app (news, benefits, raffles, events)
- **Payroll Integration**: Connects with external payroll system (SAP Business One) for vacation balance synchronization
- **Compliance & Audit**: Tracks all changes with audit trails (creation/modification user and timestamps)

## Key Features

### Employee Management
- Complete CRUD operations for employee records (90+ entities)
- Personal information, contact details, profile photos
- Employment history and organizational assignments
- Delivery Manager assignments and team structures
- Active/inactive status tracking

### Digital Employee File (Expediente)
- Academic background (degrees, institutions, graduation dates)
- Professional certifications with expiration tracking
- Courses taken and taught
- Language proficiency levels
- Soft skills assessment
- Technical experience:
  - Programming languages
  - Databases
  - Cloud platforms
  - Development tools
  - Frameworks and technologies
  - Infrastructure knowledge
  - Quality assurance tools

### Project Experience
- Project portfolio management
- Client and sector tracking
- Technology stack documentation
- Role and responsibility history
- Project duration and team size
- Reference contacts for each project

### CV Generation System
- 8 professional templates (Spanish/English, multiple brands)
- Automated document generation from database
- Customizable content selection
- Word document output
- Support for multiple company brands (Novacomp, Innovative, ITQS, NovaShore, NovaTesting)

### Leave Management
- Vacation request and approval workflow
- Vacation balance calculation and tracking
- Sick leave (incapacidades) management
- Permits and time-off requests
- Integration with payroll system for balance updates
- Bulk upload from Excel files
- Calendar visualization

### Master Data Request System
- Generic workflow engine for various request types
- Multi-level approval chains
- Email notifications at each stage
- Request tracking and history
- Comment threads on requests
- State transitions management
- Supported request types:
  - Salary certificates
  - DIMEX renewal
  - Labor service letters
  - Vehicle restriction documents
  - Custom administrative requests

### Mobile App Content Management
- News articles with categories
- Corporate benefits (convenios) with categories
- Employee raffles and contests
- Events with image galleries
- Push notifications via OneSignal
- Version control for app updates

### One-on-One Meetings
- Schedule and track 1:1 meetings between employees and managers
- Categorized discussion topics
- Meeting notes and action items
- Status tracking per employee
- Historical record of meetings

### Reporting & Analytics
- Dashboard with HR KPIs
- Custom report generation
- Excel export functionality
- Advanced search with multiple criteria
- Vacation balance reports
- Certification expiration alerts
- Project staffing reports

### Security & Access Control
- Role-based access control (10+ roles)
- Permission-based feature access
- OAuth2 + OpenID Connect authentication
- Integration with IdentityServer
- Audit logging for all operations

## Target Users

### Internal Users
- **HR Administrators**: Full system access for employee data management
- **HR Staff**: Employee records, vacation approvals, report generation
- **Delivery Managers**: Team member information, vacation approvals, 1:1 meetings
- **Business Development Managers**: CV generation, consultant profiles, project history
- **Finance Team**: Employee data for payroll and billing
- **Bid/Proposal Team**: CV generation, technical profiles, project experience
- **Employees**: View own information, submit requests, update technical skills
- **Payroll Administrators**: Vacation balance management, integration monitoring
- **Mobile App Administrators**: Content management for employee app

### External Integration Points
- **Payroll System (SAP B1)**: Vacation balance synchronization
- **Identity Server**: Authentication and authorization
- **Cloudinary**: Image storage and management
- **OneSignal**: Push notifications
- **SendGrid**: Email notifications
- **AWS Lambda**: CV generation and chat services

## Use Cases

### Primary Workflows
1. **New Employee Onboarding**: Create employee record, assign to projects, set up access
2. **CV Generation for Proposals**: Select consultant, choose template, generate professional CV
3. **Vacation Request**: Employee submits request → Manager approves → HR processes → Payroll updates
4. **Certification Tracking**: Record certifications, set expiration alerts, track renewals
5. **Project Staffing**: Assign employees to projects, track technology experience, manage transitions
6. **Mobile Content Publishing**: Create news/benefits → Review → Publish to mobile app
7. **Administrative Requests**: Submit request → Route through approval chain → Notify outcome
8. **1:1 Meeting Management**: Schedule meeting → Document discussion → Track action items
9. **Bulk Data Import**: Upload Excel files for vacations/sick leaves → Validate → Import to system
10. **Reporting**: Generate custom reports, export to Excel, analyze HR metrics
