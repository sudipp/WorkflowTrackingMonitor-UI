using System;
using System.Text;
using System.Drawing;
using System.Windows.Forms; 

namespace WorkflowTrackingClient
{
    public class GrowLabel : Label, ISelectedControl
    {
        private bool mGrowing; 
        public GrowLabel()
        {
            // Prevent flicker with double buffering and all painting inside WM_PAINT
            SetStyle(ControlStyles.DoubleBuffer, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);

            SetStyle(ControlStyles.Selectable, true);

            AutoSize = false;

            ImageAlign = ContentAlignment.TopRight;

            TabStop = true;

            BorderStyle = BorderStyle.FixedSingle;

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

        private void resizeLabel()
        {
            if (mGrowing) 
                return; 
            try
            {
                mGrowing = true; 
                Size sz = new Size(this.Width, Int32.MaxValue); 
                sz = TextRenderer.MeasureText(this.Text, this.Font, sz, TextFormatFlags.WordBreak);
                this.Width = sz.Width;

                if (this.ImageList != null && this.ImageIndex != -1)
                {
                    this.Height = 25 + sz.Height; //25 is the height of the icon
                }
            } 
            finally { mGrowing = false; }
        } 
        protected override void OnTextChanged(EventArgs e)
        {
            base.OnTextChanged(e); 
            resizeLabel();
        } 
        protected override void OnFontChanged(EventArgs e)
        {
            base.OnFontChanged(e); 
            resizeLabel();
        } 
        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e); 
            resizeLabel();
        }

        protected override void OnPaint(System.Windows.Forms.PaintEventArgs e)
        {
            //StringFormat drawFormat = new StringFormat();
            ////drawFormat.Alignment = StringAlignment.Center;
            ////drawFormat.FormatFlags = StringFormatFlags.;//.NoWrap;

            //Font drawFont = this.Font;// new Font("Microsoft Sans Serif", _FontSize);
            //SolidBrush drawBrush = new SolidBrush(this.ForeColor);
            //Pen boxPen = new Pen(this.ForeColor, 1);


            //e.Graphics.DrawString(Text, Font, drawBrush, new PointF(0, 20), drawFormat);
            ////base.OnPaint(e);

            //if (this.ImageList!=null)
            //    this.ImageList.Draw(e.Graphics, (this.Width - this.ImageList.ImageSize.Width), 0, this.ImageIndex);

            ////e.Graphics.DrawIcon();
            ////this.Height = this.ImageList.ImageSize.Height + sz.Height;
            

            //this.Parent.CreateGraphics()

            if (!string.IsNullOrEmpty(this.Text))
            {
                TextFormatFlags flags = TextFormatFlags.HorizontalCenter |
                    TextFormatFlags.Top | TextFormatFlags.WordEllipsis |
                    TextFormatFlags.SingleLine;//.WordBreak;

                if (this.ImageList != null && this.ImageIndex!=-1)
                    TextRenderer.DrawText(e.Graphics, this.Text, Font, new Rectangle(0, 20, this.Width, this.Height), ForeColor, flags);
                else
                {
                    TextRenderer.DrawText(e.Graphics, this.Text, Font, new Rectangle(0, 0, this.Width, this.Height), ForeColor, flags);
                }
            }

            //if (this.ImageList != null && this.ImageIndex!=-1)
            //    this.ImageList.Draw(e.Graphics, (this.Width - this.ImageList.ImageSize.Width), 0, this.ImageIndex);

            if (this.ImageList != null && this.ImageIndex != -1)
            {
                //this.ImageList.Draw(e.Graphics, (this.Width - this.ImageList.ImageSize.Width), 0, this.ImageIndex);
                this.ImageList.Draw(e.Graphics, (this.Width/2 - this.ImageList.ImageSize.Width/2), 0, this.ImageIndex);

                //using (Graphics parentGraphics=this.Parent.CreateGraphics())
                //{
                //    //this.ImageList.Draw(e.Graphics, this.Left+ this.Width/2, this.Top, this.ImageIndex);
                    
                //}
            }
                

            if(this.Focused)
            {
                ControlPaint.DrawFocusRectangle(e.Graphics, new Rectangle(0, 0, this.Width - 2, this.Height - 2));
            }

            if(IsSelected)
            {
                ControlPaint.DrawBorder(e.Graphics, new Rectangle(0, 0, this.Width - 2, this.Height - 2), Color.Red, ButtonBorderStyle.Dashed);
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
