using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TimeTracker.BL.Facades.Interfaces;
using TimeTracker.BL.Mappers.Interfaces;
using TimeTracker.DAL.Mappers;
using TimeTracker.BL.Models;
using TimeTracker.DAL.Entities;
using TimeTracker.DAL.UnitOfWork;



namespace TimeTracker.BL.Facades
{
    public abstract class FacadeListMixin<TEntity, TListModel, TEntityMapper> :FacadeBase<TEntity, TEntityMapper>, IFacadeListMixin<TEntity, TListModel>
        where TEntity : class, IEntity
        where TListModel : IModel
        where TEntityMapper : IEntityMapper<TEntity>, new()
    {

        protected readonly IModelListMapper<TEntity, TListModel> Mapper;

        protected FacadeListMixin(IUnitOfWorkFactory unitOfWorkFactory, IModelListMapper<TEntity, TListModel> mapper)
            : base(unitOfWorkFactory)
        {
            Mapper = mapper;
        }

        public virtual async Task<IEnumerable<TListModel>> GetAsync()
        {
            await using IUnitOfWork uow = UnitOfWorkFactory.Create();
            List<TEntity> entities = await uow
                .GetRepository<TEntity, TEntityMapper>()
                .Get()
                .ToListAsync();

            return Mapper.MapToListModel(entities);
        }
    }
}
