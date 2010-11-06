using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MyBrowser.HTML
{
    public class TreeViewEx : TreeView
    {
        WebBrowser _webBrowser;
        public WebBrowser webBrowser
        {
            get { return this._webBrowser; }
            set
            {
                this._webBrowser = value;
                webBrowser.DocumentCompleted += new WebBrowserDocumentCompletedEventHandler(webBrowser_DocumentCompleted);
            }
        }

        void webBrowser_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            DocumentSet();
        }
        public TreeViewEx()
            : base()
        {
        }

        public void DocumentSet()
        {
            this.Nodes.Clear();
            HtmlDocument document = this.webBrowser.Document; if (document != null)
            {
                this.Nodes.Add(new TreeNodeEx(this.webBrowser.Document.Body.Parent));
            }

        }
        protected override void OnBeforeExpand(TreeViewCancelEventArgs e)
        {
            foreach (TreeNode treeNode in e.Node.Nodes)
            {
                (treeNode as TreeNodeEx).RePrepareChildrenNodes();
            }
            base.OnBeforeExpand(e);
        }

        internal TreeNodeEx Search(HtmlElement elem)
        {
            HtmlElement elem_parent = elem.Parent;

            if (elem_parent == null)
            {
                TreeNodeEx node = this.Nodes[0] as TreeNodeEx;
                if (node.htmlElement == elem)
                    return node;
            }
            else
            {
                TreeNodeEx parentnode = Search(elem_parent);
                if (parentnode != null)
                {
                    parentnode.RePrepareChildrenNodes();
                    foreach (TreeNodeEx node in parentnode.Nodes)
                    {
                        if (node.htmlElement == elem)
                            return node;
                    }
                }
            }
            return null;
        }
    }
}
