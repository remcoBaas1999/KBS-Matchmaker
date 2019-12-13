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
	public class UserController : ControllerBase {
		[HttpGet("get/id={id}")]
		public UserData UserById(int id) {
			using (StreamReader r = new StreamReader("/home/student/data/users.json")) {
				string json = r.ReadToEnd();
				try {
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

		[HttpGet("get/matches/id={id}")]
		public UserData[] GetMatches(int id) {
			using (StreamReader r = new StreamReader("/home/student/data/users.json")) {
				string json = r.ReadToEnd();
				int firstRandomSelection = 120;
				int scoredSelection = 4;
				int finalRandomSelection = 4;
				try {
					var users = JsonConvert.DeserializeObject<Dictionary<int, UserData>>(json);
					return FindMatches(users, id, firstRandomSelection, scoredSelection, finalRandomSelection);
				} catch (System.Collections.Generic.KeyNotFoundException e) {
					return new UserData[finalRandomSelection];
				}
			}
		}

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
			int key = data.userid;

			using (StreamReader r = new StreamReader("/home/student/data/users.json")) {
				string json = r.ReadToEnd();

				var users = JsonConvert.DeserializeObject<Dictionary<int, UserData>>(json);

				users[key].coverImage = data.imageName;

				var text = JsonConvert.SerializeObject(users);
				System.IO.File.WriteAllText(@"/home/student/data/users.json", text);
			}

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

		public UserData[] FindMatches(Dictionary<int, UserData> users, int currentUserID, int firstRandomSelection, int scoredSelection, int finalRandomSelection) {
			UserData[] userDatas = new UserData[finalRandomSelection];

			//Make a random selection of 120 users
			Dictionary<int, UserData> profiles = RandomSelection(users, firstRandomSelection);

			Console.Write($"\nAfter selecting random 120 ({profiles.Count} users): ");
			foreach (KeyValuePair<int, UserData> profile in profiles) {
				Console.Write($"{profile.Value.realName}, ");
			}

			//Score each of the users based on hobbies, age and city
			Dictionary<KeyValuePair<int, UserData>, int> scoredUsers = ScoreUsers(profiles, currentUserID);

			Console.Write($"\nAfter scoring users ({scoredUsers.Count} users): ");
			foreach (KeyValuePair<KeyValuePair<int, UserData>, int> scoredUser in scoredUsers) {
				Console.Write($"{scoredUser.Key.Value.realName}, ");
			}

			//Sort users from highest to lowest score, throw away the scores and only keep the 12 users with the highest score
			List<UserData> profileSelection = OrderUsersByScoreDescending(scoredUsers, scoredSelection);

			Console.Write($"\nAfter ordering users by score (high to low) and only keeping the 12 users with the highest score ({profileSelection.Count} users): ");
			foreach (UserData profile in profileSelection) {
				Console.Write($"{profile.realName}, ");
			}

			//Pick 4 users randomly out of the 12 with the highest score
			profileSelection = RandomSelection(profileSelection, finalRandomSelection);

			Console.Write($"\nAfter selecting random 4 ({profileSelection.Count} users): ");
			foreach (UserData profile in profileSelection) {
				Console.Write($"{profile.realName}, ");
			}

			//Put the users in a UserData array
			int count = 0;
			foreach (UserData user in profileSelection) {
				userDatas[count] = user;
				count++;
			}
			return userDatas;
		}

		private Dictionary<int, UserData> RandomSelection (Dictionary<int, UserData> users, int randomAmountToBeSelected) {
			Dictionary<int, UserData> profiles = new Dictionary<int, UserData>();

			//Make a list of all the user IDs
			List<int> userIDs = users.Keys.ToList<int>();

			Random rnd = new Random();

			//Fill the profiles Dictionary with random users from the users Dictionary
			while (profiles.Count < Math.Min(randomAmountToBeSelected, users.Count)) {

				//Select a random user from users
				int userID = userIDs[rnd.Next(userIDs.Count)];

				//If the user is not already in profiles, add it to profiles
				if (!profiles.Keys.ToList<int>().Contains(userID)) {
					profiles.Add(userID, users[userID]);
				}
			}
			return profiles;
		}

		private List<UserData> RandomSelection (List<UserData> users, int randomAmountToBeSelected) {
			List<UserData> profiles = new List<UserData>();

			//Make a list of all the user IDs
			List<int> userIDs = new List<int>();

			//Fill the list with user IDs
			foreach (UserData user in users) {
				userIDs.Add(user.id);
			}

			Random rnd = new Random();

			//Fill the profiles List with random users from the users Dictionary
			while (profiles.Count < Math.Min(randomAmountToBeSelected, users.Count)) {

				//Select a random user from users
				int userID = userIDs[rnd.Next(userIDs.Count)];

				//If the user is not already in profiles ...
				bool profileIsAlreadyInProfiles = false;
				foreach (UserData profile in profiles) {
					if (profile.id == userID) {
						profileIsAlreadyInProfiles = true;
					}
				}
				//... add it to profiles
				if (profileIsAlreadyInProfiles) {
					foreach (UserData user in users) {
						if (user.id == userID) {
							profiles.Add(user);
						}
					}
				}
			}
			return profiles;
		}

		private Dictionary<KeyValuePair<int, UserData>, int> ScoreUsers (Dictionary<int, UserData> users, int currentUserID) {
			Dictionary<KeyValuePair<int, UserData>, int> scoredUsers = new Dictionary<KeyValuePair<int, UserData>, int>();

			//Score all users ...
			foreach (KeyValuePair<int, UserData> user in users) {

				//... apart from the current user
				if (user.Key != currentUserID) {
					UserData currentUser = users[currentUserID];

					int hobbies = 0;
					int age = 0;
					int city = 0;

					//Add 2 points to the score for each common hobby
					if (user.Value.hobbies != null && currentUser.hobbies != null) {
						if (user.Value.hobbies.Count != 0 && currentUser.hobbies.Count != 0) {
							foreach (Hobby hobby in user.Value.hobbies) {
								if (currentUser.hobbies.Contains(hobby)) {
									hobbies += 2;
								}
							}
						}
					}

					//Add max. 10 points to the score, from 10 points if the users are the same age, to 0 points if the users are 10 years apart
					if (user.Value.birthdate != 0 && currentUser.birthdate != 0) {
						int userAge = CalculateAge(UnixTimeToDate(user.Value.birthdate));
						int currentUserAge = CalculateAge(UnixTimeToDate(currentUser.birthdate));
						int ageDiff = Math.Max(userAge, currentUserAge) - Math.Min(userAge, currentUserAge);
						age = Math.Max((10 - ageDiff), 0);
					}

					//Add 10 points to the score if the cities match
					if (user.Value.city != null && currentUser.city != null) {
						if (user.Value.city == currentUser.city) {
							city = 10;
						}
					}

					//Add scores together and assign it to the user
					int score = hobbies + age + city;
					scoredUsers.Add(user, score);

					Console.WriteLine($"{user.Value.realName}: {score}");
				}
			}
			return scoredUsers;
		}

		private List<UserData> OrderUsersByScoreDescending (Dictionary<KeyValuePair<int, UserData>, int> scoredUsers, int scoredSelection) {
			List<UserData> profiles = new List<UserData>();
			List<int> scores = scoredUsers.Values.ToList<int>();
			int count = 0;
			while (scores.Count > 0 && count < scoredSelection) {
				foreach (KeyValuePair<KeyValuePair<int, UserData>, int> scoredUser in scoredUsers) {
					if (scoredUser.Value == scores.Max()) {
						profiles.Add(scoredUser.Key.Value);
					}
				}
				scores.Remove(scores.Max());
				count++;
			}
			return profiles;
		}

		private int CalculateAge(DateTime dob) {
			//Calculate the difference between years
			DateTime today = DateTime.Today;
			int age = today.Year - dob.Year;
			bool month = false;

			//Now check the months and days
			if (today.Month < dob.Month) {
				age--;
			} else {

				//Month has already passed
				month = true;
			}

			if (month == false && today.Day < dob.Day) {
				age--;
			}

			return age;
		}

		private DateTime UnixTimeToDate(long _bday) {
			DateTime start = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
			DateTime bday = start.AddSeconds(_bday).ToLocalTime();
			return bday;
		}
	}
}
