using System;

using AutoMapper;
using Epsilon.Services.Mapping;

using ManufacturerModel = Epsilon.Data.Models.Manufacturer;

namespace Epsilon.Web.ViewModels.Manufacturer
{
    public class ManufacturerInListViewModel : IMapFrom<ManufacturerModel>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Country { get; set; }

        public int ComputersCount { get; set; }

        public int PartsCount { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<ManufacturerModel, ManufacturerInListViewModel>()
              .ForMember(x => x.ComputersCount, opt =>
                  opt.MapFrom(m => m.Computers.Count));
        }
    }
}
