using System.ComponentModel.DataAnnotations;

using Epsilon.Services.Mapping;

using static Epsilon.Data.Common.DataValidation.Category;

using CategoryModel = Epsilon.Data.Models.Category;

namespace Epsilon.Web.ViewModels.Category
{
    public class CategoryEditInputModel : IMapFrom<CategoryModel>
    {
        public int Id { get; set; }

        [Required]
        [StringLength(CategoryNameMaxLength, MinimumLength = CategoryNameMinLength)]
        public string Name { get; set; }
    }
}
