using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeTracker.DAL.Entities;
using TimeTracker.DAL.Mappers;

namespace TimeTracker.BL.Facades
{
    internal class FacadeBaseFacadeDetailMixin<TEntity, TEntityMapper>
        where TEntity : class, IEntity
        where TEntityMapper : IEntityMapper<TEntity>, new()
    {
    }
}
