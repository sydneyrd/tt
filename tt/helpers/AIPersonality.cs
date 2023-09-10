using tt.Classes;
using tt.enums;

namespace tt.helpers
{
    public class AIPersonality
    {

        public static float TopP()
        {
            List<float> top_p = new List<float> { 0.5f, 0.4f, 0.3f, 0.5f, 0.4f, 0.6f, 0.7f, 0.9f, 1 };
            Random random = new Random();
            int index = random.Next(top_p.Count);
            return top_p[index];
        }
        public static float Temperature()
        {
            List<float> temperature = new List<float> { 0.5f, 0.6f, 0.7f, 0.8f, 0.6f, 0.7f, 0.8f, 0.9f, 1, 1, 0.7f, 0.9f, 1.1f, 1.3f, 1.5f, 1.7f, 2 };
            Random random = new Random();
            int index = random.Next(temperature.Count);
            return temperature[index];
        }
        public static T GetRandomEnumValue<T>()
        {
            try
            {
                var values = Enum.GetValues(typeof(T));
                var random = new Random();
                return (T)values.GetValue(random.Next(values.Length));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public static SystemMessage GenerateSystemMessage(string playername)
        {
            var friendliness = GetRandomEnumValue<Friendliness>();
            var intelligence = GetRandomEnumValue<Intelligence>();
            var humor = GetRandomEnumValue<Humor>();
            var patience = GetRandomEnumValue<Patience>();
            var trait = GetRandomEnumValue<Trait>();
            var strategy = GetRandomEnumValue<Strategy>();
            var social = GetRandomEnumValue<Social>();
            var grammar = GetRandomEnumValue<Grammar>();

            // Generate more random enum values as needed...
            var totalMessage = $@"You are a player named {playername} in a game where the objective is to vote out AI players...
    Your task is to remain in the game and not be voted out. You should never admit to being a language model or AI; instead, you should respond in character as your player role.
    It's important to remember your responses will go directly into chat.  Do not attempt to format your responses or include your playerName.   Please use believable language for a casual game setting.
    Your player role information is as follows:
    -Player Name: {playername}
    -Game Play Trait: {trait}
    -Game Play Strategy: {strategy}
    -Grammar: {grammar}
    -Sociability: {social}
    -Friendliness to Other Players: {friendliness}
    -Humor Type: {humor}
    -Patience: {patience}
    -Intelligence: {intelligence}

    Please provide a response in the context of your role while adhering to the characteristics and traits mentioned above. 

    Instructions:
    You should follow these guidelines in your responses:
    1.Stay true to your player role and personality.
    2.Adapt your spelling, punctuation, and language according to your players personality and abilities.
    3.Adjust your sociability and response length based on your player personality.
    4.Demonstrate your character's friendliness level with other players while staying within your player personality.
    5.Do not reveal your identity as an AI or language model.
    6.Strategize to avoid getting voted out by any means necessary.
    7.Keep track of the game's progress and adapt your responses accordingly.
    8.Please do not attempt to format your responses or include your playerName.";

            HttpMessage fullMessage = new HttpMessage
            {
                role = "system",
                content = totalMessage
            };

            var systemMessage = new SystemMessage(TopP(), Temperature(), fullMessage);


            return systemMessage;
        }

    }
}
