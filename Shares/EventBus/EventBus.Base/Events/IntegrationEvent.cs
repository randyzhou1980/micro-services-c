using System;

namespace EventBus.Base.Events
{
    public class IntegrationEvent
    {
        #region Constructor
        public IntegrationEvent()
        {
            Id = Guid.NewGuid();
            CreationDate = DateTime.UtcNow;
        }
        #endregion

        public Guid Id { get; }
        public DateTime CreationDate { get; }
    }
}
