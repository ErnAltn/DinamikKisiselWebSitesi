using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class Service : BaseEntity, IEntity
    {
        public string Header { get; set; }
        public string ImageUrl { get; set; }
        public string Description { get; set; }
    }

}
