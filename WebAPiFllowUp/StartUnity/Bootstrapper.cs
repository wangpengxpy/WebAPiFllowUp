using Microsoft.Practices.Unity;
using Resolver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using Unity.WebApi;

namespace WebAPiFllowUp.StartUnity
{ 
    public class Bootstrapper
    {
        //初始化容器并注册类型到Unity容器中并注册到全局文件中
        public static void Initial()
        {
            var container = BuildUnityContainer();

            //DependencyResolver.SetResolver(new UnityDependencyResolver(container));

            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }


        //建立Unity容器
        private static IUnityContainer BuildUnityContainer()
        {
            var container = new UnityContainer();
            RegisterTypes(container);
            return container;
        }

        //通过加载如下两个程序集来注册其类型到Unity容器中
        public static void RegisterTypes(IUnityContainer container)
        {
            ComponentLoader.LoadContainer(container, ".\\bin", "WebAPiFllowUp.dll");
            ComponentLoader.LoadContainer(container, ".\\bin", "AppicationServices.dll");
        }
    }
}