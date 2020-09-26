using Voting.DAL.SQLLite.Repositories.Implementation;
using Voting.DAL.SQLLite.Repositories.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using Unity;

namespace Voting.DAL.SQLLite
{
    public static class DAL_SQLLite_Config
    {
        public static void RegisterTypes(IUnityContainer container)
        {

            container.RegisterType<IUserRepository, UserRepository>();
        }

        public static void RegisterSingleton(IServiceCollection services)
        {
            services.AddSingleton<INotificationRepository, NotificationRepository>();
        }
    }
}
