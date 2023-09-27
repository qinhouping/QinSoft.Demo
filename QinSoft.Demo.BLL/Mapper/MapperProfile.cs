using AutoMapper;
using Model=QinSoft.Demo.Common.Model.Response;
using Entity=QinSoft.Demo.DAL.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace QinSoft.Demo.BLL.Mapper
{
    public class MapperProfile:Profile
    {
        public MapperProfile() {
            this.CreateMap<Entity.Project, Model.Project>();
        }
    }
}
