using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace P5TheCarHub.UI.Models.ViewModels
{
    public class Pagination
    {
        public int PrevPage { get; set; }
        public int NextPage { get; set; }

        public int FirstPage { get; set;  } = 1;
        public int LastPage { get; set; }
        
        
        public int PageSize { get; set; }
        public int PageIndex { get; set; }

        public int TotalItemCount { get; set; }
        public int TotalPages { get; set; }

        public Pagination(int totalItemCount, int pageSize, int pageIndex = 1)
        {
            PageSize = pageSize;
            TotalItemCount = totalItemCount;
            PageIndex = pageIndex;

            Initialize();
        }

        private void Initialize()
        {
            TotalPages = (int)Math.Ceiling(TotalItemCount / (float)PageSize);
            PageIndex = (PageIndex > TotalPages) ? 1 : PageIndex;
            LastPage = TotalPages;
            PrevPage = PageIndex > FirstPage ? PageIndex - 1 : FirstPage;
            NextPage = PageIndex < LastPage ? PageIndex + 1 : LastPage;

        }
    }
}
