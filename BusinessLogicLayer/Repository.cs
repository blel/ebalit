using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EbalitWebForms.DataLayer;
using System.Data.Entity;

namespace EbalitWebForms.BusinessLogicLayer
{
    public class Repository<T> : DataAccessLayer, IRepository<T> where T : class
    {
        private DbSet<T> _dbSet;

        public Repository()
        {
            _dbSet = base.EbalitDBContext.Set<T>();

        }

        public T GetItemById(int id)
        {
            return _dbSet.AsEnumerable().Where(cc => GetId(cc) == id).Select(cc => cc).FirstOrDefault();
        }

        public IList<T> GetItems()
        {
            return _dbSet.ToList();
        }

        public IList<T> GetItems(string include)
        {
            return _dbSet.Include(include).ToList();
        }


        public int CreateItem(T item)
        {
            _dbSet.Add(item);
            base.EbalitDBContext.SaveChanges();
            return Convert.ToInt32(typeof(T).GetProperty("Id").GetValue(item,null));
        }

        /// <summary>
        /// TODO:What is the 
        /// </summary>
        /// <param name="item"></param>
        public void UpdateItem(T item)
        {
            var itemToUpdate = GetItemById(GetId(item));
            if (itemToUpdate != null)
            {
                base.EbalitDBContext.Entry(itemToUpdate).CurrentValues.SetValues(item);
                base.EbalitDBContext.SaveChanges();
            }
        }

        public void DeleteItem(T item)
        {
            var itemToRemove = GetItemById(GetId(item));
            if (itemToRemove != null)
            {
                _dbSet.Remove(itemToRemove);
            }
            base.EbalitDBContext.SaveChanges();

        }



        /// <summary>
        /// returns the value of the property Id
        /// TODO:Exception handling
        /// </summary>
        /// <param name="item">the item</param>
        /// <returns>Id</returns>
        private int GetId(T item)
        {
            int returnValue = 0;
            System.Reflection.PropertyInfo idProperty = typeof(T).GetProperty("Id");
            if (idProperty != null)
            {
                var value = idProperty.GetValue(item, null);
                if (value != null)
                {
                    returnValue = Convert.ToInt32(value);
                }
            }
            return returnValue;
        }

    }
}