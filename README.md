# CitizenPortal
A full-stack Danish citizen service portal built with ASP.NET Core 10, React 18, and deployed on Azure Kubernetes Service.

## Tech Stack

Backend
- ASP.NET Core 10
- Entity Framework Core + SQL Server
- JWT + Refresh Tokens
- SignalR (real-time notifications)
- MailKit (email notifications)
- Azure Blob Storage (file uploads)

Frontend
- React 18
- React Router v6
- Axios
- Context API
- UI fully in Danish

DevOps
- Docker + Docker Compose
- GitHub Actions (CI/CD)
- Azure Container Registry (ACR)
- Azure Kubernetes Service (AKS)
- Azure Key Vault
- Azure Application Insights
- Terraform (Infrastructure as Code)

Architecture
citizen-portal-frontend (React) → nginx → port 80
citizenportal-backend (ASP.NET Core) → port 8080
SQL Server → port 1433

   Getting Started

   Run with Docker Compose

```bash
docker-compose up --build
```

Frontend: http://localhost:3000
Backend: http://localhost:8080

   Run with Kubernetes

```bash
kubectl apply -f k8s/
```

  CI/CD Pipeline
dev branch → build + test
main branch → build + test → Docker build → ACR push → AKS deploy

  Roles

- Borger — submit applications, upload documents, track status
- Sagsbehandler — review and approve/reject applications
- Admin — user management

  Tests
28 unit tests with xUnit and Moq — 28/28 passing


MIT
