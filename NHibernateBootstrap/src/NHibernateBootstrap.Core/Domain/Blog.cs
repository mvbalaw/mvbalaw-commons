using System;

namespace NHibernateBootstrap.Core.Domain
{
    public class Blog
    {
        public virtual Guid Id { get; set; }
        public virtual bool AllowsComments { get; set; }
        public virtual DateTime CreatedAt { get; set; }
        public virtual string Subtitle { get; set; }
        public virtual string Title { get; set; }
    }
}