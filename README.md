# 🧭 Odysseus - Job Application Tracker

A modern web application built with **Blazor Server** and **PostgreSQL** to help job seekers track and manage their job applications throughout their career journey. Features a comprehensive dashboard with analytics and visualization capabilities.

## 📋 Table of Contents

- [Features](#-features)
- [Technology Stack](#-technology-stack)
- [Architecture](#-architecture)
- [Getting Started](#-getting-started)
- [Project Structure](#-project-structure)
- [Database Configuration](#-database-configuration)
- [Docker Support](#-docker-support)
- [Development](#-development)
- [Contributing](#-contributing)

## ✨ Features

### 🎯 Core Functionality
- **User Authentication**: Secure registration and login with ASP.NET Identity
- **Job Application Management**: Full CRUD operations for job applications
- **Application Tracking**: Track applications through multiple stages
- **Dashboard Analytics**: View success rates, pending applications, and statistics
- **Responsive Design**: Mobile-friendly interface built with Bootstrap 5

### 📊 Job Application Features
- Company information (name, country, role)
- Application status tracking (Not Applied → Applied → In Progress → Waiting Response → Accepted/Denied)
- Multiple interview phases support
- Sponsorship and relocation requirements
- Personal notes and job posting links
- Application timeline with creation and update timestamps

### 📈 Dashboard & Analytics
- **Comprehensive Statistics**: Total applications, pending, in-progress, and rejected counts
- **Visual Analytics**: Interactive charts and data visualization
- **Country Distribution**: Geographic analysis of job applications
- **Real-time Updates**: Live dashboard with dynamic statistics
- **Status Tracking**: Detailed application status monitoring
- **Empty State Guidance**: Helpful onboarding for new users

## 🚀 Technology Stack

- **Framework**: ASP.NET Core 9.0 with Blazor Server
- **Database**: PostgreSQL 16 with Entity Framework Core
- **Database Provider**: Npgsql.EntityFrameworkCore.PostgreSQL
- **Authentication**: ASP.NET Core Identity
- **UI Framework**: Bootstrap 5 with Bootstrap Icons
- **Visualization**: Chart.js for interactive analytics
- **Architecture**: Clean Architecture with SOLID principles
- **Containerization**: Docker Compose with PostgreSQL and app containers
- **Development**: Hot reload, Entity Framework migrations

## 🏗️ Architecture

The application follows **Clean Architecture** principles with clear separation of concerns:

```
├── Domain/                 # Domain entities and business logic
│   ├── Entities/          # Domain entities (JobApply)
│   └── Enums/             # Domain enumerations (JobStatus)
├── Application/           # Application layer
│   ├── Interfaces/        # Repository contracts
│   ├── Services/          # Mapping and business services
│   └── ViewModels/        # Data transfer objects
├── Infrastructure/        # Infrastructure layer
│   └── Repositories/      # Data access implementations
├── Data/                  # Entity Framework context and models
│   ├── ApplicationDbContext.cs
│   └── ApplicationUser.cs
└── Components/            # Blazor components and pages
    ├── Pages/             # Application pages
    ├── Layout/            # Layout components
    └── Account/           # Authentication components
```

## 🚀 Getting Started

### Prerequisites

- [.NET 9.0 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
- [PostgreSQL 16+](https://www.postgresql.org/download/) or [Docker](https://www.docker.com/get-started)
- [Entity Framework Core Tools](https://docs.microsoft.com/en-us/ef/core/cli/dotnet)

### Quick Start with Docker (Recommended)

1. **Clone the repository**
   ```bash
   git clone https://github.com/nusbru/Odysseus.git
   cd Odysseus
   ```

2. **Start with Docker Compose**
   ```bash
   docker-compose up -d
   ```

3. **Access the application**
   - **Web App**: http://localhost:8080
   - **Database**: PostgreSQL on localhost:5432

### Manual Setup (Development)

1. **Clone and setup**
   ```bash
   git clone https://github.com/nusbru/Odysseus.git
   cd Odysseus
   dotnet restore
   ```

2. **Database Setup**
   
   **Option A: Use Docker for PostgreSQL only**
   ```bash
   docker run --name odysseus-postgres -e POSTGRES_DB=odysseus -e POSTGRES_USER=odysseus_user -e POSTGRES_PASSWORD=odysseus_password -p 5432:5432 -d postgres:16-alpine
   ```

   **Option B: Local PostgreSQL**
   - Install PostgreSQL 16+
   - Create database: `odysseus`
   - Create user: `odysseus_user` with password: `odysseus_password`

3. **Apply migrations**
   ```bash
   dotnet tool install --global dotnet-ef
   dotnet ef database update
   ```

4. **Run the application**
   ```bash
   dotnet run
   ```

5. **Access the application**
   - **HTTP**: http://localhost:5232
   - **HTTPS**: https://localhost:7085

### First Steps

1. Register a new account or sign in
2. Navigate to the Dashboard
3. Click "Add New Application" to create your first job application
4. Track your progress and view analytics on the dashboard

## 📁 Project Structure

### Core Components

- **`Program.cs`**: Application startup and service configuration
- **`Dockerfile`**: Container configuration for deployment
- **`.editorconfig`**: Code style and formatting rules

### Domain Layer

- **`Domain/Entities/JobApply.cs`**: Core job application entity with business rules
- **`Domain/Enums/JobStatus.cs`**: Application status enumeration

### Application Layer

- **`Application/Interfaces/IJobApplyRepository.cs`**: Repository contract
- **`Application/ViewModels/`**: Data transfer objects for UI binding
- **`Application/Services/MappingService.cs`**: Entity-ViewModel mapping

### Infrastructure Layer

- **`Infrastructure/Repositories/JobApplyRepository.cs`**: Entity Framework repository implementation

### UI Components

- **`Components/Pages/Dashboard.razor`**: Interactive dashboard with statistics and charts
- **`Components/Pages/AddJob.razor`**: Job application creation form
- **`Components/Pages/EditJob.razor`**: Job application editing form
- **`Components/Pages/ViewJob.razor`**: Detailed job application view
- **`Components/Pages/Home.razor`**: Landing page
- **`Components/Layout/MainLayout.razor`**: Application layout
- **`Components/Layout/NavMenu.razor`**: Navigation menu

### Application Status Flow

```
Applied → In Progress → Waiting Response → Waiting Job Offer → Accepted/Denied/Failed
```

## �️ Database Configuration

### PostgreSQL Configuration

The application uses PostgreSQL with the following default configuration:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Database=odysseus;Username=odysseus_user;Password=odysseus_password;Port=5432"
  }
}
```

### Entity Framework Migrations

- **Initial Migration**: `20250927145636_InitialCreate`
- **Migration Commands**:
  ```bash
  dotnet ef migrations add <MigrationName>
  dotnet ef database update
  ```

### Database Schema

- **AspNetUsers**: User authentication and profiles
- **AspNetRoles**: User roles and permissions
- **JobApplications**: Core job application data
- **Related tables**: Identity framework tables for authentication

## 🐳 Docker Support

### Full Stack with Docker Compose (Recommended)

```bash
# Start PostgreSQL + Application
docker-compose up -d

# View logs
docker-compose logs -f

# Stop services
docker-compose down
```

### Docker Configuration

- **PostgreSQL**: `postgres:16-alpine` on port 5432
- **Application**: Custom .NET 9.0 image on port 8080
- **Volumes**: PostgreSQL data persistence
- **Networks**: Isolated container network
- **Health checks**: Automatic PostgreSQL health monitoring

### Manual Docker Build

```bash
# Build the Docker image
docker build -t odysseus .

# Run with external PostgreSQL
docker run -p 8080:80 -e ConnectionStrings__DefaultConnection="Host=host.docker.internal;Database=odysseus;Username=odysseus_user;Password=odysseus_password;Port=5432" odysseus
```

## 🛠️ Development

### Development Environment

- **Hot Reload**: Enabled for Blazor components
- **Entity Framework**: Code-first migrations
- **Logging**: Comprehensive logging with different levels
- **Environment**: Development vs Production configurations

### Database Development

```bash
# Add new migration
dotnet ef migrations add NewFeature

# Update database
dotnet ef database update

# Remove last migration (if not applied)
dotnet ef migrations remove
```

### Adding New Features

1. **Domain**: Add entities to `Domain/Entities/`
2. **Repository**: Update `Application/Interfaces/` and `Infrastructure/Repositories/`
3. **ViewModels**: Create DTOs in `Application/ViewModels/`
4. **UI**: Add Blazor components in `Components/Pages/`
5. **Migration**: Generate EF Core migration

## 🤝 Contributing

1. **Fork** the repository
2. **Clone** your fork locally
3. **Create** a feature branch (`git checkout -b feature/amazing-feature`)
4. **Setup** development environment:
   ```bash
   dotnet restore
   docker-compose up -d postgres  # Start PostgreSQL only
   dotnet ef database update
   ```
5. **Make** your changes and test thoroughly
6. **Commit** your changes (`git commit -m 'Add some amazing feature'`)
7. **Push** to the branch (`git push origin feature/amazing-feature`)
8. **Open** a Pull Request with detailed description

### Development Guidelines

- Follow [Clean Architecture](https://blog.cleancoder.com/uncle-bob/2012/08/13/the-clean-architecture.html) principles
- Write comprehensive tests for new features
- Update documentation for significant changes
- Use conventional commit messages
- Ensure PostgreSQL compatibility for all database changes

## � Features Overview

### Current Dashboard Analytics
- **Total Applications**: Complete count of all job applications
- **Pending Applications**: Applications awaiting response
- **In Progress Applications**: Active interview processes
- **Rejected Applications**: Applications that were declined
- **Interactive Charts**: Visual representation of application data
- **Country Analysis**: Geographic distribution of applications

### Application Management
- **CRUD Operations**: Create, Read, Update, Delete job applications
- **Status Tracking**: Multi-stage application process tracking
- **Company Information**: Detailed company and role information
- **Timeline Tracking**: Application dates and progress history
- **Notes System**: Personal notes for each application

## �🙏 Acknowledgments

- Built with [ASP.NET Core 9.0](https://docs.microsoft.com/en-us/aspnet/core/)
- Database powered by [PostgreSQL](https://www.postgresql.org/)
- UI framework: [Bootstrap 5](https://getbootstrap.com/)
- Icons: [Bootstrap Icons](https://icons.getbootstrap.com/)
- Charts: [Chart.js](https://www.chartjs.org/)
- Containerization: [Docker](https://www.docker.com/)

---

**Track your journey to success!** 🎯 **Happy job hunting!** 🚀
