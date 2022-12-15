using AutoMapper;
using Epsilon.Services.Mapping;
using System.Linq;
using ComputerModel = Epsilon.Data.Models.Computer;

namespace Epsilon.Web.ViewModels.Computer
{
    public class ComputerInListViewModel : IMapFrom<ComputerModel>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string DisplayName { get; set; }

        public string ImageUrl { get; set; }

        public string CPUModel { get; set; }

        public string GPUModel { get; set; }

        public string StorageModel { get; set; }

        public decimal Price { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<ComputerModel, ComputerInListViewModel>()
                .ForMember(x => x.CPUModel, opt =>
                    opt.MapFrom(c => c.Parts.Where(p => p.Type == "CPU").First().Model))
                .ForMember(x => x.StorageModel, opt =>
                   opt.MapFrom(c => c.Parts.Where(p => p.Type == "Storage").First().Model))
                .ForMember(x => x.GPUModel, opt =>
                   opt.MapFrom(c => c.Parts.Where(p => p.Type == "GPU").First().Model))
                .ForMember(x => x.DisplayName, opt =>
                   opt.MapFrom(c => c.Name ?? $"{c.Manufacturer.Name} {c.Model}"))
                .ForMember(x => x.ImageUrl, opt =>
                   opt.MapFrom(c => $"/images/computers/{c.Images.FirstOrDefault().Id}{c.Images.FirstOrDefault().Extension}"));
        }
    }
}