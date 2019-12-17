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


		// Retrieve full user data by user id
		[HttpGet("get/id={id}")]
		public UserData UserById(int id) {

			try
			{

				// Load the users database into memory
				var users = LoadUsers();

				// Fetch the specific user we're looking for in a try/catch block
				// in case the input is not a valid key.
				var user = users[id];

				// Return the specified user
				return user;

			} catch (System.Collections.Generic.KeyNotFoundException) {

				// If the requested user id does not exist, return a userdata object
				// with all fields nulled and display a warning on the server console.

				Console.WriteLine(" !! EXCEPTION:");
				Console.WriteLine("    An invalid user id was requested.");

				return new UserData();

			} catch (Exception) {

				// If something else goes wrong, return a nulled UserData and display
				// a warning on the server console

				Console.WriteLine(" !! EXCEPTION:");
				Console.WriteLine("    Exception caught trying to fetch a user by id.");

				return new UserData();

			}

		}



		// Retrieve full user data by email
		[HttpGet("get/email={email}")]
		public UserData UserByEmail(string email) {

			try
			{

				// Load the userMap into memory to match the given email to a user id
				// without having to loop through every entry in the entire users.json
				// database
				var userMap = LoadUserMap();

				// Fetch the specific user we're looking for in a try/catch block
				// in case the input is not a valid key.
				var id = userMap[email];

				// Reuse the UserById() method to prevent code duplication
				return UserById(id);

			} catch (System.Collections.Generic.KeyNotFoundException) {

				// If the requested email does not exist, return a UserData object
				// with all fields nulled and display a warning on the server console.

				Console.WriteLine(" !! EXCEPTION:");
				Console.WriteLine("    An invalid email address was requested.");

				return new UserData();

			} catch (Exception) {

				// If something else goes wrong, return a nulled UserData and display
				// a warning on the server console

				Console.WriteLine(" !! EXCEPTION:");
				Console.WriteLine("    Exception caught trying to fetch a user by email.");

				return new UserData();

			}

		}



		// Retrieve the userMap
		[HttpGet("get/all")]
		public Dictionary<string, int> AllUsers() {

			try
			{

				// Load the userMap into memory
				var userMap = LoadUserMap();

				// Return the userMap
				return userMap();

			} catch (Exception) {

				// If something goes wrong, return a nulled Dictionary<string, int> and
				// display a warning on the server console

				Console.WriteLine(" !! EXCEPTION:");
				Console.WriteLine("    Exception caught trying to fetch all users.");

				return new Dictionary<string, int>();

			}

		}



		// Retrieve just the hobbies of a user
		[HttpGet("get/hobbies/id={id}")]
		public List<Hobby> GetUserHobbies(int id) {

			try
			{
				// Load the users database into memory
				var users = LoadUsers();

				// Pick the specified user
				var user = users[id];

				// Return the contents of the hobby field
				return user.hobbies;

			} catch (Exception) {

				// TODO: add specific null exception handlers

				// If something goes wrong, return a nulled List<Hobby> and display
				// a warning on the server console

				Console.WriteLine(" !! EXCEPTION:");
				Console.WriteLine("    Exception caught trying to fetch a user's hobbies.");

				return new List<Hobby>();

			}

		}


		// TODO


		[HttpGet("get/matches/id={forUserId}")]
		public UserData[] GetMatches(int forUserId) {
			int sampleNum = 120;
			int scoredNum = 12;
			int returnNum = 4;
			try {
				return MatchmakingAlgorithm.FindMatches(forUserId, sampleNum, scoredNum, returnNum);
			} catch (System.Collections.Generic.KeyNotFoundException) {
				return new UserData[returnNum];
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

		public static Dictionary<string, int> LoadUserMap()
		{
			// Open the userMap.json data file
			using (StreamReader r = new StreamReader("/home/student/data/userMap.json"))
      {
				// Load the contents into memory
        string json = r.ReadToEnd();

				// Deserialize the json to something C# can do stuff to
        var userMap = JsonConvert.DeserializeObject<Dictionary<string, int>>(json);

				// Return the deserialized data table
				return userMap;
      }
		}



	}
}
