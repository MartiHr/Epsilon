using System.Collections.Generic;
using System.Linq;

using AutoMapper;
using Epsilon.Services.Mapping;
using Epsilon.Web.ViewModels.Part;

using ComputerModel = Epsilon.Data.Models.Computer;

namespace Epsilon.Web.ViewModels.Computer
{
    public class ComputerDetailsViewModel : IMapFrom<ComputerModel>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string DisplayName { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Model { get; set; }

        public string Category { get; set; }

        public string Manufacturer { get; set; }

        public ICollection<string> ImageUrls { get; set; }

        public PartInDetailsViewModel CPU { get; set; }

        public PartInDetailsViewModel GPU { get; set; }

        public PartInDetailsViewModel Storage { get; set; }

        public decimal Price { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<ComputerModel, ComputerDetailsViewModel>()
                .ForMember(x => x.DisplayName, opt =>
                   opt.MapFrom(c => c.Name ?? $"{c.Manufacturer.Name} {c.Model}"))
                .ForMember(x => x.CPU, opt =>
                    opt.MapFrom(c => c.Parts
                        .Where(p => p.Type == "CPU")
                        .Select(p => new PartInDetailsViewModel { Id = p.Id, Description = p.Description, Model = p.Model })
                        .FirstOrDefault()))
                .ForMember(x => x.GPU, opt =>
                    opt.MapFrom(c => c.Parts
                        .Where(p => p.Type == "GPU")
                        .Select(p => new PartInDetailsViewModel { Id = p.Id, Description = p.Description, Model = p.Model })
                        .FirstOrDefault()))
                .ForMember(x => x.Storage, opt =>
                    opt.MapFrom(c => c.Parts
                        .Where(p => p.Type == "Storage")
                        .Select(p => new PartInDetailsViewModel { Id = p.Id, Description = p.Description, Model = p.Model })
                        .FirstOrDefault()))
                .ForMember(x => x.ImageUrls, opt =>
                   opt.MapFrom(c => c.Images.Select(i => $"/images/computers/{i.Id}{i.Extension}")))
                .ForMember(x => x.Category, opt =>
                    opt.MapFrom(c => c.Category.Name))
                .ForMember(x => x.Manufacturer, opt =>
                    opt.MapFrom(c => c.Manufacturer.Name));
        }
    }
}
