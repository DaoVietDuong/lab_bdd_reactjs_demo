using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LabReactjsDemo
{
    public class Item
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
    }

    public class LabDbContext
    {
        public LabDbContext()
        {
            Items = new HashSet<Item>();
        }

        public ICollection<Item> Items { get; set; }
    }

    public static class InMemoryDatabase
    {
        private static LabDbContext _db;
        public static LabDbContext LabDbContext
        {
            get
            {
                if (_db == null) { _db = new LabDbContext(); }
                return _db;
            }
        }
    }
}
