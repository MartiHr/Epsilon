using Epsilon.Services.Mapping;

using PartModel = Epsilon.Data.Models.Part;

namespace Epsilon.Web.ViewModels.Part
{
    public class PartDropdownViewModel : IMapFrom<PartModel>
    {
        public int Id { get; set; }

        public string Model { get; set; }
    }
}
