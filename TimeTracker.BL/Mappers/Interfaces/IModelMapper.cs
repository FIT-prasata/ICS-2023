using System.Collections.Generic;
using System.Linq;

namespace TimeTracker.BL.Mappers.Interfaces;

public interface IModelDetailMapper<TEntity, TDetailModel>
{
    TDetailModel MapToDetailModel(TEntity? entity);
    TEntity MapToEntity(TDetailModel model);
}