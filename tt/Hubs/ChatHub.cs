using Microsoft.AspNetCore.SignalR;
using System.Linq.Expressions;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using tt.Classes;
using tt.Interfaces;

namespace tt.Hubs
{
    // ChatHub.cs
    // Declares a Hub class that allows clients to communicate with each other.
    public class ChatHub : Hub

    {
        private readonly GameService _gameService;

       
        public ChatHub(GameService gameService)
        {
            _gameService = gameService;
           // _gameService.AIGeneratedResponse += SendAIResponseToClients;
        }
        public override async Task OnConnectedAsync()
        {
            var httpContext = Context.GetHttpContext();
            if (httpContext.Request.Query.TryGetValue("playerId", out var playerId)
                &&
                httpContext.Request.Query.TryGetValue("gameId", out var gameId))
            {
                await Groups.AddToGroupAsync(Context.ConnectionId, gameId);
            }

            await base.OnConnectedAsync();
        }
        public override async Task OnDisconnectedAsync(Exception ex)
        {
            await base.OnDisconnectedAsync(ex);
        }

        public async Task SendMessage(Message message)
        {
            try
            {
                var httpContext = Context.GetHttpContext();
                if (httpContext.Request.Query.TryGetValue("gameId", out var gameId))
                   _gameService.AddMessageToGame(gameId, message);
                    await Clients.Group(gameId).SendAsync("ReceiveMessage", message);
                    await _gameService.HandleAIResponses(gameId);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public async Task SendAIResponseToClients(string gameId, IMessage message)
        {
            try { 
            await Clients.Group(gameId).SendAsync("ReceiveMessage", message);
        }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

    }
}



