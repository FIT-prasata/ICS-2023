using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeTracker.BL.Mappers.Interfaces;
using TimeTracker.DAL.Entities;
using TimeTracker.BL.Models;

namespace TimeTracker.BL.Mappers
{
    public class ActivityModelMapper<ActivityEntity, ActivityDetailModel, ActivityListModel>: IModelDetailMapper<ActivityEntity, ActivityDetailModel>, IModelListMapper<ActivityEntity, ActivityListModel>
    {
        public ActivityDetailModel MapToDetailModel(ActivityEntity entity)
            => entity is null ?             

        public ActivityEntity MapToEntity(ActivityDetailModel model)
        {
            throw new NotImplementedException();
        }

        public ActivityListModel MapToListModel(ActivityEntity? entity)
        {
            throw new NotImplementedException();
        }
    }
}
