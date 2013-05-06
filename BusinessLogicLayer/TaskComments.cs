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
            base.EbalitDBContext.TaskComments.Add(comment);
            base.EbalitDBContext.SaveChanges();
            return comment.Id;
        }


        public void UpdateTaskComment(DataLayer.TaskComment comment)
        {
            var commentToUpdate = GetTaskCommentById(comment.Id);
            if (commentToUpdate != null)
            {
                base.EbalitDBContext.Entry(commentToUpdate).CurrentValues.SetValues(comment);
                base.EbalitDBContext.SaveChanges();
            }
        }

        public void DeleteTaskComment(DataLayer.TaskComment comment)
        {
            var commentToRemove = GetTaskCommentById(comment.Id);
            if (commentToRemove!= null)
            {
                base.EbalitDBContext.TaskComments.Remove(commentToRemove);
                base.EbalitDBContext.SaveChanges();
            }
        }

        public IList<DataLayer.TaskComment> GetTaskComments(int taskID)
        {
            var task = (from cc in base.EbalitDBContext.Tasks.Include("TaskComments")
                    where cc.Id == taskID
                    select cc).FirstOrDefault();
            if (task != null)
            {
                return task.TaskComments.ToList();
            }
            else
            {
                return new List<DataLayer.TaskComment>();
            }
        }

        public DataLayer.TaskComment GetTaskCommentById(int taskID)
        {
            return (from cc in base.EbalitDBContext.TaskComments
                    where cc.Id == taskID
                    select cc).FirstOrDefault();
        }

    }
}