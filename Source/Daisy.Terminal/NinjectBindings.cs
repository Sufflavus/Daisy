using System;

using Daisy.BusinessLogic.Services;
using Daisy.ServiceProvider;

using Ninject.Modules;


namespace Daisy.Terminal
{
    public class NinjectBindings : NinjectModule
    {
        public override void Load()
        {
            Bind<IArticleService>().To<ArticleService>();
            Bind<ICommentService>().To<CommentService>();
            Bind<IServiceClient>().To<ServiceClient>();
        }
    }
}
