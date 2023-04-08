using System.Collections.Generic;
using System.Linq;

namespace TimeTracker.BL.Mappers.Interfaces;

public interface IModelMapper<TEntity, TDetailModel, out TListModel>
{
    TDetailModel MapToDetailModel(TEntity? entity);
    TEntity MapToEntity(TDetailModel model);
    TListModel MapToListModel(TEntity? entity);

    IEnumerable<TListModel> MapToListModel(IEnumerable<TEntity>? entities)
        => entities is null ? Enumerable.Empty<TListModel>() :
            entities.Select(MapToListModel);
}