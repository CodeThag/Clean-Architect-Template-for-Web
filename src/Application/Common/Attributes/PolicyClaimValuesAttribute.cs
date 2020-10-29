using System;
using System.Collections.Generic;
using Application.Common.Enums;

namespace Application.Common.Attributes
{
    public class PolicyClaimValuesAttribute : Attribute
    {
        public string PolicyName { get; set; }
        public IList<Roles> RequiredRoles { get; set; }
        public IList<Enums.Permission> RequiredPermissions { get; set; }

        public PolicyClaimValuesAttribute(string policyName, Roles[] requiredRoles, Enums.Permission[] requiredPermissions)
        {
            this.PolicyName = policyName;
            this.RequiredPermissions = requiredPermissions;
            this.RequiredRoles = requiredRoles;
        }

        public PolicyClaimValuesAttribute(string policyName, Roles requiredRole, Enums.Permission requiredPermission)
        {
            this.PolicyName = policyName;
            this.RequiredPermissions = new List<Enums.Permission>() { requiredPermission };
            this.RequiredRoles = new List<Roles>() { requiredRole };
        }

        public PolicyClaimValuesAttribute(string policyName, Roles requiredRole, Enums.Permission[] requiredPermissions)
        {
            this.PolicyName = policyName;
            this.RequiredPermissions = requiredPermissions;
            this.RequiredRoles = new List<Roles>() { requiredRole };
        }

        public PolicyClaimValuesAttribute(string policyName, Roles[] requiredRoles, Enums.Permission requiredPermission)
        {
            this.PolicyName = policyName;
            this.RequiredPermissions = new List<Enums.Permission>() { requiredPermission };
            this.RequiredRoles = requiredRoles;
        }
    }
}
