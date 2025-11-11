using Microsoft.AspNetCore.Mvc;
using Municipal_Services_Portal.Models;

namespace Municipal_Services_Portal.Controllers
{
    public class ServiceRequestController : Controller
    {
        private static IssueLinkedList issues = ReportIssueController.issues;
        public IActionResult ServiceRequest(int? searchID)
        {
            BinSearchTree bst = new BinSearchTree();
            IssueHeap issueHeap = new IssueHeap();
            Graphs graph = new Graphs();

            Issue[] arrIssues = issues.ToArray();

            foreach (Issue issue in arrIssues) 
            { 
                bst.Insert(issue);
                issueHeap.Insert(issue);
                graph.AddCategory(issue.Category);

            }

            List<Issue> bstIssues;

            if (searchID.HasValue)
            {
                Issue found = bst.SearchID(searchID.Value);
                bstIssues = found != null ? new List<Issue> { found } : new List<Issue>();
            }
            else { 

            bstIssues = bst.IssuesInOrder();    

            }


                for (int i = 0; i < arrIssues.Length; i++)
                {
                    for (int j = i + 1; j < arrIssues.Length; j++)
                    {
                        if (arrIssues[i].Category == arrIssues[j].Category)
                        {
                            graph.Connect(arrIssues[i].Category, arrIssues[j].Category);
                        }
                    }
                }

            ViewBag.bstIssues = bstIssues;
            ViewBag.heapIssues = issueHeap.ToSortedList();
            ViewBag.Graph = graph;

            return View(arrIssues);
        }
    }
}
