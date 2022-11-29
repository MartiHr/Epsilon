using System.Collections.Generic;

namespace Epsilon.Web.ViewModels.Part
{
    public class PartGroupViewModel
    {
        public int PartId { get; set; }

        public string PartType { get; set; }

        public IEnumerable<PartDropdownViewModel> Parts { get; set; } = new List<PartDropdownViewModel>();
    }
}
