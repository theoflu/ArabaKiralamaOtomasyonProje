using System;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using Microsoft.VisualBasic;
namespace WindowsFormsApplication1
{
    class EB_Button : System.Windows.Forms.Button
    {
        public EB_Button()
        {
            ForeColor = Color.FromArgb(40, 218, 255);
        }
        private int State;
        protected override void OnMouseEnter(EventArgs e)
        {
            State = 1;
            Invalidate();
            base.OnMouseEnter(e);
        }
        protected override void OnMouseDown(MouseEventArgs e)
        {
            State = 2;
            Invalidate();
            base.OnMouseDown(e);
        }
        protected override void OnMouseLeave(EventArgs e)
        {
            State = 0;
            Invalidate();
            base.OnMouseLeave(e);
        }
        protected override void OnMouseUp(MouseEventArgs e)
        {
            State = 1;
            Invalidate();
            base.OnMouseUp(e);
        }
        Color C1 = Color.FromArgb(31, 31, 31);
        Color C2 = Color.FromArgb(41, 41, 41);
        Color C3 = Color.FromArgb(51, 51, 51);
        Color C4 = Color.FromArgb(0, 0, 0, 0);
        Color C5 = Color.FromArgb(25, 255, 255, 255);
        protected override void OnPaintBackground(PaintEventArgs e)
        {
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            using (Bitmap B = new Bitmap(Width, Height))
            {
                using (Graphics G = Graphics.FromImage(B))
                {
                    Rectangle R1 = new Rectangle(0, 0, Width, Height / 2);
                    Rectangle R2 = new Rectangle(0, Height / 2, Width, Height);
                    G.DrawRectangle(new Pen(C1), 0, 0, Width - 1, Height - 1);


                    if (State == 2)
                    {
                        Brush GB1 = new LinearGradientBrush(R1, C3, C2, 40.0F);
                        Brush GB2 = new LinearGradientBrush(R2, C2, C3, 90.0F);
                        G.FillRectangle(GB1, R1);
                        G.FillRectangle(GB2, R2);
                        //Draw.Gradient(G, C2, C3, 1, 1, Width - 2, Height - 2);
                    }
                    else
                    {
                        Brush GB1 = new LinearGradientBrush(R1, C2, C3, 90.0F);
                        Brush GB2 = new LinearGradientBrush(R2, C3, C2, 90.0F);
                        G.FillRectangle(GB1, R1);
                        G.FillRectangle(GB2, R2);
                        // Draw.Gradient(G, C3, C2, 1, 1, Width - 2, Height - 2);
                    }
                    Pen P2 = new Pen(Color.Black, 2);
                    G.DrawRectangle(P2, 0, 0, Width, Height);
                    SizeF O = G.MeasureString(Text, Font);
                    G.DrawString(Text, Font, new SolidBrush(ForeColor), Width / 2 - O.Width / 2, Height / 2 - O.Height / 2);

                    //Draw.Blend(G, C4, C5, C4, 0.5, 0, 1, 1, Width - 2, 1);
                    Bitmap B1 = B;
                    e.Graphics.DrawImage(B1, 0, 0);
                }
            }
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            this.ResumeLayout(false);

        }
    }
    class EB_Progress : System.Windows.Forms.ProgressBar
    {
        private int _Value;
        public int Value
        {
            get
            {
                return _Value;
            }
            set
            {
                _Value = value;
                Invalidate();
            }
        }
        private int _Maximum = 100;
        public int Maximum
        {
            get
            {
                return _Maximum;
            }
            set
            {
                if (value == 0)
                {
                    value = 1;
                }
                _Maximum = value;
                Invalidate();
            }
        }
        public EB_Progress()
        {
        }
        Color C1 = Color.FromArgb(31, 31, 31), C2 = Color.FromArgb(41, 41, 41), C3 = Color.FromArgb(51, 51, 51), C4 = Color.FromArgb(0, 0, 0, 0), C5 = Color.FromArgb(25, 255, 255, 255);
        protected override void OnPaintBackground(PaintEventArgs e)
        {
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            int V = Width * _Value / _Maximum;
            using (Bitmap B = new Bitmap(Width, Height))
            {
                using (Graphics G = Graphics.FromImage(B))
                {
                    Rectangle R1 = new Rectangle(1, 1, Width - 2, Height - 2);
                    Rectangle R2 = new Rectangle(2, 2, V - 4, Height - 4);
                    Brush GB1 = new LinearGradientBrush(R1, C2, C3, 90.0F);
                    Brush GB2 = new LinearGradientBrush(R2, C3, C2, 30.0F);
                    G.FillRectangle(GB1, R1);
                    G.FillRectangle(GB2, R2);
                    // Draw.Gradient(G, C2, C3, 1, 1, Width - 2, Height - 2)
                    G.DrawRectangle(new Pen(C2), 1, 1, V - 3, Height - 3);
                    //  Draw.Gradient(G, C3, C2, 2, 2, V - 4, Height - 4)

                    G.DrawRectangle(new Pen(C1), 0, 0, Width - 1, Height - 1);
                    Bitmap B1 = B;
                    e.Graphics.DrawImage(B1, 0, 0);
                    /*
                     *  Draw.Gradient(G, C2, C3, 1, 1, Width - 2, Height - 2)
                    G.DrawRectangle(New Pen(C2), 1, 1, V - 3, Height - 3)
                    Draw.Gradient(G, C3, C2, 2, 2, V - 4, Height - 4)

                    G.DrawRectangle(New Pen(C1), 0, 0, Width - 1, Height - 1)

                    e.Graphics.DrawImage(B.Clone, 0, 0)
                     */

                }
            }
        }
    }
    class EB_Theme : System.Windows.Forms.GroupBox
    {
        private int _TitleHeight = 25;
        public int TitleHeight
        {
            get
            {
                return _TitleHeight;
            }
            set
            {
                if (value > Height)
                    value = Height;
                if (value < 2)
                    Height = 1;
                _TitleHeight = value;
                Invalidate();
            }
        }
        private HorizontalAlignment _TitleAlign = (HorizontalAlignment)2;
        public HorizontalAlignment TitleAlign
        {
            get
            {
                return _TitleAlign;
            }
            set
            {
                _TitleAlign = value;
                Invalidate();
            }
        }
        protected override void OnHandleCreated(EventArgs e)
        {
            Dock = (DockStyle)5;
            if (Parent.GetType() == typeof(Form))
            {//.FormBorderStyle = 0;
                //(ParentForm)Parent;
                this.FindForm().FormBorderStyle = 0;
                //Convert.ChangeType(Parent, typeof(Form));

            }

            base.OnHandleCreated(e);
        }
        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (new Rectangle(Parent.Location.X, Parent.Location.Y, Width - 1, _TitleHeight - 1).IntersectsWith(new Rectangle(MousePosition.X, MousePosition.Y, 1, 1)))
            {
                Capture = false;
                //Message  M = Message.Create(Parent.Handle, 161, 2, 0);
                Message M = Message.Create((IntPtr)Parent.Handle, 161, (IntPtr)2, (IntPtr)0);
                //DefWndProc(M);
                DefWndProc(ref M);
            }
            base.OnMouseDown(e);
        }
        Color C1 = Color.FromArgb(40, 218, 255), C2 = Color.FromArgb(63, 63, 63), C3 = Color.FromArgb(41, 41, 41);//(74, 74, 74)
        Color C4 = Color.FromArgb(27, 27, 27), C5 = Color.FromArgb(0, 0, 0, 0), C6 = Color.FromArgb(25, 255, 255, 255);
        protected override void OnPaintBackground(PaintEventArgs e)
        {
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            using (Bitmap B = new Bitmap(Width, Height))
            {
                using (Graphics G = Graphics.FromImage(B))
                {
                    G.Clear(C3);

                    //Draw.Gradient(G, C4, C3, 0, 0, Width, _TitleHeight)

                    SizeF S = G.MeasureString(Text + "1", Font);
                    float O = 6;
                    if (_TitleAlign == (HorizontalAlignment)2)
                    {
                        O = Width / 2 - S.Width / 2;
                    }
                    if (_TitleAlign == (HorizontalAlignment)1)
                    {
                        O = Width - S.Width - 6;
                    }
                    Rectangle R = new Rectangle((int)O, (_TitleHeight + 2) / 2 - (int)S.Height / 2, (int)S.Width, (int)S.Height);
                    using (Brush T = new LinearGradientBrush(R, C1, C3, LinearGradientMode.Vertical))
                    {
                        G.DrawString(Text, Font, T, R);
                    }

                    G.DrawLine(new Pen(C3), 0, 1, Width, 1);

                    // Draw.Blend(G, C5, C6, C5, 0.5, 0, 0, _TitleHeight + 1, Width, 1)
                    ColorBlend x = new ColorBlend();
                    Color[] temp = { C5, C6, C5 };
                    x.Colors = temp;
                    float[] temp2 = { 0.5F, 0, 0, _TitleHeight + 1, Width, 1 };
                    x.Positions = temp2;
                    /*
                    LinearGradientBrush B = new LinearGradientBrush(new Point(10, 110),
                                                        new Point(140, 110),
                                                        Color.White,
                                                        Color.Black);
                    B.InterpolationColors = C_Blend;
                    */
                    G.DrawLine(new Pen(C4), 0, _TitleHeight, Width, _TitleHeight);
                    G.DrawRectangle(new Pen(C4), 0, 0, Width - 1, Height - 1);
                    Bitmap B1 = B;
                    e.Graphics.DrawImage(B, 0, 0);
                }
            }
        }
    }
    class EB_TextBox : System.Windows.Forms.TextBox
    {
        public EB_TextBox()
        {
            ForeColor = Color.FromArgb(40, 218, 255);
            BackColor = Color.FromArgb(40, 218, 255);
            BorderStyle = BorderStyle.FixedSingle;
        }
        Color C1 = Color.FromArgb(31, 31, 31);
        Color C2 = Color.FromArgb(41, 41, 41);
        Color C3 = Color.FromArgb(51, 51, 51);
        Color C4 = Color.FromArgb(0, 0, 0, 0);
        Color C5 = Color.FromArgb(25, 255, 255, 255);
        protected override void OnPaintBackground(PaintEventArgs e)
        {
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            using (Bitmap B = new Bitmap(Width, Height))
            {
                using (Graphics G = Graphics.FromImage(B))
                {
                    Rectangle R1 = new Rectangle(0, 0, Width, Height / 2);
                    Rectangle R2 = new Rectangle(0, Height / 2, Width, Height);
                    G.DrawRectangle(new Pen(C1), 0, 0, Width - 1, Height - 1);



                    Brush GB1 = new LinearGradientBrush(R1, C3, C2, 40.0F);
                    Brush GB2 = new LinearGradientBrush(R2, C2, C3, 90.0F);
                    G.FillRectangle(GB1, R1);
                    G.FillRectangle(GB2, R2);
                    //Draw.Gradient(G, C2, C3, 1, 1, Width - 2, Height - 2);


                    Pen P2 = new Pen(Color.Black, 2);
                    G.DrawRectangle(P2, 0, 0, Width, Height);
                    SizeF O = G.MeasureString(Text, Font);
                    G.DrawString(Text, Font, new SolidBrush(ForeColor), Width / 2 - O.Width / 2, Height / 2 - O.Height / 2);

                    //Draw.Blend(G, C4, C5, C4, 0.5, 0, 1, 1, Width - 2, 1);
                    Bitmap B1 = B;
                    e.Graphics.DrawImage(B1, 0, 0);
                }
            }
        }
    }
}

