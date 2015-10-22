using System;

using Daisy.BusinessLogic.Models;


namespace Daisy.BusinessLogic.Services
{
    public interface ICommentService
    {
        void RemoveComment(Guid id);
        Guid SaveComment(CommentModel comment);
    }
}
