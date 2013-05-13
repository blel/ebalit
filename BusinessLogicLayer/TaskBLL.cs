using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EbalitWebForms.DataLayer;

namespace EbalitWebForms.BusinessLogicLayer
{
    public class TaskBLL : DataAccessLayer
    {
        public IList<Task> GetTasks()
        {
            return (from cc in base.EbalitDBContext.Tasks.Include("TaskCategory")
                    select cc).ToList();
        }

        public IList<Task> GetFilteredTasks(TaskSearchDTO filter)
        {
            //Get items and apply date filter, since from and to date always contain valid values
            var tasks = from cc in base.EbalitDBContext.Tasks.Include("TaskCategory")
                        where cc.DueDate == null || (cc.DueDate >= filter.DateFrom && cc.DueDate <= filter.DateTo)
                        select cc;
            //add Text filter
            if (!string.IsNullOrWhiteSpace(filter.Text))
                tasks = tasks.Where(cc => cc.Content.Contains(filter.Text) || cc.Subject.Contains(filter.Text));

            //add Category filter
            if (filter.TaskCategoryId.Count > 0)
                tasks = tasks.Where(cc=> filter.TaskCategoryId.Any(ccc => cc.FK_TaskCategory == ccc));

            //add Task Status filter
            if (filter.TaskStatus.Count>0)
                tasks = tasks.Where(cc => filter.TaskStatus.Any(ccc=>cc.State==ccc));

            //add Task Pirority filter
            if (filter.TaskPriority.Count>0)
                tasks = tasks.Where(cc => filter.TaskPriority.Any(ccc=>cc.Priority==ccc));

            //add Task closing type filter
            if (filter.TaskClosingType.Count>0)
                tasks = tasks.Where(cc => filter.TaskClosingType.Any(ccc=>cc.ClosingType == ccc));

            return tasks.ToList();
        }


        public IList<TaskToCsvDTO> GetFilteredTasksForCsv(TaskSearchDTO filter)
        {
            IList<Task> filteredTasks = GetFilteredTasks(filter);

            return filteredTasks.Select(cc => new TaskToCsvDTO()
            {
                ChangedBy = cc.ChangedBy,
                ChangedOn = Convert.ToString(cc.ChangedOn),
                ClosingType = cc.ClosingType,
                Comments = (from ccc in new TaskComments().GetTaskComments(cc.Id)
                            select ccc).Aggregate("\"", (a, b) => (a + "Comment by " + b.CreatedBy + " on " +  Convert.ToString(b.CreatedOn) + "\r\n"+ b.Comment + "\r\n")) + "\"",
                Content = cc.Content,
                CreatedBy = cc.CreatedBy,
                CreatedOn = Convert.ToString(cc.CreatedOn),
                DueDate = Convert.ToString(cc.DueDate),
                Priority = cc.Priority,
                State = cc.State,
                Subject = cc.Subject,
                TaskCategory = cc.TaskCategory!=null?cc.TaskCategory.TaskCategory1:""
            }).ToList();
                
        }



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
                base.EbalitDBContext.Tasks.Remove(taskToDelete);
                base.EbalitDBContext.SaveChanges();
            }
        }

        public Task GetTaskById(int id)
        {
            return (from cc in base.EbalitDBContext.Tasks.Include("TaskCategory")
                    where cc.Id == id
                    select cc).FirstOrDefault();


        }

    }
}