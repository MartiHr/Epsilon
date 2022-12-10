using System.Collections.Generic;

namespace Epsilon.Web.ViewModels.Category
{
    public class CategoryListViewModel
    {
        //// TODO: implement paging

        public IEnumerable<CategoryInListViewModel> Categories { get; set; } = new List<CategoryInListViewModel>();

        public int? DeleteId { get; set; }
    }
}
