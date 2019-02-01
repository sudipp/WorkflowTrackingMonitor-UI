using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Workflow.ComponentModel;
using System.Workflow.Runtime;
using System.Xml;
using Crownwood.Magic.Common;
using XMLFileTracking;
using CheckBoxStudio.WinForms;
using System.Diagnostics;
using System.Drawing.Imaging;
using Crownwood.Magic.Docking;
using Crownwood.Magic.Menus;
using System.Text;

namespace WorkflowTrackingClient
{
    public partial class MainForm : Form
    {
        private EditBalloon m_eb = new EditBalloon();
        
        readonly Regex _regexTrimActName = new Regex(@".*\.");
        private readonly Regex _regexExtractParentActName = new Regex(@"^[^.]*(?=\.)");//@".*(?=\.)");
        
        readonly Regex rxParentName = new Regex(@"^[^.]*");


        /// <summary>
        /// Loaded Last activity Order
        /// </summary>
        private int _loadedLastActivityOrder;

        /// <summary>
        /// Current scroll pos for WFOrgPanel
        /// </summary>
        private System.Drawing.Point _wfOrgPanelCurrentScrollPoint;

        /// <summary>
        /// Prop to store selected WF node
        /// </summary>
        public Control SelectedWfNode { set; get; }

        /// <summary>
        /// Pointer to selected wf (by user) on view
        /// </summary>
        public int SelectedWfIndex = -1;

        /// <summary>
        /// Pointer to selected wf (by user) on view
        /// </summary>
        public string SelectedWfGuid = string.Empty;

        /// <summary>
        /// application settings
        /// </summary>
        private readonly ApplicationSettings _monitorSettingsValue;
        private XMLTrackingQueryManager _queryManager;
        /// <summary>
        /// list of available workflows
        /// </summary>
        IList<WorkFlowInfo> availableWorkflows = new List<WorkFlowInfo>();

        private delegate void AddNodeToParentNodeCollectionDelegate(
            XmlNode currentNode, Control currentOrgNode,
            OrgNodeCollection parentNodeColl);

        /// <summary>
        /// Activity list
        /// </summary>
        public ListView ListActivities
        {
            get { return this.lstActivities; }
        }

        public MainForm()
        {
            InitializeComponent();

            m_eb.Title = "Live Update";
            m_eb.TitleIcon = TooltipIcon.Info;
            m_eb.Text = "New workflow updates are available.";
            m_eb.Parent = textBox1;

            SelectWfTab();

            //Hide loading icon
            ShowHideLoadingIpIcon(false);

            //Initializing Settings
            _monitorSettingsValue = new ApplicationSettings();

            //Prepare Zoom dropdown
            LoadZoomSizeDropDown();

            //Retrieve default font size of panel
            _mDesignFontSize = WFOrgPanel.Font.SizeInPoints;

            //Setting WFTrackingFolderBrowser Dialog
            WFTrackingFolderBrowserDialog.ShowNewFolderButton = false;
            WFTrackingFolderBrowserDialog.RootFolder = System.Environment.SpecialFolder.Desktop;
            WFTrackingFolderBrowserDialog.Description = "Select WF tracking path";
        }

        /// <summary>
        /// Shows/hide loading in progress icon
        /// </summary>
        /// <param name="show"></param>
        private void ShowHideLoadingIpIcon(bool show)
        {
            picBoxLoading.UIThread(delegate
            {
                picBoxLoading.Visible = show;
            });
        }

        private void toolTip1_Popup(object sender, PopupEventArgs e)
        {
            if (e.AssociatedControl is OrgPanel)
                toolTip1.ToolTipTitle = ((OrgPanel)e.AssociatedControl).Text;
            if (e.AssociatedControl is Label)
                toolTip1.ToolTipTitle = ((Label)e.AssociatedControl).Text;
        }

        /// <summary> 
        /// loads WF Tracking Folder Path, from settings
        /// </summary>
        private void GetWfTrackingFolderPathSetting()
        {
            if (!_monitorSettingsValue.LoadAppSettings(Application.LocalUserAppDataPath + @"\wfmonitorClient.config"))
            {
                ShowSettingsDialog();
            }
        }

        private void LoadWorkflowFromTrackingPath()
        {
            if (string.IsNullOrEmpty(_monitorSettingsValue.WFTrackingFolderPath))
            {
                Application.Exit();
                return;
            }


            if (_queryManager==null)
                _queryManager = new XMLTrackingQueryManager();
            IList<FileInfo> trackingFiles = _queryManager.GetTrackingFiles(_monitorSettingsValue.WFTrackingFolderPath);//trackingFilePath);

            availableWorkflows.Clear();
            listViewWorkflows.Items.Clear();
            lstActivities.Items.Clear();
            WFOrgPanel.Nodes.Clear();

            //Select default Zoom =100%
            ToolStripMenuItem defaultSelected = (ToolStripMenuItem)WFZoomSizeDropDown.DropDown.Items.Find("100", true).First();
            HighlightSelectedZoomSize(defaultSelected);
            

            for (int x = 0; x < trackingFiles.Count; x++)
            {
                WorkFlowInfo wInfo = _queryManager.GetWorkflow(trackingFiles[x].FullName);

                //we will filter, if user doent want terminated WFs to be displayed
                if (!_monitorSettingsValue.DisplayTerminatedWF 
                    && wInfo.WfStatus == WorkflowStatus.Terminated)
                {
                    continue;
                }

                availableWorkflows.Add(wInfo);

                listViewWorkflows.Items.Add(
                    new ListViewItem(new string[] { availableWorkflows.Count().ToString(), wInfo.InstanceGuid, wInfo.WfPersistanceDb, "0", wInfo.WfStatus.ToString() })
                    );
            }

            //Selecting previous selected WF from listViewWorkflows
            if(!string.IsNullOrEmpty(SelectedWfGuid))
            {
                ListViewItem item = listViewWorkflows.FindItemWithText(SelectedWfGuid, true, 0, false);
                if (item != null)
                    item.Selected = true;
            }

            UpdateStatusText("Loaded " + listViewWorkflows.Items.Count + " workflow instances");
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            //Getting tracking file path
            GetWfTrackingFolderPathSetting();
            
            // if there is no path selected by user, then terminate the app
            if (string.IsNullOrEmpty(_monitorSettingsValue.WFTrackingFolderPath))
            {
                Application.Exit();
                return;
            }

            //If path loaded from saved setting, then load all WFs
            //if(!_monitorSettingsValue.ApplicationSettingsChanged)
           
            LoadWorkflowFromTrackingPath();
        }
        
        /// <summary>
        /// Select workflow tab
        /// </summary>
        private void SelectWfTab()
        {
            tabControl1.TabPages[0].Selected = true;
        }

        private void listViewWorkflows_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            if(e.IsSelected){
                //if background worker is free
                if(!backgroundWorker1.IsBusy)
                {
                    //Reset
                    _loadedLastActivityOrder = 0;

                    //WorkFlowInfo wInfo = availableWorkflows[e.ItemIndex];
                    //int lastOrder = wInfo.TrackingRecords.Last().Order;
                    //_loadedLastActivityOrder = wInfo.TrackingRecords.Last().Order;
                    
                    SelectedWfIndex=e.ItemIndex;

                    SelectedWfGuid = e.Item.SubItems[1].Text;

                    LoadWfActivityDetail(e.ItemIndex, true);

                    //Clearing any pending wf update
                    txtWfUpdates.Clear();

                    SelectWfTab();
                }
            }
        }
        
        private void lstActivities_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            if (e.IsSelected)
            {
                string selectedActName = e.Item.SubItems[1].Text;

                SelectedWfNode = WfDefinitionHelper.GetWfActivityControlByName(WFOrgPanel, selectedActName);
                if (SelectedWfNode != null)
                {
                    SelectedWfNode.Select();

                    //Force refresh
                    WFOrgPanel.Refresh();

                    if (!tabControl1.TabPages[0].Selected)
                        tabControl1.TabPages[0].Selected = true;
                }
                else
                {

                    //If ther selected job is child of Apex Custom job
                    //ex: CompleteProcessGroup.<<ExecuteTask>>
                    //Check for . inside the string

                    if (_regexExtractParentActName.Match(selectedActName).Success)
                    {
                        //Select the parent activity ex =CompleteProcessGroup
                        string containerActivity = _regexExtractParentActName.Match(selectedActName).Value;
                        SelectedWfNode = WfDefinitionHelper.GetWfActivityControlByName(WFOrgPanel, containerActivity);

                        //SelectedWfNode.
                        LoadChildActivityDetails(SelectedWfNode);

                        //double click the selected parent activity
                        //select the original selected child activity on 2 tab
                        SelectedWfNode = WfDefinitionHelper.GetWfActivityControlByName(WFActivityDetailsOrgPanel,
                            selectedActName);

                    }
                }
            }
        }

        private void UpdateStatusText(string statusText)
        {
            statusStrip1.UIThread(delegate
                {
                    ToolStripItem ti = statusStrip1.Items.Find("LabeltoolStripStatus", false).First();
                    if (ti != null)
                    {
                        ti.Text = statusText;
                    }
                }
            );
        }
        
        #region buildWf

        /// <summary>
        /// Loads workflow acvitity details, for specific workflow
        /// </summary>
        /// <param name="selectedItemIndex">selected index of availableWorkflows list</param>
        /// <param name="updateWfDefination">If True, if we need to Load, Wf definition container</param>
        private void LoadWfActivityDetail(int selectedItemIndex,
            bool updateWfDefination)
        {
            WorkFlowInfo wInfo = availableWorkflows[selectedItemIndex];

            //foreach (var data in wInfo.TrackingRecords)
            //for (int x = _loadedLastActivityOrder; x < wInfo.TrackingRecords.Count; x++)
            StringBuilder sb = new StringBuilder();

            //int lastOrder=wInfo.TrackingRecords.Last().Order;
            //for (int x = _loadedLastActivityOrder; x < lastOrder; x++)
            for (int x = _loadedLastActivityOrder; x < wInfo.TrackingRecords.Count; x++)
            {
                var data = wInfo.TrackingRecords[x];
                if (data is ActivityTrackRecord)
                {
                    ActivityTrackRecord record = (ActivityTrackRecord)data;
                    lstActivities.Items.Add(
                        new ListViewItem(new string[] { record.Order.ToString(), record.Name, record.WfStatus.ToString(), record.WfHost,record.ThreadId.ToString(), record.DateTime.ToString() 
                        }));
                }
                else if (data is WorkFlowTrackRecord)
                {
                    WorkFlowTrackRecord record = (WorkFlowTrackRecord)data;
                    ListViewItem wfAct =
                        new ListViewItem(new string[]
                                             {
                                                 record.Order.ToString(), wInfo.InstanceGuid, record.WfStatus.ToString(), record.WfHost,record.ThreadId.ToString(), record.DateTime.ToString()
                                             });
                    wfAct.BackColor = SystemColors.ActiveBorder;
                    wfAct.ToolTipText = "Workflow Activity";

                    lstActivities.Items.Add(wfAct);
                }

                if (_loadedLastActivityOrder>0)
                    sb.AppendLine(wInfo.TrackingRecords[x].ToString());
            }
            
            if (!string.IsNullOrEmpty(sb.ToString()))
            {
                txtWfUpdates.Text += sb.ToString();

                m_eb.Text = "There are " + (wInfo.TrackingRecords.Count - _loadedLastActivityOrder) + " workflow update(s) available.";
                m_eb.Show();

                //Auto WF Change Upadate
                if(_monitorSettingsValue.AutoWFChangeUpadate)
                {
                    updateWfViewMenuItem_Click(this, EventArgs.Empty);
                }
            }

            lstActivities.Update();

            //Storing last updated activityRecord Index
            //_loadedLastActivityOrder = wInfo.TrackingRecords.Last().Order;
            _loadedLastActivityOrder = wInfo.TrackingRecords.Count();

            if (updateWfDefination)
                backgroundWorker1.RunWorkerAsync(wInfo);
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            WorkFlowInfo wInfo = (WorkFlowInfo)e.Argument;

            //Load Workflow definition
            LoadWfDefinition(wInfo);
        }

        private NewOrgPanel CreateOrgPanel(string label)
        {
            NewOrgPanel orgPanel = new NewOrgPanel(label);

            //orgPanel.BackColor = Color.Transparent;

            orgPanel.ShowRootLines = false;
            orgPanel.ParentSpacing = 6;
            orgPanel.AutoSize = true;
            orgPanel.TabStop = true;
            orgPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;

            orgPanel.BeforeExpand += OrgPanelBeforeExpandCollapse;
            orgPanel.BeforeCollapse += OrgPanelBeforeExpandCollapse;

            orgPanel.Resize += new EventHandler(orgPanel_Resize);

            return orgPanel;
        }

        void orgPanel_Resize(object sender, EventArgs e)
        {
            Control cont = (Control)sender;
            foreach (Control child in cont.Controls)
            {
                child.Font = GetScaledFont(child.Font, child);
            }
        }

        static void OrgPanelBeforeExpandCollapse(object sender, OrgPanelCancelEventArgs e)
        {
            e.Cancel = true;
        }


        private void AddNodeToParentNodeCollection(XmlNode currentNode, Control currentOrgNode,
            OrgNodeCollection parentNodeColl)
        {
            if (this.InvokeRequired)
            {
                AddNodeToParentNodeCollectionDelegate delInstatnce =
                    new AddNodeToParentNodeCollectionDelegate(AddNodeToParentNodeCollection);

                this.Invoke(delInstatnce, currentNode, currentOrgNode, parentNodeColl);
                return;
            }

            //If parent is parallel activity, add it the node to parent's node collection
            if (WfDefinitionHelper.IfParentParallelActivityType(currentNode))
            {
                parentNodeColl.Add(currentOrgNode);
                parentNodeColl.Last().ShadowVisible = false;
            }
            else
            {
                if (parentNodeColl.Count > 0)
                {
                    parentNodeColl.Last().Nodes.Add(currentOrgNode);
                    parentNodeColl.Last().Nodes.Last().ShadowVisible = false;
                }
                else
                {
                    parentNodeColl.Add(currentOrgNode);
                    parentNodeColl.Last().ShadowVisible = false;
                }
            }
        }

        /// <summary>
        /// therad safe listViewWorkflows enable/disable
        /// </summary>
        /// <param name="enabled"></param>
        private void ControlListViewWorkflows(bool enabled)
        {
            listViewWorkflows.UIThread(delegate
            {
                listViewWorkflows.Enabled = Enabled;
            });

            Cursor cursor = (enabled) ? Cursors.Default :
            Cursors.WaitCursor;
            this.UIThread(delegate
            {
                this.Cursor = cursor;
            });
        }


        /// <summary>
        /// builds WF view
        /// </summary>
        /// <param name="wInfo"></param>
        /// <param name="currentNode">current xml node</param>
        /// <param name="parentNodeColl">parent node collection, where we will add new node</param>
        /// <param name="parseApexCustomActivityChilds">determines, we will process ApexCustomActivity childs</param>
        private void BuildWfActivityOrgNode(WorkFlowInfo wInfo,
            XmlNode currentNode, OrgNodeCollection parentNodeColl, bool parseApexCustomActivityChilds)
        {
            //IEnumerable<ActivityTrackRecord> trackRecords =
            //        wInfo.ActivityRecords.Where(actrec => actrec.Name
            //            == currentNode.Attributes["QualifiedName"].Value);

            /*
            IEnumerable<ActivityTrackRecord> trackRecords =
                    wInfo.ActivityRecords.Where(actrec => actrec.Name
                        == currentNode.Attributes["QualifiedName"].Value).
                        Concat(
                            wInfo.ActivityRecords.Where(actrec => actrec.Name.IndexOf(currentNode.Attributes["QualifiedName"].Value) > -1
                            && actrec.Type == "UpdateTaskStatus").Reverse().Take(1) //the last one
                        );
            */

            /*IEnumerable<ActivityTrackRecord> trackRecords =
                    wInfo.ActivityRecords.Where(actrec => actrec.Name
                        == currentNode.Attributes["QualifiedName"].Value).
                        Concat(
                            wInfo.ActivityRecords.Where(actrec => (actrec.Name.IndexOf(currentNode.Attributes["QualifiedName"].Value +".") > -1 
                                || actrec.Name.Equals(currentNode.Attributes["QualifiedName"].Value)
                                )
                            && actrec.Type == "UpdateTaskStatus" && actrec.ApexStatus == "Failed").Reverse().Take(1) //the last one    -- with Failed status
                        );
            */

            IEnumerable<ActivityTrackRecord> filteredTrackRecords =
                wInfo.ActivityRecords.Where(actrec => (
                                                        (actrec.Name.Equals(currentNode.Attributes["QualifiedName"].Value)) ||
                                                            (
                                                                (actrec.Name.IndexOf(currentNode.Attributes["QualifiedName"].Value + ".") > -1) &&
                                                                (actrec.Type == "UpdateTaskStatus" && actrec.ApexStatus == "Failed")
                                                            )
                                                       )
                                            );
            
            IEnumerable<ActivityTrackRecord> updateTaskStatusTrackRecords =
                filteredTrackRecords.Where(actrec => actrec.Type == "UpdateTaskStatus" && actrec.ApexStatus == "Failed").Reverse().Skip(1);

            //get all records, (except all UpdateTaskStatus records but the last one)
            IEnumerable<ActivityTrackRecord> trackRecords = filteredTrackRecords.Except(updateTaskStatusTrackRecords);


            string activityType = currentNode.Attributes["Type"].Value;

            if (currentNode.ChildNodes.Count > 0)
            {
                Control root = null;

                if (!WfDefinitionHelper.isApexCustomActivity(currentNode) || parseApexCustomActivityChilds)
                {
                    NewOrgPanel orgPanel =
                        CreateOrgPanel(_regexTrimActName.Replace(currentNode.Attributes["QualifiedName"].Value,
                                                                 string.Empty));
                    orgPanel.Name = currentNode.Attributes["QualifiedName"].Value;
                    orgPanel.ActivityType = activityType;
                    root = orgPanel;

                    //ContextMenuStrip = contextMenuStripActivityCopy;
                }
                else //apex custom activity
                {
                    if (_regexTrimActName.Replace(
                            currentNode.Attributes["QualifiedName"].Value, string.Empty) == "CompleteBatchNTDSimulation_I_PreCheck") //CompleteBatchNTDSimulation_I
                    {

                    }

                    GrowLabel lbl = new GrowLabel()
                    {
                        ImageList = imageList1,
                        ImageIndex = WfDefinitionHelper.GetIconIndexByStatus(trackRecords),
                        Text =
                            _regexTrimActName.Replace(
                            currentNode.Attributes["QualifiedName"].Value, string.Empty),
                        Name = currentNode.Attributes["QualifiedName"].Value,
                        BackColor = WfDefinitionHelper.GetApexCustomActivityBackColor(currentNode),
                        ActivityType = activityType,
                        ContextMenuStrip = contextMenuStripActivityCopy
                    };

                    if (!parseApexCustomActivityChilds)
                    {
                        lbl.Cursor = Cursors.Hand;
                        lbl.DoubleClick += new EventHandler(ApexCustomActivity_DoubleClick);
                    }
                    
                    root = lbl;
                }
                Debug.Print("Added Container :" + currentNode.Attributes["QualifiedName"].Value);

                ///Setting tooltip for newly created node
                toolTip1.SetToolTip(root, WfDefinitionHelper.BuildTooltipText(trackRecords, root.Name, activityType));//, currentNode));

                ///adding newly created node to partent node collection
                AddNodeToParentNodeCollection(currentNode, root, parentNodeColl);

                //we wont consider apex custom activity
                if (!WfDefinitionHelper.isApexCustomActivity(currentNode) || parseApexCustomActivityChilds)
                {
                    OrgPanel op = (root as OrgPanel);

                    for (int X = 0; X < currentNode.ChildNodes.Count; X++)
                    {
                        if (WfDefinitionHelper.IfParentParallelActivityType(currentNode.ChildNodes[X]))
                        {
                            BuildWfActivityOrgNode(wInfo, currentNode.ChildNodes[X], op.Nodes, parseApexCustomActivityChilds);
                            ;//AllowApexCustomActivityChild);
                        }
                        else
                        {
                            if (op.Nodes.Count > 0)
                            {
                                Debug.Print("Call for " + currentNode.ChildNodes[X].Attributes["QualifiedName"].Value
                                    + " to add to Parent : " + currentNode.Attributes["QualifiedName"].Value + " ' last node's collection");

                                BuildWfActivityOrgNode(wInfo, currentNode.ChildNodes[X],
                                                       WfDefinitionHelper.GetLastNodeOfNodeChain(op).Nodes, parseApexCustomActivityChilds);
                            }
                            else
                            {
                                Debug.Print("Call for " + currentNode.ChildNodes[X].Attributes["QualifiedName"].Value
                                    + " to add to Parent : " + currentNode.Attributes["QualifiedName"].Value + " ' node's collection");

                                BuildWfActivityOrgNode(wInfo, currentNode.ChildNodes[X], (op).Nodes, parseApexCustomActivityChilds);
                                ;//AllowApexCustomActivityChild);
                            }
                        }
                    }
                }
            }
            else
            {
                GrowLabel lbl = new GrowLabel()
                {
                    ImageList = imageList1,
                    ImageIndex = WfDefinitionHelper.GetIconIndexByStatus(trackRecords),
                    Text = _regexTrimActName.Replace(currentNode.Attributes["QualifiedName"].Value, string.Empty),
                    Name = currentNode.Attributes["QualifiedName"].Value,
                    BackColor = WfDefinitionHelper.GetApexCustomActivityBackColor(currentNode),

                    ActivityType = activityType,
                    //ContextMenuStrip = contextMenuStripExpandHierarchy

                    ContextMenuStrip = contextMenuStripActivityCopy
                };

                //if (!parseApexCustomActivityChilds)
                //{
                //    lbl.Cursor = Cursors.Hand;
                //    lbl.DoubleClick += new EventHandler(ApexCustomActivity_DoubleClick);
                //}

                //toolTip1.SetToolTip(lbl, BuildTooltipText(trackRecords, currentNode));
                toolTip1.SetToolTip(lbl, WfDefinitionHelper.BuildTooltipText(trackRecords, lbl.Name, activityType));

                OrgNode leaf = new OrgNode(lbl);

                AddNodeToParentNodeCollection(currentNode, leaf.Control, parentNodeColl);
            }
        }


        /// <summary>
        /// Loads child activity details for parentActivityControl
        /// </summary>
        /// <param name="parentActivityControl">whose child activities we want to load</param>
        void LoadChildActivityDetails(Control parentActivityControl)
        {
            //GrowLabel apexLabel = sender as GrowLabel;
            //apexLabel.ActivityType

            WFActivityDetailsOrgPanel.Nodes.Clear();

            WorkFlowInfo wInfo = availableWorkflows[SelectedWfIndex];

            XmlDataDocument xmldoc = new XmlDataDocument();
            xmldoc.LoadXml(wInfo.GetDefinition());

            XmlNode rootActivity = xmldoc.SelectSingleNode("descendant::Activity[@QualifiedName='" + parentActivityControl.Name + "']");
            if (rootActivity != null)
            {
                BuildWfActivityOrgNode(wInfo, rootActivity,
                    WFActivityDetailsOrgPanel.Nodes, true);

                tabControl1.TabPages[2].Title = "[Activity Details] - " + parentActivityControl.Name;
                tabControl1.TabPages[2].Selected = true;
                WFActivityDetailsOrgPanel.Nodes[0].BackColor = WfDefinitionHelper.GetApexCustomActivityBackColor(rootActivity);
            }
        }


        void ApexCustomActivity_DoubleClick(object sender, EventArgs e)
        {
            LoadChildActivityDetails(sender as Control);
        }

        /// <summary>
        /// Loads workflow definition and prepare UI
        /// </summary>
        /// <param name="wInfo"></param>
        private void LoadWfDefinition(WorkFlowInfo wInfo)
        {
            try
            {
                //Store current scroll position, before reload
                _wfOrgPanelCurrentScrollPoint = WFOrgPanel.AutoScrollPosition;

                ShowHideLoadingIpIcon(true);

                ControlListViewWorkflows(false);

                UpdateStatusText("Please wait. Loading workflow definition for " + wInfo.InstanceGuid);


                XmlDataDocument xmldoc = new XmlDataDocument();
                xmldoc.LoadXml(wInfo.GetDefinition());

                WFOrgPanel.UIThread(delegate { WFOrgPanel.Nodes.Clear(); });
                WFActivityDetailsOrgPanel.UIThread(delegate { WFActivityDetailsOrgPanel.Nodes.Clear(); });
                tabControl1.UIThread(delegate { tabControl1.TabPages[2].Title = "[Activity Details]"; });

                XmlNodeList rootActivities = xmldoc.SelectNodes("/Activity");
                if (rootActivities != null)
                {
                    for (int i = 0; i <= rootActivities.Count - 1; i++)
                    {
                        BuildWfActivityOrgNode(wInfo, rootActivities[i], WFOrgPanel.Nodes,
                            false);
                    }
                }

                //Setting the scroll position back
                WFOrgPanel.UIThread(delegate()
                {
                    WFOrgPanel.AutoScrollPosition = new Point(Math.Abs(_wfOrgPanelCurrentScrollPoint.X),
                                                              Math.Abs(_wfOrgPanelCurrentScrollPoint.Y));
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                //File system watcher for workflow tracking file
                HookFileWatcher(wInfo.TrackingFilePath);

                //lblStatusBar.Text = "Loaded Workflow definition.";
                //lblStatusBar.Update();
                UpdateStatusText("Loaded Workflow definition " + wInfo.InstanceGuid);
                //this.Cursor = Cursors.Default;

                ControlListViewWorkflows(true);

                ShowHideLoadingIpIcon(false);
            }
        }
        #endregion
        
        #region zoom feature
        
        /// <summary>
        /// holds last zoom in/out
        /// </summary>
        private bool _zoomIn = true;
        /// <summary>
        /// Stores default font size of WfPanel
        /// </summary>
        private readonly float _mDesignFontSize;
        /// <summary>
        /// Scaled font size for change in Zoom factor
        /// </summary>
        int _fontSizeChangeBy;
        /// <summary>
        ///Last saved zoom size, default is 100
        /// </summary>
        int _lastSelectedZoomSize = 100;

        private const int MinZoom = 50;
        private const int MaxZoom = 150;
        private const int ZoomIncrementBy = 10;


        private void LoadZoomSizeDropDown()
        {
            for (int i = MinZoom; i <= MaxZoom; i += ZoomIncrementBy)
            {
                ToolStripItem item = new ToolStripMenuItem(i + "%", null, null, i.ToString());
                WFZoomSizeDropDown.DropDown.Items.Add(item);
            }
            ToolStripMenuItem defaultSelected = (ToolStripMenuItem)WFZoomSizeDropDown.DropDown.Items.Find("100", true).First();
            HighlightSelectedZoomSize(defaultSelected);

            WFZoomSizeDropDown.Text = defaultSelected.Text;
        }

        private void ZoomSizeDropDown_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (WFOrgPanel.Nodes.Count == 0)
                return;

            ZoomInOut((ToolStripMenuItem)e.ClickedItem);
        }

        private void ZoomInOut(ToolStripMenuItem selectedItem)
        {
            Control wfActivity = WFOrgPanel.Nodes.Last().Control;

            HighlightSelectedZoomSize(selectedItem);

            int selectedZoomValue = int.Parse(selectedItem.Name);

            //Find the new factor
            float zoomFactor = selectedZoomValue / (float)_lastSelectedZoomSize;

            //10 is the zoom factor
            if (zoomFactor > 1)
                _fontSizeChangeBy += (selectedZoomValue - _lastSelectedZoomSize) / 10;
            else if (zoomFactor < 1)
                _fontSizeChangeBy -= (_lastSelectedZoomSize - selectedZoomValue) / 10;
            else
                _fontSizeChangeBy = 0;

            _lastSelectedZoomSize = selectedZoomValue;


            //Set its own font
            wfActivity.Font = GetScaledFont(wfActivity.Font, wfActivity);
            //Scale the first Org Panel control

            wfActivity.SuspendLayout();
            wfActivity.Scale(new SizeF(zoomFactor, zoomFactor));
            wfActivity.ResumeLayout();
        }

        private void WFZoomSizeDropDown_ButtonClick(object sender, EventArgs e)
        {
            if (WFOrgPanel.Nodes.Count == 0)
                return;

            if (_lastSelectedZoomSize == MaxZoom)
                _zoomIn = false;
            else if (_lastSelectedZoomSize == MinZoom)
                _zoomIn = true;

            int x = _lastSelectedZoomSize;

            x = (_zoomIn) ? x + ZoomIncrementBy : x - ZoomIncrementBy;

            ZoomInOut((ToolStripMenuItem)WFZoomSizeDropDown.DropDown.Items.Find(x.ToString(), true).First());
        }

        private void HighlightSelectedZoomSize(ToolStripMenuItem selectedItem)
        {
            foreach (ToolStripMenuItem item in WFZoomSizeDropDown.DropDown.Items.OfType<ToolStripMenuItem>())
            {
                if (!item.Equals(selectedItem))
                    item.Checked = false;
                else
                {
                    item.Checked = true;
                    WFZoomSizeDropDown.Text = item.Text;
                }
            }
        }

        /// <summary>
        /// return scaled font for control
        /// </summary>
        /// <param name="f"></param>
        /// <param name="cont"></param>
        /// <returns></returns>
        private Font GetScaledFont(Font f, Control cont)
        {
            float size = _mDesignFontSize * (1 + _fontSizeChangeBy / 7f);
            return new Font(f.FontFamily, (size <= 0) ? 0.1f : size);
        }

        #endregion
        
        #region menu items

        //Update workflow view menu
        private void updateWfViewMenuItem_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtWfUpdates.Text))
            {
                string[] wfUpdateArr = txtWfUpdates.Text.Split(new char[] { '\n' });

                IEnumerable<ActivityTrackRecord> updatedActivityRecords =
                    _queryManager.GetTrackRecordsFromRecordString(wfUpdateArr).OfType<ActivityTrackRecord>();

                SelectWfTab();

                ShowHideLoadingIpIcon(true);

                
                foreach (var record in updatedActivityRecords)
                {
                    string stringparentActivityName = rxParentName.Match(record.Name).Value;
                    
                    //We will process only the first item, 
                    if (record!=
                        updatedActivityRecords.Where(actrec => actrec.Name.IndexOf(stringparentActivityName)>-1).First())
                        continue;

                    //Control activityControl =
                    //    WfDefinitionHelper.GetWfActivityControlByName(WFOrgPanel,record.Name);
                    Control activityControl =
                        WfDefinitionHelper.GetWfActivityControlByName(WFOrgPanel, stringparentActivityName);
                    
                    if (activityControl != null)
                    {
                        //IEnumerable<ActivityTrackRecord> trackRecords =
                        //    availableWorkflows[SelectedWfIndex].ActivityRecords
                        //        .Where(actrec => actrec.Name == activityControl.Name);

                        IEnumerable<ActivityTrackRecord> trackRecords =
                            availableWorkflows[SelectedWfIndex].ActivityRecords
                                .Where(actrec => actrec.Name == activityControl.Name).
                                Concat
                                (
                                    updatedActivityRecords.Where
                                    (actrec => actrec.Name.IndexOf(activityControl.Name) > -1
                                            && actrec.Type == "UpdateTaskStatus").Reverse().Take(1) //taking the last record
                                );

                        if (activityControl is NewOrgPanel)
                        {
                            NewOrgPanel orgPanel = ((NewOrgPanel)activityControl);
                            ///Setting tooltip for newly created node
                            toolTip1.SetToolTip(orgPanel, WfDefinitionHelper.BuildTooltipText(trackRecords, orgPanel.Name, orgPanel.ActivityType));
                        }
                        else if (activityControl is GrowLabel)
                        {
                            GrowLabel growLbl = ((GrowLabel)activityControl);

                            //growLbl.ImageIndex = WfDefinitionHelper.GetIconIndexByStatus(record);
                            growLbl.ImageIndex = WfDefinitionHelper.GetIconIndexByStatus(trackRecords);

                            ///Setting tooltip for newly created node
                            toolTip1.SetToolTip(growLbl, WfDefinitionHelper.BuildTooltipText(trackRecords, growLbl.Name, growLbl.ActivityType));
                        }
                        activityControl.Refresh();
                    }
                }

                //Clear text box, once we update the Wf deficition UI
                txtWfUpdates.Clear();
                //txtWfUpdates.ContextMenuStrip.Enabled = false;

                ShowHideLoadingIpIcon(false);
            }
        }
        
        private void ShowSettingsDialog()
        {
            SettingsForm settingsForm = new SettingsForm(_monitorSettingsValue);
            settingsForm.ShowDialog();

            //If path is changed then load all WFs
            if (_monitorSettingsValue.ApplicationSettingsChanged)
                LoadWorkflowFromTrackingPath();
        }

        private void SettingsMenuItem_Click(object sender, EventArgs e)
        {
            ShowSettingsDialog();
        }

        private void SaveWithCommentsMenuItem_Click(object sender, EventArgs e)
        {
            if(SelectedWfIndex==-1) 
                return;

            OrgPanel selectedOrgpanel = null;
            switch(tabControl1.TabPages.IndexOf(tabControl1.SelectedTab))
            {
                case 0:
                    selectedOrgpanel = WFOrgPanel;
                    break;
                case 2:
                    selectedOrgpanel = WFActivityDetailsOrgPanel;
                    break;
                default:
                    return;
            }

            string tobeSavedControlName = (selectedOrgpanel.Nodes.Count() > 0)
                                           ? selectedOrgpanel.Nodes.Last().Control.Name
                                           : selectedOrgpanel.Name;
            try
            {
                if (WFSaveDetails.ShowDialog(this) == DialogResult.OK)
                {
                    WorkFlowInfo wInfo = availableWorkflows[SelectedWfIndex];
                    XmlDataDocument xmldoc = new XmlDataDocument();
                    xmldoc.LoadXml(wInfo.GetDefinition());

                    XmlNode rootActivity = xmldoc.SelectSingleNode("descendant::Activity[@QualifiedName='" + tobeSavedControlName + "']");
                    if (rootActivity != null)
                    {
                        StringBuilder sb = new StringBuilder();
                        if (selectedOrgpanel.Equals(WFOrgPanel))
                            GenerateActivityLog(wInfo, rootActivity, sb, false);
                        else
                            GenerateActivityLog(wInfo, rootActivity, sb, true);

                        using (StreamWriter file = new System.IO.StreamWriter(WFSaveDetails.FileName))
                        {
                            file.WriteLine(sb.ToString());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// Traverse WF def and generates details for activities 
        /// </summary>
        /// <param name="wInfo"></param>
        /// <param name="currentNode">current xml node</param>
        /// <param name="sb">instance of StringBuilder</param>
        /// <param name="parseApexCustomActivityChilds">determines, we will process ApexCustomActivity childs</param>
        private void GenerateActivityLog(WorkFlowInfo wInfo, XmlNode currentNode, StringBuilder sb, 
            bool parseApexCustomActivityChilds)
        {
            IEnumerable<ActivityTrackRecord> trackRecords =
                    wInfo.ActivityRecords.Where(actrec => actrec.Name
                        == currentNode.Attributes["QualifiedName"].Value).
                        Concat(
                            wInfo.ActivityRecords.Where(actrec => actrec.Name.IndexOf(currentNode.Attributes["QualifiedName"].Value) > -1
                            && actrec.Type == "UpdateTaskStatus").Reverse().Take(1) //the last one
                        );

            string activityType = currentNode.Attributes["Type"].Value;

            if (currentNode.ChildNodes.Count > 0)
            {
                sb.AppendLine("---------------------------------------");
                sb.AppendLine(_regexTrimActName.Replace(currentNode.Attributes["QualifiedName"].Value,
                                                               string.Empty) + " -Status:" + WfDefinitionHelper.GetActivityStatusText(trackRecords));

                sb.AppendLine(WfDefinitionHelper.BuildTooltipText(trackRecords, currentNode.Attributes["QualifiedName"].Value, activityType));

                Debug.Print("Added Container :" + currentNode.Attributes["QualifiedName"].Value);

                //we wont consider apex custom activity
                if (!WfDefinitionHelper.isApexCustomActivity(currentNode) || parseApexCustomActivityChilds)
                {
                    for (int x = 0; x < currentNode.ChildNodes.Count; x++)
                    {
                        GenerateActivityLog(wInfo, currentNode.ChildNodes[x], sb,
                                                parseApexCustomActivityChilds);
                    }
                }
            }
            else
            {
                sb.AppendLine("---------------------------------------");
                sb.AppendLine(_regexTrimActName.Replace(currentNode.Attributes["QualifiedName"].Value,
                                                               string.Empty) + " -Status:" + WfDefinitionHelper.GetActivityStatusText(trackRecords));
                sb.AppendLine(WfDefinitionHelper.BuildTooltipText(trackRecords, currentNode.Attributes["QualifiedName"].Value, activityType));
            }
        }


        private void SaveAsMenuItem_Click(object sender, EventArgs e)
        {
            OrgPanel selectedOrgpanel = null;
            
            int selectedTabIndex = tabControl1.TabPages.IndexOf(tabControl1.SelectedTab);
            if (selectedTabIndex == 0)
                selectedOrgpanel = WFOrgPanel;
            else if (selectedTabIndex == 2)
                selectedOrgpanel = WFActivityDetailsOrgPanel;
            
            if (selectedOrgpanel==null)
                return;

            Control tobeSavedControl = (selectedOrgpanel.Nodes.Count() > 0)
                                           ? selectedOrgpanel.Nodes.Last().Control
                                           : selectedOrgpanel;

            try
            {
                //string saveAsFileName = string.Empty;
                if (WFSaveImageFile.ShowDialog(this) == DialogResult.OK)
                {
                    string saveAsFileName = WFSaveImageFile.FileName;
                    Control wfActivity = tobeSavedControl;
                    Bitmap bmp = new Bitmap(wfActivity.Width, wfActivity.Height);
                    wfActivity.DrawToBitmap(bmp, new Rectangle(0, 0, wfActivity.Bounds.Width, wfActivity.Bounds.Height));
                    bmp.Save(saveAsFileName, ImageFormat.Png);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void FindMenuItem_Click(object sender, EventArgs e)
        {
            if (SelectedWfIndex == -1)
                return;

            WorkFlowInfo wInfo = availableWorkflows[SelectedWfIndex];
            AutoCompleteStringCollection taskList = new AutoCompleteStringCollection();
            foreach (var data in wInfo.ActivityRecords)
            {
                if (data is ActivityTrackRecord)
                {
                    ActivityTrackRecord record = (ActivityTrackRecord)data;
                    if (!taskList.Contains(record.Name))
                        taskList.Add(record.Name);
                }
            }

            FindForm findForm = new FindForm();
            findForm.TaskList = taskList;
            findForm.Show(this);
        }

        private void contextMenuStripActivityCopy_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(contextMenuStripActivityCopy.SourceControl.Name + Environment.NewLine +
                toolTip1.GetToolTip(contextMenuStripActivityCopy.SourceControl));
        }
        
        #endregion

        #region workflowTrackingFilePathWatcher

        private void HookFileWatcher(string workflowTrackingFilePath)
        {
            try
            {
                workflowTrackingFilePathWatcher.Path = Path.GetDirectoryName(workflowTrackingFilePath);
                /* Watch for changes in LastAccess and LastWrite times, and
                   the renaming of files or directories. */
                workflowTrackingFilePathWatcher.NotifyFilter = //NotifyFilters.LastAccess | 
                    NotifyFilters.LastWrite | NotifyFilters.FileName | NotifyFilters.DirectoryName;

                // Only watch track file
                workflowTrackingFilePathWatcher.Filter = Path.GetFileName(workflowTrackingFilePath);

                // Add event handlers.
                workflowTrackingFilePathWatcher.Changed += new FileSystemEventHandler(OnChanged);
                workflowTrackingFilePathWatcher.Deleted += new FileSystemEventHandler(OnDeleted);
                workflowTrackingFilePathWatcher.Renamed += new RenamedEventHandler(OnRenamed);

                // Begin watching.
                workflowTrackingFilePathWatcher.EnableRaisingEvents = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unable to start File watcher.");
            }
        }


        /// <summary>
        /// Has tracking file changed since last load???
        /// </summary>
        /// <param name="wInfo"></param>
        /// <returns></returns>
        private static bool IsTrackingFileChangedSinceLastLoad(WorkFlowInfo wInfo)
        {
            return (wInfo.TrackingFileLastWriteTime
                != File.GetLastWriteTime(wInfo.TrackingFilePath));
        }


        // Define the event handlers.
        private void OnChanged(object source, FileSystemEventArgs e)
        {
            try{
                if (e.ChangeType == WatcherChangeTypes.Changed && !backgroundWorker1.IsBusy)
                {
                    WorkFlowInfo wInfo = availableWorkflows.Where(
                        a => a.InstanceGuid.Equals(Path.GetFileNameWithoutExtension(e.Name))).FirstOrDefault();

                    if (wInfo != null
                        && IsTrackingFileChangedSinceLastLoad(wInfo))
                    {
                        // Specify what is done when a file is changed, created.
                        UpdateStatusText("Track Record for Instance [" + e.Name + "] has been modified. Reloading workflow definition.");

                        int deletedWFInfoIndex = availableWorkflows.IndexOf(wInfo);
                        availableWorkflows.Remove(wInfo);

                        var queryManager = new XMLTrackingQueryManager();
                        wInfo = queryManager.GetWorkflow(e.FullPath);
                        availableWorkflows.Insert(deletedWFInfoIndex, wInfo);

                        SelectedWfIndex = deletedWFInfoIndex;

                        //Update workflow Status
                        listViewWorkflows.Items[SelectedWfIndex]
                            = new ListViewItem(new string[]
                                                 {
                                                     SelectedWfIndex.ToString(), wInfo.InstanceGuid, wInfo.WfPersistanceDb, "0",
                                                     wInfo.WfStatus.ToString()
                                                 });
                        //Update workflow Status


                        //Sudip
                        LoadWfActivityDetail(deletedWFInfoIndex, false);

                        //Show new WF update panel
                        //ShowHideLiveUpdateDockPanel(true);
                    }
                }
            }
            catch (Exception exception)
            {
                UpdateStatusText(exception.Message);
            }
        }

        private void PrepareUiOnWatcherEvent(string trackingFileName)
        {
            WFOrgPanel.UIThread(delegate
            {
                WFOrgPanel.Nodes.Clear();
            });

            //Select Zoom=100%
            ToolStripMenuItem defaultSelected = (ToolStripMenuItem)WFZoomSizeDropDown.DropDown.Items.Find("100", true).First();
            HighlightSelectedZoomSize(defaultSelected);

            WorkFlowInfo wInfo = availableWorkflows.Where(
                a => a.InstanceGuid.Equals(Path.GetFileNameWithoutExtension(trackingFileName))).FirstOrDefault();
            if (wInfo != null)
            {
                availableWorkflows.Remove(wInfo);
                //No selected Wf
                SelectedWfIndex = -1;
                SelectedWfGuid = string.Empty;

                listViewWorkflows.Items.Clear();
                lstActivities.Items.Clear();
                for (int x = 0; x < availableWorkflows.Count; x++)
                {
                    listViewWorkflows.Items.Add(
                        new ListViewItem(new string[] { x.ToString(), availableWorkflows[x].InstanceGuid, availableWorkflows[x].WfPersistanceDb, "0", availableWorkflows[x].WfStatus.ToString() })
                        );
                }
            }
            workflowTrackingFilePathWatcher.EnableRaisingEvents = false;
        }

        private void OnRenamed(object source, RenamedEventArgs e)
        {
            PrepareUiOnWatcherEvent(e.OldName);
            //MessageBox.Show("Track Record file for Instance [" + e.Name + "] has been renamed.");
            UpdateStatusText("Track Record file for Instance [" + e.Name + "] has been renamed.");
        }

        private void OnDeleted(object sender, FileSystemEventArgs e)
        {
            PrepareUiOnWatcherEvent(Path.GetFileNameWithoutExtension(e.Name));
            UpdateStatusText("Track Record file for Instance [" + e.Name + "] has been deleted.");
            //MessageBox.Show("Track Record file for Instance [" + e.Name + "] has been deleted.");
        }

        #endregion

    }
}