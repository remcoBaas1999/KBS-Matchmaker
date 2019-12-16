using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Runtime.Serialization;
using MatchmakerAPI.Controllers;

namespace MatchmakerAPI
{
	public static class MatchmakingAlgorithm
  {

    // Declare weights
    public static int proximityWt = 1;
    public static int hobbiesWt = 2;
    public static int ageWt = 0;



    public static UserData[] FindMatches(int forUserId, int sampleNum, int scoredNum, int returnNum)
    {

      // Retrieve the users database
      var users = UserController.LoadUsers();
      // Dictionary<int, UserData>

      try {

        // Get user data for the user for which the matches are being found
        var forUser = users[forUserId];

        // Get a random sample from the users database
        // var sample = GetRandomUsers(users.Values.ToList(), sampleNum);
				var sample = users.Values.ToList();

        // Prepare unsorted variant of the return value
        var returnVal_unsorted = new List<KeyValuePair<int, UserData>>();


        // For each user in our sample, calculate a weighted matching score and
        // add it to a list for sorting
        foreach (UserData user in sample)
        {

          // Calculate composite scores
          var proximity = CompareLocation(forUser, user);
          var hobbiesInCommon = CompareHobbies(forUser, user);
          var ageDifference = CompareAge(forUser, user);

          // Calculate total weighted score
          var totalScore = ((proximity * proximityWt) + (hobbiesInCommon * hobbiesWt)) - (ageDifference * ageWt);

          var scoredUser = new KeyValuePair<int, UserData>(totalScore, user);

          returnVal_unsorted.Add(scoredUser);

        }

        // Sort list by user score
        List<UserData> returnVal_list = SortByScore(returnVal_unsorted, scoredNum);

        // Prepare a random subset of the scored users to be returned
        // List<UserData> returnVal_random = GetRandomUsers(returnVal_list, returnNum);

        // Convert the list to the return format (array)
        var returnVal = returnVal_list.ToArray();

        return returnVal;
      } catch (System.Collections.Generic.KeyNotFoundException) {

        // Return null if the key (user id) does not exist
        Console.WriteLine("Key not found");
        return null;

      }
    }



    public static List<UserData> GetRandomUsers(List<UserData> users, int num)
    {
      var returnVal = new List<UserData>();

      // Initialize a new Random object
      var rng = new Random();

      while (returnVal.Count < num)
      {

        // If the users list is smaller than the number of users to be selected,
        // abort the method when all users have been selected.
        if (users.Count == returnVal.Count) {
          return returnVal;
        }

        // Select a random index from the List<UserData>
        var user = users[rng.Next(users.Count)];

        // Remove the user from the pool of selectable users
        users.Remove(user);

        // Add the removed user to the return value
        returnVal.Add(user);

      }

      return returnVal;
    }



    public static List<UserData> SortByScore(List<KeyValuePair<int, UserData>> scoredUsers, int scoredNum)
    {
      Console.WriteLine("\t =====");
      Console.WriteLine("Sorting by score:");
      Console.WriteLine("\t =====");

      var returnVal = new List<UserData>();

      // Order the scored users by key and take the top N, where N == scoredNum
      var returnVal_kvps = scoredUsers.OrderByDescending(scoredUser => scoredUser.Key).Take(scoredNum).ToList();

      foreach (KeyValuePair<int, UserData> kvp in returnVal_kvps)
      {
        Console.WriteLine($"{kvp.Value.realName}: {kvp.Key}");
        returnVal.Add(kvp.Value);
      }

      return returnVal;
    }



    public static int CompareLocation(UserData forUser, UserData user)
    {

      // Terminate method when either of the user's city field is not
      // defined
      if (forUser.city == null || user.city == null)
      {
        return 0;
      }

      // NOTE: this "algorithm" can be refined in future sprints.

      if (forUser.city == user.city)
      {
        // If the cities are the same, return 1
        return 1;
      }
      else
      {
        // If the cities are not the same, return 0
        return 0;
      }
    }



    public static int CompareHobbies(UserData forUser, UserData user)
    {
			int returnVal = 0;

      // Terminate method when either of the user's hobbies field is not
      // defined
      if (forUser.hobbies == null || user.hobbies == null)
      {
        return 0;
      }

      // Terminate method when either of the user's hobbies field is empty
      if (forUser.hobbies.Count == 0 || user.hobbies.Count == 0)
      {
        return 0;
      }

      // Return the number of hobbies that appear in both hobbies fields
			foreach (Hobby x in user.hobbies)
			{
				foreach (Hobby y in forUser.hobbies)
				{
					if (x.displayName == y.displayName)
					{
						returnVal++;
					}
				}
			}

      return returnVal;
    }



    public static int CompareAge(UserData forUser, UserData user)
    {

      // Convert timestamps to a timestamp C# can read,
      // and calculate the users' ages with those
      var forUserAge = CalculateAge(UnixTimeToDate(forUser.birthdate));
      var userAge = CalculateAge(UnixTimeToDate(user.birthdate));

      // Subtract the highest age from the lowest age
      var returnVal = Math.Max(forUserAge, userAge) - Math.Min(forUserAge, userAge);

      return returnVal;
    }


    // TODO: remove commented code after testing

  //   public static UserData[] FindMatches(Dictionary<int, UserData> users, int currentUserID, int firstRandomSelection, int scoredSelection, int finalRandomSelection) {
  //     UserData[] userDatas = new UserData[finalRandomSelection];
  //
  //     //Make a random selection of 120 users
  //     Dictionary<int, UserData> profiles = RandomSelection(users, firstRandomSelection);
  //
  //     Console.Write($"\nAfter selecting random 120 ({profiles.Count} users): ");
  //     foreach (KeyValuePair<int, UserData> profile in profiles) {
  //       Console.Write($"{profile.Value.realName}, ");
  //     }
  //
  //     //Score each of the users based on hobbies, age and city
  //     Dictionary<KeyValuePair<int, UserData>, int> scoredUsers = ScoreUsers(profiles, currentUserID);
  //
  //     Console.Write($"\nAfter scoring users ({scoredUsers.Count} users): ");
  //     foreach (KeyValuePair<KeyValuePair<int, UserData>, int> scoredUser in scoredUsers) {
  //       Console.Write($"{scoredUser.Key.Value.realName} - {scoredUser.Value}, ");
  //     }
  //
  //     //Sort users from highest to lowest score, throw away the scores and only keep the 12 users with the highest score
  //     List<UserData> profileSelection = OrderUsersByScoreDescending(scoredUsers, scoredSelection);
  //
  //     Console.Write($"\nAfter ordering users by score (high to low) and only keeping the 12 users with the highest score ({profileSelection.Count} users): ");
  //     foreach (UserData profile in profileSelection) {
  //       Console.Write($"{profile.realName}, ");
  //     }
  //
  //     //Pick 4 users randomly out of the 12 with the highest score
  //     profileSelection = RandomSelection(profileSelection, finalRandomSelection);
  //
  //     Console.Write($"\nAfter selecting random 4 ({profileSelection.Count} users): ");
  //     foreach (UserData profile in profileSelection) {
  //       Console.Write($"{profile.realName}, ");
  //     }
  //
  //     //Put the users in a UserData array
  //     int count = 0;
  //     foreach (UserData user in profileSelection) {
  //       userDatas[count] = user;
  //       count++;
  //     }
  //     return userDatas;
  //   }
  //
  //   private static Dictionary<int, UserData> RandomSelection (Dictionary<int, UserData> users, int randomAmountToBeSelected) {
  //     Dictionary<int, UserData> profiles = new Dictionary<int, UserData>();
  //
  //     //Make a list of all the user IDs
  //     List<int> userIDs = users.Keys.ToList<int>();
  //
  //     Random rnd = new Random();
  //
  //     //Fill the profiles Dictionary with random users from the users Dictionary
  //     while (profiles.Count < Math.Min(randomAmountToBeSelected, users.Count)) {
  //
  //       //Select a random user from users
  //       int userID = userIDs[rnd.Next(userIDs.Count)];
  //
  //       //If the user is not already in profiles, add it to profiles
  //       if (!profiles.Keys.ToList<int>().Contains(userID)) {
  //         profiles.Add(userID, users[userID]);
  //       }
  //     }
  //     return profiles;
  //   }
  //
  //   private static List<UserData> RandomSelection (List<UserData> users, int randomAmountToBeSelected) {
  //     List<UserData> profiles = new List<UserData>();
  //
  //     //Make a list of all the user IDs
  //     List<int> userIDs = new List<int>();
  //
  //     //Fill the list with user IDs
  //     foreach (UserData user in users) {
  //       userIDs.Add(user.id);
  //     }
  //
  //     Random rnd = new Random();
  //
  //     //Fill the profiles List with random users from the users Dictionary
  //     while (profiles.Count < Math.Min(randomAmountToBeSelected, users.Count)) {
  //
  //       //Select a random user from users
  //       int userID = userIDs[rnd.Next(userIDs.Count)];
  //
  //       //If the user is not already in profiles ...
  //       bool profileIsAlreadyInProfiles = false;
  //       foreach (UserData profile in profiles) {
  //         if (profile.id == userID) {
  //           profileIsAlreadyInProfiles = true;
  //         }
  //       }
  //       //... add it to profiles
  //       if (profileIsAlreadyInProfiles) {
  //         foreach (UserData user in users) {
  //           if (user.id == userID) {
  //             profiles.Add(user);
  //           }
  //         }
  //       }
  //     }
  //     return profiles;
  //   }
  //
  //   private static Dictionary<KeyValuePair<int, UserData>, int> ScoreUsers (Dictionary<int, UserData> users, int currentUserID) {
  //     Dictionary<KeyValuePair<int, UserData>, int> scoredUsers = new Dictionary<KeyValuePair<int, UserData>, int>();
  //
  //     //Score all users ...
  //     foreach (KeyValuePair<int, UserData> user in users) {
  //
  //       //... apart from the current user
  //       if (user.Key != currentUserID) {
  //         UserData currentUser = users[currentUserID];
  //
  //         int hobbies = 0;
  //         int age = 0;
  //         int city = 0;
  //
  //         //Add 2 points to the score for each common hobby
  //         if (user.Value.hobbies != null && currentUser.hobbies != null) {
  //           if (user.Value.hobbies.Count != 0 && currentUser.hobbies.Count != 0) {
  //             foreach (Hobby hobby in user.Value.hobbies) {
  //               if (currentUser.hobbies.Contains(hobby)) {
  //                 hobbies += 2;
  //               }
  //             }
  //           }
  //         }
  //
  //         //Add max. 10 points to the score, from 10 points if the users are the same age, to 0 points if the users are 10 years apart
  //         if (user.Value.birthdate != 0 && currentUser.birthdate != 0) {
  //           int userAge = CalculateAge(UnixTimeToDate(user.Value.birthdate));
  //           int currentUserAge = CalculateAge(UnixTimeToDate(currentUser.birthdate));
  //           int ageDiff = Math.Max(userAge, currentUserAge) - Math.Min(userAge, currentUserAge);
  //           age = Math.Max((10 - ageDiff), 0);
  //         }
  //
  //         //Add 10 points to the score if the cities match
  //         if (user.Value.city != null && currentUser.city != null) {
  //           if (user.Value.city == currentUser.city) {
  //             city = 10;
  //           }
  //         }
  //
  //         //Add scores together and assign it to the user
  //         int score = hobbies + age + city;
  //         scoredUsers.Add(user, score);
  //       }
  //     }
  //     return scoredUsers;
  //   }
  //
  //   private static List<UserData> OrderUsersByScoreDescending (Dictionary<KeyValuePair<int, UserData>, int> scoredUsers, int scoredSelection) {
  //     List<UserData> profiles = new List<UserData>();
  //     List<int> scores = scoredUsers.Values.ToList<int>();
  //     int count = 0;
  //     while (scores.Count > 0 && count < scoredSelection) {
  //       foreach (KeyValuePair<KeyValuePair<int, UserData>, int> scoredUser in scoredUsers) {
  //         if (scoredUser.Value == scores.Max()) {
  //           profiles.Add(scoredUser.Key.Value);
  //         }
  //       }
  //       scores.Remove(scores.Max());
  //       count++;
  //     }
  //     return profiles;
  //   }


    private static int CalculateAge(DateTime dob) {
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

    private static DateTime UnixTimeToDate(long _bday) {
      DateTime start = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
      DateTime bday = start.AddSeconds(_bday).ToLocalTime();
      return bday;
    }
  }

}
