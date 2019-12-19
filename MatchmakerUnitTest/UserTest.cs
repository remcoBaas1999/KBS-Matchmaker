using Matchmaker;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace MatchmakerUnitTest
{
    [TestFixture]
    class UserTest
    {
        [Test]
        public void User_CalculateAge_ReturnsAge()
        {
            //test data
            long birthDate = 787313560;
            int age = DateTime.Now.Year - 1994;
            //act
            int result = User.CalculateAge(birthDate);
            //Test
            Assert.IsTrue(result >= 24 && result <= 26);
        }
    }
}
