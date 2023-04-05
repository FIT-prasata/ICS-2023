using System;
using System.Threading.Tasks;
using TimeTracker.DAL.Entities;
using TimeTracker.DAL.Mappers;
using TimeTracker.DAL.Repositories;

namespace TimeTracker.DAL.UnitOfWork;

public interface IUnitOfWork : IAsyncDisposable
{
    IRepository<TEntity> GetRepository<TEntity, TEntityMapper>()
        where TEntity : class, IEntity
        where TEntityMapper : IEntityMapper<TEntity>, new();

    Task CommitAsync();
}
