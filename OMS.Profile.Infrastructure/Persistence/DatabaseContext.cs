using System.Reflection;
using Microsoft.EntityFrameworkCore;
using OMS.Profile.Application.Common.Interfaces;
using OMS.Profile.Domain.Common;
using OMS.Profile.Infrastructure.Services;

namespace OMS.Profile.Infrastructure.Persistence;

public class DatabaseContext : DbContext, IDatabaseContext
{
    private readonly IDomainEventService _domainEventService;

    public DatabaseContext(DbContextOptions options, IDomainEventService domainEventService) : base(options)
    {
        _domainEventService = domainEventService;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(modelBuilder);
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        foreach (var entry in ChangeTracker.Entries<BaseEntity>())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.CreationDate = DateTime.UtcNow;
                    break;
                case EntityState.Modified:
                    entry.Entity.LastModifiedDate = DateTime.UtcNow;
                    break;
                default:
                    break;
            }
        }

        int result = await base.SaveChangesAsync(cancellationToken);
        await DispatchEvents();
        return result;
    }

    private async Task DispatchEvents()
    {
        while (true)
        {
            var domainEventEntity = ChangeTracker
                .Entries<IHasDomainEvent>()
                .Select(x => x.Entity.DomainEvents)
                .SelectMany(x => x)
                .FirstOrDefault(domainEvent => !domainEvent.IsPublished);

            if (domainEventEntity == null) break;

            domainEventEntity.IsPublished = true;
            await _domainEventService.Publish(domainEventEntity);
        }
    }
}