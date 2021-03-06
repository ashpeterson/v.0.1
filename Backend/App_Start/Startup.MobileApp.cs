﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Web.Http;
using Microsoft.Azure.Mobile.Server.Config;
using Backend.DataObjects;
using Backend.Models;
using Owin;
using System.Data.Entity.Migrations;

namespace Backend
{
    public partial class Startup
    {
        /*-------------------------------------------------------------------------------
         * Mobile Apps SDK is initialized within App_Start\Startup.MobileApp.cs 
         * (with the call to the configuration routine happening within Startup.cs).
         * The default startup routine is reasonable but it hides what it is doing 
         * behind extension methods. This technique is fairly common in ASP.NET programs.
         ---------------------------------------------------------------------------------*/
        public static void ConfigureMobileApp(IAppBuilder app)
        {
            var httpConfig = new HttpConfiguration();
            var mobileConfig = new MobileAppConfiguration();

            mobileConfig
                .AddTablesWithEntityFramework()
                .ApplyTo(httpConfig);

            // Automatic Code First Migrations
             var migrator = new DbMigrator(new Migrations.Configuration());
             migrator.Update();

            // Database First
            //Database.SetInitializer<DbContext>(null);

            app.UseWebApi(httpConfig);


            #region DB first migration
            /*****************************************************
             * Remove due to Code-First Migration
             * ***************************************************/

            //HttpConfiguration config = new HttpConfiguration();

            //new MobileAppConfiguration()
            //    .UseDefaultConfiguration()
            //    .ApplyTo(config);

            //// Use Entity Framework Code First to create database tables based on your DbContext
            //Database.SetInitializer(new MobileServiceInitializer());

            //MobileAppSettingsDictionary settings = config.GetMobileAppSettingsProvider().GetMobileAppSettings();

            //if (string.IsNullOrEmpty(settings.HostName))
            //{
            //    app.UseAppServiceAuthentication(new AppServiceAuthenticationOptions
            //    {
            //        // This middleware is intended to be used locally for debugging. By default, HostName will
            //        // only have a value when running in an App Service application.
            //        SigningKey = ConfigurationManager.AppSettings["SigningKey"],
            //        ValidAudiences = new[] { ConfigurationManager.AppSettings["ValidAudience"] },
            //        ValidIssuers = new[] { ConfigurationManager.AppSettings["ValidIssuer"] },
            //        TokenHandler = config.GetAppServiceTokenHandler()
            //    });
            //}

            //app.UseWebApi(config);
            #endregion
        }
    }

    public class MobileServiceInitializer : CreateDatabaseIfNotExists<MobileServiceContext>
    {
        protected override void Seed(MobileServiceContext context)
        {
            List<TodoItem> todoItems = new List<TodoItem>
            {
                new TodoItem { Id = Guid.NewGuid().ToString(), Text = "First item", Complete = false },
                new TodoItem { Id = Guid.NewGuid().ToString(), Text = "Second item", Complete = false },
                new TodoItem { Id = Guid.NewGuid().ToString(), Text = "Third Item", Complete = false }
            };

            foreach (TodoItem todoItem in todoItems)
            {
                context.Set<TodoItem>().Add(todoItem);
            }

            base.Seed(context);
        }
    }
}

