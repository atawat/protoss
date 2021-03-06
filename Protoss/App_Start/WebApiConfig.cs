﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Formatting;
using System.Web.Http;
using Autofac.Integration.WebApi;
using Newtonsoft.Json.Converters;
using YooPoon.Core.Autofac;

namespace Protoss
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API 配置和服务

            // Web API 路由
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.EnableCors();

            //DI配置
            var initialize = new InitializeContainer();
            initialize.Initializing();
            //TODO:实现自己的Resolver，未实现前暂时使用Auto自带的
            config.DependencyResolver = new AutofacWebApiDependencyResolver(initialize.ContainerManager.Container);

//            var jsonFormatter = new JsonMediaTypeFormatter();
//            var settings = jsonFormatter.SerializerSettings;
//            IsoDateTimeConverter timeConverter = new IsoDateTimeConverter();
//            //这里使用自定义日期格式
//            timeConverter.DateTimeFormat = "yyyy-MM-dd HH:mm:ss";
//            settings.Converters.Add(timeConverter);
        }
    }
}
