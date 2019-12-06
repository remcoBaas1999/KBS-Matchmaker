using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Runtime.Serialization;

namespace MatchmakerAPI.Controllers
{
    [ApiController]
    [Route("/hobbies/")]
    public class HobbyController : ControllerBase
    {
        [HttpGet("get/list")]
        public List<Hobby> HobbyList(int id)
        {
			return null;
        }
	}
}
