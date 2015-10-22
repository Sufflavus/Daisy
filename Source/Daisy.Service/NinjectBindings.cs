using System;

using Daisy.Dal.Context;
using Daisy.Dal.Repository;
using Daisy.Dal.Repository.Interfaces;

using Ninject.Modules;


namespace Daisy.Service
{
    public class NinjectBindings : NinjectModule
    {
        public override void Load()
        {
            Bind<IArticleRepository>().To<ArticleRepository>();
            Bind<ICommentRepository>().To<CommentRepository>();
            Bind<IContext>().To<NHibernateContext>();
        }
    }
}
