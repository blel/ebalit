using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EbalitWebForms.DataLayer;

namespace EbalitWebForms.BusinessLogicLayer
{
    /// <summary>
    /// Repository pattern for Wines. 
    /// NOt sure if this approach is good, but it works.
    /// 
    /// </summary>
    public class WineBLL:DataAccessLayer
    {
        /// <summary>
        /// Get a wine by primary key
        /// </summary>
        /// <param name="id">Primary key</param>
        /// <returns></returns>
        public Wine GetWineById(int id)
        {
            return (from wine in base.EbalitDBContext.Wines
                    where wine.Id == id
                    select wine).FirstOrDefault();
        }

        /// <summary>
        /// Returns all wines 
        /// </summary>
        /// <returns></returns>
        public IList<Wine> GetWines()
        {
            return (from wine in base.EbalitDBContext.Wines
                    select wine).ToList();
        }

        /// <summary>
        /// Returns wines as concatenated strings
        /// </summary>
        /// <returns></returns>
        public IDictionary<string,string> GetWineAsString()
        {

            return (from wine in base.EbalitDBContext.Wines
                    select wine).ToDictionary(cc => cc.Id.ToString(), cc => cc.Label + ", " + cc.Year + ", " + cc.Grape + "," + cc.Origin);
        }


        /// <summary>
        /// Insert a new wine
        /// </summary>
        /// <param name="wine">the wine</param>
        /// <returns></returns>
        public int CreateWine(Wine wine)
        {
            base.EbalitDBContext.Wines.Add(wine);
            base.EbalitDBContext.SaveChanges();
            return wine.Id;
        }

        /// <summary>
        /// Update a wine
        /// </summary>
        /// <param name="wine"></param>
        public void UpdateWine(Wine wine)
        {
            var wineToUpdate = GetWineById(wine.Id);
            if (wineToUpdate != null)
            {
                base.EbalitDBContext.Entry(wineToUpdate).CurrentValues.SetValues(wine);
                base.EbalitDBContext.SaveChanges();
            }
        }

        /// <summary>
        /// Delete a wine
        /// </summary>
        /// <param name="wine"></param>
        public void DeleteWine(Wine wine)
        {
            var wineToDelete = GetWineById(wine.Id);
            if (wineToDelete != null)
            {
                base.EbalitDBContext.Wines.Remove(wineToDelete);
                base.EbalitDBContext.SaveChanges();
            }
        }
    }
}