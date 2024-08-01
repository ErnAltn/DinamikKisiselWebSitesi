﻿using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class Article : BaseEntity, IEntity
    {
        public string Header { get; set; }
        public string ImageUrl { get; set; }
        public string Description { get; set; }
        public string Subject { get; set; }
    }
}
