using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EbalitWebForms.DataLayer;

namespace EbalitWebForms.BusinessLogicLayer
{
    public class TaskBLL: DataAccessLayer
    {
        public IList<Task> GetTasks()
        {
            return (from cc in base.EbalitDBContext.Tasks.Include("TaskCategory")
                    select cc).ToList();
        }

        //public IList<Task> GetFilteredTasks(string filter)
        //{
        
        //}


        public int CreateTask(Task task)
        {
            base.EbalitDBContext.Tasks.Add(task);
            base.EbalitDBContext.SaveChanges();
            return task.Id;
        }

        public void UpdateTask(Task task)
        {
            var taskToUpdate = GetTaskById(task.Id);
            if (taskToUpdate != null)
            {
                base.EbalitDBContext.Entry(taskToUpdate).CurrentValues.SetValues(task);
                base.EbalitDBContext.SaveChanges();
            }

        }

        public void DeleteTask(Task task)
        {
            var taskToDelete = GetTaskById(task.Id);
            if (taskToDelete != null)
            {
                base.EbalitDBContext.Tasks.Remove(task);
                base.EbalitDBContext.SaveChanges();
            }
        }

        public Task GetTaskById(int id)
        {
            return (from cc in base.EbalitDBContext.Tasks
                    where cc.Id == id
                    select cc).FirstOrDefault();
        }

    }
}