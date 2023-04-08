using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeTracker.BL.Facades.Interfaces;
using TimeTracker.DAL.Mappers;
using TimeTracker.BL.Models;
using TimeTracker.DAL.Entities;



namespace TimeTracker.BL.Facades
{
    public abstract class FacadeListMixin<TEntity, TListModel, TEntityMapper> : IFacadeListMixin<TEntity, TListModel>
        where TEntity : class, IEntity
        where TListModel : IModel
        where TEntityMapper : IEntityMapper<TEntity>, new()
    {
        public async Task<TListModel> GeAsync(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
