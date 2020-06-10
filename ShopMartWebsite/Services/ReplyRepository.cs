using ShopMartWebsite.Data;
using ShopMartWebsite.Entities;
using ShopMartWebsite.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopMartWebsite.Services
{
    public class ReplyRepository : IReplyRepository
    {
        private ShopDbContext _ctx;

        public ReplyRepository(ShopDbContext ctx)
        {
            _ctx = ctx;
        }
        public bool DeleteReply(Reply rp)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Reply> GetAllRepliesByCommentId(int commentId)
        {
            throw new NotImplementedException();
        }

        public bool SaveReply(Reply rp)
        {
            _ctx.replies.Add(rp);

            return _ctx.SaveChanges() > 0;
        }

        public bool UpdateReply(Reply rp)
        {
            throw new NotImplementedException();
        }
    }
}
