using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using QinSoft.Core.Data.Database;
using QinSoft.Demo.DAL.Repository;
using QinSoft.Demo.DAL.Repository.Impl;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class DALExtensions
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.TryProxyAddSingleton<IProjectRepository, ProjectRepositoryImpl>(typeof(DatabaseContextInterceptor));
            return services;
        }
    }
}
