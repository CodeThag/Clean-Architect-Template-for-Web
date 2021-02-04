using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Common.Enums;
using Application.Common.Helper;
using Microsoft.AspNetCore.Authorization;

namespace WebUI.Dependencies
{
    public static class PolicyAuthorizationDependencyInjection
    {
        public static AuthorizationOptions AddAuthorizationPolicy(this AuthorizationOptions options)
        {
            foreach (var policy in EnumHelper.GetValues<Policies>())
            {
                var policyClaims = policy.GetPolicyClaimValues();

                options.AddPolicy(policyClaims.PolicyName, // The name of the policy here
                    builder =>
                    {
                        // Ensure the current user is logged in
                        builder.RequireAuthenticatedUser();

                        // Put the permission here
                        var roles = policyClaims.RequiredRoles;
                        var strRoles = roles.Select(r => r.GetAttributeStringValue()).ToList();
                        builder.RequireRole(strRoles);

                        // Add permission
                        var permissions = policyClaims.RequiredPermissions;
                        var strPermission = permissions.Select(p => p.GetAttributeStringValue()).ToList();
                        builder.RequireClaim(ClaimTypes.Permissions.GetAttributeStringValue(), strPermission);
                    });
            }

            return options;
        }
    }
}
