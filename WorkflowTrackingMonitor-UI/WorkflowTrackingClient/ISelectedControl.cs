using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WorkflowTrackingClient
{
    interface ISelectedControl
    {
        /// <summary>
        /// determines of the control is currently selected
        /// </summary>
        bool IsSelected {get;}

        /// <summary>
        /// Stores types of Activity
        /// </summary>
        string ActivityType { get; set; }
    }
}
