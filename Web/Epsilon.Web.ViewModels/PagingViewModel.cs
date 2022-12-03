using System;

namespace Epsilon.Web.ViewModels
{
    public class PagingViewModel
    {
        public int PagesCount => (int)Math.Ceiling((double)ComputersCount / ItemsPerPage);

        public int PageNumber { get; set; }

        public int ComputersCount { get; set; }

        public int ItemsPerPage { get; set; }

        public bool HasPreviousPage => PageNumber > 1;

        public int PreviousPageNumber => PageNumber - 1;

        public bool HasNextPage => PageNumber < PagesCount;

        public int NextPageNumber => PageNumber + 1;
    }
}
