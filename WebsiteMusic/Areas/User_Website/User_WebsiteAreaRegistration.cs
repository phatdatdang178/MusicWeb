using System.Web.Mvc;

namespace WebsiteMusic.Areas.User_Website
{
    public class User_WebsiteAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "User_Website";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "User_Website_default",
                "User_Website/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}