using System.Collections.Generic;

namespace CaptureTheIsland.Models
{
    public class LandingViewModel
    {
        public IEnumerable<ModuleInfo> Modules { get; set; } = new List<ModuleInfo>();
        public IEnumerable<ChallengeSummary> Challenges { get; set; } = new List<ChallengeSummary>();
    }
}
