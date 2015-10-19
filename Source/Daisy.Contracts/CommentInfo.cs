using System;
using System.Runtime.Serialization;


namespace Daisy.Contracts
{
    [DataContract]
    public class CommentInfo
    {
        [DataMember(IsRequired = true)]
        public Guid ArticleId { get; set; }

        [DataMember(IsRequired = false)]
        public DateTime? CreateDate { get; set; }

        [DataMember(IsRequired = false)]
        public Guid? Id { get; set; }

        [DataMember(IsRequired = false)]
        public string Text { get; set; }
    }
}
