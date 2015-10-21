using System;

using Daisy.Dal.Domain;

using FluentNHibernate.Mapping;


namespace Daisy.Dal.Mappings
{
    public class CommentEntityMap : ClassMap<CommentEntity>
    {
        public CommentEntityMap()
        {
            Table("dbo.Comment");

            Id(x => x.Id)
                .Column("Id")
                .GeneratedBy
                .GuidComb();

            Map(x => x.Text)
                .Nullable();

            Map(x => x.CreateDate)
                .Generated
                .Insert()
                .Not.Nullable();

            Map(x => x.ArticleId, "ArticleId")
                .Not.Nullable();

            References(x => x.Article, "ArticleId")
                .Unique()
                .LazyLoad(Laziness.Proxy)
                .Not.Insert()
                .Not.Update();
        }
    }
}
