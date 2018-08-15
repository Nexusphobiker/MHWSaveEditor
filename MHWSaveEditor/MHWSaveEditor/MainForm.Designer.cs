namespace MHWSaveEditor
{
    partial class MainForm
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButton_Load = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton_Save = new System.Windows.Forms.ToolStripButton();
            this.consoleGroupBox = new System.Windows.Forms.GroupBox();
            this.toolStripButton_About = new System.Windows.Forms.ToolStripButton();
            this.progressBar_MainForm = new System.Windows.Forms.ProgressBar();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton_Load,
            this.toolStripButton_Save,
            this.toolStripButton_About});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1327, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "MainForm_ToolStrip";
            // 
            // toolStripButton_Load
            // 
            this.toolStripButton_Load.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButton_Load.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton_Load.Image")));
            this.toolStripButton_Load.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton_Load.Name = "toolStripButton_Load";
            this.toolStripButton_Load.Size = new System.Drawing.Size(37, 22);
            this.toolStripButton_Load.Text = "Load";
            this.toolStripButton_Load.Click += new System.EventHandler(this.toolStripButton_Load_Click);
            // 
            // toolStripButton_Save
            // 
            this.toolStripButton_Save.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButton_Save.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton_Save.Image")));
            this.toolStripButton_Save.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton_Save.Name = "toolStripButton_Save";
            this.toolStripButton_Save.Size = new System.Drawing.Size(35, 22);
            this.toolStripButton_Save.Text = "Save";
            this.toolStripButton_Save.Click += new System.EventHandler(this.toolStripButton_Save_Click);
            // 
            // consoleGroupBox
            // 
            this.consoleGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.consoleGroupBox.Location = new System.Drawing.Point(12, 415);
            this.consoleGroupBox.Name = "consoleGroupBox";
            this.consoleGroupBox.Size = new System.Drawing.Size(1303, 162);
            this.consoleGroupBox.TabIndex = 1;
            this.consoleGroupBox.TabStop = false;
            // 
            // toolStripButton_About
            // 
            this.toolStripButton_About.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButton_About.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton_About.Image")));
            this.toolStripButton_About.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton_About.Name = "toolStripButton_About";
            this.toolStripButton_About.Size = new System.Drawing.Size(44, 22);
            this.toolStripButton_About.Text = "About";
            this.toolStripButton_About.Click += new System.EventHandler(this.toolStripButton_About_Click);
            // 
            // progressBar_MainForm
            // 
            this.progressBar_MainForm.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBar_MainForm.Location = new System.Drawing.Point(12, 584);
            this.progressBar_MainForm.Name = "progressBar_MainForm";
            this.progressBar_MainForm.Size = new System.Drawing.Size(1303, 23);
            this.progressBar_MainForm.TabIndex = 2;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1327, 626);
            this.Controls.Add(this.progressBar_MainForm);
            this.Controls.Add(this.consoleGroupBox);
            this.Controls.Add(this.toolStrip1);
            this.Name = "MainForm";
            this.Text = "MHW Save Editor by Nexusphobiker";
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripButton_Load;
        private System.Windows.Forms.ToolStripButton toolStripButton_Save;
        private System.Windows.Forms.GroupBox consoleGroupBox;
        private System.Windows.Forms.ToolStripButton toolStripButton_About;
        private System.Windows.Forms.ProgressBar progressBar_MainForm;
    }
}