using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EbalitWebForms.DataLayer;

namespace EbalitWebForms.BusinessLogicLayer
{
    public class BlogCategoryDAL : DataAccessLayer
    {
        public BlogCategoryDAL() { }

        public int CreateBlogCategory(BlogCategory blogCategory)
        {
            base.EbalitDBContext.BlogCategories.Add(blogCategory);
            base.EbalitDBContext.SaveChanges();
            return blogCategory.Id;
        }

        public void UpdateBlogCategory(BlogCategory blogCategory)
        {
            var originalRecord = base.EbalitDBContext.BlogCategories.Find(blogCategory.Id);
            if (originalRecord != null)
            {
                base.EbalitDBContext.Entry(originalRecord).CurrentValues.SetValues(blogCategory);
                base.EbalitDBContext.SaveChanges();
            }
        }

        public IEnumerable<BlogCategory> ReadBlogCategory(int blogTopicID)
        {
            Ebalit_WebFormsEntities context = new Ebalit_WebFormsEntities();
            return from cc in context.BlogCategories
                   where cc.FK_Topic == blogTopicID
                   select cc;
        }

        public IEnumerable<BlogCategory> ReadBlogCategory(string blogTopic)
        {
            return base.EbalitDBContext.BlogCategories.Include("BlogTopic").Where(cc => cc.BlogTopic.Topic == blogTopic);
        }

        public List<BlogCategory> ReadBlogCategory()
        {
            //Ebalit_WebFormsEntities context = new Ebalit_WebFormsEntities();
            return (from cc in base.EbalitDBContext.BlogCategories.Include("BlogTopic")
                   select cc).ToList();
        }

        public int GetCategoryAccordionIndex(int blogCategoryID)
        {
            int index = 0;
            int blogTopicID = base.EbalitDBContext.BlogCategories.Include("BlogTopic").Where(cc=>cc.Id == blogCategoryID).Select(cc=>cc.BlogTopic.Id).FirstOrDefault();
            var elements = base.EbalitDBContext.BlogCategories.Include("BlogTopic").Where(cc=>cc.BlogTopic.Id==blogTopicID).Select(cc=>cc);
 
            var enumerator = elements.GetEnumerator();

            while (enumerator.MoveNext() && enumerator.Current.Id != blogCategoryID)
            {
                index += 1;
            }
            return index;
                   
        }


        public bool DeleteBlogCategory(BlogCategory  blogCategory)
        {
            bool result = true;
            var originalRecord = base.EbalitDBContext.BlogCategories.Find(blogCategory.Id);
            if (originalRecord != null)
            {
                try
                {
                    base.EbalitDBContext.BlogCategories.Remove(originalRecord);
                    base.EbalitDBContext.SaveChanges();
                }
                catch (Exception ex)
                {
                    result = false;
                }
            }
            return result;
        }
    }
}