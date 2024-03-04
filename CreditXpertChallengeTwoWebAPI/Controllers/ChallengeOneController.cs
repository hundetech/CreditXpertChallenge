using Microsoft.AspNetCore.Mvc;
using CreditXpertChallengeWebAPI.AppServices;
using System.Xml.Linq;

namespace CreditXpertChallengeWebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ChallengeOneController  : ControllerBase
    {

        public ChallengeOneController()
        {

        }

        [HttpGet(Name = "ChallengeOneBoards")]
        public ActionResult<ShapeColorInfo> Get()
        {
            var challengeTwoAppService = new ChallengeOneService();

            return challengeTwoAppService.GetShapeColorInfo();
        }

    }




}