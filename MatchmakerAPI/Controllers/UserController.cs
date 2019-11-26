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
			// var users = new Dictionary<int, string>();
			using (StreamReader r = new StreamReader("/home/guus/users.json"))
		    {
		        string json = r.ReadToEnd();

				var test = JsonConvert.DeserializeObject<Dictionary<int, UserData>>(json)[id];
				test.id = id;

				return test;
		    }

			// 1. Retrieve user data from database

			// 2. Yeet the user data back
            // return JsonConvert.DeserializeObject<string>(json);
        }

		[HttpGet("email={email}")]
        public UserData GetByEmail(string email)
        {
			using (StreamReader r = new StreamReader("/home/guus/userMap.json"))
		    {
		        string json = r.ReadToEnd();

				var id = JsonConvert.DeserializeObject<Dictionary<string, int>>(json)[email];

				return GetByID(id);
		    }
        }
    }
}
