using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Workflow.ComponentModel;
using System.Xml;
using CheckBoxStudio.WinForms;
using XMLFileTracking;

namespace WorkflowTrackingClient
{
    class WfDefinitionHelper
    {
        /// <summary>
        /// Return the last node from Node chain
        /// </summary>
        /// <param name="root"></param>
        /// <returns></returns>
        public static OrgNode GetLastNodeOfNodeChain(OrgPanel root)
        {
            OrgNode theLastNode = root.Nodes.Last();
            while (theLastNode != null)
            {
                if (theLastNode.Nodes.Count > 0)
                    theLastNode = theLastNode.Nodes.Last();
                else
                {
                    break;
                }
            }
            return theLastNode;
        }

        /// <summary>
        /// Build tooltip text for node
        /// </summary>
        /// <param name="trackRecords"></param>
        /// <param name="qualifiedName"></param>
        /// <param name="activityType"></param>
        /// <returns></returns>
        public static string BuildTooltipText(IEnumerable<ActivityTrackRecord> trackRecords,
            string qualifiedName, string activityType)
        {
            StringBuilder toolTip = new StringBuilder();
            if (trackRecords.Count() > 0)
            {
                toolTip.AppendLine("[" +
                          (string.IsNullOrEmpty(trackRecords.First().DisplayName)
                               ? qualifiedName
                               : trackRecords.First().DisplayName)
                          + "-" + activityType + "]");

                toolTip.AppendLine(Environment.NewLine + "[WF Task Status Log]");
                foreach (ActivityTrackRecord record in trackRecords)
                {
                    if (record.Type != "UpdateTaskStatus") //excludidng UpdateTaskStatus from the list, as it contains APEX status
                    {
                        toolTip.AppendLine("[" + record.WfHost + "][" + record.ThreadId +"] " + record.WfStatus.ToString() + " @ " + record.DateTime.ToString() + ((!string.IsNullOrEmpty(record.User)) ? " by " + record.User : ""));
                    }
                }

                IEnumerable<ActivityTrackRecord> recordWithErrMessage=
                    trackRecords.Where(record => record.ErrorMessage.Length > 0);

                if (recordWithErrMessage.Count() > 0)
                    toolTip.AppendLine(Environment.NewLine + "[Exceptions]");

                foreach (ActivityTrackRecord record in recordWithErrMessage)
                {
                    toolTip.AppendLine(record.ErrorMessage);
                }
            }
            else
            {
                toolTip.AppendLine("[" + qualifiedName + "-" + activityType + "]");
            }
            return toolTip.ToString();
        }

        /// <summary>
        /// Determine if parentNode is ParallelActivity or IfElseActivity
        /// </summary>
        /// <param name="currentNode"></param>
        /// <returns></returns>
        public static bool IfParentParallelActivityType(XmlNode currentNode)
        {
            if (currentNode.ParentNode.Attributes != null)
            {
                string Type = currentNode.ParentNode.Attributes["Type"].Value;
                if (Type == "ParallelActivity" || Type == "IfElseActivity")
                    return true;
            }
            return false;
        }

        /// <summary>
        /// If the current node is derived from Base Activity
        /// </summary>
        /// <param name="currentNode"></param>
        /// <returns></returns>
        public static bool isApexCustomActivity(XmlNode currentNode)
        {
            bool isApexCustomActivity = false;
            if (currentNode.Attributes != null)
            {
                switch (currentNode.Attributes["Type"].Value)
                {
                    case "AutoStartStandaloneJobActivity":
                        isApexCustomActivity = true;
                        break;
                    case "ManualStandaloneJobActivity":
                        isApexCustomActivity = true;
                        break;
                    case "MultipleApprovalOnlineActivity":
                        isApexCustomActivity = true;
                        break;
                    case "MultipleCompleteManualJobActivity":
                        isApexCustomActivity = true;
                        break;
                    case "MultipleCompleteOnlineActivity":
                        isApexCustomActivity = true;
                        break;

                    case "MultiSubmissionActivity":
                        isApexCustomActivity = true;
                        break;

                    case "OnlineStandaloneActivity":
                        isApexCustomActivity = true;
                        break;

                    case "SingleApprovalOnlineActivity":
                        isApexCustomActivity = true;
                        break;

                    case "StandaloneActivity":
                        isApexCustomActivity = true;
                        break;
                        
                    case "AutoStartSendDataStandaloneJobActivity":
                        isApexCustomActivity = true;
                        break;

                    case "AutoStartReceiveDataStandaloneJobActivity":
                        isApexCustomActivity = true;
                        break;
                }
            }
            return isApexCustomActivity;

        }

        /// <summary>
        /// returns Apex activity back color by type
        /// </summary>
        /// <param name="currentNode"></param>
        /// <returns></returns>
        public static Color GetApexCustomActivityBackColor(XmlNode currentNode)
        {
            Color nodeColor = Color.LightGray;//.Transparent;
            if (currentNode.Attributes != null)
            {
                switch (currentNode.Attributes["Type"].Value)
                {
                    case "AutoStartStandaloneJobActivity":
                        nodeColor = Color.DarkOrange;
                        break;
                    case "ManualStandaloneJobActivity":
                        nodeColor = Color.DarkGoldenrod;
                        break;
                    case "MultipleApprovalOnlineActivity":
                        nodeColor = Color.LightGreen;//.DarkGreen;
                        break;
                    case "MultipleCompleteManualJobActivity":
                        nodeColor = Color.DarkGoldenrod;
                        break;
                    case "MultipleCompleteOnlineActivity":
                        nodeColor = Color.LightBlue;
                        break;

                    case "MultiSubmissionActivity":
                        nodeColor = Color.DarkOrange;
                        break;

                    case "OnlineStandaloneActivity":
                        nodeColor = Color.AntiqueWhite;
                        break;

                    case "SingleApprovalOnlineActivity":
                        nodeColor = Color.Yellow;
                        break;

                    case "StandaloneActivity":
                        nodeColor = Color.DarkOrange;
                        break;
                }
            }
            return nodeColor;
        }

        public static int GetIconIndexByStatus(ActivityTrackRecord trackRecord)
        {
            //if last record is of type (UpdateTaskStatus), we will show APEX status icon
            //else show microsft WF status icon
            if (trackRecord.Type == "UpdateTaskStatus")
            {
                switch (trackRecord.ApexStatus)
                {
                    case "Completed":
                        return 0;
                    case "InProgress":
                    case "Ready":
                        return 1;
                    case "Failed":
                        return 2;
                    case "PastDue":
                        return 4;
                    case "MarkedForRerun":
                        return 5;
                    default:
                        return -1;
                }
            }
            else
            {
                switch (trackRecord.WfStatus)
                {
                    case ActivityExecutionStatus.Closed:
                        return 0;
                    case ActivityExecutionStatus.Executing:
                        return 1;
                    case ActivityExecutionStatus.Faulting:
                        return 2;
                    default:
                        return -1;
                }
            }
        }
        
        public static int GetIconIndexByStatus(IEnumerable<ActivityTrackRecord> trackRecords)
        {
            if (trackRecords.Count() == 0)
                return 3;
            else
                return GetIconIndexByStatus(trackRecords.Last());
        }

        public static string GetActivityStatusText(ActivityTrackRecord trackRecord)
        {
            //if last record is of type (UpdateTaskStatus), we will show APEX status icon
            //else show microsft WF status icon
            if (trackRecord.Type == "UpdateTaskStatus")
            {
                return trackRecord.ApexStatus;
            }
            else
            {
                return trackRecord.WfStatus.ToString();
            }
        }

        public static string GetActivityStatusText(IEnumerable<ActivityTrackRecord> trackRecords)
        {
            if (trackRecords.Count() == 0)
                return "Unknown";
            else
                return GetActivityStatusText(trackRecords.Last());
        }

        /// <summary>
        /// Returns Wf activity by name from wfOrgPanel
        /// </summary>
        /// <param name="wfOrgPanel"></param>
        /// <param name="activityName"></param>
        /// <returns></returns>
        public static Control GetWfActivityControlByName(OrgPanel wfOrgPanel, string activityName)
        {
            Control[] matchControls = wfOrgPanel.Controls.Find(activityName, true);
            return (matchControls.Count() > 0) ? matchControls.First() : null;
        }
    }
}
