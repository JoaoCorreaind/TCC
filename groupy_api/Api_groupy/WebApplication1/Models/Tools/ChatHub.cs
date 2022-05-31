using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace WebApplication1.Models.Tools
{
    public class ChatHub : Hub
    {
        public async IAsyncEnumerable<DateTime> Streaming(CancellationToken cancellationToken )
        {
            
            while (true)
            {
                yield return DateTime.Now;
                await Task.Delay(1000, cancellationToken);
            }
        }
    }
}
