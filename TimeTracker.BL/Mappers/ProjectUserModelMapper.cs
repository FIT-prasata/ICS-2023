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
    public class ProjectUserModelMapper<TEntity, TListModel> : IModelListMapper<TEntity, TListModel>
        where TEntity : class, IEntity
        where TListModel : IModel
    {
        public TListModel MapToListModel(TEntity? entity)
        {
            throw new NotImplementedException();
        }
    }
}



