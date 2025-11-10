using Microsoft.AspNetCore.Mvc;
using Municipal_Services_Portal.Models;

namespace Municipal_Services_Portal.Controllers
{
    public class ServiceRequestController : Controller
    {
        private static IssueLinkedList issues = ReportIssueController.issues;
        public IActionResult ServiceRequest()
        {
            BinSearchTree bst = new BinSearchTree();
            AVLTree avl = new AVLTree();
            IssueHeap issueHeap = new IssueHeap();
            Graphs graph = new Graphs();

            Issue[] arrIssues = issues.ToArray();

            foreach (Issue issue in arrIssues) 
            { 
                bst.Insert(issue);
                avl.Insert(issue);
                issueHeap.Insert(issue);
                graph.AddCategory(issue.Category);

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

            ViewBag.bstIssues = bst.IssuesInOrder();
            ViewBag.avlIssues = avl.IssuesInOrder();
            ViewBag.heapIssues = issueHeap.ToList();
            ViewBag.Graph = graph;

            return View(arrIssues);
        }
    }
}
