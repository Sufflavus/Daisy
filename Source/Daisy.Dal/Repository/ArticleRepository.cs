using System;
using System.Collections.Generic;
using System.Linq;

using Daisy.Dal.Context;
using Daisy.Dal.Context.Interfaces;
using Daisy.Dal.Domain;
using Daisy.Dal.Repository.Interfaces;

using Dapper;


namespace Daisy.Dal.Repository
{
    public class ArticleRepository : Repository<ArticleEntity>, IArticleRepository
    {
        private readonly IConnectionFactory _connectionFactory;


        public ArticleRepository(IContext context, IConnectionFactory connectionFactory)
            : base(context)
        {
            _connectionFactory = connectionFactory;
        }


        public override List<ArticleEntity> GetAll()
        {
            using (IConnection connection = _connectionFactory.CreateDapperConnection())
            {
                string sqlCommand = DbQueries.GetAllArticles;
                connection.Open();
                List<ArticleEntity> articles = connection.Query<ArticleEntity>(sqlCommand).ToList();
                connection.Close();
                return articles;
            }
        }


        public override ArticleEntity GetById(Guid id)
        {
            string sqlCommand = DbQueries.GetArticleWithComments;

            using (IConnection connection = _connectionFactory.CreateDapperConnection())
            {
                ArticleEntity article;
                connection.Open();
                using (SqlMapper.GridReader reader = connection.QueryMultiple(sqlCommand, new { id }))
                {
                    article = reader.Read<ArticleEntity>().SingleOrDefault();
                    if (article == null)
                    {
                        return null;
                    }

                    // http://blogs.msdn.com/b/endpoint/archive/2010/01/21/error-handling-in-wcf-webhttp-services-with-webfaultexception.aspx
                    List<CommentEntity> comments = reader.Read<CommentEntity>().ToList();
                    article.Comments = comments;
                }
                connection.Close();
                return article;
            }
        }
    }
}
