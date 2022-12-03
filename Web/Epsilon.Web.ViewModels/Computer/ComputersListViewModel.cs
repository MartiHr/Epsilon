using System;
using System.Collections.Generic;

namespace Epsilon.Web.ViewModels.Computer
{
    public class ComputersListViewModel
    {
        public IEnumerable<ComputerInListViewModel> Computers { get; set; } = new List<ComputerInListViewModel>();

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
