using Faker;
using Bogus;
namespace tt.helpers
{
    public class GenerateUserName
    {
        public static string Generate()

        {
            var username = $"{Lorem.Words(1).First()}{Lorem.Words(1).First()}";
            Console.WriteLine(username);
            return username;
        }



    }
}
