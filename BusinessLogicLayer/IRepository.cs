using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EbalitWebForms.BusinessLogicLayer
{
    public interface IRepository<T>
    {
        T GetItemById(int id);
        IList<T> GetItems();
        IList<T> GetItems(string include);

        int CreateItem(T item);
        void UpdateItem(T item);
        void DeleteItem(T item);
    }
}
