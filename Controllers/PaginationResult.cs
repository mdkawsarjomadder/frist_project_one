using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace frist_project_one.Controllers
{
    public class PaginationResult<T>
    {
        public IEnumerable<T> Items { get; set; } = new List<T>();

        public int TotalCount { get; set; } //total_page 100
        public int PageNumber { get; set; } // page singel serial page..
        public int PageSize { get; set; } // fornt koy Ti Items rakhte cai
        public int TotalPage => (int)Math.Ceiling((double)TotalCount / PageSize);
    }
}