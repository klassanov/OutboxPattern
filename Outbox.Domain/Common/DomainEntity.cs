namespace Outbox.Domain.Common
{
    public abstract class DomainEntity
    {
        private List<IDomainEvent> events = [];

        //Creates a new list and returns it, so it does not point to the same reference
        public IEnumerable<IDomainEvent> DomainEvents => [..events];

        public DomainEntity Raise(IDomainEvent domainEvent)
        {
            this.events.Add(domainEvent);
            return this;
        }

        public void ClearDomainEvents()
        {
            this.events.Clear();
        }

    }
}
