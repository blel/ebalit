using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;

namespace EbalitWebForms.BusinessLogicLayer
{
    /// <summary>
    /// Generic repository.
    /// See Projects for an implementation
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Repository<T> : DataAccessLayer, IRepository<T> where T : class
    {
        private readonly DbSet<T> _dbSet;

        public Repository()
        {
            _dbSet = EbalitDbContext.Set<T>();

        }

        /// <summary>
        /// Get item by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public T GetItemById(int id)
        {
            return _dbSet.AsEnumerable().Where(cc => GetId(cc) == id).Select(cc => cc).FirstOrDefault();
        }

        /// <summary>
        /// Get all items
        /// </summary>
        /// <returns></returns>
        public List<T> GetItems()
        {
            return _dbSet.ToList();
        }

        /// <summary>
        /// Get all items including a subitem.
        /// TODO: This can be improved by using EF extensions
        /// </summary>
        /// <param name="include"></param>
        /// <returns></returns>
        public List<T> GetItems(string include)
        {
            return _dbSet.Include(include).ToList();
        }

        /// <summary>
        /// Create a new item
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public int CreateItem(T item)
        {
            _dbSet.Add(item);
            EbalitDbContext.SaveChanges();
            return Convert.ToInt32(typeof(T).GetProperty("Id").GetValue(item,null));
        }

        /// <summary>
        /// Update item
        /// </summary>
        /// <param name="item"></param>
        public void UpdateItem(T item)
        {
            var itemToUpdate = GetItemById(GetId(item));
            if (itemToUpdate != null)
            {
                EbalitDbContext.Entry(itemToUpdate).CurrentValues.SetValues(item);
                EbalitDbContext.SaveChanges();
            }
        }

        /// <summary>
        /// Delete item
        /// </summary>
        /// <param name="item"></param>
        public void DeleteItem(T item)
        {
            var itemToRemove = GetItemById(GetId(item));
            if (itemToRemove != null)
            {
                _dbSet.Remove(itemToRemove);
            }
            EbalitDbContext.SaveChanges();

        }

        /// <summary>
        /// returns the value of the property Id
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