using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EbalitWebForms.BusinessLogicLayer
{
    public static class Tests
    {
        public static void TestMethod()
        {
            Type type = typeof(Repository<EbalitWebForms.DataLayer.Wine>);
            string assemblyQualifiedName = type.AssemblyQualifiedName;
        }
    }
}