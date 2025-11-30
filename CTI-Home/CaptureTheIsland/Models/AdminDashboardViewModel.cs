namespace CaptureTheIsland.Models
{
    public class AdminDashboardViewModel
    {
        public int TotalUsers { get; set; }
        public int AdminCount { get; set; }
        public int TotalChallenges { get; set; }
        public int TotalCompletedChallenges { get; set; }

        public List<AdminUserRow> Users { get; set; }
    }

    public class AdminUserRow
    {
        public string Email { get; set; }
        public string Role { get; set; }
        public DateTime DateCreated { get; set; }
        public int CompletedChallenges { get; set; }
    }
}
