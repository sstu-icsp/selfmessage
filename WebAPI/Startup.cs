using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Microsoft.Owin;
using Owin;
using Microsoft.Owin.Cors;
using WebAPI.Models;

[assembly: OwinStartup(typeof(WebAPI.Startup))]

namespace WebAPI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            using (var db = new Model())
            {
                Database.SetInitializer(new DropCreateDatabaseIfModelChanges<Model>());
                db.Database.Initialize(false);
            }

            app.UseCors(CorsOptions.AllowAll);
            ConfigureAuth(app);
        }
    }
}
