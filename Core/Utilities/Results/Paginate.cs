using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Results
{
    public class Paginate<T>
    {
        public Paginate()
        {
            Items = Array.Empty<T>();
        }

        public int Size { get; set; }
        public int Index { get; set; }
        public int Count { get; set; }
        public int Pages { get; set; }
        public IList<T> Items { get; set; }
        public bool HasPrevious => Index > 1;
        public bool HasNext => Index < Pages;
    }
}
