using System;

using AutoMapper;
using Epsilon.Services.Mapping;

using PartModel = Epsilon.Data.Models.Part;

namespace Epsilon.Web.ViewModels.Part
{
    public class PartInListViewModel : IMapFrom<PartModel>, IHaveCustomMappings
    {
        public string Type { get; set; }

        public string Model { get; set; }

        public string Description { get; set; }

        public string Manufacturer { get; set; }

        public int ComputersCount { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<PartModel, PartInListViewModel>()
              .ForMember(x => x.ComputersCount, opt =>
                  opt.MapFrom(p => p.Computers.Count));
        }
    }
}
