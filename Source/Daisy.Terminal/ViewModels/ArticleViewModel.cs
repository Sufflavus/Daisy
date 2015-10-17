using System;

using Daisy.Terminal.Models;


namespace Daisy.Terminal.ViewModels
{
    public abstract class ArticleViewModel : WindowViewModelBase
    {
        protected Article _article = new Article();
    }
}
