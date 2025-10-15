using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace frist_project_one.Helpers
{
    public class QueryParamiters
    {
        private const int MaxPageSize = 50;  
        // [FromQuery] int PageNumber = 1,
        // [FromQuery] int PageSize = 6,
        // [FromQuery] string? search = null,
        // [FromQuery] string? SortOrder = null

        public int ppageNumber { get; set; } = 1;
        public int PpageSize { get; set; } = 6;
        
        public string? Ssearch { get; set; }
        public string? SsortOrder { get; set; }

     public     QueryParamiters Validate()
        {
            if (ppageNumber < 1)
            {
                ppageNumber = 1;
            }
            if (PpageSize < 1)
            {
                PpageSize = 6;
            }
            if (PpageSize > MaxPageSize)
            {
                PpageSize = MaxPageSize;
            }
            return this;
        }
    }
}



