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

        public List<BlogCategory> ReadBlogCategory()
        {
            //Ebalit_WebFormsEntities context = new Ebalit_WebFormsEntities();
            return (from cc in base.EbalitDBContext.BlogCategories.Include("BlogTopic")
                   select cc).ToList();
        }

        public int GetCategoryAccordionIndex(int blogCategoryID)
        {
            int index = -1;
            var elements = from cc in base.EbalitDBContext.BlogCategories select cc;
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