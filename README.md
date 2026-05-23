# Sponsorship Workflow

> ⚠️ Backend is hosted on Render Free Tier.  
> Due to free hosting limitations, the backend instance may spin down during inactivity and the first request can take 30–50 seconds to respond. The app may also occasionally become temporarily unavailable due to free-tier constraints.

---

# About This Project

This project demonstrates my ability to design and implement a full-stack enterprise workflow system using ABP Framework, with a strong focus on clean architecture, RBAC, and approval-based business logic.

It was developed using an AI-assisted engineering workflow (using free-tier AI tools for design support, debugging, and implementation acceleration), while all architectural decisions, system design, RBAC implementation, ABP integration, OpenIddict authentication, Angular frontend structure, CI/CD flow, and deployment strategy were planned and implemented by me.

The project also reflects practical DevOps experience using Dockerized backend deployment, Vercel frontend CI/CD, and Render hosting with Neon Serverless PostgreSQL.

---

# Live URLs

🌐 **Frontend**  
https://sponsorship-workflow.vercel.app/

⚙️ **Backend API**  
https://sponsorship-workflow-l8d2.onrender.com/

📘 **Swagger Documentation**  
https://sponsorship-workflow-l8d2.onrender.com/swagger/index.html

---

# Tech Stack

## Backend
- 🔷 ASP.NET Core .NET 10
- 🧩 ABP Framework 10.4
- 🗄️ Entity Framework Core
- 🔐 OpenIddict Authentication & Authorization
- 🐘 PostgreSQL (Neon Serverless)
- 📄 Serilog Logging
- 🐳 Dockerized deployment

## Frontend
- 🅰️ Angular 21
- 🎨 ABP Angular UI
- 🔄 RxJS
- 🚀 Vercel CI/CD deployment

## DevOps / Infrastructure
- 🐳 Docker (Backend containerization)
- 🚀 Render (Backend hosting – free tier)
- ▲ Vercel (Frontend CI/CD)
- ☁️ Neon Serverless PostgreSQL

---

# Setup Guide

## Prerequisites
- .NET 10 SDK
- Node.js v18 or v20
- ABP CLI
- PostgreSQL (Neon recommended)

---

## Run Database Migrator

```bash
cd src/SponsorshipWorkflow.DbMigrator
dotnet run
```

---

## Run Backend

```bash
cd src/SponsorshipWorkflow.HttpApi.Host
dotnet run
```

---

## Run Frontend

```bash
cd angular
npm install
npm start
```

---

## Database Configuration

Update connection strings:

```text
src/SponsorshipWorkflow.HttpApi.Host/appsettings.json
src/SponsorshipWorkflow.DbMigrator/appsettings.json
```

---

# Test Accounts

## Admin (Full Permissions)

- Email: `admin@test.com`  
- Password: `1q2w3E*`  
- Access: Full system permissions (System Admin with complete RBAC access)

---

## All Users

| Email | Role | Password |
|---|---|---|
| admin@test.com | System Admin | 1q2w3E* |
| finance@test.com | Finance Admin | 1q2w3E* |
| manager@test.com | Manager | 1q2w3E* |
| requestor@test.com | Requestor | 1q2w3E* |

---

# Workflow

```text
Draft
 → Pending Manager Approval
 → Pending Finance Review
 → Approved
```

Also supported:
- Rejected
- Cancelled

---

# Features

- Role-Based Access Control (RBAC)
- Sponsorship Request Form (full lifecycle)
- Draft / Submit workflow handling
- Manager approval stage with permissions
- Finance approval stage with validation
- Workflow status tracking
- Audit and history logging
- Permission-based route guards (Angular)
- Swagger API documentation
- Role-based dashboards per user type

---

# Architecture Overview

## Backend (ABP Framework)
- Domain Driven Design (DDD)
- Domain Layer
- Application Layer
- Entity Framework Core Layer
- HTTP API Layer
- OpenIddict authentication & authorization

## Frontend (Angular)
- Modular architecture
- ABP Angular UI integration
- Route guards + permission-based rendering
- Role-specific dashboard views

## Workflow Engine
- Centralized in Application Services
- Strict state transition rules
- Permission-validated approvals at each stage
- Audit logging for all transitions

---

# DevOps / Deployment

- 🐳 Backend containerized using Docker
- 🚀 Backend deployed on Render (Free Tier)
- ▲ Frontend deployed on Vercel (CI/CD pipeline)
- ☁️ PostgreSQL hosted on Neon Serverless
- 🔐 Authentication via OpenIddict

---

# AI-Assisted Development Note

This project demonstrates the effective use of AI-assisted development tools to accelerate software engineering tasks such as debugging, code generation, and architectural exploration.

Free-tier AI tools were used as supporting aids, while all core engineering decisions—including system design, ABP framework integration, workflow architecture, RBAC design, and deployment strategy—were independently designed and implemented.

This reflects a modern development approach where AI is used as an augmentation tool within a structured engineering workflow, combined with CI/CD practices and real-world cloud deployment.

---

# TODO / Future Improvements

- ⚠️ Improve reliability due to Render free-tier cold start delays and potential downtime
- 🔐 Strengthen identity and authentication hardening (session/token robustness)
- 🛡️ Improve RBAC edge-case consistency across workflow transitions
- 🔑 Improve role/permission seeding stability during initial deployment
- ⚙️ Enhance backend startup performance under cold-start conditions
- 🧾 Improve audit trail consistency for failed or interrupted workflows
- 📦 Further optimize CI/CD pipeline reliability and deployment speed

---
