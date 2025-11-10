namespace Municipal_Services_Portal.Models
{
    public class Graphs
    {
        public Dictionary<string, List<string>> connections { get; private set; } = new Dictionary<string, List<string>>();

        public void AddCategory(string category)
        {
            if (!connections.ContainsKey(category))
            {
                connections[category] = new List<string>();
            }
        }

        public void Connect(string start, string end)
        {
            AddCategory(start);
            AddCategory(end);
            
            if (!connections[start].Contains(end))
            {
                connections[start].Add(end);
            }
            if (!connections[end].Contains(start))
            {
                connections[end].Add(start);
            }
        }

        public List<string> GetConnected(string category) 
        {
            List<string> connected = new List<string>();
            if (!connections.ContainsKey(category))
            {
                return connected;
            }

            Queue<string> queue = new Queue<string>();
            HashSet<string> visited = new HashSet<string>();

            queue.Enqueue(category);
            visited.Add(category);

            while (queue.Count > 0)
            {
                string current = queue.Dequeue();
                connected.Add(current);

                foreach (var neighbor in connections[current])
                {
                    if (!visited.Contains(neighbor))
                    {
                        visited.Add((neighbor));
                        queue.Enqueue((neighbor));
                    }
                }
            }

            return connected;

        }



    }
}
