using System;


namespace Daisy.Dal.Domain
{
    public abstract class BaseEntity
    {
        public virtual Guid? Id { get; set; }
    }
}
