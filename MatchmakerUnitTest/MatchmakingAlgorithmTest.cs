using NUnit.Framework;
using MatchmakerAPI;
using System.Collections.Generic;
using System;

namespace MatchmakerUnitTest {
    [TestFixture]
    public class MatchMakingAlgorithmTest {
        [Test]
        public void MatchmakingAlgorithm_GetRandomUsers_AllUsers() {
            //Arrange
            int num = 120;
            int users = 20;
            List<UserData> testList = new List<UserData>();
            for (int i = 0; i < users; i++) {
                testList.Add(new UserData());
            }

            //Act
            var result = MatchmakerAPI.MatchmakingAlgorithm.GetRandomUsers(testList, num);

            //Assert
            Console.WriteLine(result.Count);
            Assert.IsTrue(result.Count == users);
        }

        [Test]
        public void MatchmakingAlgorithm_GetRandomUsers_MaxUsers() {
            //Arrange
            int num = 4;
            int users = 12;
            List<UserData> testList = new List<UserData>();
            for (int i = 0; i < users; i++) {
                testList.Add(new UserData());
            }

            //Act
            var result = MatchmakerAPI.MatchmakingAlgorithm.GetRandomUsers(testList, num);

            //Assert
            Assert.IsTrue(result.Count == num);
        }
    }
}