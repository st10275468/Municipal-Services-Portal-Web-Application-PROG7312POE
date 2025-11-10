using Municipal_Services_Portal.Models;
using System;

namespace Municipal_Services_Portal.Models
{
    
    
    public class TreeNode
    {
        public Issue data {  get; set; }
        public int height { get; set; }
        public TreeNode left { get; set; }
        public TreeNode right { get; set; }

        public TreeNode(Issue issue) {
            data = issue;
            height = 1;
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
    }


    public class AVLTree : BinSearchTree
    {
        private int GetHeight(TreeNode node) => node?.height ?? 0;
        private int GetBalance(TreeNode node) => node == null ? 0 : GetHeight(node.left) - GetHeight(node.right); 

        private new TreeNode Insert(TreeNode node, Issue issue)
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

            node.height = 1 + Math.Max(GetHeight(node.left), GetHeight(node.right));
            int balance = GetBalance(node);

            if (balance > 1 && issue.DateSubmitted < node.left.data.DateSubmitted)
            {
                return RightRotate(node);
            }

            if (balance < -1 && issue.DateSubmitted > node.right.data.DateSubmitted)
            {
                return LeftRotate(node);
            }
            if (balance > 1 && issue.DateSubmitted > node.left.data.DateSubmitted)
            {
                node.left = LeftRotate(node.left);
                return RightRotate(node);
            }
            if (balance < -1 && issue.DateSubmitted < node.right.data.DateSubmitted)
            {
                node.right = RightRotate(node.right);
                return LeftRotate(node);
            }

            return node;

        }

        private TreeNode RightRotate(TreeNode y)
        {
            TreeNode x = y.left;
            TreeNode t2 = x.right;
            x.right = y;
            y.left = t2;
            y.height = Math.Max(GetHeight(y.left), GetHeight(y.right)) + 1;
            x.height = Math.Max(GetHeight(x.left), GetHeight(x.right) + 1);

            return x;
        }

        private TreeNode LeftRotate(TreeNode x)
        {
            TreeNode y = x.right;
            TreeNode t2 = y.left;
            y.left = x;
            x.right = t2;
            x.height = Math.Max(GetHeight(x.left), GetHeight(x.right)) + 1;
            y.height = Math.Max(GetHeight(y.left), GetHeight(y.right) + 1);

            return y;
        }

        public new List<Issue> IssuesInOrder()
        {
            List<Issue> issues = new List<Issue>();
            Traverse(root, issues);
            return issues;
        }


    }

    
}
