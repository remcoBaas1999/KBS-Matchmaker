using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Runtime.Serialization;
using MatchmakerAPI.Models;

namespace MatchmakerAPI.Controllers
{
    [Route("/messages/")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        [HttpPost("post/new")]
        public CreatedAtActionResult AddNewMessage(Message data)
        {
            //data misschien veranderen
            string fileLocation = "/home/student/data/chats.json";
            using (StreamReader r = new StreamReader(fileLocation))
            {
                string json = r.ReadToEnd();
                var chats = JsonConvert.DeserializeObject<Dictionary<string, List<Message>>>(json);
                if (chats.ContainsKey(data.id))
                {
                    List<Message> manipulateChat;
                    chats.TryGetValue(data.id, out manipulateChat);
                    manipulateChat.Add(data);

                }
                else
                {
                    var newChat = new List<Message>();
                    newChat.Add(data);
                    chats.Add(data.id, newChat);

                }
                var text = JsonConvert.SerializeObject(chats);
                System.IO.File.WriteAllText(fileLocation, text);
            }


            try
            {
                return CreatedAtAction("AddNewMessage", new { success = true });
            }
            catch (System.InvalidOperationException)
            {
                return null;
            }
        }
        [HttpGet("get/id={id}")]
        public List<Message> RetrieveMessages(string id)
        {
            string fileLocation = "/home/student/data/chats.json";
            using (StreamReader r = new StreamReader(fileLocation))
            {
                string json = r.ReadToEnd();
                var chat = JsonConvert.DeserializeObject<Dictionary<string, List<Message>>>(json);
                if (chat.ContainsKey(id))
                {
                    List<Message> returnChat;
                    chat.TryGetValue(id, out returnChat);
                    return returnChat;
                }
                else
                {
                    return new List<Message>();
                }
            }
        }
    }
}
