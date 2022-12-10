using System.Collections.Generic;

namespace Epsilon.Web.ViewModels.Part
{
    public class PartListViewModel
    {
        public IEnumerable<PartInListViewModel> Parts { get; set; } = new List<PartInListViewModel>();
    }
}
