using TimeTracker.BL.Mappers;
using TimeTracker.BL.Models;
using TimeTracker.DAL.Entities;
using TimeTracker.DAL.Mappers;
using TimeTracker.DAL.UnitOfWork;

namespace TimeTracker.BL.Facades
{
    public class ActivityFacade: FacadeBase<ActivityEntity, ActivityDetailModel, ActivityListModel, ActivityEntityMapper>
    {
        public ActivityFacade(ActivityModelMapper mapper, IUnitOfWorkFactory uow) : base(mapper, uow)
        {
        }
        protected override List<string> IncludesNavigationPathDetail => new()
        {
            $"{nameof(ActivityEntity.Assigned)}",
            $"{nameof(ActivityEntity.CreatedBy)}",
        };

    }
}
