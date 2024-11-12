using EldenBoost.Infrastructure.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EldenBoost.Core.Models.Chat
{
    public class ChatViewModel
    {
        public int OrderId { get; set; }
        public string ReceiverId { get; set; } = null!;
        public List<ChatMessage> Messages { get; set; } = new List<ChatMessage>();
    }
}
