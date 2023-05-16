using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TimeTracker.BL.Mappers;
using TimeTracker.BL.Models;
using TimeTracker.DAL.Entities;
using TimeTracker.DAL.Mappers;
using TimeTracker.DAL.Repositories;
using TimeTracker.DAL.UnitOfWork;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace TimeTracker.BL.Facades
{
    public class FacadeBase<TEntity, TListModel, TDetailModel , TEntityMapper>: IFacade<TEntity, TListModel, TDetailModel>
        where TEntity : class, IEntity
        where TDetailModel : class, IModel
        where TListModel : IModel
        where TEntityMapper : IEntityMapper<TEntity>, new()
    {
        protected readonly IModelMapper<TEntity,TListModel, TDetailModel> Mapper;
        protected readonly IUnitOfWorkFactory UnitOfWorkFactory;

        protected FacadeBase(IModelMapper<TEntity,TListModel, TDetailModel> mapper, IUnitOfWorkFactory unitOfWorkFactory)
        {
            Mapper = mapper;
            UnitOfWorkFactory = unitOfWorkFactory;
        }

        protected virtual List<string> IncludesNavigationPathDetail => new();

        public async Task DeleteAsync(Guid id)
        {
            await using IUnitOfWork uow = UnitOfWorkFactory.Create();
            try
            {
                uow.GetRepository<TEntity, TEntityMapper>().Delete(id);
                await uow.CommitAsync().ConfigureAwait(false);

            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Deletion failed.", ex);
            }
        }

        public virtual async Task<TDetailModel?> GetAsync(Guid id)
        {
            await using IUnitOfWork uow = UnitOfWorkFactory.Create();

            IQueryable<TEntity> query = uow.GetRepository<TEntity, TEntityMapper>().Get();

            if (IncludesNavigationPathDetail.Any())
            {
                IncludesNavigationPathDetail.ForEach(include => query = query.Include(include));
            }


            var entity = await query.SingleOrDefaultAsync(e => e.Id == id);

            return entity is null ?
                null :
                Mapper.MapToDetailModel(entity);
        }

        public virtual async Task<TDetailModel> SaveAsync(TDetailModel model)
        {
            TDetailModel result;

            GuardCollectionsAreNotSet(model);

            TEntity entity = Mapper.MapToEntity(model);

            IUnitOfWork uow = UnitOfWorkFactory.Create();
            IRepository<TEntity> repository = uow.GetRepository<TEntity, TEntityMapper>();

            if (await repository.ExistsAsync(entity))
            {
                TEntity updatedEntity = await repository.UpdateAsync(entity);
                result = Mapper.MapToDetailModel(updatedEntity);
            }
            else
            {
                if (entity.Id == Guid.Empty)
                {
                    entity.Id = Guid.NewGuid();
                }
                TEntity insertedEntity = await repository.InsertAsync(entity);
                result = Mapper.MapToDetailModel(insertedEntity);
            }

            await uow.CommitAsync();

            return result;
        }
        public static void GuardCollectionsAreNotSet(TDetailModel model)
        {
            IEnumerable<PropertyInfo> collectionProperties = model
                .GetType()
                .GetProperties()
                .Where(i => typeof(ICollection).IsAssignableFrom(i.PropertyType));

            foreach (PropertyInfo collectionProperty in collectionProperties)
            {
                if (collectionProperty.GetValue(model) is ICollection { Count: > 0 })
                {
                    throw new InvalidOperationException(
                        "Current BL and DAL infrastructure disallows insert or update of models with adjacent collections.");
                }
            }
        }

        public virtual async Task<IEnumerable<TListModel>> GetAsync()
        {
            await using IUnitOfWork uow = UnitOfWorkFactory.Create();
            IQueryable<TEntity> query = uow
                .GetRepository<TEntity, TEntityMapper>()
                .Get();

            if (IncludesNavigationPathDetail.Any())
            {
                IncludesNavigationPathDetail.ForEach(include => query = query.Include(include));
            }

            List<TEntity> entities = await query.ToListAsync();

            return Mapper.MapToListModel(entities);
        }

    }
}
