namespace Municipal_Services_Portal.Models
{
    public class Graphs
    {
        public Dictionary<string, HashSet<string>> connections { get; private set; } = new Dictionary<string, HashSet<string>>();

        public void Connect(string category1, string category2)
        {
            
            if (!connections.ContainsKey(category1))
            {
                connections[category1] = new HashSet<string>();
            }
            if (!connections.ContainsKey(category2))
            {
                connections[category2] = new HashSet<string>();
            }
           
            if (category1 != category2) {
            
                connections[category1].Add(category2);
                connections[category2].Add(category1);
            }
        }

        public List<string> GetConnectedCategories(string category)
        {
            return connections.ContainsKey(category) ? connections[category].ToList() : new List<string>();
        }

        public void SetupConnections()
        {
            Connect("Roads", "Traffic");
            Connect("Roads", "Safety");
            Connect("Roads", "Drainage");
            Connect("Water", "Health");
            Connect("Water", "Sanitation");
            Connect("Electricity", "Safety");
            Connect("Electricity", "Lighting");
            Connect("Waste", "Health");
            Connect("Sanitation", "Health");
            Connect("Parks", "Environment");
            Connect("Environment", "Health");
            Connect("Buildings", "Safety");
            Connect("Buildings", "Parks");
        }
   
    }
}
