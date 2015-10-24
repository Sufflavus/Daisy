using System;

using Daisy.BusinessLogic.Services;
using Daisy.ServiceProvider;
using Daisy.ServiceProvider.Interfaces;
using Daisy.Terminal.Log;

using Ninject.Modules;


namespace Daisy.Terminal
{
    public class NinjectBindings : NinjectModule
    {
        public override void Load()
        {
            Bind<ILogger>().To<FileLogger>();
            Bind<IArticleService>().To<ArticleService>();
            Bind<ICommentService>().To<CommentService>();
            Bind<IServiceClient>().To<ServiceClient>();
            Bind<ISettingsProvider>().To<SettingsProvider>();
            Bind<IUrlAddressFactory>().To<UrlAddressFactory>();
        }
    }
}
