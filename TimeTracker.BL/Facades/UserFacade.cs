﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeTracker.BL.Mappers;
using TimeTracker.BL.Models;
using TimeTracker.DAL.Entities;
using TimeTracker.DAL.Mappers;
using TimeTracker.DAL.UnitOfWork;

namespace TimeTracker.BL.Facades
{
    public class UserFacade: FacadeBase<UserEntity, UserDetailModel, UserListModel, UserEntityMapper>
    {
        public UserFacade(UserModelMapper mapper, IUnitOfWorkFactory uow) : base(mapper, uow)
        {
        }

    }
}
