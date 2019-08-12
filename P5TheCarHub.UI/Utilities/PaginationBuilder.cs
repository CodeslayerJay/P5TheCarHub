using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace P5TheCarHub.UI.Utilities
{
    public class PaginationBuilder
    {
        private readonly int totalItemCount;
        private readonly int pageSize;

        public PaginationBuilder(int totalItemCount, int pageSize)
        {
            this.totalItemCount = totalItemCount;
            this.pageSize = pageSize;
        }

        
    }
}
