using tt.Interfaces;
using tt.httpservices;
using Microsoft.AspNetCore.Routing.Template;
using Microsoft.AspNetCore.Http;
using tt.Services;

namespace tt.Classes
{
    public class AIService
    {
        private OpenAIApiService _openAIApiService;

        public AIService(OpenAIApiService openAIApiService)
        {
     
            _openAIApiService = openAIApiService;
            
        }

        public async Task<string> GetResponse(List<IMessage> messages, AIPlayer player)
        {
            var response = await _openAIApiService.GenerateResponse(player, messages);
            return response;
        }


    }
}