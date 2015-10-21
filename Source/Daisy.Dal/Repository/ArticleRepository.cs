using System;

using Daisy.Dal.Context;
using Daisy.Dal.Domain;
using Daisy.Dal.Repository.Interfaces;


namespace Daisy.Dal.Repository
{
    public class ArticleRepository : Repository<ArticleEntity>, IArticleRepository
    {
        public ArticleRepository(IContext context) : base(context)
        {
        }
    }
}
