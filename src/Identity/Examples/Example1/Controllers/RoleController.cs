﻿using uBeac.Identity;
using uBeac.Web.Identity;

namespace Example1.Controllers
{
    public class RoleController : RoleControllerBase<Role>
    {
        public RoleController(IRoleService<Role> roleService) : base(roleService)
        {
        }
    }
}