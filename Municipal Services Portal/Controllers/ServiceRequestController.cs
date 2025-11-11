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
            graph.SetupConnections();

            Issue[] arrIssues = issues.ToArray();

            foreach (Issue issue in arrIssues) 
            { 
                bst.Insert(issue);
                issueHeap.Insert(issue);
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

            List<string> relatedCategories = new List<string>();
            List<Issue> relatedCategoryIssues = new List<Issue>();

            if (searchID.HasValue && bstIssues.Count > 0)
            {
                string category = bstIssues[0].Category;    
                relatedCategories = graph.GetConnectedCategories(category);
                relatedCategoryIssues = arrIssues.Where(i => relatedCategories.Contains(i.Category) && i.IssueID != searchID.Value).ToList();
            }
          
            ViewBag.bstIssues = bstIssues;
            ViewBag.heapIssues = issueHeap.ToSortedList();
            ViewBag.relatedCategories = relatedCategories;
            ViewBag.relatedCategoryIssues = relatedCategoryIssues;
            ViewBag.SearchID = searchID;

            return View(arrIssues);
        }
    }
}
