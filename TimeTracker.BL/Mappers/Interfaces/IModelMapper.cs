using System.Collections.Generic;
using System.Linq;

namespace TimeTracker.BL.Mappers;

public interface IModelMapper<TEntity, out TListModel, TDetailModel>
{
    TDetailModel MapToDetailModel(TEntity? entity);
    TEntity MapToEntity(TDetailModel model);
    TListModel MapToListModel(TEntity? entity);

    IEnumerable<TListModel> MapToListModel(IEnumerable<TEntity>? entities)
        => entities is null ? Enumerable.Empty<TListModel>() :
            entities.Select(MapToListModel);
}