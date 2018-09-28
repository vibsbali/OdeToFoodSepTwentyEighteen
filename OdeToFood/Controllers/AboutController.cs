using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace OdeToFood.Controllers
{
   [Route("[controller]/[action]")]
   public class AboutController : Controller
   {
      public string Phone()
      {
         return "+61-401-193-731";
      }

      public string Address()
      {
         return "Sydney";
      }
   }
}
