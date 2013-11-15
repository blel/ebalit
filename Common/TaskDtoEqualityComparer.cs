﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EbalitWebForms.WebService;

namespace EbalitWebForms.Common
{
    public class TaskDtoEqualityComparer:IEqualityComparer<TaskDto>
    {
        public bool Equals(TaskDto x, TaskDto y)
        {
            return x.Guid == y.Guid;
        }

        public int GetHashCode(TaskDto obj)
        {
            return obj.Guid.GetHashCode();
        }
    }
}