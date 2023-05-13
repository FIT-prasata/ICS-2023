using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeTracker.DAL.Entities;
using TimeTracker.BL.Models;

namespace TimeTracker.BL.Facades.Interfaces
{
    public interface IFacade<TEntity, TDetailModel, TListModel>
        where TEntity : class, IEntity
        where TDetailModel : class, IModel
        where TListModel : IModel

    {
        Task DeleteAsync(Guid id);
        Task<TDetailModel?> GetAsync(Guid id);
        Task<TDetailModel> SaveAsync(TDetailModel model);
        Task<IEnumerable<TListModel>> GetAsync();

    }
}
