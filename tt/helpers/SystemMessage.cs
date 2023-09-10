using tt.Classes;

namespace tt.helpers
{
    public class SystemMessage
    {
        public float top_p { get; set; }
        public float temperature { get; set; }
        public List<HttpMessage> messages = new List<HttpMessage>();
        public string model = "gpt-4";

     public SystemMessage(float topP, float temp, HttpMessage message)
        {
            top_p = topP;
            temperature = temp;
            messages.Add(message);
        }
    }
}