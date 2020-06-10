using ShopMartWebsite.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopMartWebsite.Interfaces
{
    public interface IReplyRepository
    {
        IEnumerable<Reply> GetAllRepliesByCommentId(int commentId);
        bool SaveReply(Reply rp);
        bool UpdateReply(Reply rp);
        bool DeleteReply(Reply rp);
    }
}
