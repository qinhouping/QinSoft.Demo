using QinSoft.Core.Data.Database;
using QinSoft.Demo.DAL.Repository.Impl;
using QinSoft.Demo.DAL.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection.Extensions;
using QinSoft.Demo.BLL.Services;
using QinSoft.Demo.BLL.Services.Impl;
using QinSoft.Demo.BLL.Mapper;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class BLLExtensions
    {
        public static void AddServices(this IServiceCollection services)
        {
            services.TryProxyAddSingleton<ITestService, TestServiceImpl>(typeof(DatabaseContextInterceptor));
        }

        public static void AddMappers(this IServiceCollection services)
        {
            services.AddAutoMapper(s => s.AddProfile(typeof(MapperProfile)));
        }
    }
}
