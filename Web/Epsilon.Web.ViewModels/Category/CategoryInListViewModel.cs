using System;
using AutoMapper;
using Epsilon.Services.Mapping;

using CategoryModel = Epsilon.Data.Models.Category;

namespace Epsilon.Web.ViewModels.Category
{
    public class CategoryInListViewModel : IMapFrom<CategoryModel>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int ComputersCount { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<CategoryModel, CategoryInListViewModel>()
              .ForMember(x => x.ComputersCount, opt =>
                  opt.MapFrom(c => c.Computers.Count));
        }
    }
}
