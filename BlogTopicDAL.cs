using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EbalitWebForms
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
            return base.EbalitDBContext.BlogTopics.Where(cc => cc.Topic == "IT").First().Id;
        }


    }
}