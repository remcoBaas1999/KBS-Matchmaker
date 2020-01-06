using MatchMakerClassLibrary;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace MatchmakerUnitTest
{
    [TestFixture]
    class MatchmakerAPI_ClientTest
    {
        [Test]
        public void MatchmakerAPI_DeserializeUserData_returnsDeserializedData()
        {
            //test data
            string user = MatchmakerAPI_Client.GetUserData("info@guusapeldoorn.nl");
            //act
            UserData result = MatchMakerClassLibrary.MatchmakerAPI_Client.DeserializeUserData(user);

            //test
            Assert.IsTrue(result.realName == "Guus Apeldoorn");

        }
        [Test]
        public void MatchmakerAPI_Client_DeserializeMessageData_createNewDataWhenJsonIsNull()
        {
            //data
            string json = null;
            //act
            var result = MatchmakerAPI_Client.DeserializeMessageData(json);
            //test
            Assert.IsTrue(result.Count == 0);

        }
    }
}
