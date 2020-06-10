using Microsoft.EntityFrameworkCore;
using ShopMartWebsite.Data;
using ShopMartWebsite.Entities;
using ShopMartWebsite.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopMartWebsite.Services
{
    public class CommentRepository : ICommentRepository
    {
        private ShopDbContext _ctx;

        public CommentRepository(ShopDbContext ctx)
        {
            _ctx = ctx;
        }

        

        public bool DeleteComment(Comment cmt)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Comment> GetAllCommentByProductId(int productId)
        {
            return _ctx.comments.Include(rp=>rp.Replies).Include(u=>u.user).Where(x=>x.productId==productId).ToList();
        }

        public bool SaveComment(Comment cmt)
        {
            _ctx.comments.Add(cmt);

            return _ctx.SaveChanges() > 0;
        }

        public bool UpdateComment(Comment cmt)
        {
            throw new NotImplementedException();
        }
    }
}
