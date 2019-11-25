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
        public Dictionary<string, Dictionary<string>> GetByID(int id)
        {
			// var users = new Dictionary<int, string>();
			using (StreamReader r = new StreamReader("/home/guus/users.json"))
		    {
		        string json = r.ReadToEnd();
				// string json = @"{10: {""email"": ""info@guusapeldoorn.nl"", ""password"": ""737d5a7b64b234d1da5add7caf59e447"", ""salt"": ""aaa"", ""realName"": ""Guus Apeldoorn"", ""about"": ""lorem ipsum"", ""address"": { ""city"": ""Emmeloord"" }, ""hobbies"": [""h100"", ""h200""], ""eventsAtt"": [""e110""], ""eventsOrg"": [""e330""], ""pictures"": [""p2000"", ""p3000""], ""profilePicture"": ""p1000"",""matches"": [""m1100""], ""chats"": [""c1110"", ""c2220""] }}";

				var test = JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string>>>(json);
				return test;
		    }

			// 1. Retrieve user data from database

			// 2. Yeet the user data back
            // return JsonConvert.DeserializeObject<string>(json);
        }

		[HttpGet("email={email}")]
        public string GetByEmail(string email)
        {
            return "fuck";
			// new UserData
            // {
            //     Date = DateTime.Now
            // }
            // .ToArray();
        }
    }
}
