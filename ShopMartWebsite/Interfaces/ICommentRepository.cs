using ShopMartWebsite.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopMartWebsite.Interfaces
{
    public interface ICommentRepository
    {
        IEnumerable<Comment> GetAllCommentByProductId(int productId);
        bool SaveComment(Comment cmt);
        bool UpdateComment(Comment cmt);
        bool DeleteComment(Comment cmt);
    }
}
