using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<Country> Countries { get; set; }
        DbSet<City> Cities { get; set; }
        DbSet<State> States { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
