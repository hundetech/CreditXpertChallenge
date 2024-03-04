using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;
using CreditXpertChallengeWebAPI.AppServices;

namespace CreditXpertChallengeWebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ChallengeTwoController : ControllerBase
    {

        public ChallengeTwoController()
        {
        }

        [HttpGet(Name = "ChallengeTwoBoards")]
        public ActionResult<List<UIBoard>> Get(int numberOfMoves)
        {
            var toReturn = new ChallengeTwoService(numberOfMoves);

            toReturn.ConstructBoards();

            return Ok(toReturn.BoardStates);
        }
    }
}