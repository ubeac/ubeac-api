using System.Reflection;
using Microsoft.AspNetCore.Mvc;

namespace uBeac.Web.Identity;

public abstract class DiscoveryControllerBase : BaseController
{
    [HttpGet]
    public IApiListResult<ActionInfo> Actions()
    {
        var assembly = Assembly.GetExecutingAssembly();

        // Get type of all controller classes from executing assembly
        var controllers = assembly.GetTypes().Where(type => typeof(BaseController).IsAssignableFrom(type)).ToList();

        // Get all action methods from controller classes
        var actionMethods = controllers.SelectMany(type =>
            type.GetMethods(BindingFlags.Instance | BindingFlags.DeclaredOnly | BindingFlags.Public)).ToList();

        return actionMethods.Select(actionMethod => new ActionInfo
        {
            ControllerName = actionMethod.DeclaringType.Name,
            ActionName = actionMethod.Name,
            AreaName = actionMethod.DeclaringType.GetAreaName()
        }).ToList().ToApiListResult();
    }
}