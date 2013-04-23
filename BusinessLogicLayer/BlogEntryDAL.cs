﻿using System;
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
            base.EbalitDBContext.BlogEntries.Add(blogEntry);
            base.EbalitDBContext.SaveChanges();
            return blogEntry.Id;
        }

        public List<BlogEntry> GetBlogEntries()
        {
            return (from cc in base.EbalitDBContext.BlogEntries.Include("BlogCategory")
                   select cc).ToList<BlogEntry>();
        }

        public IEnumerable<BlogEntry> GetBlogEntries(int categoryID)
        {
            return
                from cc in base.EbalitDBContext.BlogEntries
                where cc.Category == categoryID
                select cc;
        }

        public IEnumerable<BlogEntry> FindBlogEntries(string blogTopic, string searchText)
        {
            Regex regex = new Regex(@"<(.|\n)*?>");
            
            return base.EbalitDBContext.BlogEntries.Include("BlogCategory").Include("BlogCategory.BlogTopic").Where(
                cc => (cc.BlogCategory.BlogTopic.Topic == blogTopic &&
                          (cc.Subject.Contains(searchText) ||
                          cc.Content.Contains(searchText)))).ToList().Select(cc => new BlogEntry() { Content = regex.Replace(cc.Content, ""), PublishedOn = cc.PublishedOn, Subject = cc.Subject, Id=cc.Id });

        }

        public int GetDefaultBlogEntryId(int BlogTopicId)
        {
            return (from cc in base.EbalitDBContext.BlogEntries.Include("BlogCategory")
                    where cc.BlogCategory.FK_Topic == BlogTopicId
                    orderby cc.PublishedOn descending
                    select cc).First().Id;
        }

        public BlogEntry GetDefaultBlogEntry(string blogTopic)
        {
            return (from cc in base.EbalitDBContext.BlogEntries.Include("BlogCategory").Include("BlogCategory.BlogTopic")
                    where cc.BlogCategory.BlogTopic.Topic == blogTopic
                    orderby cc.PublishedOn descending
                    select cc).FirstOrDefault();
        }

        
        public BlogEntry GetBlogEntry(int Id)
        {
            var result = (from cc in base.EbalitDBContext.BlogEntries.Include("BlogCategory").Include("BlogCategory.BlogTopic")
                    where cc.Id == Id 
                    select cc).FirstOrDefault();
            return result;
        }

        public void DeleteBlogEntry(int Id)
        {

        }

        public void DeleteBlogEntry(BlogEntry blogEntry)
        {
            base.EbalitDBContext.BlogEntries.Attach(blogEntry);
            base.EbalitDBContext.BlogEntries.Remove(blogEntry);
            base.EbalitDBContext.SaveChanges();
        }


        public void UpdateBlogEntry(BlogEntry blogEntry)
        {
            
            var originalRecord = base.EbalitDBContext.BlogEntries.Find(blogEntry.Id);
            if (originalRecord != null)
            {
                base.EbalitDBContext.Entry(originalRecord).CurrentValues.SetValues(blogEntry);
                base.EbalitDBContext.SaveChanges();
            }

        }


    }
}