using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EbalitWebForms.DataLayer;
using System.Text.RegularExpressions;

namespace EbalitWebForms.BusinessLogicLayer
{
    public class BlogEntryDAL:DataAccessLayer
    {
        public BlogEntryDAL()
        {
 
        }

        public int CreateBlogEntry(BlogEntry blogEntry)
        {
            base.EbalitDbContext.BlogEntries.Add(blogEntry);
            base.EbalitDbContext.SaveChanges();
            return blogEntry.Id;
        }

        public List<BlogEntry> GetBlogEntries()
        {
            return (from cc in base.EbalitDbContext.BlogEntries.Include("BlogCategory")
                   select cc).ToList<BlogEntry>();
        }

        public IEnumerable<IGrouping<DateTime,BlogEntry>> GetBlogEntriesGroupedByMonths()
        {
            return base.EbalitDbContext.BlogEntries.Include("BlogCategory").Include("BlogCategory.BlogTopic").ToList().GroupBy(cc => new DateTime(cc.PublishedOn.Year,cc.PublishedOn.Month,1)).OrderByDescending(cc => cc.Key);
            
        }


        public IEnumerable<BlogEntry> GetBlogEntries(int categoryID)
        {
            return
                from cc in base.EbalitDbContext.BlogEntries
                where cc.Category == categoryID
                select cc;
        }

        public IEnumerable<BlogEntry> FindBlogEntries(string blogTopic, string searchText)
        {
            Regex regex = new Regex(@"<(.|\n)*?>");
            
            return base.EbalitDbContext.BlogEntries.Include("BlogCategory").Include("BlogCategory.BlogTopic").Where(
                cc => (cc.BlogCategory.BlogTopic.Topic == blogTopic &&
                          (cc.Subject.Contains(searchText) ||
                          cc.Content.Contains(searchText)))).ToList().Select(cc => new BlogEntry() { Content = regex.Replace(cc.Content, ""), PublishedOn = cc.PublishedOn, Subject = cc.Subject, Id=cc.Id });
        }

        public IEnumerable<BlogEntry> FindBlogEntries(string searchText)
        {
            Regex regex = new Regex(@"<(.|\n)*?>");

            return base.EbalitDbContext.BlogEntries.Include("BlogCategory").Include("BlogCategory.BlogTopic").Where(
                cc => (cc.Subject.Contains(searchText) ||
                          cc.Content.Contains(searchText))).ToList().Select(cc => new BlogEntry()
                          {
                              Content = regex.Replace(cc.Content, ""),
                              PublishedOn = cc.PublishedOn,
                              Subject = cc.Subject,
                              Id = cc.Id,
                              BlogCategory = new BlogCategory() { BlogTopic = new BlogTopic() { Topic = cc.BlogCategory.BlogTopic.Topic, PageName=cc.BlogCategory.BlogTopic.PageName } }
                          });
        }

        public int GetDefaultBlogEntryId(int BlogTopicId)
        {
            BlogEntry blogEntry = (from cc in base.EbalitDbContext.BlogEntries.Include("BlogCategory")
                                   where cc.BlogCategory.FK_Topic == BlogTopicId
                                   orderby cc.PublishedOn descending
                                   select cc).FirstOrDefault();


            if (blogEntry != null)
                return blogEntry.Id;
            else
                return 0;
        }

        public IList<BlogEntry> GetRecentBlogEntries(int count)
        {
            return (from cc in base.EbalitDbContext.BlogEntries.Include("BlogCategory").Include("BlogCategory.BlogTopic")
                    orderby cc.PublishedOn descending
                    select cc).Take(count).ToList();
        }


        public BlogEntry GetDefaultBlogEntry(string blogTopic)
        {
            return (from cc in base.EbalitDbContext.BlogEntries.Include("BlogCategory").Include("BlogCategory.BlogTopic")
                    where cc.BlogCategory.BlogTopic.Topic == blogTopic
                    orderby cc.PublishedOn descending
                    select cc).FirstOrDefault();
        }

        
        public BlogEntry GetBlogEntry(int Id)
        {
            var result = (from cc in base.EbalitDbContext.BlogEntries.Include("BlogCategory").Include("BlogCategory.BlogTopic")
                    where cc.Id == Id 
                    select cc).FirstOrDefault();
            return result;
        }

        public void DeleteBlogEntry(int Id)
        {

        }

        public void DeleteBlogEntry(BlogEntry blogEntry)
        {
            base.EbalitDbContext.BlogEntries.Attach(blogEntry);
            base.EbalitDbContext.BlogEntries.Remove(blogEntry);
            base.EbalitDbContext.SaveChanges();
        }


        public void UpdateBlogEntry(BlogEntry blogEntry)
        {
            
            var originalRecord = base.EbalitDbContext.BlogEntries.Find(blogEntry.Id);
            if (originalRecord != null)
            {
                base.EbalitDbContext.Entry(originalRecord).CurrentValues.SetValues(blogEntry);
                base.EbalitDbContext.SaveChanges();
            }

        }


    }
}