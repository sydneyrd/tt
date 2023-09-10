using shortid.Configuration;
using shortid;

namespace tt.helpers
{
    public class GeneratePlayerId
    {
        public static string Generate()
        {
            string id = ShortId.Generate(options: new GenerationOptions(useNumbers: true, length: 8));
            return id;
        }
    }
}
