using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace OMS.Profile.Domain.Common
{
    public class AggregateRoot : BaseEntity, IHasDomainEvent
    {
        public AggregateRoot() : base()
        {
        }

        public AggregateRoot(Guid id) : base(id)
        {
        }

        [NotMapped]
        public List<DomainEvent> DomainEvents { get; set; } = new List<DomainEvent>();
    }
}