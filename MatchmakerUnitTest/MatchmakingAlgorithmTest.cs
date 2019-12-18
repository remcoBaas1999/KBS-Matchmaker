using NUnit.Framework;
using MatchmakerAPI;
using System.Collections.Generic;
using System;

namespace MatchmakerUnitTest {
    [TestFixture]
    public class MatchMakingAlgorithmTest {
        //Tests if the returned List is as long as it should be
        [Test]
        public void MatchmakingAlgorithm_GetRandomUsers_AllUsers() {
            //Arrange
            int num = 120;
            int usersCount = 20;
            List<UserData> users = new List<UserData>();
            for (int i = 0; i < usersCount; i++) {
                users.Add(new UserData());
            }

            //Act
            var result = MatchmakerAPI.MatchmakingAlgorithm.GetRandomUsers(users, num);

            //Assert
            Console.WriteLine(result.Count);
            Assert.IsTrue(result.Count == usersCount);
        }

        //Tests if the returned List is as long as it should be
        [Test]
        public void MatchmakingAlgorithm_GetRandomUsers_MaxUsers() {
            //Arrange
            int num = 4;
            int usersCount = 12;
            List<UserData> users = new List<UserData>();
            for (int i = 0; i < usersCount; i++) {
                users.Add(new UserData());
            }

            //Act
            var result = MatchmakerAPI.MatchmakingAlgorithm.GetRandomUsers(users, num);

            //Assert
            Assert.IsTrue(result.Count == num);
        }

        //Tests if the returned List is as long as it should be
        [Test]
        public void MatchmakingAlgorithm_SortByScore_UsersSorted() {
            //Arrange
            int scoredNum = 12;
            int scoredUsersCount = 20;
            List<KeyValuePair<int, UserData>> scoredUsers = new List<KeyValuePair<int, UserData>>();
            for (int i = 0; i < scoredUsersCount; i++) {
                scoredUsers.Add(new KeyValuePair<int, UserData>(i, new UserData()));
            }

            //Act
            var result = MatchmakerAPI.MatchmakingAlgorithm.SortByScore(scoredUsers, scoredNum);

            //Assert
            Assert.IsTrue(result.Count == scoredNum);
        }

        //Tests if one of the cities is null
        [Test]
        public void MatchmakingAlgorithm_CompareLocation_CityIsNull() {
            //Arrange
            UserData forUser = new UserData() { city = null };
            UserData user = new UserData() { city = "Amsterdam" };

            //Act
            var result = MatchmakerAPI.MatchmakingAlgorithm.CompareLocation(forUser, user);

            //Assert
            Assert.IsTrue(result == 0);
        }

        //Tests if the cities are equal
        [Test]
        public void MatchmakingAlgorithm_CompareLocation_CitiesAreEqual() {
            //Arrange
            UserData forUser = new UserData() { city = "Amsterdam" };
            UserData user = new UserData() { city = "Amsterdam" };

            //Act
            var result = MatchmakerAPI.MatchmakingAlgorithm.CompareLocation(forUser, user);

            //Assert
            Assert.IsTrue(result == 1);
        }

        //Tests if the cities are not equal
        [Test]
        public void MatchmakingAlgorithm_CompareLocation_CitiesAreNotEqual() {
            //Arrange
            UserData forUser = new UserData() { city = "Utrecht" };
            UserData user = new UserData() { city = "Amsterdam" };

            //Act
            var result = MatchmakerAPI.MatchmakingAlgorithm.CompareLocation(forUser, user);

            //Assert
            Assert.IsTrue(result == 0);
        }

        //Tests if one of the hobby fields is null
        [Test]
        public void MatchmakingAlgorithm_CompareHobbies_HobbiesIsNull() {
            //Arrange
            UserData forUser = new UserData() { hobbies = null };
            UserData user = new UserData() { hobbies = new List<Hobby>() };
            user.hobbies.Add(new Hobby() { displayName = "test1" });
            user.hobbies.Add(new Hobby() { displayName = "test2" });
            user.hobbies.Add(new Hobby() { displayName = "test3" });
            user.hobbies.Add(new Hobby() { displayName = "test4" });
            user.hobbies.Add(new Hobby() { displayName = "test5" });

            //Act
            var result = MatchmakerAPI.MatchmakingAlgorithm.CompareHobbies(forUser, user);

            //Assert
            Assert.IsTrue(result == 0);
        }

        //Tests if one of the hobby fields is empty
        [Test]
        public void MatchmakingAlgorithm_CompareHobbies_HobbiesIsEmpty() {
            //Arrange
            UserData forUser = new UserData() { hobbies = new List<Hobby>() };
            UserData user = new UserData() { hobbies = new List<Hobby>() };
            user.hobbies.Add(new Hobby() { displayName = "test1" });
            user.hobbies.Add(new Hobby() { displayName = "test2" });
            user.hobbies.Add(new Hobby() { displayName = "test3" });
            user.hobbies.Add(new Hobby() { displayName = "test4" });
            user.hobbies.Add(new Hobby() { displayName = "test5" });

            //Act
            var result = MatchmakerAPI.MatchmakingAlgorithm.CompareHobbies(forUser, user);

            //Assert
            Assert.IsTrue(result == 0);
        }

        //Tests if 0 of the hobbies are equal
        [Test]
        public void MatchmakingAlgorithm_CompareHobbies_0HobbiesAreEqual() {
            //Arrange
            UserData forUser = new UserData() { hobbies = new List<Hobby>() };
            UserData user = new UserData() { hobbies = new List<Hobby>() };
            forUser.hobbies.Add(new Hobby() { displayName = "test6" });
            forUser.hobbies.Add(new Hobby() { displayName = "test7" });
            forUser.hobbies.Add(new Hobby() { displayName = "test8" });
            forUser.hobbies.Add(new Hobby() { displayName = "test9" });
            forUser.hobbies.Add(new Hobby() { displayName = "test10" });
            user.hobbies.Add(new Hobby() { displayName = "test1" });
            user.hobbies.Add(new Hobby() { displayName = "test2" });
            user.hobbies.Add(new Hobby() { displayName = "test3" });
            user.hobbies.Add(new Hobby() { displayName = "test4" });
            user.hobbies.Add(new Hobby() { displayName = "test5" });

            //Act
            var result = MatchmakerAPI.MatchmakingAlgorithm.CompareHobbies(forUser, user);

            //Assert
            Assert.IsTrue(result == 0);
        }

        //Tests if 3 of the hobbies are equal
        [Test]
        public void MatchmakingAlgorithm_CompareHobbies_3HobbiesAreEqual() {
            //Arrange
            UserData forUser = new UserData() { hobbies = new List<Hobby>() };
            UserData user = new UserData() { hobbies = new List<Hobby>() };
            forUser.hobbies.Add(new Hobby() { displayName = "test1" });
            forUser.hobbies.Add(new Hobby() { displayName = "test6" });
            forUser.hobbies.Add(new Hobby() { displayName = "test7" });
            forUser.hobbies.Add(new Hobby() { displayName = "test4" });
            forUser.hobbies.Add(new Hobby() { displayName = "test5" });
            user.hobbies.Add(new Hobby() { displayName = "test1" });
            user.hobbies.Add(new Hobby() { displayName = "test2" });
            user.hobbies.Add(new Hobby() { displayName = "test3" });
            user.hobbies.Add(new Hobby() { displayName = "test4" });
            user.hobbies.Add(new Hobby() { displayName = "test5" });

            //Act
            var result = MatchmakerAPI.MatchmakingAlgorithm.CompareHobbies(forUser, user);

            //Assert
            Assert.IsTrue(result == 3);
        }

        //Tests if the age difference is correct
        [Test]
        public void MatchmakingAlgorithm_CompareAge_AgeDiffCorrect() {
            //Arrange
            long bd1 = 631148400;
            long bd2 = 946681200;
            UserData forUser = new UserData() { birthdate = bd1 };
            UserData user = new UserData() { birthdate = bd2 };

            //Act
            var result = MatchmakerAPI.MatchmakingAlgorithm.CompareAge(forUser, user);

            //Assert
            Assert.IsTrue(result == 10);
        }
    }
}