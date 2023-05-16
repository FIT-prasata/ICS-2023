using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;
using Microsoft.IdentityModel.Tokens;
using System.Diagnostics;
using TimeTracker.BL.Enums;
using TimeTracker.BL.Mappers;
using TimeTracker.BL.Models;
using TimeTracker.DAL.Entities;
using TimeTracker.DAL.Mappers;
using TimeTracker.DAL.UnitOfWork;

namespace TimeTracker.BL.Facades
{
    public class ActivityFacade : FacadeBase<ActivityEntity, ActivityListModel, ActivityDetailModel, ActivityEntityMapper>, IActivityFacade
    {
        public ActivityFacade(IActivityModelMapper mapper, IUnitOfWorkFactory uow) : base(mapper, uow)
        {
        }
        protected override List<string> IncludesNavigationPathDetail => new()
        {
            $"{nameof(ActivityEntity.Assigned)}",
            $"{nameof(ActivityEntity.CreatedBy)}",
        };

        private IQueryable<ActivityEntity> GetQuery(IUnitOfWork uow)
        {
            IQueryable<ActivityEntity> query = uow.GetRepository<ActivityEntity, ActivityEntityMapper>().Get();
            IncludesNavigationPathDetail.ForEach(include => query = query.Include(include));
            return query;

        }

        public override async Task<ActivityDetailModel> SaveAsync(ActivityDetailModel model)
        {
            await using IUnitOfWork uow = UnitOfWorkFactory.Create();
            IQueryable<ActivityEntity> query = GetQuery(uow);


            query = query.Where(activity =>
                    (
                        (model.Start >= activity.Start && activity.End >= model.Start) ||
                        (model.End >= activity.Start && model.End <= activity.End) ||
                        (model.Start <= activity.Start && model.End >= activity.End)
                    ) &&
                        (activity.AssignedId == model.Assigned.Id)
                     && (activity.Id != model.Id)
                );

            if (await query.AnyAsync())
            {
                throw new SecurityTokenException("Activity for this user in this range is already planned");
            }
            
            return await base.SaveAsync(model);

        }

        public async Task<IEnumerable<ActivityListModel>> GetActivitiesByDateAsync(DateTime? dateStart, DateTime? dateEnd)
        {
            await using IUnitOfWork uow = UnitOfWorkFactory.Create();
            IQueryable<ActivityEntity> query = GetQuery(uow);
            if (dateStart.HasValue)
            {
                query = query.Where(activity => activity.Start >= dateStart.Value);
            }

            if (dateEnd.HasValue)
            {
                query = query.Where(activity => activity.End <= dateEnd.Value);
            }
            return Mapper.MapToListModel(await query.ToListAsync());
        }

        public async Task<IEnumerable<ActivityListModel>> GetActivitiesByUserCreatedAsync(Guid userId)
        {
            await using IUnitOfWork uow = UnitOfWorkFactory.Create();
            return   Mapper.MapToListModel(await (
                     GetQuery(uow)).Where(
                    activity => activity.CreatedById == userId).OrderBy(a => a.Start)
                .ToListAsync()
            );
        }
        public async Task<IEnumerable<ActivityListModel>> GetActivitiesByUserAssignedAsync(Guid userId)
        {
            await using IUnitOfWork uow = UnitOfWorkFactory.Create();
            return Mapper.MapToListModel(await (
                     GetQuery(uow)).Where(
                    activity => activity.AssignedId == userId).OrderBy(a => a.Start)
                .ToListAsync()
            );
        }

        public async Task<IEnumerable<ActivityListModel>> GetActivitiesByDateLazyAsync(LazyDateType typeDate)
        {
            switch (typeDate)
            {
                case LazyDateType.Day:
                    return await GetActivitiesByDateAsync(DateTime.Today, null);
                case LazyDateType.Week:
                    return await GetActivitiesByDateAsync(DateTime.Today.AddDays(-7), null);
                case LazyDateType.Month:
                    return await GetActivitiesByDateAsync(DateTime.Today.AddMonths(-1), null);
                case LazyDateType.Year:
                    return await GetActivitiesByDateAsync(DateTime.Today.AddYears(-1), null);
                default:
                    return await GetActivitiesByDateAsync(null, null);
            }
        }

        public override async Task<IEnumerable<ActivityListModel>> GetAsync()
        {
            await using IUnitOfWork uow = UnitOfWorkFactory.Create();
            IQueryable<ActivityEntity> query = uow
                .GetRepository<ActivityEntity, ActivityEntityMapper>()
                .Get().OrderBy(a=>a.Start);

            if (IncludesNavigationPathDetail.Any())
            {
                IncludesNavigationPathDetail.ForEach(include => query = query.Include(include));
            }

            List<ActivityEntity> entities = await query.ToListAsync();

            return Mapper.MapToListModel(entities);
        }

        public async Task DeleteUserActivitiesOnProject(Guid projectId, Guid userId)
        {
            await using IUnitOfWork uow = UnitOfWorkFactory.Create();
            IQueryable<ActivityEntity> query = GetQuery(uow);
            query = query.Where(activity => activity.ProjectId == projectId && activity.AssignedId == userId);
            
            List<Guid> toDelete = await query.Select(e => e.Id).ToListAsync();
            toDelete.ForEach(id => uow.GetRepository<ActivityEntity, ActivityEntityMapper>().Delete(id));
            await uow.CommitAsync();

        }
    }
}
