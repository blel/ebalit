using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EbalitWebForms
{
    public class BlogCategoryDAL : DataAccessLayer
    {
        public BlogCategoryDAL() { }

        public int CreateBlogCategory(BlogCategory blogCategory)
        {
            return 0;
        }

        public bool UpdateBlogCategory(int blogCategoryID)
        {
            return true;
        }

        public IQueryable<BlogCategory> ReadBlogCategory(int blogTopicID)
        {
            Ebalit_WebFormsEntities context = new Ebalit_WebFormsEntities();
            return from cc in context.BlogCategories
                   where cc.FK_Topic == blogTopicID
                   select cc;
        }

        public bool DeleteBlogCategory(int blogCategoryID)
        {
            return true;
        }
    }
}