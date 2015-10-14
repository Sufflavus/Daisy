﻿using System;

using Daisy.Terminal.Models;


namespace Daisy.Terminal.ViewModels
{
    public sealed class ArticleAddViewModel : WindowViewModelBase
    {
        private Article _article = new Article();
        private string _errorMessage;

        public DateTime CreateDate
        {
            get { return _article.CreateDate; }
        }

        public override string DisplayName
        {
            get { return "Article"; }
            protected set { base.DisplayName = value; }
        }

        public string ErrorMessage
        {
            get { return _errorMessage; }
            set
            {
                _errorMessage = value;
                RaisePropertyChangedEvent("ErrorMessage");
            }
        }

        public string Text
        {
            get { return _article.Text; }
            set
            {
                _article.Text = value;
                RaisePropertyChangedEvent("Text");
            }
        }

        public string Title
        {
            get { return _article.Title; }
            set
            {
                _article.Title = value;
                RaisePropertyChangedEvent("Title");
            }
        }

        public Article Article
        {
            get { return _article; }
            set
            {
                _article = value;
                RaisePropertyChangedEvent("Article");
            }
        }
    }
}
