using AutoMapper;
using Model = QinSoft.Demo.Common.Model.Response;
using Entity = QinSoft.Demo.DAL.Entity;
using QinSoft.Demo.DAL.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace QinSoft.Demo.BLL.Services.Impl
{
    public class TestServiceImpl : ITestService
    {
        private IProjectRepository projectRepository;
        private IMapper mapper;

        public TestServiceImpl(IProjectRepository projectRepository, IMapper mapper)
        {
            this.projectRepository = projectRepository;
            this.mapper = mapper;
        }

        public List<Model.Project> GetProjects()
        {
            return mapper.Map<List<Entity.Project>, List<Model.Project>>(projectRepository.Select().ToList());
        }
    }
}
