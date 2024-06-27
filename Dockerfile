# Base image for running the application
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app

# Stage for building the application
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Target.API/Target.API.csproj", "Target.API/"]
COPY ["Target.Application/Target.Application.csproj", "Target.Application/"]
COPY ["Target.Domain/Target.Domain.csproj", "Target.Domain/"]
COPY ["Target.Persistence/Target.Persistence.csproj", "Target.Persistence/"]
RUN dotnet restore "./Target.API/Target.API.csproj"
COPY . .
WORKDIR "/src/Target.API"
RUN dotnet build "./Target.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Stage for publishing the application
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Target.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Final stage for running the application
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENV ASPNETCORE_ENVIRONMENT Production
ENTRYPOINT ["dotnet", "Target.API.dll"]

