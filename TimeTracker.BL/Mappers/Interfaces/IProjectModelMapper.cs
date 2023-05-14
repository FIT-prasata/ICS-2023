using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeTracker.BL.Models;
using TimeTracker.DAL.Entities;

namespace TimeTracker.BL.Mappers;

public interface IProjectModelMapper : IModelMapper<ProjectEntity, ProjectListModel, ProjectDetailModel>
{
}