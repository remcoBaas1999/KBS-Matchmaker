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

      // Retrieve the users database as Dictionary<int, UserData>
      var users = UserController.LoadUsers();

      try {

        // Get user data for the user for which the matches are being found
        var forUser = users[forUserId];

				// Remove that user from the pool of potential matches
				users.Remove(forUserId);

        // Get a random sample from the users database
        var sample = GetRandomUsers(users.Values.ToList(), sampleNum);
				// var sample = users.Values.ToList();

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
        List<UserData> returnVal_random = GetRandomUsers(returnVal_list, returnNum);

        // Convert the list to the return format (array)
        var returnVal = returnVal_random.ToArray();

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
        if (users.Count == 0) {
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
