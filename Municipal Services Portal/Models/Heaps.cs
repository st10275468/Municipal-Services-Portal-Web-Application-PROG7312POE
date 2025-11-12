/* Reference:
OpenAI, 2025. ChatGPT (Version GPT-5 mini).[Large language model].Available at: https://chatgpt.com/c/68cd30d8-86c4-8323-866a-e2bc1d538387 [Accessed: 10 November 2025].
*/
namespace Municipal_Services_Portal.Models
{
    /// <summary>
    /// IssueHeap class helps retrieve and display the most urgent requests based off of their calculated priority
    public class IssueHeap
    {
        private List<Issue> issueHeap = new List<Issue>();

        /// <summary>
        /// Adds a new issue into the heap
        /// </summary>
        /// <param name="issue"></param>
        public void Insert(Issue issue)
        {
            issueHeap.Add(issue);   
            MoveUp(issueHeap.Count - 1);
        }

        /// <summary>
        /// Moves the current issue up in the heap if the priority is higher
        /// </summary>
        /// <param name="i"></param>
        private void MoveUp(int i)
        {
            while (i > 0)
            {
                int p = (i - 1) / 2;
                if (GetPriority(issueHeap[i]) <= GetPriority(issueHeap[p]))
                {
                    break;
                }

                Swap(i, p);
                i = p;
            }
        }

        /// <summary>
        /// Used to rearrange issue in the heap by swapping them
        /// </summary>
        /// <param name="i"></param>
        /// <param name="j"></param>
        private void Swap(int i, int j)
        {
            Issue tempIssue = issueHeap[i];
            issueHeap[i] = issueHeap[j];
            issueHeap[j] = tempIssue;
        }

        /// <summary>
        /// Calculating the priority of each issue based on their status
        /// </summary>
        /// <param name="issue"></param>
        /// <returns></returns>
        private int GetPriority(Issue issue)
        {
            return issue.Status switch
            {
                "Pending" => 3,
                "In Progress" => 2,
                "Resolved" => 1,
                _ => 0,
            };
        }

        /// <summary>
        /// Retrieves the highest priority issue from the heap and removes it
        /// </summary>
        /// <returns></returns>
        public Issue ExtractMax()
        {
            if (issueHeap.Count == 0)
            {
                return null;
            }

            Issue topIssue = issueHeap[0];
            issueHeap[0] = issueHeap[issueHeap.Count - 1];
            issueHeap.RemoveAt(issueHeap.Count - 1);
            MoveDown(0);

            return topIssue;
        }

        /// <summary>
        /// Organizes the heap by moving the current issue down the heap if its priority is less
        /// </summary>
        /// <param name="i"></param>
        private void MoveDown(int i)
        {
            int largest = i;
            int left = 2 * i + 1;
            int right = 2 * i + 2;

            if (left < issueHeap.Count && GetPriority(issueHeap[left]) > GetPriority(issueHeap[largest])) 
            {
                largest = left;
            }
            if (right < issueHeap.Count && GetPriority(issueHeap[right]) > GetPriority(issueHeap[largest]))
            {
                largest = right;
            }
            if (largest != i)
            {
                Swap(i, largest);
                MoveDown(largest);
            }
        }


        /// <summary>
        /// Returns a sorted list of issues based on their priority
        /// </summary>
        /// <returns></returns>
        public List<Issue> ToSortedList()
        {
            var sortedIssues = new List<Issue>(issueHeap);
            sortedIssues.Sort((a, b) => GetPriority(b).CompareTo(GetPriority(a)));
            return sortedIssues;
        }

    }
}

//-----------------------------------------------------------END OF FILE----------------------------------------------------------------------------//