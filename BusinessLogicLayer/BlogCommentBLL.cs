using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EbalitWebForms.DataLayer;

namespace EbalitWebForms.BusinessLogicLayer
{
    public class BlogCommentBLL:DataAccessLayer
    {
        /// <summary>
        /// Creates a blog comment and returns its id
        /// </summary>
        /// <param name="blogComment"></param>
        /// <returns></returns>
        public BlogComment CreateBlogComment(BlogComment blogComment)
        {
            base.EbalitDBContext.BlogComments.Add(blogComment);
            base.EbalitDBContext.SaveChanges();
            blogComment.BlogEntry = new BlogEntryDAL().GetBlogEntry(blogComment.FK_BlogEntry);
            return blogComment;
        }

        /// <summary>
        /// Returns all blog comments belonging to the entry
        /// with given id
        /// </summary>
        /// <param name="blogEntryId"></param>
        /// <returns></returns>
        public List<BlogComment> GetBlogComments(int blogEntryId)
        {
            return
                (from cc in base.EbalitDBContext.BlogComments
                where cc.FK_BlogEntry == blogEntryId
                orderby cc.PostedOn
                select cc).ToList();
        }

        public BlogComment GetBlogComment(int blogCommentId)
        {
            return
                (from cc in base.EbalitDBContext.BlogComments
                where cc.Id == blogCommentId
                select cc).FirstOrDefault();
        }

    }
}