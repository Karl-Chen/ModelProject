//using System.Security.Cryptography;
//using Microsoft.AspNetCore.SignalR;
//using Newtonsoft.Json.Linq;

//namespace WebProject.Hubs
//{
//    public class PushMessage : Hub
//    {
//        public async Task SendMessage(string user, string message)
//        {
//            await Clients.All.SendAsync("ReceiveMessage", user, message);
//        }
//    }
//}
