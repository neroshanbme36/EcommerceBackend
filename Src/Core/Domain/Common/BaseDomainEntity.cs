using System;

namespace Domain.Common
{
    public abstract class BaseDomainEntity
    {
        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
    }
}
