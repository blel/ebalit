using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EbalitWebForms.WebService;

namespace EbalitWebForms.Common
{
    public class ResourceDtoEqualityComparer:IEqualityComparer<ResourceDto>
    {
        public bool Equals(ResourceDto x, ResourceDto y)
        {
            return x.MpsServerGuid == y.MpsServerGuid;
        }

        public int GetHashCode(ResourceDto obj)
        {
            return obj.MpsServerGuid.GetHashCode();
        }
    }
}
