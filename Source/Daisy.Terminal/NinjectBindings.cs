using System;

using Daisy.BusinessLogic.Services;
using Daisy.ServiceProvider;
using Daisy.ServiceProvider.Interfaces;

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
            Bind<ISettingsProvider>().To<SettingsProvider>();
            Bind<IUrlAddressFactory>().To<UrlAddressFactory>();
        }
    }
}
