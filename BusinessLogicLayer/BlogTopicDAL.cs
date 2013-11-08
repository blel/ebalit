using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EbalitWebForms.DataLayer;

namespace EbalitWebForms.BusinessLogicLayer
{
    public class BlogTopicDAL:DataAccessLayer
    {
        public BlogTopicDAL() { }

        public int CreateBlogTopic(BlogTopic blogTopic)
        {
            return 0;
        }

        public bool UpdateBlogTopic(int BlogTopicID)
        {
            return true;
        }

        public IQueryable<BlogTopic> ReadBlogTopic()
        {
            Ebalit_WebFormsEntities context = new Ebalit_WebFormsEntities();
            
                
                return from cc in context.BlogTopics
                       select cc;
            
        }

        public bool DeleteBlogTopic(int BlogTopicID)
        {
            return true;
        }

        public int GetBlogTopicId(string blogTopic)
        {
            var blogTopicEntity = base.EbalitDBContext.BlogTopics.Where(cc => cc.Topic == blogTopic).FirstOrDefault();
            if (blogTopicEntity != null)
            {
                return blogTopicEntity.Id;
            }
            return 0;
        }


    }
}