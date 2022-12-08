using System.ComponentModel.DataAnnotations;

using Epsilon.Services.Mapping;

using CategoryModel = Epsilon.Data.Models.Category;

namespace Epsilon.Web.ViewModels.Category
{
    public class CategoryCreateInputModel : IMapFrom<CategoryModel>
    {
        [Required]
        public string Name { get; set; }
    }
}
