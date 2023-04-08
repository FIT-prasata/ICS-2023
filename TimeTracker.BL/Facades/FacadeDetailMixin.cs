using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeTracker.BL.Facades.Interfaces;
using TimeTracker.BL.Mappers.Interfaces;
using TimeTracker.DAL.Entities;
using TimeTracker.BL.Models;
using TimeTracker.DAL.Mappers;
using TimeTracker.DAL.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using TimeTracker.DAL.Repositories;
using System.Collections;
using System.Reflection;

namespace TimeTracker.BL.Facades
{
    public abstract class FacadeDetailMixin<TEntity, TDetailModel, TEntityMapper> : FacadeBase<TEntity, TEntityMapper>, IFacadeDetailMixin<TEntity, TDetailModel>
    where TEntity : class, IEntity
    where TDetailModel : class, IModel
    where TEntityMapper : IEntityMapper<TEntity>, new()
    {
        protected readonly IModelDetailMapper<TEntity, TDetailModel> Mapper;

        protected FacadeDetailMixin(IUnitOfWorkFactory unitOfWorkFactory, IModelDetailMapper<TEntity, TDetailModel> mapper)
            : base(unitOfWorkFactory)
        {
            Mapper = mapper;
        }

        public async Task DeleteAsync(Guid id)
        {
            await using IUnitOfWork uow = UnitOfWorkFactory.Create();
            try
            {
                uow.GetRepository<TEntity, TEntityMapper>().Delete(id);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Deletion failed.", ex);
            }
        }

        public virtual async Task<TDetailModel?> GeAsync(Guid id)
        {
            await using IUnitOfWork uow = UnitOfWorkFactory.Create();

            IQueryable<TEntity> query = uow.GetRepository<TEntity, TEntityMapper>().Get();

            if (IncludesNavigationPathDetail.Any())
            {
                IncludesNavigationPathDetail.ForEach(include => query = query.Include(include)); // TODO: may explode, good testing needed
            }

            TEntity? entity = await query.SingleOrDefaultAsync(e => e.Id == id);

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
                entity.Id = Guid.NewGuid();
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
    }
}
