# Sponsorship Workflow

> ⚠️ Backend is hosted on Render Free Tier.  
> Due to free hosting limitations, the backend instance may spin down during inactivity and the first request can take 30–50 seconds to respond. The app may also occasionally become temporarily unavailable due to free-tier constraints.

A sponsorship request approval workflow system built with modern enterprise technologies using ABP Framework and .NET 10.

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
- 🔐 OpenIddict Authentication
- 🐘 PostgreSQL (Neon Serverless)
- 📄 Serilog Logging

## Frontend
- 🅰️ Angular 21
- 🎨 ABP Angular UI
- 🔄 RxJS

## DevOps / Hosting
- 🐳 Dockerized Backend
- 🚀 Render Deployment (Free Tier)
- ▲ Vercel Frontend Hosting
- ☁️ Neon Serverless PostgreSQL

---

# Setup Guide

## Run Backend

```bash
cd src/SponsorshipWorkflow.HttpApi.Host
dotnet run
```

## Run Frontend

```bash
cd angular
npm install
npm start
```

## Run Database Migrator

```bash
cd src/SponsorshipWorkflow.DbMigrator
dotnet run
```

Update PostgreSQL connection strings in:

```text
src/SponsorshipWorkflow.HttpApi.Host/appsettings.json
src/SponsorshipWorkflow.DbMigrator/appsettings.json
```

---

# Test Accounts

## Admin (Full Permissions)

- Email: `admin`  
- Password: `1q2w3E*`  
- Access: Full system permissions (System Admin with all roles and privileges)

---

## All Test Users

| Email | Role | Password |
|---|---|---|
| admin@test.com | System Admin (All Permissions) | 1q2w3E* |
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

# Key Features

- Role-Based Access Control (RBAC)
- Sponsorship Request Form
- Draft / Submit workflow handling
- Manager approval stage
- Finance approval stage
- Workflow status tracking
- Audit/history logging
- Permission-based route guards
- Swagger API documentation
- Role-based dashboards per user type

---

# Architecture

## Backend
- Domain Driven Design (DDD) with ABP Framework
- Domain / Application / EF Core / API layers
- OpenIddict-based authentication & authorization

## Frontend
- Angular modular architecture
- ABP Angular UI integration
- Route guards + permission-based UI rendering

## Workflow Logic
- Centralized in application services
- Strict state transition validation
- Role + permission checks at every approval stage

---

# TODO / Known Limitations

- ⚠️ App reliability depends on Render free-tier hosting (may sleep or temporarily fail under inactivity)
- 🔐 Improve identity & authentication robustness (token/session handling hardening)
- 🛡️ Strengthen role-based access control validation consistency across edge cases
- 🔑 Improve permission seeding & role initialization stability during first deployment
- ⚙️ Improve deployment resilience (cold start handling / retry strategy)
- 🧾 Improve audit consistency for failed/aborted workflow actions
- 📦 Optimize backend startup performance on cold starts