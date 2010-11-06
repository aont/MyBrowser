using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MyBrowser.HTML
{
    class TreeNodeEx : TreeNode
    {
        public HtmlElement htmlElement;
        public TreeNodeEx(HtmlElement htmlElement)
            : base()
        {
            this.htmlElement = htmlElement;
            this.Text = htmlElement.TagName;
            this.PrepareChildrenNodes();
        }
        public new TreeNodeEx Parent
        {
            get
            {
                return base.Parent as TreeNodeEx;
            }
        }
        public void parentalexpand()
        {
            this.Expand();
            TreeNodeEx parent = this.Parent;
            if (parent != null)
            {
                parent.parentalexpand();
            }
        }

        public new TreeNodeCollection Nodes
        {
            get
            {
                PrepareChildrenNodes();
                return base.Nodes;
            }
        }
        bool ChildrenNodesPrepared = false;

        public void PrepareChildrenNodes()
        {
            if (!this.ChildrenNodesPrepared)
            {
                this.ChildrenNodesPrepared = true;
                foreach (HtmlElement element in this.htmlElement.Children)
                {
                    this.Nodes.Add(new TreeNodeEx(element));
                }
            }
        }

        public void RePrepareChildrenNodes()
        {
            if (this.ChildrenNodesPrepared)
            {
                
                this.ChildrenNodesPrepared = true;
                List<HtmlElement> list_htmlelements = new List<HtmlElement>();
                foreach (HtmlElement element in this.htmlElement.Children)
                {
                    list_htmlelements.Add(element);
                }
                foreach (TreeNodeEx node in this.Nodes)
                {
                    foreach (HtmlElement element in list_htmlelements.ToArray())
                    {
                        if (element == node.htmlElement)
                        {
                            list_htmlelements.Remove(element);
                            break;
                        }
                        node.Remove();
                    }
                }
                foreach (HtmlElement element in list_htmlelements)
                {
                    this.Nodes.Add(new TreeNodeEx(element));
                }
            }
            else { PrepareChildrenNodes(); }
        }
    }
}