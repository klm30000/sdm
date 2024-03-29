﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EHRDomain
{
    public class Role
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int IsInBuilt { get; set; }

        public DateTime CreateTime { get; set; }

        public DateTime UpdateTime { get; set; }

        public List<UserRole> UserRoles { get; set; }
    }
}
