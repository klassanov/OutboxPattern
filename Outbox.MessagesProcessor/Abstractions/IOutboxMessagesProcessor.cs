using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Outbox.MessagesProcessor.Models;

namespace Outbox.MessagesProcessor.Abstractions
{
    public interface IOutboxMessagesProcessor
    {
        Task<IEnumerable<OutboxMessage>> ProcessOutboxMessages();
    }
}
