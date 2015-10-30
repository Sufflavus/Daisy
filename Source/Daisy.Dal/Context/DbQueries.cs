using System;


namespace Daisy.Dal.Context
{
    public static class DbQueries
    {
        public const string GetAllArticles = "select * from Article";

        public const string GetArticleWithComments = @" select * from Article where Id = @id
                    select * from Comment where ArticleId = @id";
    }
}
