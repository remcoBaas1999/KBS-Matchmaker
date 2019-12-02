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
    [Route("/user/")]
    public class UserController : ControllerBase
    {
        [HttpGet("get/id={id}")]
        public UserData UserById(int id)
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

		[HttpGet("get/email={email}")]
        public UserData UserByEmail(string email)
        {
			using (StreamReader r = new StreamReader("/home/student/data/userMap.json"))
		    {
		        string json = r.ReadToEnd();

				var id = JsonConvert.DeserializeObject<Dictionary<string, int>>(json)[email];
				try {
					return UserById(id);
				} catch (System.Collections.Generic.KeyNotFoundException e) {
					return new UserData();
				}
		    }
        }

		[HttpPost("post/new")]
		public CreatedAtActionResult AddNewUser(NewUserData data)
		{
			using (StreamReader r = new StreamReader("/home/student/data/users.json"))
		    {
		        string json = r.ReadToEnd();

				var users = JsonConvert.DeserializeObject<Dictionary<int, UserData>>(json);

				var rng = new Random();
				int key;

				do {
					key = rng.Next();
				} while (users.ContainsKey(key));

				var udata = new UserData {
					email = data.email,
					password = data.password,
					salt = data.salt,
					realName = data.realName,
					birthdate = data.birthdate
				};

				users.Add(key, udata);

				var text = JsonConvert.SerializeObject(users);
				System.IO.File.WriteAllText(@"/home/student/data/users.json", text);
		    }

			return CreatedAtAction($"{data.email}", new { success = true });
		}
    }
}
