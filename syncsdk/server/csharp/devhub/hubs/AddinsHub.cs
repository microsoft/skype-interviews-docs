using System;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Microsoft.Skype.Interviews.Samples.DevHub.hubs
{
    public class AddinsHub : Hub<IAddinsHub>
    {
        private readonly ILogger<AddinsHub> logger;

        private static readonly ConcurrentDictionary<string, TokenInfo> ConnectionInfo 
            = new ConcurrentDictionary<string, TokenInfo>();

        private static readonly ConcurrentDictionary<string, string> SessionContext 
            = new ConcurrentDictionary<string, string>();

        /// <summary>
        /// Initializes a new instance of the <see cref="AddinsHub"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        public AddinsHub(ILogger<AddinsHub> logger)
        {
            this.logger = logger;
        }

        public void SendMessage(AddinMessageRequest message)
        {
            if (ConnectionInfo.TryGetValue(this.Context.ConnectionId, out var tokenInfo))
            {
                this.Clients.OthersInGroup(tokenInfo.asid).messageReceived(message);
                this.logger.LogError("[SendMessage] message Receied invoked for asid:" + tokenInfo.asid);
            }
            else
            {
                this.logger.LogError("[SendMessage] can't find info about connection:" + this.Context.ConnectionId);
            }
        }

        public void StoreContext(string content)
        {
            if (ConnectionInfo.TryGetValue(this.Context.ConnectionId, out var tokenInfo))
            {
                SessionContext.AddOrUpdate(tokenInfo.asid, content, (s, s1) => content);
                this.logger.LogInformation("[StoreContext] session context stored OK with asid:" + tokenInfo.asid);
            }
            else
            {
                this.logger.LogError("[StoreContext] can't find info about connection:" + this.Context.ConnectionId);
                
            }
        }

        public string FetchContext()
        {
            if (ConnectionInfo.TryGetValue(this.Context.ConnectionId, out var tokenInfo))
            {
                if (SessionContext.TryGetValue(tokenInfo.asid, out var context))
                {
                    this.logger.LogInformation("[FetchContext] session context retrieved OK for asid:" + tokenInfo.asid);
                    return context;
                }
                else
                {
                    this.logger.LogError("[FetchContext] can't find session context with asid:" + tokenInfo.asid);
                }
            }
            else
            {
                this.logger.LogError("[FetchContext] can't find info about connection:" + this.Context.ConnectionId);
            }

            return null;
        }

        public override async Task OnConnectedAsync()
        {
            var token = JsonConvert.DeserializeObject<TokenInfo>(this.Context.GetHttpContext().Request.Query["token"]);
            ConnectionInfo.AddOrUpdate(this.Context.ConnectionId, s => token, (s, info) => token);

            await this.Groups.AddAsync(this.Context.ConnectionId, token.asid);

            await base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            ConnectionInfo.TryRemove(this.Context.ConnectionId, out _);
            return base.OnDisconnectedAsync(exception);
       }
    }

    public class TokenInfo
    {
        public string adid { get; set; }
        public string asid { get; set; }
        public string auid { get; set; }
        public string sid { get; set; }
    }

}