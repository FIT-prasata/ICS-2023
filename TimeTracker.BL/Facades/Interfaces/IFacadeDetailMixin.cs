using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeTracker.DAL.Entities;
using TimeTracker.BL.Models;

namespace TimeTracker.BL.Facades.Interfaces
{
    internal interface IFacadeDetailMixin<TEntity, TDetailModel>
        where TEntity : class, IEntity
        where TDetailModel : class, IModel

    {
        Task DeleteAsync(Guid id);
        Task<TDetailModel?> GeAsync(Guid id);
        Task<TDetailModel> SaveAsync(TDetailModel model);
    }
}
