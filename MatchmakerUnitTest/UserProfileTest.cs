using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace MatchmakerUnitTest
{
    [TestFixture]
    class UserProfileTest
    {
        [Test]
        public void UserProfile_CalculateAge_ReturnsAge()
        {
            //test data
            DateTime birthDate = new DateTime(1994,12,13);
            int age = DateTime.Now().years - 1994;

            //act
            int result = CalculateAge(birthDate);

            //Test
            Assert.isTrue(result >= 24 && result <= 26);
        }
    }
}
