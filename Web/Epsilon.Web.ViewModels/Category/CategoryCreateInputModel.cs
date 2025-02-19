﻿using System.ComponentModel.DataAnnotations;

using Epsilon.Services.Mapping;

using static Epsilon.Data.Common.DataValidation.Category;

using CategoryModel = Epsilon.Data.Models.Category;

namespace Epsilon.Web.ViewModels.Category
{
    public class CategoryCreateInputModel : IMapFrom<CategoryModel>
    {
        [Required]
        [StringLength(CategoryNameMaxLength, MinimumLength = CategoryNameMinLength)]
        public string Name { get; set; }
    }
}
