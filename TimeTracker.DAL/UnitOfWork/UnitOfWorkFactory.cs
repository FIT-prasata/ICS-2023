using Microsoft.EntityFrameworkCore;

namespace TimeTracker.DAL.UnitOfWork;

public class UnitOfWorkFactory : IUnitOfWorkFactory
{
    private readonly IDbContextFactory<TimeTrackerDbContext> _dbContextFactory;

    public UnitOfWorkFactory(IDbContextFactory<TimeTrackerDbContext> dbContextFactory) =>
        _dbContextFactory = dbContextFactory;

    public IUnitOfWork Create() => new UnitOfWork(_dbContextFactory.CreateDbContext());
}