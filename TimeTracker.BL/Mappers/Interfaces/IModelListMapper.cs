using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeTracker.DAL.Entities;

namespace TimeTracker.BL.Mappers.Interfaces
{
    public interface IModelListMapper<in TEntity, out TListModel>
    {
        TListModel MapToListModel(TEntity? entity);

        IEnumerable<TListModel> MapToListModel(IEnumerable<TEntity> entities)
            => entities.Select(MapToListModel);
    }
}
