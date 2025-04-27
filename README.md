# target-api

target-api is a core component of TargetApp, a web application designed to diagnose biotic stress in coffee leaves using advanced image analysis. This API provides endpoints for user management, image processing, report generation, and more, enabling seamless integration with the TargetApp ecosystem.

## Features

- **User Management**: Create, update, and manage user accounts.
- **Image Upload and Processing**: Upload coffee leaf images for analysis and store them securely.
- **Report Generation**: Generate detailed reports on biotic stress, including disease classification and severity.
- **Queue Management**: Efficiently process image analysis tasks using a queue system.
- **Authentication**: Secure access using JWT-based authentication.
- **Email Notifications**: Send email notifications for user actions, such as login tokens.

## Technologies Used

- **.NET 6**: Backend framework for building the API.
- **Entity Framework Core**: ORM for database interactions.
- **MySQL**: Database for storing application data.
- **Docker**: Containerization for deployment.
- **JWT**: Authentication and authorization.
- **AutoMapper**: Object mapping for DTOs and models.
- **Swagger**: API documentation and testing.

## Prerequisites

- [.NET 6 SDK](https://dotnet.microsoft.com/download/dotnet/6.0)
- [Docker](https://www.docker.com/)
- MySQL database instance
- SMTP server for email notifications

## Getting Started

### Clone the Repository

```bash
git clone https://github.com/your-organization/target-api.git
cd target-api
```
### Configuration

1. Update the connection strings in `Target.API/appsettings.Development.json` and `Target.API/appsettings.Production.json`:

```json
"ConnectionStrings": {
  "Database": "your-database-connection-string",
  "Storage": "your-storage-connection-string",
  "Queue": "your-queue-connection-string"
}
```

2. Configure SMTP credentials for email notifications:

```json
"SmtpCredentials": {
  "Host": "your-smtp-host",
  "Port": 587,
  "Username": "your-smtp-username",
  "Password": "your-smtp-password",
  "EnableSSL": true
}
```

3. Set the TokenKey for JWT authentication:

```json
"TokenKey": "your-secret-key"
```

### Build and Run

#### Using .NET CLI

1. Restore dependencies:

```bash
dotnet restore
```
2. Build the project:

```bash
dotnet build
```
3. Run the application:

```bash
dotnet run --project Target.API
```
4. Access the API at `https://localhost:7130/swagger`.

#### Using Docker
1. Build the Docker image:

```bash
docker build -t target-api .
```
2. Run the Docker container:

```bash
docker run -d -p 7130:80 target-api
```

### API Documentation

The API is documented using Swagger. Once the API is running, you can access the documentation at:
```
http://localhost:7130/swagger
```

### Deployment
This repository includes a GitHub Actions workflow for building and deploying the API as a Docker container. The workflow is defined in `.github/workflows/deploy-api.yml`.

#### Steps:
1. Push changes to the main branch.
2. The workflow builds the Docker image, uploads it to the VPS, and deploys the container.

Ensure the following secrets are configured in your GitHub repository:

- VPS_HOST: The IP address or hostname of your VPS.
- VPS_USER: The username for SSH access.
- VPS_PORT: The SSH port.
- SSH_PRIVATE_KEY: The private key for SSH authentication.