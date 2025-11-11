using Microsoft.AspNetCore.Mvc;
using Municipal_Services_Portal.Models;

namespace Municipal_Services_Portal.Controllers
{
    public class ReportIssueController : Controller
    {

        public static IssueLinkedList issues { get; } = new IssueLinkedList();

        static ReportIssueController()
        {
            //Sample data that will be on the website when opened for demo purposes
            var issue1 = new Issue("2 Howe Road, Observatory", "Roads", "Road is flooded", "", 1 );
            var issue2 = new Issue("41 Main Road, Bergvliet", "Electrical", "Street light down", "", 2 );
            var issue3 = new Issue("3 Lower Main Road, Observatory", "Water", "Burst pipe", "", 2);
            var issue4 = new Issue("69 Sandown Road, Rondebosch", "Sanitation", "Blocked Sewer", "", 2);
            var issue5 = new Issue("32 Klipfontein road, Mowbray", "Parks", "Broken Swing", "", 2);


            issue1.Status = "Resolved";
            issue2.Status = "In Progress";
            issue3.Status = "Pending";
            issue4.Status = "Resolved";
            issue5.Status = "Pending";

            issues.AddIssue(issue1);
            issues.AddIssue(issue2);
            issues.AddIssue(issue3);
            issues.AddIssue(issue4);
            issues.AddIssue(issue5);

        }

        [HttpGet]
        public IActionResult ReportIssues()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ReportIssues(string location, string category, string description, Microsoft.AspNetCore.Http.IFormFile media)
        {
            string mediaPath = "";
            if (media != null && media.Length > 0)
            {
                string folder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/IssueImages");

                //Creating image/document folder if it doesn't exist
                if (!Directory.Exists(folder)) { 
                    Directory.CreateDirectory(folder);
                }

                string filePath = Path.Combine(folder, media.FileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    media.CopyTo(stream);
                }

                mediaPath = "/IssueImages/" + media.FileName;

            }

            //Creating a mew issue and adding it to the linkedlist
            int priority = 3;
            Guid id = Guid.NewGuid();
            Issue newIssue = new Issue(location, category, description, mediaPath, priority);
            issues.AddIssue(newIssue);
            ViewBag.Message = "Issue submitted";

            return View();
        }

        [HttpGet]
        public IActionResult ViewIssues()
        {
            Issue[] issueArray = issues.ToArray();
            return View(issueArray);
        }

    }
}