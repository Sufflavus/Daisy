using System;

using Daisy.Dal.Context;
using Daisy.Dal.Context.Interfaces;
using Daisy.Dal.Domain;
using Daisy.Dal.Repository.Interfaces;


namespace Daisy.Dal.Repository
{
    public class CommentRepository : Repository<CommentEntity>, ICommentRepository
    {
        public CommentRepository(IContext context) : base(context)
        {
        }
    }
}
