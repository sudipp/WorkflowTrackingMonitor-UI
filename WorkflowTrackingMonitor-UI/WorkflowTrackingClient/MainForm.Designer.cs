namespace WorkflowTrackingClient
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.WFSaveImageFile = new System.Windows.Forms.SaveFileDialog();
            this.workflowTrackingFilePathWatcher = new System.IO.FileSystemWatcher();
            this.pageSetupDialog1 = new System.Windows.Forms.PageSetupDialog();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.tabPageWfUpdates = new Crownwood.Magic.Controls.TabPage();
            this.txtWfUpdates = new System.Windows.Forms.TextBox();
            this.contextMenuStripWfUpdate = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.updateWfViewMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.WFTrackingFolderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.LabeltoolStripStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.WFZoomSizeDropDown = new System.Windows.Forms.ToolStripSplitButton();
            this.MainContainer = new System.Windows.Forms.Panel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.listViewWorkflows = new WorkflowTrackingClient.WfTrackingListView();
            this.workflowsIdHeader = new System.Windows.Forms.ColumnHeader();
            this.workflowsNameHeader = new System.Windows.Forms.ColumnHeader();
            this.workflowsDBName = new System.Windows.Forms.ColumnHeader();
            this.workflowsAllocationID = new System.Windows.Forms.ColumnHeader();
            this.workflowsStatusHeader = new System.Windows.Forms.ColumnHeader();
            this.workflowsLabel = new System.Windows.Forms.Label();
            this.lstActivities = new WorkflowTrackingClient.WfTrackingListView();
            this.ActivityId = new System.Windows.Forms.ColumnHeader();
            this.ActivityName = new System.Windows.Forms.ColumnHeader();
            this.ActivityStatus = new System.Windows.Forms.ColumnHeader();
            this.ActivityHost = new System.Windows.Forms.ColumnHeader();
            this.ActivityThread = new System.Windows.Forms.ColumnHeader();
            this.ActivityDate = new System.Windows.Forms.ColumnHeader();
            this.activitiesLabel = new System.Windows.Forms.Label();
            this.tabControl1 = new Crownwood.Magic.Controls.TabControl();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.tabPageWorkflow = new Crownwood.Magic.Controls.TabPage();
            this.picBoxLoading = new System.Windows.Forms.PictureBox();
            this.WFOrgPanel = new CheckBoxStudio.WinForms.OrgPanel();
            this.tabPageActivityDetails = new Crownwood.Magic.Controls.TabPage();
            this.WFActivityDetailsOrgPanel = new CheckBoxStudio.WinForms.OrgPanel();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.FileMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.FindMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SaveAsMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SaveWithCommentsMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.PrintMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.SettingsMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.WFSaveDetails = new System.Windows.Forms.SaveFileDialog();
            this.contextMenuStripActivityCopy = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ActivityCopyMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.workflowTrackingFilePathWatcher)).BeginInit();
            this.tabPageWfUpdates.SuspendLayout();
            this.contextMenuStripWfUpdate.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.MainContainer.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPageWorkflow.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picBoxLoading)).BeginInit();
            this.tabPageActivityDetails.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.contextMenuStripActivityCopy.SuspendLayout();
            this.SuspendLayout();
            // 
            // WFSaveImageFile
            // 
            this.WFSaveImageFile.Filter = "PNG files (*.png)|*.png";
            // 
            // workflowTrackingFilePathWatcher
            // 
            this.workflowTrackingFilePathWatcher.EnableRaisingEvents = true;
            this.workflowTrackingFilePathWatcher.Filter = "*.xml";
            this.workflowTrackingFilePathWatcher.SynchronizingObject = this;
            // 
            // toolTip1
            // 
            this.toolTip1.IsBalloon = true;
            this.toolTip1.ShowAlways = true;
            this.toolTip1.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.toolTip1.Popup += new System.Windows.Forms.PopupEventHandler(this.toolTip1_Popup);
            // 
            // tabPageWfUpdates
            // 
            this.tabPageWfUpdates.Controls.Add(this.txtWfUpdates);
            this.tabPageWfUpdates.ImageIndex = 6;
            this.tabPageWfUpdates.ImageList = this.imageList1;
            this.tabPageWfUpdates.Location = new System.Drawing.Point(0, 0);
            this.tabPageWfUpdates.Name = "tabPageWfUpdates";
            this.tabPageWfUpdates.Selected = false;
            this.tabPageWfUpdates.Size = new System.Drawing.Size(699, 460);
            this.tabPageWfUpdates.TabIndex = 2;
            this.tabPageWfUpdates.Title = "Wf Updates";
            // 
            // txtWfUpdates
            // 
            this.txtWfUpdates.AcceptsReturn = true;
            this.txtWfUpdates.BackColor = System.Drawing.Color.White;
            this.txtWfUpdates.ContextMenuStrip = this.contextMenuStripWfUpdate;
            this.txtWfUpdates.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtWfUpdates.Location = new System.Drawing.Point(0, 0);
            this.txtWfUpdates.Multiline = true;
            this.txtWfUpdates.Name = "txtWfUpdates";
            this.txtWfUpdates.ReadOnly = true;
            this.txtWfUpdates.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtWfUpdates.Size = new System.Drawing.Size(699, 460);
            this.txtWfUpdates.TabIndex = 9;
            // 
            // contextMenuStripWfUpdate
            // 
            this.contextMenuStripWfUpdate.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.updateWfViewMenuItem});
            this.contextMenuStripWfUpdate.Name = "contextMenuStripWfUpdate";
            this.contextMenuStripWfUpdate.Size = new System.Drawing.Size(165, 26);
            // 
            // updateWfViewMenuItem
            // 
            this.updateWfViewMenuItem.Name = "updateWfViewMenuItem";
            this.updateWfViewMenuItem.Size = new System.Drawing.Size(164, 22);
            this.updateWfViewMenuItem.Text = "Update WF View";
            this.updateWfViewMenuItem.Click += new System.EventHandler(this.updateWfViewMenuItem_Click);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "Closed.ico");
            this.imageList1.Images.SetKeyName(1, "Executing.ico");
            this.imageList1.Images.SetKeyName(2, "Error.ico");
            this.imageList1.Images.SetKeyName(3, "Unknown.ico");
            this.imageList1.Images.SetKeyName(4, "PastDue.ico");
            this.imageList1.Images.SetKeyName(5, "Mark4Rerun.ico");
            this.imageList1.Images.SetKeyName(6, "wfupdate.ico");
            this.imageList1.Images.SetKeyName(7, "wf.ico");
            this.imageList1.Images.SetKeyName(8, "Act_detail.ico");
            this.imageList1.Images.SetKeyName(9, "tracking.ico");
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            // 
            // WFTrackingFolderBrowserDialog
            // 
            this.WFTrackingFolderBrowserDialog.ShowNewFolderButton = false;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.LabeltoolStripStatus,
            this.WFZoomSizeDropDown});
            this.statusStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.statusStrip1.Location = new System.Drawing.Point(0, 509);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.statusStrip1.Size = new System.Drawing.Size(1065, 22);
            this.statusStrip1.Stretch = false;
            this.statusStrip1.TabIndex = 12;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // LabeltoolStripStatus
            // 
            this.LabeltoolStripStatus.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.LabeltoolStripStatus.Name = "LabeltoolStripStatus";
            this.LabeltoolStripStatus.Overflow = System.Windows.Forms.ToolStripItemOverflow.Never;
            this.LabeltoolStripStatus.Size = new System.Drawing.Size(62, 17);
            this.LabeltoolStripStatus.Spring = true;
            this.LabeltoolStripStatus.Text = "WF Monitor";
            this.LabeltoolStripStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.LabeltoolStripStatus.TextDirection = System.Windows.Forms.ToolStripTextDirection.Horizontal;
            // 
            // WFZoomSizeDropDown
            // 
            this.WFZoomSizeDropDown.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.WFZoomSizeDropDown.AutoSize = false;
            this.WFZoomSizeDropDown.Image = ((System.Drawing.Image)(resources.GetObject("WFZoomSizeDropDown.Image")));
            this.WFZoomSizeDropDown.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.WFZoomSizeDropDown.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.WFZoomSizeDropDown.Name = "WFZoomSizeDropDown";
            this.WFZoomSizeDropDown.Size = new System.Drawing.Size(80, 20);
            this.WFZoomSizeDropDown.ButtonClick += new System.EventHandler(this.WFZoomSizeDropDown_ButtonClick);
            this.WFZoomSizeDropDown.DropDownItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.ZoomSizeDropDown_ItemClicked);
            // 
            // MainContainer
            // 
            this.MainContainer.Controls.Add(this.splitContainer1);
            this.MainContainer.Controls.Add(this.menuStrip1);
            this.MainContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainContainer.Location = new System.Drawing.Point(0, 0);
            this.MainContainer.Name = "MainContainer";
            this.MainContainer.Size = new System.Drawing.Size(1065, 509);
            this.MainContainer.TabIndex = 13;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 24);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.splitContainer2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.tabControl1);
            this.splitContainer1.Size = new System.Drawing.Size(1065, 485);
            this.splitContainer1.SplitterDistance = 362;
            this.splitContainer1.TabIndex = 6;
            this.splitContainer1.TabStop = false;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.listViewWorkflows);
            this.splitContainer2.Panel1.Controls.Add(this.workflowsLabel);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.lstActivities);
            this.splitContainer2.Panel2.Controls.Add(this.activitiesLabel);
            this.splitContainer2.Size = new System.Drawing.Size(362, 485);
            this.splitContainer2.SplitterDistance = 136;
            this.splitContainer2.TabIndex = 0;
            this.splitContainer2.TabStop = false;
            // 
            // listViewWorkflows
            // 
            this.listViewWorkflows.Activation = System.Windows.Forms.ItemActivation.OneClick;
            this.listViewWorkflows.AllowColumnReorder = true;
            this.listViewWorkflows.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.workflowsIdHeader,
            this.workflowsNameHeader,
            this.workflowsDBName,
            this.workflowsAllocationID,
            this.workflowsStatusHeader});
            this.listViewWorkflows.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listViewWorkflows.FullRowSelect = true;
            this.listViewWorkflows.GridLines = true;
            this.listViewWorkflows.HideSelection = false;
            this.listViewWorkflows.LabelWrap = false;
            this.listViewWorkflows.Location = new System.Drawing.Point(0, 15);
            this.listViewWorkflows.Margin = new System.Windows.Forms.Padding(2, 3, 3, 3);
            this.listViewWorkflows.MultiSelect = false;
            this.listViewWorkflows.Name = "listViewWorkflows";
            this.listViewWorkflows.ShowGroups = false;
            this.listViewWorkflows.ShowItemToolTips = true;
            this.listViewWorkflows.Size = new System.Drawing.Size(362, 121);
            this.listViewWorkflows.TabIndex = 5;
            this.listViewWorkflows.UseCompatibleStateImageBehavior = false;
            this.listViewWorkflows.View = System.Windows.Forms.View.Details;
            this.listViewWorkflows.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.listViewWorkflows_ItemSelectionChanged);
            // 
            // workflowsIdHeader
            // 
            this.workflowsIdHeader.Text = "Id";
            this.workflowsIdHeader.Width = 35;
            // 
            // workflowsNameHeader
            // 
            this.workflowsNameHeader.Text = "Name";
            this.workflowsNameHeader.Width = 144;
            // 
            // workflowsDBName
            // 
            this.workflowsDBName.Text = "DB";
            // 
            // workflowsAllocationID
            // 
            this.workflowsAllocationID.Text = "SetupID";
            this.workflowsAllocationID.Width = 56;
            // 
            // workflowsStatusHeader
            // 
            this.workflowsStatusHeader.Text = "Status";
            // 
            // workflowsLabel
            // 
            this.workflowsLabel.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.workflowsLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this.workflowsLabel.Location = new System.Drawing.Point(0, 0);
            this.workflowsLabel.Name = "workflowsLabel";
            this.workflowsLabel.Size = new System.Drawing.Size(362, 15);
            this.workflowsLabel.TabIndex = 4;
            this.workflowsLabel.Text = "Workflows";
            // 
            // lstActivities
            // 
            this.lstActivities.Activation = System.Windows.Forms.ItemActivation.OneClick;
            this.lstActivities.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.ActivityId,
            this.ActivityName,
            this.ActivityStatus,
            this.ActivityHost,
            this.ActivityThread,
            this.ActivityDate});
            this.lstActivities.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstActivities.FullRowSelect = true;
            this.lstActivities.Location = new System.Drawing.Point(0, 15);
            this.lstActivities.MultiSelect = false;
            this.lstActivities.Name = "lstActivities";
            this.lstActivities.ShowItemToolTips = true;
            this.lstActivities.Size = new System.Drawing.Size(362, 330);
            this.lstActivities.TabIndex = 3;
            this.lstActivities.UseCompatibleStateImageBehavior = false;
            this.lstActivities.View = System.Windows.Forms.View.Details;
            this.lstActivities.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.lstActivities_ItemSelectionChanged);
            // 
            // ActivityId
            // 
            this.ActivityId.Text = "Id";
            this.ActivityId.Width = 36;
            // 
            // ActivityName
            // 
            this.ActivityName.Text = "Name";
            this.ActivityName.Width = 155;
            // 
            // ActivityStatus
            // 
            this.ActivityStatus.Text = "Status";
            this.ActivityStatus.Width = 65;
            // 
            // ActivityHost
            // 
            this.ActivityHost.Text = "Host";
            // 
            // ActivityThread
            // 
            this.ActivityThread.Text = "Thread";
            this.ActivityThread.Width = 50;
            // 
            // ActivityDate
            // 
            this.ActivityDate.Text = "Date";
            this.ActivityDate.Width = 121;
            // 
            // activitiesLabel
            // 
            this.activitiesLabel.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.activitiesLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this.activitiesLabel.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.activitiesLabel.Location = new System.Drawing.Point(0, 0);
            this.activitiesLabel.Name = "activitiesLabel";
            this.activitiesLabel.Size = new System.Drawing.Size(362, 15);
            this.activitiesLabel.TabIndex = 2;
            this.activitiesLabel.Text = "Activities";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.textBox1);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.HideTabsMode = Crownwood.Magic.Controls.TabControl.HideTabsModes.ShowAlways;
            this.tabControl1.HotTrack = true;
            this.tabControl1.ImageList = this.imageList1;
            this.tabControl1.InsetBorderPagesOnly = true;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.PositionTop = true;
            this.tabControl1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.SelectedTab = this.tabPageWorkflow;
            this.tabControl1.Size = new System.Drawing.Size(699, 485);
            this.tabControl1.TabIndex = 4;
            this.tabControl1.TabPages.AddRange(new Crownwood.Magic.Controls.TabPage[] {
            this.tabPageWorkflow,
            this.tabPageWfUpdates,
            this.tabPageActivityDetails});
            this.tabControl1.TabStop = false;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(80, 3);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(10, 21);
            this.textBox1.TabIndex = 4;
            this.textBox1.TabStop = false;
            this.textBox1.Visible = false;
            // 
            // tabPageWorkflow
            // 
            this.tabPageWorkflow.Controls.Add(this.picBoxLoading);
            this.tabPageWorkflow.Controls.Add(this.WFOrgPanel);
            this.tabPageWorkflow.ImageIndex = 7;
            this.tabPageWorkflow.Location = new System.Drawing.Point(0, 0);
            this.tabPageWorkflow.Name = "tabPageWorkflow";
            this.tabPageWorkflow.Size = new System.Drawing.Size(699, 460);
            this.tabPageWorkflow.TabIndex = 0;
            this.tabPageWorkflow.Title = "Workflow";
            // 
            // picBoxLoading
            // 
            this.picBoxLoading.BackColor = System.Drawing.SystemColors.ControlLight;
            this.picBoxLoading.Dock = System.Windows.Forms.DockStyle.Fill;
            this.picBoxLoading.Image = ((System.Drawing.Image)(resources.GetObject("picBoxLoading.Image")));
            this.picBoxLoading.Location = new System.Drawing.Point(0, 0);
            this.picBoxLoading.Margin = new System.Windows.Forms.Padding(4);
            this.picBoxLoading.Name = "picBoxLoading";
            this.picBoxLoading.Size = new System.Drawing.Size(699, 460);
            this.picBoxLoading.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.picBoxLoading.TabIndex = 4;
            this.picBoxLoading.TabStop = false;
            // 
            // WFOrgPanel
            // 
            this.WFOrgPanel.AutoScroll = true;
            this.WFOrgPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.WFOrgPanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.WFOrgPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.WFOrgPanel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.WFOrgPanel.Location = new System.Drawing.Point(0, 0);
            this.WFOrgPanel.Name = "WFOrgPanel";
            this.WFOrgPanel.ParentSpacing = 9;
            this.WFOrgPanel.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.WFOrgPanel.ShowPlusMinus = false;
            this.WFOrgPanel.SiblingSpacing = 7;
            this.WFOrgPanel.Size = new System.Drawing.Size(699, 460);
            this.WFOrgPanel.TabIndex = 2;
            this.WFOrgPanel.TabStop = true;
            // 
            // tabPageActivityDetails
            // 
            this.tabPageActivityDetails.Controls.Add(this.WFActivityDetailsOrgPanel);
            this.tabPageActivityDetails.ImageIndex = 8;
            this.tabPageActivityDetails.Location = new System.Drawing.Point(0, 0);
            this.tabPageActivityDetails.Name = "tabPageActivityDetails";
            this.tabPageActivityDetails.Selected = false;
            this.tabPageActivityDetails.Size = new System.Drawing.Size(699, 460);
            this.tabPageActivityDetails.TabIndex = 1;
            this.tabPageActivityDetails.Title = "[Activity Details]";
            // 
            // WFActivityDetailsOrgPanel
            // 
            this.WFActivityDetailsOrgPanel.AutoScroll = true;
            this.WFActivityDetailsOrgPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.WFActivityDetailsOrgPanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.WFActivityDetailsOrgPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.WFActivityDetailsOrgPanel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.WFActivityDetailsOrgPanel.Location = new System.Drawing.Point(0, 0);
            this.WFActivityDetailsOrgPanel.Name = "WFActivityDetailsOrgPanel";
            this.WFActivityDetailsOrgPanel.ParentSpacing = 9;
            this.WFActivityDetailsOrgPanel.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.WFActivityDetailsOrgPanel.ShowPlusMinus = false;
            this.WFActivityDetailsOrgPanel.SiblingSpacing = 7;
            this.WFActivityDetailsOrgPanel.Size = new System.Drawing.Size(699, 460);
            this.WFActivityDetailsOrgPanel.TabIndex = 3;
            this.WFActivityDetailsOrgPanel.TabStop = true;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.FileMenuItem,
            this.toolStripMenuItem1});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1065, 24);
            this.menuStrip1.TabIndex = 5;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // FileMenuItem
            // 
            this.FileMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.FindMenuItem,
            this.SaveAsMenuItem,
            this.SaveWithCommentsMenuItem,
            this.PrintMenuItem});
            this.FileMenuItem.Name = "FileMenuItem";
            this.FileMenuItem.Size = new System.Drawing.Size(35, 20);
            this.FileMenuItem.Text = "File";
            // 
            // FindMenuItem
            // 
            this.FindMenuItem.Name = "FindMenuItem";
            this.FindMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F)));
            this.FindMenuItem.Size = new System.Drawing.Size(206, 22);
            this.FindMenuItem.Text = "Find";
            this.FindMenuItem.Click += new System.EventHandler(this.FindMenuItem_Click);
            // 
            // SaveAsMenuItem
            // 
            this.SaveAsMenuItem.Name = "SaveAsMenuItem";
            this.SaveAsMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.SaveAsMenuItem.Size = new System.Drawing.Size(206, 22);
            this.SaveAsMenuItem.Text = "Save As Image";
            this.SaveAsMenuItem.Click += new System.EventHandler(this.SaveAsMenuItem_Click);
            // 
            // SaveWithCommentsMenuItem
            // 
            this.SaveWithCommentsMenuItem.AccessibleDescription = "";
            this.SaveWithCommentsMenuItem.Name = "SaveWithCommentsMenuItem";
            this.SaveWithCommentsMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D)));
            this.SaveWithCommentsMenuItem.Size = new System.Drawing.Size(206, 22);
            this.SaveWithCommentsMenuItem.Text = "Save with Details";
            this.SaveWithCommentsMenuItem.Click += new System.EventHandler(this.SaveWithCommentsMenuItem_Click);
            // 
            // PrintMenuItem
            // 
            this.PrintMenuItem.Name = "PrintMenuItem";
            this.PrintMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.P)));
            this.PrintMenuItem.Size = new System.Drawing.Size(206, 22);
            this.PrintMenuItem.Text = "Print";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.SettingsMenuItem});
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(44, 20);
            this.toolStripMenuItem1.Text = "Tools";
            // 
            // SettingsMenuItem
            // 
            this.SettingsMenuItem.Name = "SettingsMenuItem";
            this.SettingsMenuItem.Size = new System.Drawing.Size(124, 22);
            this.SettingsMenuItem.Text = "Settings";
            this.SettingsMenuItem.Click += new System.EventHandler(this.SettingsMenuItem_Click);
            // 
            // WFSaveDetails
            // 
            this.WFSaveDetails.Filter = "Text files (*.txt)|*.txt";
            // 
            // contextMenuStripActivityCopy
            // 
            this.contextMenuStripActivityCopy.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ActivityCopyMenuItem});
            this.contextMenuStripActivityCopy.Name = "contextMenuStripWfUpdate";
            this.contextMenuStripActivityCopy.Size = new System.Drawing.Size(146, 26);
            this.contextMenuStripActivityCopy.Click += new System.EventHandler(this.contextMenuStripActivityCopy_Click);
            // 
            // ActivityCopyMenuItem
            // 
            this.ActivityCopyMenuItem.Name = "ActivityCopyMenuItem";
            this.ActivityCopyMenuItem.Size = new System.Drawing.Size(145, 22);
            this.ActivityCopyMenuItem.Text = "Copy Details";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1065, 531);
            this.Controls.Add(this.MainContainer);
            this.Controls.Add(this.statusStrip1);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "WF Monitor";
            this.Load += new System.EventHandler(this.MainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.workflowTrackingFilePathWatcher)).EndInit();
            this.tabPageWfUpdates.ResumeLayout(false);
            this.tabPageWfUpdates.PerformLayout();
            this.contextMenuStripWfUpdate.ResumeLayout(false);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.MainContainer.ResumeLayout(false);
            this.MainContainer.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabControl1.PerformLayout();
            this.tabPageWorkflow.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picBoxLoading)).EndInit();
            this.tabPageActivityDetails.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.contextMenuStripActivityCopy.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.SaveFileDialog WFSaveImageFile;
        private System.IO.FileSystemWatcher workflowTrackingFilePathWatcher;
        private System.Windows.Forms.PageSetupDialog pageSetupDialog1;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.FolderBrowserDialog WFTrackingFolderBrowserDialog;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel LabeltoolStripStatus;
        private System.Windows.Forms.ToolStripSplitButton WFZoomSizeDropDown;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripWfUpdate;
        private System.Windows.Forms.ToolStripMenuItem updateWfViewMenuItem;
        private System.Windows.Forms.Panel MainContainer;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        //private System.Windows.Forms.ListView listViewWorkflows;
        private WfTrackingListView listViewWorkflows;
        
        private System.Windows.Forms.ColumnHeader workflowsIdHeader;
        private System.Windows.Forms.ColumnHeader workflowsNameHeader;
        private System.Windows.Forms.ColumnHeader workflowsAllocationID;
        private System.Windows.Forms.ColumnHeader workflowsStatusHeader;
        private System.Windows.Forms.Label workflowsLabel;
        private WfTrackingListView lstActivities;
        private System.Windows.Forms.ColumnHeader ActivityId;
        private System.Windows.Forms.ColumnHeader ActivityName;
        private System.Windows.Forms.ColumnHeader ActivityStatus;
        private System.Windows.Forms.ColumnHeader ActivityDate;
        private System.Windows.Forms.Label activitiesLabel;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem FileMenuItem;
        private System.Windows.Forms.ToolStripMenuItem FindMenuItem;
        private System.Windows.Forms.ToolStripMenuItem SaveAsMenuItem;
        private System.Windows.Forms.ToolStripMenuItem PrintMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem SettingsMenuItem;
        private Crownwood.Magic.Controls.TabControl tabControl1;
        private Crownwood.Magic.Controls.TabPage tabPageWorkflow;
        private Crownwood.Magic.Controls.TabPage tabPageActivityDetails;
        private CheckBoxStudio.WinForms.OrgPanel WFOrgPanel;
        private System.Windows.Forms.PictureBox picBoxLoading;
        private Crownwood.Magic.Controls.TabPage tabPageWfUpdates;
        private System.Windows.Forms.TextBox txtWfUpdates;
        private System.Windows.Forms.TextBox textBox1;
        private CheckBoxStudio.WinForms.OrgPanel WFActivityDetailsOrgPanel;
        private System.Windows.Forms.ColumnHeader workflowsDBName;
        private System.Windows.Forms.ToolStripMenuItem SaveWithCommentsMenuItem;
        private System.Windows.Forms.SaveFileDialog WFSaveDetails;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripActivityCopy;
        private System.Windows.Forms.ToolStripMenuItem ActivityCopyMenuItem;
        private System.Windows.Forms.ColumnHeader ActivityHost;
        private System.Windows.Forms.ColumnHeader ActivityThread;



    }
}

