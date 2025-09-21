# GitHub CI/CD Workflows Setup Guide

This guide explains how to set up and use the CI/CD workflows for the Odysseus job application tracker.

## 📋 Overview

The project uses two separate workflows following GitOps best practices:

### 🔧 **CI Workflow** (`ci.yml`)
- **Trigger**: Every commit push to any branch
- **Purpose**: Continuous Integration with multistage build validation
- **Stages**: Code Analysis → Build & Test → Docker Build Test → Integration Tests

### 🚀 **CD Workflow** (`cd.yml`)
- **Trigger**: After successful CI completion on `main` branch
- **Purpose**: Continuous Deployment with Docker image publishing
- **Features**: Docker Hub publishing with `latest` and versioned tags (`1.0.{build_number}`)

## 🛠️ Prerequisites

### 1. GitHub Repository Secrets

Navigate to your GitHub repository → Settings → Secrets and Variables → Actions

Add the following secrets:

| Secret Name | Description | How to Get |
|------------|-------------|------------|
| `DOCKERHUB_USERNAME` | Your Docker Hub username | Your Docker Hub account username |
| `DOCKERHUB_TOKEN` | Docker Hub access token | Create from Docker Hub → Settings → Security |

### 2. Docker Hub Setup

1. **Create Docker Hub Account**: [hub.docker.com](https://hub.docker.com)
2. **Create Repository**: 
   - Repository name: `odysseus-job-tracker`
   - Visibility: Public (or Private if you have Pro account)
3. **Generate Access Token**:
   - Go to Account Settings → Security
   - Click "New Access Token"
   - Name: `github-actions-odysseus`
   - Permissions: Read, Write, Delete
   - Copy the generated token

## 🔄 Workflow Details

### CI Workflow - Multistage Build

The CI workflow follows a multistage approach:

#### **Stage 1: Code Analysis & Security**
```yaml
- Checkout code
- Setup .NET 9.0
- Cache NuGet packages
- Restore dependencies  
- Run Trivy security scan
- Upload security results to GitHub Security tab
```

#### **Stage 2: Build & Test**
```yaml
- Build application in Release mode
- Run unit tests with coverage
- Upload test results as artifacts
- Upload build artifacts
```

#### **Stage 3: Docker Build Test**
```yaml
- Set up Docker Buildx
- Build Docker image (no push)
- Test container functionality
- Verify application responds
```

#### **Stage 4: Integration Tests**
```yaml
- Run integration tests
- Placeholder for additional tests
```

### CD Workflow - Docker Publishing

The CD workflow publishes Docker images with specific tagging:

#### **Version Tagging Strategy**
- **Latest tag**: `latest` (always points to main branch)
- **Version tag**: `1.0.{build_number}` (e.g., `1.0.45`)
- **Branch tag**: `main` (branch name)
- **SHA tag**: `main-abc1234` (for traceability)

#### **CD Pipeline Stages**
1. **Check CI Status**: Ensures CI passed before deployment
2. **Build & Push**: Creates and publishes Docker images
3. **Verify Deployment**: Tests published images
4. **Create Release**: Creates GitHub release with version tag
5. **Summary**: Provides deployment information

## 🚀 Usage

### Automatic Triggers

#### **CI Workflow Runs On:**
- Push to any branch (`main`, `develop`, `feature/*`)
- Pull request to `main` or `develop`

#### **CD Workflow Runs On:**
- Successful CI completion on `main` branch
- Manual trigger (workflow_dispatch)

### Manual Deployment

You can manually trigger the CD workflow:

1. Go to Actions tab in GitHub
2. Select "CD - Continuous Deployment"
3. Click "Run workflow"
4. Choose options:
   - **Branch**: Select branch (usually `main`)
   - **Force Deploy**: Override CI failure (use with caution)

### Quick Start with Docker

```bash
# Run latest version
docker run -p 8080:80 yourusername/odysseus-job-tracker:latest

# With persistent data
docker run -p 8080:80 -v odysseus_data:/app/Data yourusername/odysseus-job-tracker:latest
```

## 📊 Monitoring

### GitHub Actions

- **CI Status**: Check Actions tab for build status on every commit
- **CD Status**: Monitor deployment progress and Docker publishing
- **Security**: Review security scan results in Security tab
- **Artifacts**: Download test results and build artifacts

### Docker Hub

- **Image Stats**: View download statistics and image details
- **Tags**: See all published versions and their details
- **Vulnerabilities**: Monitor security scan results (Pro accounts)

