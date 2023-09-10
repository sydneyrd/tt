using tt.Interfaces;

namespace tt.Classes
{
    public class Message : IMessage
    {
        public string Text { get; set; }
        public string User { get; set; }
        public Message(string text, string user) { 
        
            Text = text;
            User = user;
        }
    }
}
