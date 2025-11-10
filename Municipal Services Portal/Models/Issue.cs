namespace Municipal_Services_Portal.Models
{
    public class Issue
    {
        public string Location { get; set; }
        public string Category {  get; set; }
        public string Description { get; set; }
        public string MediaPath {  get; set; }
        public DateTime DateSubmitted { get; set; }
        public string Status {  get; set; }

        public int Priority { get; set; }

        public Guid IssueID { get; set; }

        public Issue(string location, string category, string description, string mediaPath, int priority, Guid issueID)
        {
            Location = location;
            Category = category;
            Description = description;
            MediaPath = mediaPath;
            DateSubmitted = DateTime.Now;
            Status = "Pending";
            Priority = priority;
            IssueID = Guid.NewGuid();
        }


    }
}
