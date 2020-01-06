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
    [Route("/auth/")]
    public class AuthController : ControllerBase
    {
        [HttpGet("get/id={id}")]
        public AuthData AuthById(int id)
        {
  			using (StreamReader r = new StreamReader("/home/student/data/users.json"))
  		    {
  		        string json = r.ReadToEnd();
  				try {
  					UserData user = JsonConvert.DeserializeObject<Dictionary<string, UserData>>(json)[id.ToString()];
  					var data = new AuthData {
  						email = user.email,
  						password = user.password,
  						salt = user.salt
  					};
  					return data;
  				} catch (System.Collections.Generic.KeyNotFoundException e) {
  					return new AuthData();
  				}

  		    }
        }

		[HttpGet("get/email={email}")]
        public AuthData AuthByEmail(string email)
        {
			Console.WriteLine(email);
			using (StreamReader r = new StreamReader("/home/student/data/userMap.json"))
		    {
		        string json = r.ReadToEnd();
				try {
					var id = JsonConvert.DeserializeObject<Dictionary<string, int>>(json)[email];

					return AuthById(id);
				} catch (System.Collections.Generic.KeyNotFoundException e) {
					Console.WriteLine("key not found");
					return new AuthData();
				}
		    }
        }
    }
}
