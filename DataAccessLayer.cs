using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EbalitWebForms
{
    public class DataAccessLayer:IDisposable
    {
        private Ebalit_WebFormsEntities _ebalitDBContext;
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