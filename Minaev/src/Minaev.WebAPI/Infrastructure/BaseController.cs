using Microsoft.AspNetCore.Mvc;

namespace Minaev.WebAPI.Infrastructure
{
    public class BaseController : Controller
    {
        public SystemUser SystemUser => (HttpContext.Items["Account"] as SystemUser)!; 
    }
}
