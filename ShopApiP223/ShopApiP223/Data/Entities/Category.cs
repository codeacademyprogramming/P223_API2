using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopApiP223.Data.Entities
{
    public class Category:BaseEntity
    {
        public string Name { get; set; }
        public string Image { get; set; }
        public bool IsDeleted { get; set; }

        public List<Product> Products { get; set; }
    }
}
