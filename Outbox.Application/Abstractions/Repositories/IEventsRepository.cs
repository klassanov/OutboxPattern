using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Outbox.Application.Abstractions.Repositories
{
    public interface IEventsRepository
    {
        Task AddToOutbox<T>(T @event);
    }
}
