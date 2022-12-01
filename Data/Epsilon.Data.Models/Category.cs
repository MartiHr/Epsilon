using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using Epsilon.Data.Common.Models;

using static Epsilon.Data.Common.DataValidation.Category;

namespace Epsilon.Data.Models
{
    public class Category : BaseDeletableModel<int>
    {
        [Required]
        [MaxLength(CategoryNameMaxLength)]
        public string Name { get; set; }

        public ICollection<Computer> Computers { get; set; } = new HashSet<Computer>();
    }
}
