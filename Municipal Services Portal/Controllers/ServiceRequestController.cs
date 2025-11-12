/* Reference:
OpenAI, 2025. ChatGPT (Version GPT-5 mini).[Large language model].Available at: https://chatgpt.com/c/68cd30d8-86c4-8323-866a-e2bc1d538387 [Accessed: 10 November 2025].
*/
using Microsoft.AspNetCore.Mvc;
using Municipal_Services_Portal.Models;

namespace Municipal_Services_Portal.Controllers
{
    public class ServiceRequestController : Controller
    {
        private static IssueLinkedList issues = ReportIssueController.issues;

        /// <summary>
        /// Returns the service request view
        /// </summary>
        /// <param name="searchID"></param>
        /// <returns></returns>
        public IActionResult ServiceRequest(int? searchID)
        {
            //Create new instances of the BinarySearchTree, IssueHeap and Graph
            BinSearchTree bst = new BinSearchTree();
            IssueHeap issueHeap = new IssueHeap();
            Graphs graph = new Graphs();
            graph.SetupConnections();

            //Creating an array of issues
            Issue[] arrIssues = issues.ToArray();

            //Adding each issue into the BinarySearchTree and IssueHeap
            foreach (Issue issue in arrIssues) 
            { 
                bst.Insert(issue);
                issueHeap.Insert(issue);
            }

            //Storing the results from the BinarySearchTree
            List<Issue> bstIssues;

            //If the search form has a value then it will search for the issue relating to the issueID
            if (searchID.HasValue)
            {
                Issue found = bst.SearchID(searchID.Value);
                bstIssues = found != null ? new List<Issue> { found } : new List<Issue>();
            }
            else { 

            //If nothing found then displays all issues in order
            bstIssues = bst.IssuesInOrder();    

            }

            //Storing relatedCategories and relatedCategoryIssues in lists
            List<string> relatedCategories = new List<string>();
            List<Issue> relatedCategoryIssues = new List<Issue>();

            //If an issue matches the users input...it displays the related categories and the issues
            if (searchID.HasValue && bstIssues.Count > 0)
            {
                string category = bstIssues[0].Category;    
                relatedCategories = graph.GetConnectedCategories(category);
                //Finding issues with related categories
                relatedCategoryIssues = arrIssues.Where(i => relatedCategories.Contains(i.Category) && i.IssueID != searchID.Value).ToList();
            }
          
            //Sending all the data so the viw can access it
            ViewBag.bstIssues = bstIssues;
            ViewBag.heapIssues = issueHeap.ToSortedList();
            ViewBag.relatedCategories = relatedCategories;
            ViewBag.relatedCategoryIssues = relatedCategoryIssues;
            ViewBag.SearchID = searchID;

            //Returning the view and the issue data
            return View(arrIssues);
        }
    }
}

//-----------------------------------------------------------END OF FILE----------------------------------------------------------------------------//