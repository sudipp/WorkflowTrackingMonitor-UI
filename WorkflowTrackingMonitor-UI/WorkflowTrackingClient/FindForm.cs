using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;


namespace WorkflowTrackingClient
{
    public partial class FindForm : Form
    {
        private AutoCompleteStringCollection _TaskList;

        public FindForm()
        {
            InitializeComponent();
        }


        public AutoCompleteStringCollection TaskList
        {
            get { return _TaskList; }
            set { 
                _TaskList = value;
                txtFindTask.AutoCompleteCustomSource = _TaskList;
            }
        }

        private void FindForm_Deactivate(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FindForm_Load(object sender, EventArgs e)
        {
            txtFindTask.Text = "";
            txtFindTask.Focus();

            //txtFindTask.AutoCompleteCustomSource = TaskList;
        }

        private void cboFindTask_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                foreach (string s in this.txtFindTask.AutoCompleteCustomSource)
                {
                    if (s.Equals(this.txtFindTask.Text,StringComparison.OrdinalIgnoreCase))//.ToUpperInvariant().StartsWith(this.txtFindTask.Text))
                    {
                        if(this.Owner is MainForm){
                            ListViewItem item=((MainForm)this.Owner).ListActivities.FindItemWithText(s, true, 0,false);
                            if (item!=null)
                                item.Selected = true;
                        }
                        this.Close();
                    }
                } 

                //foreach (string s in this.cboFindTask.Items)//.AutoCompleteSource)//.AutoCompleteCustomSource)
                //{
                //    if (s.ToUpperInvariant().StartsWith(text.ToUpperInvariant()))
                //    {
                //        //e.Handled = true;
                //        return;
                //    }
                //} 
            } 
            else if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void FindForm_Paint(object sender, PaintEventArgs e)
        {
            ControlPaint.DrawBorder3D(e.Graphics, new Rectangle(0, 0, this.Width, this.Height),Border3DStyle.RaisedInner);
        }
    }
}
