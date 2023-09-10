namespace tt.helpers
{
    public class GetDynamicDelay
    {
        public static int GetResponseDelay(string response)
        {
            const int baseDelay = 1000;
            const int delayPerWord = 300;
            int wordCount = response.Split(' ').Length;
            return baseDelay + wordCount * delayPerWord;
        }

    }
}
