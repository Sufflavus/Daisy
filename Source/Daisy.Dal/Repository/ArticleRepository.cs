using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

using Daisy.Dal.Context;
using Daisy.Dal.Domain;
using Daisy.Dal.Repository.Interfaces;

using Dapper;


namespace Daisy.Dal.Repository
{
    public class ArticleRepository : Repository<ArticleEntity>, IArticleRepository
    {
        public ArticleRepository(IContext context) : base(context)
        {
        }

        public override List<ArticleEntity> GetAll()
        {
            using (var connection = new SqlConnection(SettingsProvider.GetDbConnectionString()))
            {
                connection.Open();
                List<ArticleEntity> articles = connection.Query<ArticleEntity>("select * from Article").ToList();
                connection.Close();
                return articles;
            }
        }


        public override ArticleEntity GetById(Guid id)
        {
            string sql = @" select * from Article where Id = @id
                    select * from Comment where ArticleId = @id";

            using (var connection = new SqlConnection(SettingsProvider.GetDbConnectionString()))
            {
                ArticleEntity article;
                connection.Open();
                using (SqlMapper.GridReader multi = connection.QueryMultiple(sql, new { id }))
                {
                    article = multi.Read<ArticleEntity>().SingleOrDefault();
                    if (article == null)
                    {
                        return null;
                    }

                    // http://blogs.msdn.com/b/endpoint/archive/2010/01/21/error-handling-in-wcf-webhttp-services-with-webfaultexception.aspx
                    List<CommentEntity> comments = multi.Read<CommentEntity>().ToList();
                    article.Comments = comments;
                }
                connection.Close();
                return article;
            }
        }
    }
}
