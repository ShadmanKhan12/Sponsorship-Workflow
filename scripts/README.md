# Database scripts

## Recommended: DbMigrator (schema + all seed data)

```powershell
cd src/SponsorshipWorkflow.DbMigrator
$env:DOTNET_ENVIRONMENT = "Production"   # use Production OpenIddict URLs on Neon
dotnet run
```

This applies EF migrations and runs all `IDataSeedContributor` classes (identity users, OpenIddict, sponsorship types/requests, role permissions).

## Alternative: SQL on Neon console

Paste and run `seed-sponsorship-business-data.sql` **after** DbMigrator has created the schema and demo users.

The script is idempotent (skips rows that already exist).
