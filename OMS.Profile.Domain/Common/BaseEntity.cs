using System;

namespace OMS.Profile.Domain.Common
{
    public class BaseEntity
    {
        public BaseEntity()
        {
            Id = Guid.NewGuid();
        }

        public BaseEntity(Guid id)
        {
        }

        public Guid Id { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime LastModifiedDate { get; set; }
    }
}