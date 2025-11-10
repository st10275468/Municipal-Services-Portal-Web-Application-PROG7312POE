using System.Collections.Generic;
using System;

namespace Municipal_Services_Portal.Models
{
    public class IssueHeap
    {
        private List<Issue> issueHeap = new List<Issue>();

        public void InsertIssue(Issue issue)
        {
            issueHeap.Add(issue);   
            MoveUp(issueHeap.Count - 1);
        }

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

        private void Swap(int i, int j)
        {
            Issue tempIssue = issueHeap[i];
            issueHeap[i] = issueHeap[j];
            issueHeap[j] = tempIssue;
        }

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

        public List<Issue> ToList() => new List<Issue>(issueHeap);

    }
}
