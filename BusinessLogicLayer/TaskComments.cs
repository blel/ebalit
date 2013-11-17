using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EbalitWebForms.BusinessLogicLayer
{
    public class TaskComments:DataAccessLayer
    {
        public int CreateTaskComment(DataLayer.TaskComment comment)
        {
            base.EbalitDbContext.TaskComments.Add(comment);
            base.EbalitDbContext.SaveChanges();
            return comment.Id;
        }


        public void UpdateTaskComment(DataLayer.TaskComment comment)
        {
            var commentToUpdate = GetTaskCommentById(comment.Id);
            if (commentToUpdate != null)
            {
                base.EbalitDbContext.Entry(commentToUpdate).CurrentValues.SetValues(comment);
                base.EbalitDbContext.SaveChanges();
            }
        }

        public void DeleteTaskComment(DataLayer.TaskComment comment)
        {
            var commentToRemove = GetTaskCommentById(comment.Id);
            if (commentToRemove!= null)
            {
                base.EbalitDbContext.TaskComments.Remove(commentToRemove);
                base.EbalitDbContext.SaveChanges();
            }
        }

        public IList<DataLayer.TaskComment> GetTaskComments(int taskID)
        {
            var task = (from cc in base.EbalitDbContext.Tasks.Include("TaskComments")
                    where cc.Id == taskID
                    
                    select cc).FirstOrDefault();
            if (task != null)
            {
                return task.TaskComments.OrderByDescending(cc=>cc.CreatedOn).ToList();
            }
            else
            {
                return new List<DataLayer.TaskComment>();
            }
        }

        public DataLayer.TaskComment GetTaskCommentById(int taskID)
        {
            return (from cc in base.EbalitDbContext.TaskComments
                    where cc.Id == taskID
                    select cc).FirstOrDefault();
        }

    }
}