using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using MyBrowser.HTML;

namespace MyBrowser
{
    public partial class BrowserForm : Form
    {
        /// <summary>
        /// アプリケーションのメイン エントリ ポイントです。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new BrowserForm());
        }

        public BrowserForm()
        {
            InitializeComponent();
            this.webBrowser1.Navigate(this.address_textBox1.Text);
        }

        void webBrowser1_Navigating(object sender, WebBrowserNavigatingEventArgs e)
        {
            Uri url = this.webBrowser1.Url;
            if (url != null)
                this.address_textBox1.Text = url.ToString();
            else
                this.address_textBox1.Text = e.Url.ToString();
        }

        void webBrowser1_DocumentTitleChanged(object sender, EventArgs e)
        {
            this.Text = this.webBrowser1.DocumentTitle;
        }

        void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            HtmlDocument document = this.webBrowser1.Document;
            if (document != null)
            {
                document.MouseDown += new HtmlElementEventHandler(Document_MouseDown);
            }
            this.address_textBox1.Text = this.webBrowser1.Url.ToString();
        }

        TreeNodeEx searchednode = null;
        void Document_MouseDown(object sender, HtmlElementEventArgs e)
        {
            if (e.CtrlKeyPressed)
            {
                HtmlElement elem;
                this.propertyGrid1.SelectedObject = elem = this.webBrowser1.Document.GetElementFromPoint(e.MousePosition);
                if (searchednode != null)
                    searchednode.BackColor = Color.Transparent;
                searchednode = this.treeViewEx.Search(elem);
                if (searchednode != null)
                {
                    //this.treeViewEx.Nodes[0].Toggle();
                    searchednode.BackColor = Color.Red;
                    TreeNodeEx parent = searchednode.Parent;
                    if (parent != null)
                    {
                        TreeNodeEx grandparent = parent.Parent;
                        if (grandparent != null)
                            grandparent.parentalexpand();
                        parent.Collapse();
                        parent.Expand();
                    }
                }
            }
        }


        private void address_textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            Keys e_KeyCode = e.KeyCode;
            Keys e_Modifiers = e.Modifiers;
            if (e_KeyCode == Keys.Enter)
            {
                this.webBrowser1.Navigate(this.address_textBox1.Text);
            }
            else if (e_KeyCode == Keys.Escape)
            {
                this.address_textBox1.Text = this.webBrowser1.Url.ToString();
            }
        }

        private void SwitchTreeViewExVisibility(object sender, EventArgs e)
        {
            this.treeViewEx.Visible = !this.treeViewEx.Visible;
        }

        private void SwitchHTMLElementPropertyVisibility(object sender, EventArgs e)
        {
            this.propertyGrid1.Visible = !this.propertyGrid1.Visible;
        }

        private void treeView2_AfterSelect(object sender, TreeViewEventArgs e)
        {
            this.propertyGrid1.SelectedObject = (e.Node as TreeNodeEx).htmlElement;
        }



        void ExecCommand(string command, bool ui, object obj)
        {
            HtmlDocument document = this.webBrowser1.Document;
            if (document != null)
            {
                document.ExecCommand(command, ui, obj);
            }
        }
        private void Reload_Click(object sender, EventArgs e) { treeViewEx.DocumentSet(); }

        private void MoveTo_Click(object sender, EventArgs e)
        {
            (this.propertyGrid1.SelectedObject as HtmlElement).ScrollIntoView(true);
        }


        void webBrowser1_StatusTextChanged(object sender, System.EventArgs e)
        {
            this.StatusLabel1.Text = this.webBrowser1.StatusText;
        }

        private void Copy_Click(object sender, EventArgs e)
        {
            ExecCommand("Copy", true, null);

        }

        private void Cut_Click(object sender, EventArgs e)
        {
            ExecCommand("Cut", true, null);

        }

        private void Delete_Click(object sender, EventArgs e)
        {
            ExecCommand("Delete", true, null);
        }

        private void Paste_Click(object sender, EventArgs e)
        {
            ExecCommand("Paste", true, null);
        }

        private void SelectAll_Click(object sender, EventArgs e)
        {
            ExecCommand("SelectAll", true, null);
        }

        private void SaveAs_Click(object sender, EventArgs e)
        {
            this.webBrowser1.ShowSaveAsDialog();
        }

        private void Undo_Click(object sender, EventArgs e)
        {
            ExecCommand("Undo", true, null);
        }

        private void Redo_Click(object sender, EventArgs e)
        {
            ExecCommand("Redo", true, null);
        }


        private void ShowProperties_Click(object sender, EventArgs e)
        {
            this.webBrowser1.ShowPropertiesDialog();
        }



        private void NewPage_Click(object sender, EventArgs e)
        {
            this.webBrowser1.Navigate("about:blank");
        }

        private void Print_Click(object sender, EventArgs e)
        {
            this.webBrowser1.ShowPrintDialog();
        }

        private void PrintPreview_Click(object sender, EventArgs e)
        {
            this.webBrowser1.ShowPrintPreviewDialog();
        }

        private void Bold_Click(object sender, EventArgs e)
        {
            ExecCommand("Bold", true, null);
        }

        private void Italic_Click(object sender, EventArgs e)
        {
            ExecCommand("Italic", true, null);
        }

        private void Underline_Click(object sender, EventArgs e)
        {
            ExecCommand("Underline", true, null);
        }

        private void Indent_Click(object sender, EventArgs e)
        {
            ExecCommand("Indent", true, null);

        }

        private void JustyfyLeft_Click(object sender, EventArgs e)
        {
            ExecCommand("JustifyLeft", true, null);
        }

        private void JustofyCenter_Click(object sender, EventArgs e)
        {
            ExecCommand("JustifyCenter", true, null);
        }

        private void JustifyRight_Click(object sender, EventArgs e)
        {
            ExecCommand("JustifyRight", true, null);
        }

        private void JusifyNone_Click(object sender, EventArgs e)
        {
            ExecCommand("JustifyNone", true, null);

        }

        private void SetFont_Click(object sender, EventArgs e)
        {
            if (this.fontDialog1.ShowDialog() == DialogResult.OK)
            {
                ExecCommand("FontName", true, this.fontDialog1.Font.Name);
                //    this.webBrowser1.Document.ExecCommand("FontSize", true, this.fontDialog1.Font.Size);
                //     this.webBrowser1.Document.ExecCommand("ForeColor", true, this.fontDialog1.Color.ToKnownColor()) ;           

            }
        }

        private void Exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void GoBack_Click(object sender, EventArgs e)
        {
            this.webBrowser1.GoBack();
        }

        private void Forward_Click(object sender, EventArgs e)
        {
            this.webBrowser1.GoForward();
        }


        private void GoHome_Click(object sender, EventArgs e)
        {
            this.webBrowser1.GoHome();
        }

        private void Go_Click(object sender, EventArgs e)
        {
            this.webBrowser1.Navigate(this.address_textBox1.Text);
        }
        private void Refresh_Click(object sender, EventArgs e)
        {
            this.webBrowser1.Refresh();
        }
        private void Abort_Click(object sender, EventArgs e)
        {
            this.webBrowser1.Document.ExecCommand("Abort", true, null);
        }
        /*
        private void BrowserForm_Activated(object sender, EventArgs e)
        {

            MenuStrip MainMenuStrip = this.MainMenuStrip;
            if (MainMenuStrip != null)
            {
                MainMenuStrip.Visible =
                    this.tableLayoutPanel1.Visible =
                    this.treeViewEx.Visible =
                    this.propertyGrid1.Visible =
                    true;
            }
        }

        private void BrowserForm_Deactivate(object sender, EventArgs e)
        {
            MenuStrip MainMenuStrip = this.MainMenuStrip;
            if (MainMenuStrip != null)
            {
                MainMenuStrip.Visible =
                    this.tableLayoutPanel1.Visible =
                    this.treeViewEx.Visible =
                    this.propertyGrid1.Visible =
                    false;
            }
        }*/

        private void bottom_splitter1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.propertyGrid1.Visible = !this.propertyGrid1.Visible;
        }

        private void vertical_splitter_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.treeViewEx.Visible =! this.treeViewEx.Visible;
        }





    }
}
