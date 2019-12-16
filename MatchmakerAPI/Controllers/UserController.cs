using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Runtime.Serialization;
using MatchmakerAPI;

namespace MatchmakerAPI.Controllers
{
	[ApiController]
	[Route("/user/")]
	public class UserController : ControllerBase {

		// Retrieving full user data by user id
		[HttpGet("get/id={id}")]
		public UserData UserById(int id) {

			// Open the users.json data file
			using (StreamReader r = new StreamReader("/home/student/data/users.json")) {

				// Load the users.json contents into memory
				string json = r.ReadToEnd();

				try {

					// Deserialize the json string into a dictionary of ints (the user's id)
					// and the rest of their data, and take the one where the key is equal to
					// the requested id in the url.
					var user = JsonConvert.DeserializeObject<Dictionary<int, UserData>>(json)[id];


					user.id = id;
					return user;
				} catch (System.Collections.Generic.KeyNotFoundException e) {
					return new UserData();
				}
			}
		}

		[HttpGet("get/email={email}")]
		public UserData UserByEmail(string email) {
			using (StreamReader r = new StreamReader("/home/student/data/userMap.json")) {
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
		public Dictionary<string, int> AllUsers() {
			using (StreamReader r = new StreamReader("/home/student/data/userMap.json")) {
				string json = r.ReadToEnd();
				var dict = JsonConvert.DeserializeObject<Dictionary<string, int>>(json);
				return dict;
			}
		}

		[HttpGet("get/hobbies/id={id}")]
		public List<Hobby> GetUserHobbies(int id) {
			using (StreamReader r = new StreamReader("/home/student/data/users.json")) {
				string json = r.ReadToEnd();
				try {
					var user = JsonConvert.DeserializeObject<Dictionary<int, UserData>>(json)[id];
					return user.hobbies;
				} catch (System.Collections.Generic.KeyNotFoundException e) {
					return null;
				}
			}
		}


		// TODO


		[HttpGet("get/matches/id={id}")]
		public UserData[] GetMatches(int id) {
			int firstRandomSelection = 120;
			int scoredSelection = 12;
			int finalRandomSelection = 5;
			try {
				return MatchmakingAlgorithm.FindMatches(id, firstRandomSelection, scoredSelection, finalRandomSelection);
			} catch (System.Collections.Generic.KeyNotFoundException e) {
				return new UserData[finalRandomSelection];
			}
		}


		// TODO: refactor code to use the LoadUsers() method


		[HttpPost("post/new")]
		public CreatedAtActionResult AddNewUser(NewUserData data) {
			int key;

			using (StreamReader r = new StreamReader("/home/student/data/users.json")) {
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
			using (StreamReader r = new StreamReader("/home/student/data/userMap.json")) {
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

		[HttpPost("post/update/images")]
		public CreatedAtActionResult UpdateCoverImage(CoverImageData data) {

			// Get the user id
			int key = data.userid;

			// Read the users data file
			var users = LoadUsers();

			// Change the specified user's coverImage property
			users[key].coverImage = data.imageName;

			// Serialize the modified user data for storage
			var modifiedData = JsonConvert.SerializeObject(users);

			// Write the modified user data to the users data file
			System.IO.File.WriteAllText(@"/home/student/data/users.json", modifiedData);

			try {
				return CreatedAtAction("UpdateUser", new { success = true });
			} catch (System.InvalidOperationException) {
				return null;
			}
		}

		[HttpPost("post/update")]
		public CreatedAtActionResult UpdateUser(UserData data) {
			int key;

			using (StreamReader r = new StreamReader("/home/student/data/userMap.json")) {
				string json = r.ReadToEnd();
				try {
					key = JsonConvert.DeserializeObject<Dictionary<string, int>>(json)[data.email];
				} catch (System.Collections.Generic.KeyNotFoundException e) {
					key = 0;
				}
			}

			using (StreamReader r = new StreamReader("/home/student/data/users.json")) {
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



		public static Dictionary<int, UserData> LoadUsers()
		{
			// Open the users.json data file
			using (StreamReader r = new StreamReader("/home/student/data/users.json"))
      {
				// Load the contents into memory
        string json = r.ReadToEnd();

				// Deserialize the json to something C# can do stuff to
        var users = JsonConvert.DeserializeObject<Dictionary<int, UserData>>(json);

				// Return the deserialized data table
				return users;
      }
		}



	}
}
