using Municipal_Services_Portal.Models;
using System;

namespace Municipal_Services_Portal.Models
{
    
    
    public class TreeNode
    {
        public Issue data {  get; set; }
        public TreeNode left { get; set; }
        public TreeNode right { get; set; }

        public TreeNode(Issue issue) {
            data = issue;
            left = null;
            right = null;

        }

    }

    public class BinSearchTree
    {
        public TreeNode root { get; private set; }

        public void Insert(Issue issue)
        {
            root = Insert(root, issue);
        }

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

        public List<Issue> IssuesInOrder()
        {
            List<Issue> issues = new List<Issue>();
            Traverse(root, issues);
            return issues;
        }

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

        public Issue SearchID(int issueID)
        {
            return SearchRec(root, issueID);
        }

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
