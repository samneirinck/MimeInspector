namespace MimeInspector
{
    partial class MimeView
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.messageTree = new System.Windows.Forms.TreeView();
            this.rawTab = new System.Windows.Forms.TabPage();
            this.tabs = new System.Windows.Forms.TabControl();
            this.xmlTab = new System.Windows.Forms.TabPage();
            this.headersTab = new System.Windows.Forms.TabPage();
            this.tabs.SuspendLayout();
            this.SuspendLayout();
            // 
            // messageTree
            // 
            this.messageTree.Dock = System.Windows.Forms.DockStyle.Left;
            this.messageTree.HideSelection = false;
            this.messageTree.Location = new System.Drawing.Point(0, 0);
            this.messageTree.Name = "messageTree";
            this.messageTree.Size = new System.Drawing.Size(121, 458);
            this.messageTree.TabIndex = 0;
            this.messageTree.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.OnNodeSelected);
            // 
            // rawTab
            // 
            this.rawTab.Location = new System.Drawing.Point(4, 22);
            this.rawTab.Name = "rawTab";
            this.rawTab.Padding = new System.Windows.Forms.Padding(3);
            this.rawTab.Size = new System.Drawing.Size(458, 432);
            this.rawTab.TabIndex = 0;
            this.rawTab.Text = "Raw";
            this.rawTab.UseVisualStyleBackColor = true;
            // 
            // tabs
            // 
            this.tabs.Controls.Add(this.rawTab);
            this.tabs.Controls.Add(this.xmlTab);
            this.tabs.Controls.Add(this.headersTab);
            this.tabs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabs.Location = new System.Drawing.Point(121, 0);
            this.tabs.Name = "tabs";
            this.tabs.SelectedIndex = 0;
            this.tabs.Size = new System.Drawing.Size(466, 458);
            this.tabs.TabIndex = 1;
            // 
            // xmlTab
            // 
            this.xmlTab.Location = new System.Drawing.Point(4, 22);
            this.xmlTab.Name = "xmlTab";
            this.xmlTab.Padding = new System.Windows.Forms.Padding(3);
            this.xmlTab.Size = new System.Drawing.Size(458, 432);
            this.xmlTab.TabIndex = 1;
            this.xmlTab.Text = "XML";
            this.xmlTab.UseVisualStyleBackColor = true;
            // 
            // headersTab
            // 
            this.headersTab.Location = new System.Drawing.Point(4, 22);
            this.headersTab.Name = "headersTab";
            this.headersTab.Padding = new System.Windows.Forms.Padding(3);
            this.headersTab.Size = new System.Drawing.Size(458, 432);
            this.headersTab.TabIndex = 2;
            this.headersTab.Text = "Headers";
            this.headersTab.UseVisualStyleBackColor = true;
            // 
            // MimeView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tabs);
            this.Controls.Add(this.messageTree);
            this.Name = "MimeView";
            this.Size = new System.Drawing.Size(587, 458);
            this.tabs.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView messageTree;
        private System.Windows.Forms.TabPage rawTab;
        private System.Windows.Forms.TabControl tabs;
        private System.Windows.Forms.TabPage xmlTab;
        private System.Windows.Forms.TabPage headersTab;
    }
}
