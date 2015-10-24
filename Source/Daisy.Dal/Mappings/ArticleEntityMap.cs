using System;

using Daisy.Dal.Domain;

using FluentNHibernate.Mapping;


namespace Daisy.Dal.Mappings
{
    public class ArticleEntityMap : ClassMap<ArticleEntity>
    {
        public ArticleEntityMap()
        {
            Table("dbo.Article");

            Id(x => x.Id)
                .Column("Id")
                .GeneratedBy
                .GuidComb();

            Map(x => x.Title)
                .Nullable();

            Map(x => x.Text)
                .Nullable();

            Map(x => x.CreateDate)
                .Generated
                .Insert()
                .Not.Nullable();

            HasMany(x => x.Comments)
                .Table("dbo.Comment")
                .KeyColumn("ArticleId")
                .Cascade
                .All()
                .Inverse(); // for cascade deleteing
        }
    }
}
