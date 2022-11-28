using Epsilon.Services.Mapping;

using CategoryModel = Epsilon.Data.Models.Category;

namespace Epsilon.Web.ViewModels.Category
{
    public class CategoryDropdownViewModel : IMapFrom<CategoryModel>
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
