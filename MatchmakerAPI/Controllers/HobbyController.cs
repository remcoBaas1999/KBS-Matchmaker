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
        [HttpGet("get/all")]
        public List<Hobby> HobbyList(int id)
        {
			using (StreamReader r = new StreamReader("/home/student/data/hobbies.json"))
		    {
		        string json = r.ReadToEnd();
				try {
					var test = JsonConvert.DeserializeObject<List<Hobby>>(json);
					return test;
				} catch (System.Collections.Generic.KeyNotFoundException e) {
					return null;
				}
			}
        }
	}
}
