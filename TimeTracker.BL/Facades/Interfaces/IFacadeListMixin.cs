using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeTracker.DAL.Entities;
using TimeTracker.BL.Models;

namespace TimeTracker.BL.Facades.Interfaces
{
    internal interface IFacadeListMixin<TEntity, TListModel>
        where TEntity : class, IEntity
        where TListModel : IModel

    {
        Task<TListModel> GeAsync(Guid id);

    }
}