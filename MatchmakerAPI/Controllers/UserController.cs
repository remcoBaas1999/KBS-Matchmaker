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
	public class UserController : ControllerBase
	{

		public static string UsersFile = "/home/student/data/users.json";
		public static string UserMapFile = "/home/student/data/userMap.json";



		// // // //  GET REQUESTS  // // // // // // // // // // // // // // // //



		// Retrieve full user data by user id
		[HttpGet("get/id={id}")]
		public UserData UserById(int id)
		{

			try
			{

				// Load the users database into memory
				var users = ReadUsers();

				// Fetch the specific user we're looking for in a try/catch block
				// in case the input is not a valid key.
				var user = users[id.ToString()];
				user.id = id.ToString();

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
		public UserData UserByEmail(string email)
		{

			try
			{

				// Load the userMap into memory to match the given email to a user id
				// without having to loop through every entry in the entire users.json
				// database
				var userMap = ReadUserMap();

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
		public Dictionary<string, int> AllUsers()
		{

			try
			{

				// Load the userMap into memory
				var userMap = ReadUserMap();

				// Return the userMap
				return userMap;

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
		public List<Hobby> GetUserHobbies(int id)
		{

			try
			{
				// Load the users database into memory
				var users = ReadUsers();

				// Pick the specified user
				var user = users[id.ToString()];

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


		// Retrieve matches for a specific user
		[HttpGet("get/matches/id={forUserId}")]
		public UserData[] GetMatches(int forUserId) {

			// Define some parameters for the algorithm
			int sampleNum = 120;
			int scoredNum = 12;
			int returnNum = 4;

			try {

				// Run the algorithm and store the results in an array of UserData
				var matches = MatchmakingAlgorithm.FindMatches(forUserId, sampleNum, scoredNum, returnNum);

				// Return the matches
				return matches;

			} catch (System.Collections.Generic.KeyNotFoundException) {

				// If the user does not exist, return an empty array of UserData
				return new UserData[returnNum];

			}

		}



		// // // //  POST REQUESTS  // // // // // // // // // // // // // // // //



		// Add a new user
		[HttpPost("post/new")]
		public CreatedAtActionResult AddNewUser(NewUserData data) {

			// Initialize the new user id variable
			int tmp_key;

			// Update the users database
			try
			{
				// Load the users database into memory
				var users = ReadUsers();

				// Initialize a Random object (because it isn't static)
				var rng = new Random();

				do {
					// Generate a random user id, and if it's taken, pick a new one
					// until you find one that isn't.

					tmp_key = rng.Next();

					// The user id is random for security/privacy reasons;
					// keys are harder to guess and it makes it harder for
					// potential attackers to download all the records
				} while (users.ContainsKey(tmp_key.ToString()));

				// Generate some placeholder data
				var tmp_hobbies = new List<Hobby>();
				var tmp_cover = $"{rng.Next(5)}.jpg";

				// Fill a new UserData object with the supplied data
				// and some placeholders
				var newUser = new UserData {
					email 					= data.email,
					password 				= data.password,
					salt 						= data.salt,
					realName 				= data.realName,
					birthdate 			= data.birthdate,
					profilePicture	= "0.jpg",
					hobbies					= tmp_hobbies,
					coverImage			= tmp_cover,
					id							= tmp_key.ToString(),
				};

				// Add the new user data to the in-memory users database with
				// the newly generated key
				users.Add(tmp_key.ToString(), newUser);

				// Write the updated user data to the database
				UpdateUsers(users);

			} catch (Exception) {

				// If something goes wrong, throw a fit in the console and
				// return a failure state

				Console.WriteLine(" !! EXCEPTION:");
				Console.WriteLine("    An error occurred attempting to add a user to the users database.");

				return CreatedAtAction("AddNewUser", new { success = false });

			}

			// Update the user map
			try
			{

				// Load the userMap into memory
				var userMap = ReadUserMap();

				// Add a new entry mapping the supplied email address to the random
				// user id
				userMap.Add(data.email, tmp_key);

				UpdateUserMap(userMap);

			} catch (Exception) {

				// If something goes wrong, throw a fit in the console and
				// return a failure state

				Console.WriteLine(" !! EXCEPTION:");
				Console.WriteLine("    An error occurred attempting to add a user to the user map.");

				return CreatedAtAction("AddNewUser", new { success = false });

			}

			try
			{

				return CreatedAtAction("AddNewUser", new { success = true });

			} catch (System.InvalidOperationException) {

				return CreatedAtAction("AddNewUser", new { success = false });

			}

		}


		// Update an exisiting user's profile
		[HttpPost("post/update")]
		public AcceptedAtActionResult UpdateUser(UserData data) {

			// Initialize the user id variable
			int key;

			try
			{

				// Load the user map into memory
				var userMap = ReadUserMap();

				key = userMap[data.email];

				// Validate user id integrity
				if (key != int.Parse(data.id))
				{
					// If the new data's user id does not match the key found for the user's
					// email address, update it
					data.id = key.ToString();
				}

			} catch (System.Collections.Generic.KeyNotFoundException) {

				// If something goes wrong, throw a fit in the console and
				// return a failure state

				Console.WriteLine(" !! EXCEPTION:");
				Console.WriteLine("    An invalid key was specified.");

				return AcceptedAtAction("UpdateUser", new { success = false });

			}


			try
			{

				// Load the users database into memory
				var users = ReadUsers();

				// Pick the user with the specified key
				var user = users[key.ToString()];

				// Compare the passwords to see if they match
				if (user.password != data.password)
				{

					// Stop if the passwords don't match
					return AcceptedAtAction("UpdateUser", new { success = false });

				}

				// Overwrite the old user data with the specified data
				user = data;

				// Put the updated user data back in the database
				users[key.ToString()] = user;

				// Update the database
				UpdateUsers(users);

			} catch (Exception) {

				// If something goes wrong, throw a fit in the console and
				// return a failure state

				Console.WriteLine(" !! EXCEPTION:");
				Console.WriteLine("    An error occurred attempting to update a user in the users database.");

				return AcceptedAtAction("UpdateUser", new { success = false });

			}

			try {

				return AcceptedAtAction("UpdateUser", new { success = true });

			} catch (System.InvalidOperationException) {

				return AcceptedAtAction("UpdateUser", new { success = false });

			}
		}


		// Update an exisiting user's cover image
		[HttpPost("post/update/images")]
		public AcceptedAtActionResult UpdateCoverImage(CoverImageData data) {

			// Get the user id
			int key = data.userid;

			// Read the users data file
			var users = ReadUsers();

			// Change the specified user's coverImage property
			users[key.ToString()].coverImage = data.imageName;

			// Update the users database
			UpdateUsers(users);

			try {

				return AcceptedAtAction("UpdateCoverImage", new { success = true });

			} catch (System.InvalidOperationException) {

				return AcceptedAtAction("UpdateCoverImage", new { success = false });

			}
		}



		// // // //  STATIC METHODS  // // // // // // // // // // // // // // // //


		// // // //  READING METHODS

		public static Dictionary<string, UserData> ReadUsers()
		{

			// Open the users.json data file
			using (StreamReader r = new StreamReader(UsersFile))
      {
				// Load the contents into memory
        string json = r.ReadToEnd();

				// Deserialize the json to something C# can do stuff to
        var users = JsonConvert.DeserializeObject<Dictionary<string, UserData>>(json);

				// Return the deserialized data table
				return users;
      }

		}


		public static Dictionary<string, int> ReadUserMap()
		{

			// Open the userMap.json data file
			using (StreamReader r = new StreamReader(UserMapFile))
      {
				// Load the contents into memory
        string json = r.ReadToEnd();

				// Deserialize the json to something C# can do stuff to
        var userMap = JsonConvert.DeserializeObject<Dictionary<string, int>>(json);

				// Return the deserialized data table
				return userMap;
      }

		}


		// // // //  WRITING METHODS

		public static void UpdateUsers(Dictionary<string, UserData> data)
		{

			try
			{

				// Serialize the updated users database
				var text = JsonConvert.SerializeObject(data);
				
				// Write the serialized updated database to the proper file
				System.IO.File.WriteAllText(UsersFile, text);

			} catch (Exception) {

				// If an exception is caught, throw a fit in the console

				Console.WriteLine(" !! EXCEPTION:");
				Console.WriteLine("    An error occurred attempting to update the users database.");

			}

		}


		public static void UpdateUserMap(Dictionary<string, int> data)
		{

			try
			{

				// Serialize the updated mapping
				var text = JsonConvert.SerializeObject(data);

				// Write the serialized updated mapping to the proper file
				System.IO.File.WriteAllText(UserMapFile, text);

			} catch (Exception) {

				// If an exception is caught, throw a fit in the console

				Console.WriteLine(" !! EXCEPTION:");
				Console.WriteLine("    An error occurred attempting to update the user map.");

			}


		}

	}
}
