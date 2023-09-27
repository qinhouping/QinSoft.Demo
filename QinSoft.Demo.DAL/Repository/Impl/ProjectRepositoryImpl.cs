using QinSoft.Core.Data.Database;
using QinSoft.Demo.DAL.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace QinSoft.Demo.DAL.Repository.Impl
{
    [DatabaseContext(true)]
    public class ProjectRepositoryImpl : DatabaseRepository<Project>, IProjectRepository
    {

    }
}
