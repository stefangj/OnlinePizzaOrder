using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;
using PizzaProject.Models;
using System.Data.Entity;

[assembly: OwinStartupAttribute(typeof(PizzaProject.Startup))]
namespace PizzaProject
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            Database.SetInitializer<PizzaProjectContext>(new DropCreateDatabaseIfModelChanges<PizzaProjectContext>());


        }
    }
}
