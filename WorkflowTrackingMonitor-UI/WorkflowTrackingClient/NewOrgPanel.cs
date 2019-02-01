using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms.VisualStyles;
using CheckBoxStudio.WinForms;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace WorkflowTrackingClient
{
    class NewOrgPanel : OrgPanel, ISelectedControl
    {
        public NewOrgPanel(string text)
        {
            // Prevent flicker with double buffering and all painting inside WM_PAINT
            SetStyle(ControlStyles.DoubleBuffer, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);

            SetStyle(ControlStyles.Selectable,true);
            

            Text = text;

            BorderStyle = BorderStyle.None;


            Click += new EventHandler(GrowLabel_Click);
            GotFocus += new EventHandler(GrowLabel_FocusHandler);
            LostFocus += new EventHandler(GrowLabel_FocusHandler);
        }

        void GrowLabel_FocusHandler(object sender, EventArgs e)
        {
            this.Refresh();
        }

        void GrowLabel_Click(object sender, EventArgs e)
        {
            this.Focus();
            this.Refresh();
        }

        protected override void OnPaint(System.Windows.Forms.PaintEventArgs e)
        {
            base.OnPaint(e);

            //clearing background
            e.Graphics.Clear(this.BackColor);

            //ControlPaint.DrawContainerGrabHandle(e.Graphics, new Rectangle(0, 0, this.Width, this.Height));
            //ControlPaint.DrawSelectionFrame(e.Graphics, true, new Rectangle(0, 0, this.Width, this.Height), new Rectangle(0, 0, this.Width, this.Height),Color.Red);//, Color.Red, 10, ButtonBorderStyle.Solid, Color.Green, 10, ButtonBorderStyle.Solid, Color.Blue, 10, ButtonBorderStyle.Solid, Color.Green, 10, ButtonBorderStyle.Solid);//, true);
            
            if(!string.IsNullOrEmpty(this.Text)){
                TextFormatFlags flags = TextFormatFlags.HorizontalCenter |
                    TextFormatFlags.Top | TextFormatFlags.WordEllipsis | 
                    TextFormatFlags.SingleLine;//.WordBreak;
                
                TextRenderer.DrawText(e.Graphics, this.Text, Font, new Rectangle(0, 0, this.Width, this.Height), ForeColor, flags);
            }

            //ControlPaint.DrawGrid(e.Graphics,new Rectangle(0, 0, this.Width, this.Height),new Size(9,9),Color.DarkGray );

            

            int shadowSize = 5;
            int shadowMargin = 2;
            using (SolidBrush shadowDownBrush = new SolidBrush(this.BackColor))
            {
                int currentLevel = 1;
                for (int x = 0; x < this.Controls.Count;x++ )
                {
                    Control cont = this.Controls[x];
                    Rectangle filledRect1 = new Rectangle(cont.Location.X - shadowSize, cont.Location.Y - shadowSize,
                                                             cont.Width + (shadowSize * 2), cont.Height + (shadowSize * 4));

                    e.Graphics.FillRectangle(shadowDownBrush, filledRect1);

                    OrgNode current=this.Nodes.Find(cont);

                    if (Controls.Count >1 || current.Level != currentLevel) //leaving first element 
                    {
                        using (Pen pen = new Pen(Color.Black))
                        {
                            e.Graphics.DrawLine(pen, new Point(cont.Left + cont.Width / 2, cont.Top - 7), new Point(cont.Left + cont.Width / 2, cont.Top - 20));
                        }
                    }

                    currentLevel = current.Level;

                    continue;

                    //OrgNode currentCont = this.Nodes.Find(cont);
                    //if (currentCont.NextNode != null)
                    //{
                    //    Rectangle filledRect = new Rectangle(cont.Location.X - shadowSize, cont.Location.Y - shadowSize,
                    //                                         cont.Width + (shadowSize * 2), cont.Height + (shadowSize * 2));

                    //    e.Graphics.FillRectangle(shadowDownBrush, filledRect);
                    //}
                    //else
                    //{
                    //    //Rectangle filledRect = new Rectangle(cont.Location.X - shadowSize, cont.Location.Y - shadowSize,
                    //    //                                     cont.Width + (shadowSize * 2), cont.Height + (shadowSize *2));

                    //    Rectangle filledRect = new Rectangle(cont.Location.X - shadowSize, cont.Location.Y - shadowSize,
                    //                                         cont.Width + (shadowSize * 2), cont.Height + (shadowSize) - 2);

                    //    e.Graphics.FillRectangle(shadowDownBrush, filledRect);

                    //    Rectangle filledRectLeft = new Rectangle(filledRect.Left, filledRect.Top,
                    //                                                             filledRect.Width / 2 - shadowSize, cont.Height + (shadowSize * 2));

                    //    e.Graphics.FillRectangle(shadowDownBrush, filledRectLeft);


                    //    Rectangle filledRectRight = new Rectangle(filledRectLeft.Left + filledRectLeft.Width + (shadowSize * 2), filledRect.Top,
                    //                                                             filledRect.Width / 2 - shadowSize, cont.Height + (shadowSize * 2));

                    //    e.Graphics.FillRectangle(shadowDownBrush, filledRectRight);
                    //}

                    //continue;

                    //if (this.Text == "receiveStart")
                    //{

                    //}




                    //OrgNode on1 = this.Nodes.Find(this);
                    //OrgNode parent = on1.Parent;// (this.Parent as OrgPanel);
                    //if (parent != null)
                    //{
                    //    //OrgNode on = parent.Nodes.Find(this);
                    //    if (this.Nodes.Count > 0)//(on.Nodes.Count > 0)
                    //    {
                    //        Icon ic;
                    //        if (on1.Expanded)
                    //        {
                    //            ic = Icon.FromHandle((Resource1.Minus).GetHicon());
                    //        }
                    //        else
                    //        {
                    //            ic = Icon.FromHandle((Resource1.plus).GetHicon());
                    //        }
                    //        e.Graphics.DrawIcon(ic, cont.Location.X + cont.Width / 2 - shadowSize,
                    //            cont.Location.Y + cont.Height + shadowSize);
                    //        ic.Dispose();
                    //    }
                    //}
                }   
            }
            

            //drawing border
            ControlPaint.DrawBorder(e.Graphics,new Rectangle(0,0,Width,Height),Color.Gray,ButtonBorderStyle.Dashed);

            if (this.Focused)
            {
                ControlPaint.DrawFocusRectangle(e.Graphics, new Rectangle(2, 2, this.Width-4, this.Height-4));
            }

            if (IsSelected)
            {
                ControlPaint.DrawBorder(e.Graphics, new Rectangle(1, 1, Width-2, Height-2), Color.Red, ButtonBorderStyle.Dashed);
            }
        }

        public string ActivityType { get; set; }

        public bool IsSelected
        {
            get
            {
                Form containerForm = this.FindForm();
                if (containerForm is MainForm)
                {
                    return this.Equals((containerForm as MainForm).SelectedWfNode);
                }
                return false;
            }
        }
    }
}