namespace WorkflowTrackingClient
{
    partial class SettingsForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.btnBrowseFolder = new System.Windows.Forms.Button();
            this.lblWFTrackingPath = new System.Windows.Forms.Label();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.chkDisplayTerminatedWF = new System.Windows.Forms.CheckBox();
            this.chkAutoWFChanges = new System.Windows.Forms.CheckBox();
            this.WFTrackingFolderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.LabeltoolStripStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "WF tracking path";
            // 
            // btnBrowseFolder
            // 
            this.btnBrowseFolder.Location = new System.Drawing.Point(543, 33);
            this.btnBrowseFolder.Name = "btnBrowseFolder";
            this.btnBrowseFolder.Size = new System.Drawing.Size(32, 20);
            this.btnBrowseFolder.TabIndex = 1;
            this.btnBrowseFolder.Text = "...";
            this.btnBrowseFolder.UseVisualStyleBackColor = true;
            this.btnBrowseFolder.Click += new System.EventHandler(this.btnBrowseFolder_Click);
            // 
            // lblWFTrackingPath
            // 
            this.lblWFTrackingPath.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblWFTrackingPath.Location = new System.Drawing.Point(15, 33);
            this.lblWFTrackingPath.Name = "lblWFTrackingPath";
            this.lblWFTrackingPath.Size = new System.Drawing.Size(531, 20);
            this.lblWFTrackingPath.TabIndex = 2;
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(417, 133);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 3;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(500, 133);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // chkDisplayTerminatedWF
            // 
            this.chkDisplayTerminatedWF.AutoSize = true;
            this.chkDisplayTerminatedWF.Location = new System.Drawing.Point(15, 73);
            this.chkDisplayTerminatedWF.Name = "chkDisplayTerminatedWF";
            this.chkDisplayTerminatedWF.Size = new System.Drawing.Size(134, 17);
            this.chkDisplayTerminatedWF.TabIndex = 0;
            this.chkDisplayTerminatedWF.Text = "Display Terninated WF";
            this.chkDisplayTerminatedWF.UseVisualStyleBackColor = true;
            // 
            // chkAutoWFChanges
            // 
            this.chkAutoWFChanges.AutoSize = true;
            this.chkAutoWFChanges.Location = new System.Drawing.Point(15, 96);
            this.chkAutoWFChanges.Name = "chkAutoWFChanges";
            this.chkAutoWFChanges.Size = new System.Drawing.Size(143, 17);
            this.chkAutoWFChanges.TabIndex = 2;
            this.chkAutoWFChanges.Text = "Auto WF change update";
            this.chkAutoWFChanges.UseVisualStyleBackColor = true;
            // 
            // WFTrackingFolderBrowserDialog
            // 
            this.WFTrackingFolderBrowserDialog.ShowNewFolderButton = false;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.LabeltoolStripStatus});
            this.statusStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.statusStrip1.Location = new System.Drawing.Point(0, 166);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.statusStrip1.Size = new System.Drawing.Size(590, 22);
            this.statusStrip1.SizingGrip = false;
            this.statusStrip1.Stretch = false;
            this.statusStrip1.TabIndex = 13;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // LabeltoolStripStatus
            // 
            this.LabeltoolStripStatus.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.LabeltoolStripStatus.Name = "LabeltoolStripStatus";
            this.LabeltoolStripStatus.Overflow = System.Windows.Forms.ToolStripItemOverflow.Never;
            this.LabeltoolStripStatus.Size = new System.Drawing.Size(16, 17);
            this.LabeltoolStripStatus.Spring = true;
            this.LabeltoolStripStatus.Text = "   ";
            this.LabeltoolStripStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.LabeltoolStripStatus.TextDirection = System.Windows.Forms.ToolStripTextDirection.Horizontal;
            // 
            // SettingsForm
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(590, 188);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.chkAutoWFChanges);
            this.Controls.Add(this.chkDisplayTerminatedWF);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.lblWFTrackingPath);
            this.Controls.Add(this.btnBrowseFolder);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SettingsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Settings";
            this.Load += new System.EventHandler(this.SettingsForm_Load);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnBrowseFolder;
        private System.Windows.Forms.Label lblWFTrackingPath;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.CheckBox chkDisplayTerminatedWF;
        private System.Windows.Forms.CheckBox chkAutoWFChanges;
        private System.Windows.Forms.FolderBrowserDialog WFTrackingFolderBrowserDialog;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel LabeltoolStripStatus;
    }
}