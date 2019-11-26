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
    [Route("/get/user/")]
    public class UserController : ControllerBase
    {
        [HttpGet("id={id}")]
        public UserData GetByID(int id)
        {
			using (StreamReader r = new StreamReader("/home/student/data/users.json"))
		    {
		        string json = r.ReadToEnd();
				try {
					var test = JsonConvert.DeserializeObject<Dictionary<int, UserData>>(json)[id];
					test.id = id;
					return test;
				} catch (System.Collections.Generic.KeyNotFoundException e) {
					return new UserData();
				}

		    }
        }

		[HttpGet("email={email}")]
        public UserData GetByEmail(string email)
        {
			using (StreamReader r = new StreamReader("/home/student/data/userMap.json"))
		    {
		        string json = r.ReadToEnd();

				var id = JsonConvert.DeserializeObject<Dictionary<string, int>>(json)[email];

				return GetByID(id);
		    }
        }
    }
}
