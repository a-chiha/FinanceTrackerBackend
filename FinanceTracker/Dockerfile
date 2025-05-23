# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080

# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy the project file and restore dependencies
COPY ["FinanceTracker/FinanceTracker.csproj", "FinanceTracker/"]
RUN dotnet restore "FinanceTracker/FinanceTracker.csproj"

# Copy the entire source code
COPY . .

# Build and publish the application
WORKDIR /src/FinanceTracker
RUN dotnet publish -c Release -o /app/publish --no-restore

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .

# Ensure we have the environment set
ENV ASPNETCORE_ENVIRONMENT=Production

# Let the PORT environment variable be set by the hosting platform
ENV ASPNETCORE_URLS=http://+:${PORT:-8080}

ENTRYPOINT ["dotnet", "FinanceTracker.dll"]