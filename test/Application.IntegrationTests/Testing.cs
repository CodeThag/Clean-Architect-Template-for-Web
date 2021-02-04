﻿using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using NUnit.Framework;
using Respawn;
using Application.Common.Enums;
using Application.Common.Interfaces;
using Infrastructure.Persistence;
using WebUI;
using Domain.Entities;

namespace Application.IntegrationTests
{
    [SetUpFixture]
    public class Testing
    {
        private static IConfigurationRoot _configuration;
        private static IServiceScopeFactory _scopeFactory;
        private static Checkpoint _checkpoint;

        [OneTimeSetUp]
        public void RunBeforeAnyTests()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", true, true)
                .AddEnvironmentVariables();

            _configuration = builder.Build();

            var startup = new Startup(_configuration);

            var services = new ServiceCollection();

            services.AddSingleton(Mock.Of<IWebHostEnvironment>(w =>
                                                                   w.EnvironmentName == "Development" &&
                                                                   w.ApplicationName == "WebUI"));

            services.AddLogging();

            startup.ConfigureServices(services);

            // Setup testing user (need to add a user to identity and use a real guid)
            var currentUserServiceDescriptor = services.FirstOrDefault(d =>
                                                                           d.ServiceType == typeof(ICurrentUserService));

            services.Remove(currentUserServiceDescriptor);

            services.AddTransient<ICurrentUserService, CurrentUserService>();

            _scopeFactory = services.BuildServiceProvider().GetService<IServiceScopeFactory>();

            _checkpoint = new Checkpoint
            {
                TablesToIgnore = new[] { "__EFMigrationsHistory" }
            };

            EnsureDatabase();
        }

        private static void EnsureDatabase()
        {
            using var scope = _scopeFactory.CreateScope();

            var context = scope.ServiceProvider.GetService<ApplicationDbContext>();

            context.Database.Migrate();
        }

        public static async Task<TResponse> SendAsync<TResponse>(IRequest<TResponse> request)
        {
            using var scope = _scopeFactory.CreateScope();

            var mediator = scope.ServiceProvider.GetService<IMediator>();

            return await mediator.Send(request);
        }

        private class CurrentUserService : ICurrentUserService
        {
            public string GetUserId()
            {
                return "userid";
            }

            public string GetUserName(string userId)
            {
                return "mawuli";
            }

            public bool UserIsInRole(string role)
            {
                return true;
            }

            public IList<string> GetCurrentUserRoles()
            {
                var list = new List<string> { "Cube" };
                return list;
            }

            public string GetUserClaim(ClaimTypes claimTypes)
            {
                return "Elvis";
            }

            public async Task<IList<string>> GetUserPermissions()
            {
                return await Task.FromResult(new List<string>());
            }

            public int GetUserOrganisationId()
            {
                throw new System.NotImplementedException();
            }

            public string GetUserName()
            {
                throw new System.NotImplementedException();
            }

            public bool UserHasRole(Roles role)
            {
                throw new System.NotImplementedException();
            }

            public Task<Organisation> GetOrganisation()
            {
                throw new System.NotImplementedException();
            }
        }

        private static string _currentUserId;


        public static async Task ResetState()
        {
            await _checkpoint.Reset(_configuration.GetConnectionString("DefaultConnection"));
            _currentUserId = null;
        }

        public static async Task<T> FindAsync<T>(int id)
            where T : class
        {
            using var scope = _scopeFactory.CreateScope();

            var context = scope.ServiceProvider.GetService<ApplicationDbContext>();

            return await context.FindAsync<T>(id);
        }

        [OneTimeTearDown]
        public void RunAfterAnyTests()
        {
        }
    }
}