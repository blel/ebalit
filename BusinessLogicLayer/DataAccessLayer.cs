using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EbalitWebForms.DataLayer;

namespace EbalitWebForms.BusinessLogicLayer
{
    public class DataAccessLayer:IDisposable
    {
        private readonly Ebalit_WebFormsEntities _ebalitDBContext;
        public Ebalit_WebFormsEntities EbalitDBContext { get { return _ebalitDBContext; } }

        public DataAccessLayer()
        {
            _ebalitDBContext = new Ebalit_WebFormsEntities();
         
        }

        public void Dispose()
        {

            Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _ebalitDBContext.Dispose();
            }
        }
    }
}