using System;
using EbalitWebForms.DataLayer;

namespace EbalitWebForms.BusinessLogicLayer
{
    /// <summary>
    /// The base class for all Dal (bll) classes
    /// Implements IDisposable
    /// </summary>
    public class DataAccessLayer: IDisposable
    {
        private readonly Ebalit_WebFormsEntities _ebalitDbContext;

        /// <summary>
        /// The db context
        /// </summary>
        public Ebalit_WebFormsEntities EbalitDbContext
        {
            get
            {
                return _ebalitDbContext;
            }
        }

        public DataAccessLayer()
        {
            _ebalitDbContext = new Ebalit_WebFormsEntities();
         
        }

        /// <summary>
        /// Implementation of IDisposable
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
        }

        /// <summary>
        /// Implementation of IDisposable
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _ebalitDbContext.Dispose();
            }
        }
    }
}