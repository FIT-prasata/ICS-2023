using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeTracker.BL.Mappers.Interfaces;
using TimeTracker.DAL.Entities;
using TimeTracker.DAL.Mappers;
using TimeTracker.DAL.UnitOfWork;

namespace TimeTracker.BL.Facades
{
    public class FacadeBase<TEntity, TEntityMapper>
        where TEntity : class, IEntity
        where TEntityMapper : IEntityMapper<TEntity>, new()
    {
        protected readonly IUnitOfWorkFactory UnitOfWorkFactory;

        protected FacadeBase(IUnitOfWorkFactory unitOfWorkFactory)
        {
            UnitOfWorkFactory = unitOfWorkFactory;
        }

        protected virtual List<string> IncludesNavigationPathDetail => new();
    }
}
