using Microsoft.Extensions.Configuration;



namespace tt.Services
{
    public class OpenaiService
    {
        private readonly string _apiKey;
        public string ApiKey => _apiKey;

        public OpenaiService(IConfiguration configuration)
    {
            try
            {
                _apiKey = configuration["OPENAIAPIKEY"] ?? Environment.GetEnvironmentVariable("OPENAIAPIKEY");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}