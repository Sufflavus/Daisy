using System;
using System.Collections.Generic;
using System.Runtime.Serialization;


namespace Daisy.Contracts
{
    [DataContract]
    public class ArticleInfo
    {
        [DataMember(IsRequired = false)]
        public List<CommentInfo> Comments { get; set; }

        [DataMember(IsRequired = false)]
        public DateTime? CreateDate { get; set; }

        [DataMember(IsRequired = false)]
        public Guid? Id { get; set; }

        [DataMember(IsRequired = false)]
        public string Text { get; set; }

        [DataMember(IsRequired = false)]
        public string Title { get; set; }
    }
}
