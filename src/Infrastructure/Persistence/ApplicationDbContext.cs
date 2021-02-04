using System;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Configuration;
using Application.Common.Interfaces;
using Domain.Common;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Infrastructure.Persistence
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>, IApplicationDbContext
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IDateTime _dateTime;
        private IDbContextTransaction _currentTransaction;

        public ApplicationDbContext(DbContextOptions options, ICurrentUserService currentUserService, IDateTime dateTime, IConfiguration config) : base(options)
        {
            _currentUserService = currentUserService;
            _dateTime = dateTime;
        }

        public DbSet<ApplicationType> ApplicationTypes { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Gender> Genders { get; set; }
        public DbSet<MenuCollection> MenuCollections { get; set; }
        public DbSet<MenuItem> MenuItems { get; set; }
        public DbSet<Organisation> Organisations { get; set; }
        public DbSet<OrganisationType> OrganisationTypes { get; set; }
        public DbSet<PaymentType> PaymentTypes { get; set; }
        public DbSet<PaymentPurpose> PaymentPurposes { get; set; }
        public DbSet<PaymentPurposeFixedLineItem> PaymentPurposeFixedLineItems { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Setting> Settings { get; set; }
        public DbSet<State> States { get; set; }
        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<WorkflowScheme> WorkflowSchemes { get; set; }
        public DbSet<WorkflowInbox> WorkflowInboxes { get; set; }
        public DbSet<NotificationTemplate> NotificationTemplates { get; set; }
        public DbSet<SystemNotification> SystemNotifications { get; set; }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            var userId = _currentUserService.GetUserId() ?? "anonymous";

            foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
            {
                if (entry.State == EntityState.Added)
                {
                    if (string.IsNullOrEmpty(entry.Entity.CreatedBy))
                    {
                        entry.Entity.CreatedBy = userId;
                    }

                    entry.Entity.Created = _dateTime.Now;
                }

                if (entry.State == EntityState.Added || entry.State == EntityState.Modified)
                {
                    if (string.IsNullOrEmpty(entry.Entity.LastModifiedBy))
                    {
                        entry.Entity.LastModifiedBy = userId;
                    }

                    entry.Entity.LastModified = _dateTime.Now;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }

        public async Task BeginTransactionAsync()
        {
            if (_currentTransaction != null) return;

            _currentTransaction = await base.Database.BeginTransactionAsync(IsolationLevel.ReadCommitted).ConfigureAwait(false);
        }

        public async Task CommitTransactionAsync()
        {
            try
            {
                await SaveChangesAsync().ConfigureAwait(false);
                _currentTransaction?.Commit();
            }
            catch
            {
                RollbackTransaction();
                throw;
            }
            finally
            {
                if (_currentTransaction != null)
                {
                    _currentTransaction.Dispose();
                    _currentTransaction = null;
                }
            }
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(builder);
            // Prevent Cascade on Delete

            builder.Entity<MenuItem>()
                .HasOne(x => x.Parent)
                .WithMany(x => x.Children)
                .OnDelete(DeleteBehavior.NoAction);
        }

        public void RollbackTransaction()
        {
            try
            {
                _currentTransaction?.Rollback();
            }
            finally
            {
                if (_currentTransaction != null)
                {
                    _currentTransaction.Dispose();
                    _currentTransaction = null;
                }
            }
        }
    }
}