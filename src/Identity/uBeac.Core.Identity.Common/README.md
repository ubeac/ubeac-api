## uBeac.Identity.Common: Includes prerequisites of identity management
[![Nuget version](https://img.shields.io/nuget/v/uBeac.Identity.Common?label=nuget%20version&logo=nuget&style=flat)](https://www.nuget.org/packages/uBeac.Identity.Common/) [![Nuget downloads](https://img.shields.io/nuget/dt/uBeac.Identity.Common?label=nuget%20downloads&logo=nuget&style=flat)](https://www.nuget.org/packages/uBeac.Identity.Common/)

### Start
Install the package with NuGet:
```
dotnet add package uBeac.Identity.Common
```

<hr>

### Entities
- [User](uBeac.Core.Identity.Common/Entities/User.cs)
- [Role](uBeac.Core.Identity.Common/Entities/Role.cs)
- [Unit](uBeac.Core.Identity.Common/Entities/Unit.cs)
- [UnitRole](uBeac.Core.Identity.Common/Entities/UnitRole.cs)
- [UnitType](uBeac.Core.Identity.Common/Entities/UnitType.cs)
> User and Role inherits from IdentityUser and IdentityRole

<hr>

### Models
- [ChangePassword](uBeac.Core.Identity.Common/Models/User/ChangePassword.cs)
- [SignInResult](uBeac.Core.Identity.Common/Models/User/SignInResult.cs)
- [TokenResult](uBeac.Core.Identity.Common/Models/User/TokenResult.cs)
- [TwoFactorRecoveryCode](uBeac.Core.Identity.Common/Models/User/TwoFactorRecoveryCode.cs)
- [UserToken](uBeac.Core.Identity.Common/Models/User/UserToken.cs)

<hr>

### Options
- [UserOptions](uBeac.Core.Identity.Common/Options/UserOptions.cs)
- [RoleOptions](uBeac.Core.Identity.Common/Options/RoleOptions.cs)
- [UnitOptions](uBeac.Core.Identity.Common/Options/UnitOptions.cs)
- [UnitRoleOptions](uBeac.Core.Identity.Common/Options/UnitRoleOptions.cs)
- [UnitTypeOptions](uBeac.Core.Identity.Common/Options/UnitTypeOptions.cs)

<hr>

### Repositories
- [IUserRepository](uBeac.Core.Identity.Common/Repositories/IUserRepository.cs)
- [IUserTokenRepository](uBeac.Core.Identity.Common/Repositories/IUserTokenRepository.cs)
- [IRoleRepository](uBeac.Core.Identity.Common/Repositories/IRoleRepository.cs)
- [IUnitRepository](uBeac.Core.Identity.Common/Repositories/IUnitRepository.cs)
- [IUnitTypeRepository](uBeac.Core.Identity.Common/Repositories/IUnitTypeRepository.cs)
- [IUnitRoleRepository](uBeac.Core.Identity.Common/Repositories/IUnitRoleRepository.cs)
