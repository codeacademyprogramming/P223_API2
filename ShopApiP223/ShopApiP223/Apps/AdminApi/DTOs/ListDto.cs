using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopApiP223.Apps.AdminApi.DTOs
{
    public class ListDto<TItem>
    {
        public int TotalCount { get; set; }
        public List<TItem> Items { get; set; }
    }
}
