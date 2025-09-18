# 🧭 Odysseus - Job Application Tracker

A modern web application built with **Blazor Server** to help job seekers track and manage their job applications throughout their career journey.

## 📋 Table of Contents

- [Features](#-features)
- [Technology Stack](#-technology-stack)
- [Architecture](#-architecture)
- [Getting Started](#-getting-started)
- [Project Structure](#-project-structure)
- [Database Schema](#-database-schema)
- [Docker Support](#-docker-support)
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
- Real-time statistics cards
- Success rate calculation
- Recent applications highlighting
- Status-based filtering and organization
- Empty state guidance for new users

## 🚀 Technology Stack

- **Framework**: ASP.NET Core 9.0 with Blazor Server
- **Database**: SQLite with Entity Framework Core
- **Authentication**: ASP.NET Core Identity
- **UI Framework**: Bootstrap 5 with custom styling
- **Architecture**: Clean Architecture with SOLID principles
- **Containerization**: Docker support included

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
- [Entity Framework Core Tools](https://docs.microsoft.com/en-us/ef/core/cli/dotnet)

### Installation

1. **Clone the repository**
   ```bash
   git clone https://github.com/yourusername/Odysseus.git
   cd Odysseus
   ```

2. **Install EF Core tools** (if not already installed)
   ```bash
   dotnet tool install --global dotnet-ef
   ```

3. **Restore dependencies**
   ```bash
   dotnet restore
   ```

4. **Apply database migrations**
   ```bash
   dotnet ef database update
   ```

5. **Run the application**
   ```bash
   dotnet run
   ```

6. **Open your browser** and navigate to `http://localhost:5232`

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

- **`Components/Pages/Dashboard.razor`**: Main dashboard with statistics
- **`Components/Pages/AddJob.razor`**: Job application creation form
- **`Components/Pages/EditJob.razor`**: Job application editing form
- **`Components/Pages/ViewJob.razor`**: Detailed job application view

### Application Status Flow

```
Not Applied → Applied → In Progress → Waiting Response → Waiting Job Offer → Accepted/Denied/Failed
```

## 🐳 Docker Support

### Build and Run with Docker

```bash
# Build the Docker image
docker build -t odysseus .

# Run the container
docker run -p 8080:80 odysseus
```

The application will be available at `http://localhost:8080`

### Docker Configuration

- **Base Image**: `mcr.microsoft.com/dotnet/aspnet:9.0`
- **Build Image**: `mcr.microsoft.com/dotnet/sdk:9.0`
- **Port**: Exposes port 80
- **Database**: SQLite database persisted in `/app/Data/`

## 🤝 Contributing

1. Fork the repository
2. Create a feature branch (`git checkout -b feature/amazing-feature`)
3. Commit your changes (`git commit -m 'Add some amazing feature'`)
4. Push to the branch (`git push origin feature/amazing-feature`)
5. Open a Pull Request

## 🙏 Acknowledgments

- Built with [ASP.NET Core](https://docs.microsoft.com/en-us/aspnet/core/)
- UI components from [Bootstrap 5](https://getbootstrap.com/)
- Icons from [Bootstrap Icons](https://icons.getbootstrap.com/)

---

**Happy job hunting!** 🎯
