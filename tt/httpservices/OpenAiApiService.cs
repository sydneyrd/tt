using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;
using tt.Classes;
using tt.helpers;
using tt.Interfaces;
using tt.Services;

namespace tt.httpservices
{
    public class OpenAIApiService
    {
        private string _apiKey;

        public OpenAIApiService(OpenaiService openaiService)
        {
            try
            {
                _apiKey = openaiService.ApiKey; 
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

    
      private  List<HttpMessage> ConvertMessages(List<IMessage> existingMessages)
        {
            var newMessages = new List<HttpMessage>();

            foreach (var existingMessage in existingMessages)
            {
                var newMessage = new HttpMessage
                {
                    role = "user", 
                    content = $" PlayerName: {existingMessage.User}\n{existingMessage.Text}"
                };

                newMessages.Add(newMessage);
            }

            return newMessages;
        }

        public  async  Task<string> GenerateResponse(AIPlayer player, List<IMessage> messages)
        {
            try
            {
               SystemMessage fullMessage = player.SystemMessage;
              var convertedMessages = ConvertMessages(messages);
                fullMessage.messages.AddRange(convertedMessages);
                var httpClient = new HttpClient();

                // Set the "Authorization" header
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _apiKey);

                // Convert the request body to JSON
                var jsonRequestBody = JsonConvert.SerializeObject(fullMessage);

                // Set the content type of the request
                var httpContent = new StringContent(jsonRequestBody, Encoding.UTF8, "application/json");

                // Send the POST request
                var response = await httpClient.PostAsync("https://api.openai.com/v1/chat/completions", httpContent);

                // Get the response content
                var responseContent = await response.Content.ReadAsStringAsync();

                // Parse the response content as JSON
                var jsonResponse = JsonConvert.DeserializeObject<dynamic>(responseContent);
                Console.WriteLine(jsonResponse);

                // Return the generated text
                return jsonResponse.choices[0].message.content.ToString();
            }
                       catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return "error";
            }
        }

    }
    }
