using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeTracker.BL.Mappers.Interfaces;
using TimeTracker.DAL.Entities;
using TimeTracker.BL.Models;

namespace TimeTracker.BL.Mappers
{
    public class ActivityModelMapper<TEntity, TDetailModel, TListModel>: IModelDetailMapper<TEntity, TDetailModel>, IModelListMapper<TEntity, TListModel>
    where TEntity : class, IEntity
    where TDetailModel : class, IModel
    where TListModel : IModel
    {
        public TDetailModel MapToDetailModel(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public TEntity MapToEntity(TDetailModel model)
        {
            throw new NotImplementedException();
        }

        public TListModel MapToListModel(TEntity? entity)
        {
            throw new NotImplementedException();
        }
    }
}
