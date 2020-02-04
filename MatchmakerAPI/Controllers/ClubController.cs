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
	[Route("/club/")]
	public class ClubController : ControllerBase
	{

		public static string UsersFile = "/home/student/data/users.json";
		public static string UserMapFile = "/home/student/data/userMap.json";
		public static string ClubsFile = "/home/student/data/clubs.json";



		// // // //  GET REQUESTS  // // // // // // // // // // // // // // // //



		// Retrieve full user data by user id
		[HttpGet("get/id={id}")]
		public ClubData ClubById(int id)
		{

			try
			{

				// Load the users database into memory
				var clubs = ReadClubs();

				// Fetch the specific user we're looking for in a try/catch block
				// in case the input is not a valid key.
				var club = clubs[id.ToString()];

				// Return the specified user
				return club;

			} catch (System.Collections.Generic.KeyNotFoundException) {

				// If the requested user id does not exist, return a userdata object
				// with all fields nulled and display a warning on the server console.

				Console.WriteLine(" !! EXCEPTION:");
				Console.WriteLine("    An invalid club id was requested.");

				return new ClubData();

			} catch (Exception) {

				// If something else goes wrong, return a nulled UserData and display
				// a warning on the server console

				Console.WriteLine(" !! EXCEPTION:");
				Console.WriteLine("    Exception caught trying to fetch a club by id.");

				return new ClubData();

			}

		}





		// // // //  POST REQUESTS  // // // // // // // // // // // // // // // //



		// Add a new club
		[HttpPost("post/new")]
		public CreatedAtActionResult AddNewClub(ClubData data) {

			// Initialize the new club id variable
			int tmp_key;

			// Update the clubs database
			try
			{
				// Load the clubs database into memory
				var clubs = ReadClubs();

				// Initialize a Random object (because it isn't static)
				var rng = new Random();

				do {
					// Generate a random club id, and if it's taken, pick a new one
					// until you find one that isn't.

					tmp_key = rng.Next();

					// The club id is random for security/privacy reasons;
					// keys are harder to guess and it makes it harder for
					// potential attackers to download all the records
				} while (clubs.ContainsKey(tmp_key.ToString()));

				// Add the new club data to the in-memory clubs database with
				// the newly generated key
				clubs.Add(tmp_key.ToString(), data);

				// Write the updated clubs data to the database
				UpdateClubs(clubs);

			} catch (Exception) {

				// If something goes wrong, throw a fit in the console and
				// return a failure state

				Console.WriteLine(" !! EXCEPTION:");
				Console.WriteLine("    An error occurred attempting to add a club to the clubs database.");

				return CreatedAtAction("AddNewClub", new { success = false });

			}

			try
			{

				return CreatedAtAction("AddNewClub", new { success = true });

			} catch (System.InvalidOperationException) {

				return CreatedAtAction("AddNewClub", new { success = false });

			}

		}


		// Update an exisiting user's profile
		[HttpPost("post/update")]
		public AcceptedAtActionResult AddUserToClub(int userid, int clubid) {

			ClubData cdata;
			var clubs = ReadClubs();

			try {

				var udata = UserController.UserById(userid);

				if (udata.email == null)
				{
					throw new System.Collections.Generic.KeyNotFoundException();
				}

			} catch (System.Collections.Generic.KeyNotFoundException) {

				return AcceptedAtAction("AddUserToClub", new { success = false });

			}

			try {

				cdata = clubs[clubid];

				if (cdata.name == null)
				{
					throw new System.Collections.Generic.KeyNotFoundException();
				}

			} catch (System.Collections.Generic.KeyNotFoundException) {

				return AcceptedAtAction("AddUserToClub", new { success = false });

			}

			try {

				cdata.members.Add(userid);

				UpdateClubs(clubs);

			} catch (System.Collections.Generic.KeyNotFoundException) {

				return AcceptedAtAction("AddUserToClub", new { success = false });

			}

			try {

				return AcceptedAtAction("AddUserToClub", new { success = true });

			} catch (System.InvalidOperationException) {

				return AcceptedAtAction("AddUserToClub", new { success = false });

			}
		}



		// // // //  STATIC METHODS  // // // // // // // // // // // // // // // //


		// // // //  READING METHODS

		public static Dictionary<string, ClubData> ReadClubs()
		{

			// Open the users.json data file
			using (StreamReader r = new StreamReader(ClubsFile))
		    {
				// Load the contents into memory
		        string json = r.ReadToEnd();

				// Deserialize the json to something C# can do stuff to
		        var users = JsonConvert.DeserializeObject<Dictionary<string, UserData>>(json);

				// Return the deserialized data table
				return users;
		    }

		}





		// // // //  WRITING METHODS

		public static void UpdateClubs(Dictionary<string, ClubData> data)
		{

			try
			{

				// Serialize the updated users database
				var text = JsonConvert.SerializeObject(data);

				// Write the serialized updated database to the proper file
				System.IO.File.WriteAllText(ClubsFile, text);

			} catch (Exception) {

				// If an exception is caught, throw a fit in the console

				Console.WriteLine(" !! EXCEPTION:");
				Console.WriteLine("    An error occurred attempting to update the clubs database.");

			}

		}
	}
}
