using Epsilon.Services.Mapping;

using ManufacturerModel = Epsilon.Data.Models.Manufacturer;

namespace Epsilon.Web.ViewModels.Manufacturer
{
    public class ManufacturerDropdownViewModel : IMapFrom<ManufacturerModel>
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
