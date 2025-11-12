/* Reference:
OpenAI, 2025. ChatGPT (Version GPT-5 mini).[Large language model].Available at: https://chatgpt.com/c/68cd30d8-86c4-8323-866a-e2bc1d538387 [Accessed: 10 November 2025].
*/
namespace Municipal_Services_Portal.Models
{
    /// <summary>
    /// Used to show how different service request categories are connected
    /// </summary>
    public class Graphs
    {
        public Dictionary<string, HashSet<string>> connections { get; private set; } = new Dictionary<string, HashSet<string>>();

        /// <summary>
        /// Connects two service request categories if they are related
        /// </summary>
        /// <param name="category1"></param>
        /// <param name="category2"></param>
        public void Connect(string category1, string category2)
        {
            //Adds category1 if its not in the list
            if (!connections.ContainsKey(category1))
            {
                connections[category1] = new HashSet<string>();
            }

            //Adds category2 if its not in the list
            if (!connections.ContainsKey(category2))
            {
                connections[category2] = new HashSet<string>();
            }

            //Only adds the connection if they are not the same categories
            if (category1 != category2) {
            
                connections[category1].Add(category2);
                connections[category2].Add(category1);
            }
        }

        /// <summary>
        /// Retrieves all the service request categories that are connected to the one the user searches
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        public List<string> GetConnectedCategories(string category)
        {
            return connections.ContainsKey(category) ? connections[category].ToList() : new List<string>();
        }

        /// <summary>
        /// Sets up the relationship rules of the categories so it can identify relationships when new issues are reported
        /// </summary>
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

//-----------------------------------------------------------END OF FILE----------------------------------------------------------------------------//