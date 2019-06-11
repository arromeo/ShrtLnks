using System.Collections.Generic;
using ShrtLnks.Models;

namespace ShrtLnks.ViewModels
{
    public class DashboardViewModel
    {
        public IEnumerable<Link> Links { get; set; }
    }
}
