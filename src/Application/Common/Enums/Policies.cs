using Application.Common.Attributes;

namespace Application.Common.Enums
{
    public enum Policies
    {
        [PolicyClaimValues("CanListMenuCollection", Roles.Administrator, Permission.MenuCollectionList)]
        CanListMenuCollection,
        [PolicyClaimValues("CanCreateMenuCollection", Roles.Administrator, Permission.MenuCollectionCreate)]
        CanCreateMenuCollection,
        [PolicyClaimValues("CanEditMenuCollection", Roles.Administrator, Permission.MenuCollectionEdit)]
        CanEditMenuCollection,
        [PolicyClaimValues("CanDeleteMenuCollection", Roles.Administrator, Permission.MenuCollectionDelete)]
        CanDeleteMenuCollection,



        //[PolicyClaimValues("CanUpdateTopResource", Roles.CubeAdmin, Permission.UpdateTopResource)]
        //CanUpdateTopResource,
        //[PolicyClaimValues("CanDeleteTopResource", Roles.CubeAdmin, Permission.DeleteTopResource)]
        //CanDeleteTopResource,
        //[PolicyClaimValues("CanAddTopResource", Roles.CubeAdmin, Permission.AddTopResource)]
        //CanAddTopResource,
        //[PolicyClaimValues("CanListTopResource", Roles.CubeAdmin, new[] { Permission.UpdateTopResource, Permission.DeleteTopResource, Permission.AddTopResource, Permission.ListTopResource })]
        //CanListTopResource,
        //[PolicyClaimValues("CanAddResource", Roles.CubeAdmin, Permission.AddResource)]
        //CanAddResource,
        //[PolicyClaimValues("CanUpdateResource", Roles.CubeAdmin, Permission.UpdateResource)]
        //CanUpdateResource,
        //[PolicyClaimValues("CanDeleteResource", Roles.CubeAdmin, Permission.DeleteResource)]
        //CanDeleteResource,
        //[PolicyClaimValues("CanListResource", Roles.CubeAdmin, new[] { Permission.AddResource, Permission.UpdateResource, Permission.DeleteResource, Permission.ListResource })]
        //CanListResource,
        //[PolicyClaimValues("CanListResourceArea", Roles.CubeAdmin, new[] { Permission.UpdateResourceArea, Permission.DeleteResourceArea, Permission.AddResourceArea, Permission.ListResourceArea })]
        //CanListResourceArea,
        //[PolicyClaimValues("CanAddResourceArea", Roles.CubeAdmin, Permission.AddResourceArea)]
        //CanAddResourceArea,
        //[PolicyClaimValues("CanUpdateResourceArea", Roles.CubeAdmin, Permission.UpdateResourceArea)]
        //CanUpdateResourceArea,
        //[PolicyClaimValues("CanDeleteResourceArea", Roles.CubeAdmin, Permission.DeleteResourceArea)]
        //CanDeleteResourceArea,
        //[PolicyClaimValues("CanAddPermission", Roles.CubeAdmin, Permission.AddPermission)]
        //CanAddPermission,
        //[PolicyClaimValues("CanUpdatePermission", Roles.CubeAdmin, Permission.UpdatePermission)]
        //CanUpdatePermission,
        //[PolicyClaimValues("CanDeletePermission", Roles.CubeAdmin, Permission.DeletePermission)]
        //CanDeletePermission,
        //[PolicyClaimValues("CanListPermission", Roles.CubeAdmin, new[] { Permission.AddPermission, Permission.UpdatePermission, Permission.DeletePermission, Permission.ListPermission })]
        //CanListPermission,
        //[PolicyClaimValues("CanAddRequiredDocument", new[] { Roles.CubeAdmin, Roles.CubeClientAdmin, Roles.CubeClientUser }, Permission.AddRequiredDocument)]
        //CanAddRequiredDocument,
        //[PolicyClaimValues("CanUpdateRequiredDocument", new[] { Roles.CubeAdmin, Roles.CubeClientAdmin, Roles.CubeClientUser }, Permission.UpdateRequiredDocument)]
        //CanUpdateRequiredDocument,
        //[PolicyClaimValues("CanDeleteRequiredDocument", new[] { Roles.CubeAdmin, Roles.CubeClientAdmin, Roles.CubeClientUser }, Permission.DeleteRequiredDocument)]
        //CanDeleteRequiredDocument,
        //[PolicyClaimValues("CanListRequiredDocument", new[] { Roles.CubeAdmin, Roles.CubeClientAdmin, Roles.CubeClientUser }, new[] { Permission.AddRequiredDocument, Permission.UpdateRequiredDocument, Permission.DeleteRequiredDocument, Permission.ListRequiredDocument })]
        //CanListRequiredDocument,
        //[PolicyClaimValues("CanAddProcess", Roles.CubeAdmin, Permission.AddProcess)]
        //CanAddProcess,
        //[PolicyClaimValues("CanUpdateProcess", new[] { Roles.CubeAdmin, Roles.CubeClientAdmin, Roles.CubeClientUser }, Permission.UpdateProcess)]
        //CanUpdateProcess,
        //[PolicyClaimValues("CanDeleteProcess", Roles.CubeAdmin, Permission.DeleteProcess)]
        //CanDeleteProcess,
        //[PolicyClaimValues("CanListProcess", new[] { Roles.CubeAdmin, Roles.CubeClientAdmin, Roles.CubeClientUser }, new[] { Permission.AddProcess, Permission.UpdateProcess, Permission.DeleteRequiredDocument, Permission.ListRequiredDocument })]
        //CanListProcess,
        //[PolicyClaimValues("CanAddProcessRequiredDocument", new[] { Roles.CubeAdmin, Roles.CubeClientAdmin, Roles.CubeClientUser }, Permission.AddProcessRequiredDocument)]
        //CanAddProcessRequiredDocument,
        //[PolicyClaimValues("CanUpdateProcessRequiredDocument", Roles.CubeAdmin, Permission.UpdateProcessRequiredDocument)]
        //CanUpdateProcessRequiredDocument,
        //[PolicyClaimValues("CanDeleteProcessRequiredDocument", new[] { Roles.CubeAdmin, Roles.CubeClientAdmin, Roles.CubeClientUser }, Permission.DeleteProcessRequiredDocument)]
        //CanDeleteProcessRequiredDocument,
        //[PolicyClaimValues("CanListProcessRequiredDocument", new[] { Roles.CubeAdmin, Roles.CubeClientAdmin, Roles.CubeClientUser }, new[] { Permission.AddProcessRequiredDocument, Permission.UpdateProcessRequiredDocument, Permission.DeleteProcessRequiredDocument, Permission.ListProcessRequiredDocument })]
        //CanListProcessRequiredDocument,
        //[PolicyClaimValues("CanApproveChamberRegistration", Roles.CubeAdmin, Permission.ApproveChamberRegistration)]
        //CanApproveChamberRegistration,
        //[PolicyClaimValues("CanListChamberRegistration", Roles.CubeAdmin, new[] { Permission.ListChamberRegistration, Permission.ApproveChamberRegistration })]
        //CanListChamberRegistration,
        //[PolicyClaimValues("CanListOrganisationalUsers", Roles.CubeClientAdmin, new[] { Permission.ListOrganisationalUsers, Permission.ViewOrganisationalUserDetails, Permission.AddOrganisationalUser })]
        //CanListOrganisationalUsers,
        //[PolicyClaimValues("CanViewOrganisationalUserDetails", new[] { Roles.CubeAdmin, Roles.CubeClientAdmin, Roles.CubeClientUser }, Permission.ViewOrganisationalUserDetails)]
        //CanViewOrganisationalUserDetails,
        //[PolicyClaimValues("CanAddOrganisationalUser", Roles.CubeClientAdmin, Permission.AddOrganisationalUser)]
        //CanAddOrganisationalUser,
        //[PolicyClaimValues("CanAddWorkflowScheme", new[] { Roles.CubeAdmin, Roles.CubeClientAdmin, Roles.CubeClientUser }, Permission.AddWorkflowScheme)]
        //CanAddWorkflowScheme,
        //[PolicyClaimValues("CanListApprovalInbox", new[] { Roles.CubeAdmin, Roles.CubeClientAdmin, Roles.CubeClientUser }, Permission.ListApprovalInbox)]
        //CanListApprovalInbox,
        //[PolicyClaimValues("CanAddOrganisationApprovalRole", new[] { Roles.CubeAdmin, Roles.CubeClientAdmin, Roles.CubeClientUser }, Permission.AddOrganisationApprovalRole)]
        //CanAddOrganisationApprovalRole,
        //[PolicyClaimValues("CanListOrganisationApprovalRole", new[] { Roles.CubeAdmin, Roles.CubeClientAdmin, Roles.CubeClientUser }, new[] { Permission.AddOrganisationApprovalRole, Permission.UpdateOrganisationApprovalRole, Permission.DeleteOrganisationApprovalRole, Permission.ListOrganisationApprovalRole })]
        //CanListOrganisationApprovalRole,
        //[PolicyClaimValues("CanUpdateOrganisationApprovalRole", new[] { Roles.CubeAdmin, Roles.CubeClientAdmin, Roles.CubeClientUser }, Permission.UpdateOrganisationApprovalRole)]
        //CanUpdateOrganisationApprovalRole,
        //[PolicyClaimValues("CanDeleteOrganisationApprovalRole", new[] { Roles.CubeAdmin, Roles.CubeClientAdmin, Roles.CubeClientUser }, Permission.DeleteOrganisationApprovalRole)]
        //CanDeleteOrganisationApprovalRole,
        //[PolicyClaimValues("CanAssignUserPermissions", new[] { Roles.CubeAdmin, Roles.CubeClientAdmin, Roles.CubeClientUser }, Permission.AssignPermission)]
        //CanAssignUserPermissions
    }
}
