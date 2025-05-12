using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.ComponentModel.Design;

namespace GuardTour_API.Controllers
{
    [Route("API/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        db_Utility util=new db_Utility();

       
    }
}
