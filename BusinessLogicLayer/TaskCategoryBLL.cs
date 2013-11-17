using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EbalitWebForms.DataLayer;

namespace EbalitWebForms.BusinessLogicLayer
{
    public class TaskCategoryBLL:DataAccessLayer
    {
        public IList<TaskCategory> GetTaskCategories()
        {
            IList<TaskCategory> returnList = (from cc in base.EbalitDbContext.TaskCategories
                    select cc).ToList();

            return returnList;
        }

        public int Create(TaskCategory taskCategory)
        {
            base.EbalitDbContext.TaskCategories.Add(taskCategory);
            base.EbalitDbContext.SaveChanges();
            return taskCategory.Id;
        }

        public TaskCategory GetTaskCategoryById(int taskCategoryID)
        {
            return (from cc in base.EbalitDbContext.TaskCategories
                    where cc.Id == taskCategoryID
                    select cc).FirstOrDefault();
        }

        public void Update(TaskCategory taskCategory)
        {
            var originalRecord = base.EbalitDbContext.TaskCategories.Find(taskCategory.Id);
            if (originalRecord != null)
            {
                base.EbalitDbContext.Entry(originalRecord).CurrentValues.SetValues(taskCategory);
                base.EbalitDbContext.SaveChanges();
            }
        }

        public void Delete(TaskCategory taskCategory)
        {
            var originalRecord = base.EbalitDbContext.TaskCategories.Find(taskCategory.Id);
            if (originalRecord != null)
            {
                base.EbalitDbContext.TaskCategories.Remove(originalRecord);
                base.EbalitDbContext.SaveChanges();
            }
        }


        

    }
}