using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;

namespace EbalitWebForms.Common
{
    /// <summary>
    /// Extension methods to existing framework types
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// Extension to IQueryable
        /// returns an orderedEnumberable according to the orderParameter and sortDescending params
        /// </summary>
        /// <typeparam name="TSource">Type of the enumerable</typeparam>
        /// <param name="source">IQueryable</param>
        /// <param name="orderParameter">string order parameter</param>
        /// <param name="sortDescending">true if sort descending, otherwise false</param>
        /// <returns></returns>
        public static IOrderedEnumerable<TSource> OrderByStringSelector<TSource>(this IQueryable<TSource> source, string orderParameter, bool sortDescending)
        {
            //get the search Property
            var searchProperty = source.FirstOrDefault().GetType().GetProperties().Where(property => property.Name == orderParameter).FirstOrDefault();

            if (searchProperty != null)
            {
                //return an ordered list according to sort direction
                if (sortDescending)
                {
                    return source.ToList().OrderByDescending(task => (searchProperty.GetValue(task, null)));
                }
                else
                {
                    return source.ToList().OrderBy(task => (searchProperty.GetValue(task, null)));
                }
            }
            else
            {
                throw new Exception("Order parameter cannot be null");
            }
            
        }

        /// <summary>
        /// Applies the converter to each item of the Source Collection
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TDest"></typeparam>
        /// <param name="source"></param>
        /// <param name="converter"></param>
        /// <returns></returns>
        public static IList<TDest> ForEach<TSource, TDest>(this IEnumerable<TSource> source,
            Func<TSource, TDest> converter)
        {
            var resultCollection = new Collection<TDest>();
            foreach (var item in source)
            {
                var resultItem = converter(item);
                resultCollection.Add(resultItem);
            }
            return resultCollection;
        }
    }
}