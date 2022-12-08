using System.Collections.Generic;

namespace Epsilon.Web.ViewModels.Category
{
    public class CategoryListViewModel
    {
        //// TODO: implement paging

        public IEnumerable<CategoryInListViewModel> Computers { get; set; } = new List<CategoryInListViewModel>();
    }
}
