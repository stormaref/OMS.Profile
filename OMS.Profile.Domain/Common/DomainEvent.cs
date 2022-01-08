using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace OMS.Profile.Domain.Common
{
    public interface IHasDomainEvent
    {
        [NotMapped]
        public List<DomainEvent> DomainEvents { get; set; }
    }

    public abstract class DomainEvent
    {
        protected DomainEvent()
        {
            DateOccurred = DateTimeOffset.UtcNow;
        }
        public bool IsPublished { get; set; }
        public DateTimeOffset DateOccurred { get; protected set; } = DateTime.UtcNow;
    }
}