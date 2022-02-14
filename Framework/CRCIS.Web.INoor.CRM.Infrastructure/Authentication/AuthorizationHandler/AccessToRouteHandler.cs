using CRCIS.Web.INoor.CRM.Contract.Service;
using CRCIS.Web.INoor.CRM.Infrastructure.Authentication.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRCIS.Web.INoor.CRM.Infrastructure.Authentication.AuthorizationHandler
{
    //public class AccessToRouteHandler : AuthorizationHandler<AccessToRouteRequirement>
    //{
    //    private readonly IHttpContextAccessor _httpContextAccessor;
    //    private readonly IPermissionService _permissionService;
    //    public AccessToRouteHandler(IHttpContextAccessor httpContextAccessor, IPermissionService permissionService)
    //    {
    //        _httpContextAccessor = httpContextAccessor;
    //        _permissionService = permissionService;
    //    }

    //    public override Task HandleAsync(AuthorizationHandlerContext context)
    //    {
    //        return base.HandleAsync(context);
    //    }

    //    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, AccessToRouteRequirement requirement)
    //    {
    //        var filterContext = context.Resource as AuthorizationFilterContext;
    //        var response = filterContext?.HttpContext.Response;
    //        var routeInfo = context.Resource as RouteEndpoint;

    //        if (context.User.Identity.IsAuthenticated ==false)
    //        {
    //            response?.OnStarting(async () =>
    //            {
    //                filterContext.HttpContext.Response.StatusCode = (int)System.Net.HttpStatusCode.Unauthorized;
    //            });

    //            context.Fail();
    //            return ;
    //        }
    //        if (context.Resource is Endpoint endpoint)
    //        {
    //            var cad = endpoint.Metadata.OfType<ControllerActionDescriptor>().FirstOrDefault();

    //            var controllerFullName = cad.ControllerTypeInfo.FullName;
    //            var actionName = cad.ActionName;
    //            var bindings = cad.Parameters;
    //            var actionParams = ".";

    //            var verb = _httpContextAccessor.HttpContext.Request.Method;
    //            var routeKey = string.Empty;

    //            if (bindings.Any())
    //            {
    //                bindings.ToList().ForEach(p => actionParams += p.ParameterType.Name + ".");
    //            }

    //            routeKey = $"{controllerFullName}.{actionName}{actionParams}{verb}";
    //        }





    //        var adminId = _httpContextAccessor.HttpContext.User.Identity.GetAdminId();
    //    }
    //}
}
