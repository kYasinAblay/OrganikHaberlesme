# Clean Architecture ASP.NET Core 6
Clean Architecture sample on ASP.NET Core - Visual Studio 2022

Create database 'HrLeaveManagementIdentity':
```powershell
dotnet ef migrations add AddUsers --project .\src\Infrastructure\OrganikHaberlesme.Identity\OrganikHaberlesme.Identity.csproj --startup-project .\src\API\OrganikHaberlesme.Api\OrganikHaberlesme.Api.csproj --context LeaveManagementIdentityDbContext
```

Update database 'HrLeaveManagementIdentity':
```powershell
dotnet ef database update --project .\src\Infrastructure\OrganikHaberlesme.Identity\OrganikHaberlesme.Identity.csproj --startup-project .\src\API\OrganikHaberlesme.Api\OrganikHaberlesme.Api.csproj --context LeaveManagementIdentityDbContext
```

Update database 'HrLeaveManagement':
```powershell
dotnet ef database update --project .\src\Infrastructure\OrganikHaberlesme.Persistence\OrganikHaberlesme.Persistence.csproj --startup-project .\src\API\OrganikHaberlesme.Api\OrganikHaberlesme.Api.csproj --context HrLeaveManagementDbContext
```

add migration 'HrLeaveManagement':
```powershell
dotnet ef migrations add name --project .\src\Infrastructure\OrganikHaberlesme.Persistence\OrganikHaberlesme.Persistence.csproj --startup-project .\src\API\OrganikHaberlesme.Api\OrganikHaberlesme.Api.csproj --context HrLeaveManagementDbContext
```