using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeTracker.BL.Facades.Interfaces;
using TimeTracker.DAL.Entities;
using TimeTracker.BL.Models;
using TimeTracker.DAL.Mappers;

namespace TimeTracker.BL.Facades
{
    internal class FacadeDetailMixin<TEntity, TDetailModel, TEntityMapper>: IFacadeDetailMixin<TEntity, TDetailModel>
    where TEntity : class, IEntity
    where TDetailModel : class, IModel
    where TEntityMapper: IEntityMapper<TEntity>, new()
    {
        public async Task DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<TDetailModel> GeAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<TDetailModel> SaveAsync(TDetailModel model)
        {
            throw new NotImplementedException();
        }
    }
}
