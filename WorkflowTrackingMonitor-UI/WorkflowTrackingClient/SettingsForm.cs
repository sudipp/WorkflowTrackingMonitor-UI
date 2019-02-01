using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WorkflowTrackingClient
{
    public partial class SettingsForm : Form
    {
        private readonly ApplicationSettings _monitorSettingsValue;

        public SettingsForm(ApplicationSettings monitorSettingsValue)
        {
            InitializeComponent();

            //Initializing Settings
            _monitorSettingsValue = monitorSettingsValue;
        }

        private void SettingsForm_Load(object sender, EventArgs e)
        {
            //Getting tracking file path
            //_monitorSettingsValue.LoadAppSettings(Application.LocalUserAppDataPath + @"\wfmonitorClient.config");
            
            lblWFTrackingPath.Text = _monitorSettingsValue.WFTrackingFolderPath;
            chkDisplayTerminatedWF.Checked = _monitorSettingsValue.DisplayTerminatedWF;
            chkAutoWFChanges.Checked = _monitorSettingsValue.AutoWFChangeUpadate;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(lblWFTrackingPath.Text))
            {
                LabeltoolStripStatus.Text = "Please select WF tracking path";
                return;
            }

            if (lblWFTrackingPath.Text != _monitorSettingsValue.WFTrackingFolderPath
                || chkDisplayTerminatedWF.Checked!= _monitorSettingsValue.DisplayTerminatedWF
                || chkAutoWFChanges.Checked != _monitorSettingsValue.AutoWFChangeUpadate)
            {
                _monitorSettingsValue.WFTrackingFolderPath = lblWFTrackingPath.Text;
                _monitorSettingsValue.AutoWFChangeUpadate = chkAutoWFChanges.Checked;
                _monitorSettingsValue.DisplayTerminatedWF = chkDisplayTerminatedWF.Checked;
                _monitorSettingsValue.SaveSettings(Application.LocalUserAppDataPath + @"\wfmonitorClient.config");
            }

            this.Close();
        }

        /// <summary>
        /// Displays Tracking folder path selection dialog
        /// </summary>
        private void ShowWfTrackingFolderSelectDialog()
        {
            WFTrackingFolderBrowserDialog.Description = "Select WF tracking path\n" +
            "Monitor Path: " + _monitorSettingsValue.WFTrackingFolderPath;

            if (!string.IsNullOrEmpty(_monitorSettingsValue.WFTrackingFolderPath))
                WFTrackingFolderBrowserDialog.SelectedPath = _monitorSettingsValue.WFTrackingFolderPath;

            DialogResult result = WFTrackingFolderBrowserDialog.ShowDialog(this);
            if (result == DialogResult.OK)
            {
                //_monitorSettingsValue.WFTrackingFolderPath = WFTrackingFolderBrowserDialog.SelectedPath.Trim();
                lblWFTrackingPath.Text = WFTrackingFolderBrowserDialog.SelectedPath.Trim(); //_monitorSettingsValue.WFTrackingFolderPath;
                
                //_monitorSettingsValue.SaveSettings(Application.LocalUserAppDataPath + @"\wfmonitorClient.config");
                //LoadWorkflowFromTrackingPath();
            }
        }

        private void btnBrowseFolder_Click(object sender, EventArgs e)
        {
            ShowWfTrackingFolderSelectDialog();
        }
    }
}
