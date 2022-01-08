namespace OMS.Profile.Application.Common.Interfaces;

public interface IDatabaseContext
{
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}