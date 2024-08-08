using System.Web.Mvc;

namespace WebsiteMusic.Areas.Admin_Website
{
    public class Admin_WebsiteAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Admin_Website";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Admin_Website_default",
                "Admin_Website/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}