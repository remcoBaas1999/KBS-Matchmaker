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
    [Route("/images/")]
    public class ImageController : ControllerBase
    {
        [HttpGet("get/list")]
        public Dictionary<string, string> ImageList(int id)
        {
			DirectoryInfo d = new DirectoryInfo(@"/home/student/data/images/");
			FileInfo[] Files = d.GetFiles("*.jpg");
			var dict = new Dictionary<string, string>();
			foreach(FileInfo file in Files )
			{
			  dict.Add(file.Name, $"https://145.44.233.207/images/covers/{file.Name}");
			}
			return dict;
        }
	}
}
