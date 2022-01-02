using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using uBeac.Identity;
using uBeac.Web.Identity;

namespace Example1.Controllers
{
    public class RoleController : RoleControllerBase<Role>
    {
        public RoleController(IRoleService<Role> roleService) : base(roleService)
        {
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task Test()
        {
            var insertTasks = new List<Task>();

            for (int i = 0; i < 10; i++)
            {
                var role = new Role { Name = $"Role{i}" };
                insertTasks.Add(RoleService.Insert(role));
            }
            await Task.WhenAll(insertTasks);

            var roles = await RoleService.GetAll();

            var updateTasks = new List<Task>();
            foreach (var role in roles)
            {
                role.Name += "updated";
                updateTasks.Add(RoleService.Update(role));
            }

            await Task.WhenAll(updateTasks);

            roles = await RoleService.GetAll();

            var deleteTasks = new List<Task>();
            foreach (var role in roles)
            {
                await RoleService.Delete(role.Id);
                deleteTasks.Add(RoleService.Delete(role.Id));
            }

            await Task.WhenAll(deleteTasks);

        }
    }
}
