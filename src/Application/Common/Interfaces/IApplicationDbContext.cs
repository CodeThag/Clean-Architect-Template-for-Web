using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<ApplicationType> ApplicationTypes { get; set; }
        DbSet<City> Cities { get; set; }
        DbSet<Country> Countries { get; set; }
        DbSet<Department> Departments { get; set; }
        DbSet<Gender> Genders { get; set; }
        DbSet<MenuCollection> MenuCollections { get; set; }
        DbSet<MenuItem> MenuItems { get; set; }
        DbSet<Organisation> Organisations { get; set; }
        DbSet<OrganisationType> OrganisationTypes { get; set; }
        DbSet<PaymentType> PaymentTypes { get; set; }
        DbSet<PaymentPurpose> PaymentPurposes { get; set; }
        DbSet<PaymentPurposeFixedLineItem> PaymentPurposeFixedLineItems { get; set; }
        DbSet<Post> Posts { get; set; }
        DbSet<Setting> Settings { get; set; }
        DbSet<State> States { get; set; }
        DbSet<UserProfile> UserProfiles { get; set; }
        DbSet<WorkflowScheme> WorkflowSchemes { get; set; }
        DbSet<WorkflowInbox> WorkflowInboxes { get; set; }
        DbSet<NotificationTemplate> NotificationTemplates { get; set; }
        DbSet<SystemNotification> SystemNotifications { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
