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
        public CreatedAtActionResult AddNewMessage(NewMessage data)
        {
            string fileLocation = "/home/student/data/chats.json";
            using (StreamReader r = new StreamReader(fileLocation))
            {
                string json = r.ReadToEnd();
                var chats = JsonConvert.DeserializeObject<Dictionary<string, List<Message>>>(json);
                if (chats.ContainsKey(data.chat))
                {
                    List<Message> manipulateChat;
                    chats.TryGetValue(data.chat, out manipulateChat);
                    manipulateChat.Add(data.content);
                }
                else
                {
                    var newChat = new List<Message>();
                    newChat.Add(data.content);
                    chats.Add(data.chat, newChat);
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
        //set msg to read
        [HttpGet("read/id={id}/u={u}")]
        public void SetToRead(string id,string u)
        {
            int i = Int32.Parse(u);
            string fileLocation = "/home/student/data/chats.json";
            using (StreamReader r = new StreamReader(fileLocation))
            {
                string json = r.ReadToEnd();
                var chats = JsonConvert.DeserializeObject<Dictionary<string, List<Message>>>(json);
                List<Message> mList = chats[id];
                foreach (var m in mList.Where(x=>x.sender!=i&&!x.seen))
                {
                    m.seen = true;
                }                                 
                var text = JsonConvert.SerializeObject(chats);
                System.IO.File.WriteAllText(fileLocation, text);
            }
        }

        [HttpGet("get/id={id}")]
        public List<Message> RetrieveMessages(string id)
        {
            string fileLocation = "/home/student/data/chats.json";
            using (StreamReader r = new StreamReader(fileLocation))
            {
                string json = r.ReadToEnd();
                var chats = JsonConvert.DeserializeObject<Dictionary<string, List<Message>>>(json);
                if (chats.ContainsKey(id))
                {
                    List<Message> returnChat;
                    chats.TryGetValue(id, out returnChat);
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
