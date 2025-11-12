/* Reference:
OpenAI, 2025. ChatGPT (Version GPT-5 mini).[Large language model].Available at: https://chatgpt.com/c/68cd30d8-86c4-8323-866a-e2bc1d538387 [Accessed: 10 November 2025].
*/
namespace Municipal_Services_Portal.Models
{
    /// <summary>
    /// TreeNode to store an issue and references its left and right child nodes
    /// </summary>
    public class TreeNode
    {
        public Issue data {  get; set; }
        public TreeNode left { get; set; }
        public TreeNode right { get; set; }

        //Creating the node with the issue
        public TreeNode(Issue issue) {
            data = issue;
            left = null;
            right = null;

        }

    }

    /// <summary>
    /// Binary Search Tree used to retrievem organize and display issues efficiently.
    /// </summary>
    public class BinSearchTree
    {
        public TreeNode root { get; private set; }

        /// <summary>
        /// Stores issues into the tree using the submission date with older ones to the left and new ones to the right
        /// </summary>
        /// <param name="issue"></param>
        public void Insert(Issue issue)
        {
            root = Insert(root, issue);
        }

        /// <summary>
        /// Organizing the tree by finding the correct position for each new issue added
        /// </summary>
        /// <param name="node"></param>
        /// <param name="issue"></param>
        /// <returns></returns>
        private TreeNode Insert(TreeNode node, Issue issue)
        {
            if (node == null)
            {
                return new TreeNode(issue);
            }

            if (issue.DateSubmitted < node.data.DateSubmitted)
            {
                node.left = Insert(node.left, issue);
            }
            else
            {
                node.right = Insert(node.right, issue);
            }

            return node;
        }

        /// <summary>
        /// Sorts the issues into an orderered list from oldest to newest issues
        /// </summary>
        /// <returns></returns>
        public List<Issue> IssuesInOrder()
        {
            List<Issue> issues = new List<Issue>();
            Traverse(root, issues);
            return issues;
        }

        /// <summary>
        /// Organizes output by submission date by visiting each node that is sorted
        /// </summary>
        /// <param name="node"></param>
        /// <param name="issues"></param>
        protected void Traverse(TreeNode node, List<Issue> issues)
        {
            if (node == null)
            {
                return;
            }
            Traverse(node.left, issues);
            issues.Add(node.data);
            Traverse(node.right, issues);
        }

        /// <summary>
        /// Finds a specific issue in the tree using the IssueID
        /// </summary>
        /// <param name="issueID"></param>
        /// <returns></returns>
        public Issue SearchID(int issueID)
        {
            return SearchRec(root, issueID);
        }

        /// <summary>
        /// Searches each node for the matching issueID
        /// </summary>
        /// <param name="node"></param>
        /// <param name="issueID"></param>
        /// <returns></returns>
        private Issue SearchRec(TreeNode node, int issueID)
        {
            if (node == null)
            {
                return null;
            }
            if (node.data.IssueID == issueID) 
            {
                return node.data;
            }

            var lSearch = SearchRec(node.left, issueID);
            if (lSearch != null)
            {
                return lSearch;
            }

            return SearchRec(node.right, issueID);
        }
    }

    
}
//-----------------------------------------------------------END OF FILE----------------------------------------------------------------------------//