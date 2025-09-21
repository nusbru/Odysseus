# Use the official ASP.NET Core runtime as the base image
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

# Use the SDK image to build the application
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copy the project file and restore dependencies
COPY ["Odysseus.csproj", "."]
RUN dotnet restore "Odysseus.csproj"

# Copy the entire source code and build the application
COPY . .
WORKDIR "/src"
RUN dotnet build "Odysseus.csproj" -c Release -o /app/build

# Publish the application
FROM build AS publish
RUN dotnet publish "Odysseus.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Create the final runtime image
FROM base AS final
WORKDIR /app

# Copy the published application first
COPY --from=publish /app/publish .

# Create the Data directory for SQLite database with proper permissions
# Do this after copying to ensure proper ownership
RUN mkdir -p /app/Data && \
    chmod 777 /app/Data

# Set up environment for production
ENV ASPNETCORE_ENVIRONMENT=Production
ENV ASPNETCORE_URLS=http://+:80

# Configure the database path
ENV ConnectionStrings__DefaultConnection="DataSource=/app/Data/app.db;Cache=Shared"

# Switch to non-root user for better security (if available)
# USER app

ENTRYPOINT ["dotnet", "Odysseus.dll"]
