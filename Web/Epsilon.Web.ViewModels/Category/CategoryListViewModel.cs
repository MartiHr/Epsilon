using System.Collections.Generic;

namespace Epsilon.Web.ViewModels.Category
{
    public class CategoryListViewModel
    {
        public IEnumerable<CategoryInListViewModel> Categories { get; set; } = new List<CategoryInListViewModel>();
    }
}
