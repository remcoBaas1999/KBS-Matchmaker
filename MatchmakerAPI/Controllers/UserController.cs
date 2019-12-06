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
				try {
					var id = JsonConvert.DeserializeObject<Dictionary<string, int>>(json)[email];
					return UserById(id);
				} catch (System.Collections.Generic.KeyNotFoundException e) {
					return new UserData();
				}
		    }
        }

		[HttpGet("get/all")]
        public Dictionary<string, int> AllUsers()
        {
			using (StreamReader r = new StreamReader("/home/student/data/userMap.json"))
		    {
		        string json = r.ReadToEnd();
				var dict = JsonConvert.DeserializeObject<Dictionary<string, int>>(json);
				return dict;
		    }
        }

		[HttpPost("post/new")]
		public CreatedAtActionResult AddNewUser(NewUserData data)
		{
			int key;

			using (StreamReader r = new StreamReader("/home/student/data/users.json"))
		    {
		        string json = r.ReadToEnd();

				var users = JsonConvert.DeserializeObject<Dictionary<int, UserData>>(json);

				var rng = new Random();

				do {
					key = rng.Next();
				} while (users.ContainsKey(key));

				var udata = new UserData {
					email = data.email,
					password = data.password,
					salt = data.salt,
					realName = data.realName,
					birthdate = data.birthdate,
					profilePicture = "0.jpg"
				};

				users.Add(key, udata);

				var text = JsonConvert.SerializeObject(users);
				System.IO.File.WriteAllText(@"/home/student/data/users.json", text);
		    }
			using (StreamReader r = new StreamReader("/home/student/data/userMap.json"))
		    {
		        string json = r.ReadToEnd();
				var userMap = JsonConvert.DeserializeObject<Dictionary<string, int>>(json);
				userMap.Add(data.email, key);
				var text = JsonConvert.SerializeObject(userMap);

				System.IO.File.WriteAllText(@"/home/student/data/userMap.json", text);
			}

			try {
				return CreatedAtAction("AddNewUser", new { success = true });
			} catch (System.InvalidOperationException) {
				return null;
			}
		}

		[HttpPost("post/update")]
		public CreatedAtActionResult UpdateUser(UserData data)
		{
			int key;

			using (StreamReader r = new StreamReader("/home/student/data/userMap.json"))
		    {
		        string json = r.ReadToEnd();
				try {
					key = JsonConvert.DeserializeObject<Dictionary<string, int>>(json)[data.email];
				} catch (System.Collections.Generic.KeyNotFoundException e) {
					key = 0;
				}
		    }

			using (StreamReader r = new StreamReader("/home/student/data/users.json"))
		    {
		        string json = r.ReadToEnd();

				var users = JsonConvert.DeserializeObject<Dictionary<int, UserData>>(json);

				users[key] = data;

				var text = JsonConvert.SerializeObject(users);
				System.IO.File.WriteAllText(@"/home/student/data/users.json", text);
		    }

			try {
				return CreatedAtAction("UpdateUser", new { success = true });
			} catch (System.InvalidOperationException) {
				return null;
			}
		}
    }
}
