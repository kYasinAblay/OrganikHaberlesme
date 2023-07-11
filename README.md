# Clean Architecture ASP.NET Core 6
Clean Architecture sample on ASP.NET Core - Visual Studio 2022

Create database 'OrganikHaberlesmeIdentity':
```powershell
dotnet ef migrations add AddUsers --project .\src\Infrastructure\OrganikHaberlesme.Identity\OrganikHaberlesme.Identity.csproj --startup-project .\src\API\OrganikHaberlesme.Api\OrganikHaberlesme.Api.csproj --context LeaveManagementIdentityDbContext
```

Update database 'OrganikHaberlesmeIdentity':
```powershell
dotnet ef database update --project .\src\Infrastructure\OrganikHaberlesme.Identity\OrganikHaberlesme.Identity.csproj --startup-project .\src\API\OrganikHaberlesme.Api\OrganikHaberlesme.Api.csproj --context LeaveManagementIdentityDbContext
```

Update database 'OrganikHaberlesme':
```powershell
dotnet ef database update --project .\src\Infrastructure\OrganikHaberlesme.Persistence\OrganikHaberlesme.Persistence.csproj --startup-project .\src\API\OrganikHaberlesme.Api\OrganikHaberlesme.Api.csproj --context OrganikHaberlesmeDbContext
```

add migration 'OrganikHaberlesme':
```powershell
dotnet ef migrations add name --project .\src\Infrastructure\OrganikHaberlesme.Persistence\OrganikHaberlesme.Persistence.csproj --startup-project .\src\API\OrganikHaberlesme.Api\OrganikHaberlesme.Api.csproj --context OrganikHaberlesmeDbContext
```
