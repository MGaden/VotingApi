using Voting.DAL.SQLLite;
using Voting.Service.Implementation;
using Voting.Service.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using Unity;

namespace Voting.Service
{
    public static class Service_Config
    {
        public static void RegisterTypes(IUnityContainer container)
        {
            DAL_SQLLite_Config.RegisterTypes(container);
            container.RegisterType<IUserService, UserService>();

            Service_Mapper.Map();
        }

        public static void RegisterSingleton(IServiceCollection services)
        {
            DAL_SQLLite_Config.RegisterSingleton(services);
            //services.AddSingleton<INotificationService, NotificationService>();
        }
    }
}
