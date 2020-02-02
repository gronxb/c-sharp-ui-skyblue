using System;
using System.Drawing;
using System.Windows.Forms;
using System.Threading;

namespace c_sharp_ui
{
    public class StyleTextBox : Panel
    {
        private Rectangle rect;
        private TextBox txtBox = new TextBox();
        private int cx, cy;
        private bool m_leave_flag = false;

        private int ani_w = 2;
        bool p_Multiline = false;


        public bool Multiline
        {
            get
            {
                return this.p_Multiline;
            }
            set
            {
                this.p_Multiline = value;
                if(value == false)
                {
                    this.cy = 50;
                    this.Size = new Size(this.cx, cy);
                    this.rect = new Rectangle(0, 0, this.cx - 5, this.cy - 3);
                    this.txtBox.Size = new Size(this.cx - 15, this.cy);
                }
                this.txtBox.Multiline = value;
            }
        }

        public String Caption
        {
            get
            {
                return this.Text;
            }

            set
            {
                this.Text = value;
            }
        }

        public string GetText()
        {
            return this.txtBox.Text;
        }

        public void SetText(string val)
        {
            this.txtBox.Text = val;
        }

        public StyleTextBox(int x,int y,int cx,int cy)
        {

            this.Location = new Point(x, y);
            this.Size = new Size(cx, cy);
            this.cx = cx;
            this.cy = cy;
            this.rect = new Rectangle(0, 0, cx - 5, cy - 3);

            this.txtBox.Location = new Point(8, 23);
            this.txtBox.Size = new Size(cx - 20, cy - 40);
            this.txtBox.BorderStyle = BorderStyle.None;
            this.txtBox.TabStop = false;
            this.txtBox.Font = new Font(FontFamily.GenericSansSerif, 9);
            
            this.Paint += this.Panel_Paint;
            this.MouseEnter += this.Panel_MouseEnter;
            this.MouseLeave += this.Panel_MouseLeave;
            this.Click += this.Panel_Click;
            this.txtBox.Click += this.Panel_Click;

            this.Text = "StyleTextBox";
            this.Controls.Add(this.txtBox);
        }

        private static int fibo(int x)
        {
            if (x <= 2) return 1;
            return fibo(x - 2) + fibo(x - 1);
        }

        private void Ani_Line(object obj)
        {
            Pen color = (Pen)obj;
            int line_start = 0, line_end = 0;
            int updown_start = 0, updown_end = 0;
            int lr_start = 0, lr_end = 0;

            int[] break_freq = new int[6];

            this.ani_w = 2;

            for(int i =0; ;i++)
            {
                int break_stack = 0;
                for(int j = 0;j<break_freq.Length;j++)
                {
                    if (break_freq[j] == 1)
                        break_stack++;
                }
                if (break_stack == 6)
                {
                    break;
                }
                line_start = this.rect.Size.Width / 2 - ani_w;
                line_end = this.rect.Size.Width / 2 + ani_w;

                updown_start = this.rect.Size.Width / 2 - ani_w;
                updown_end = this.rect.Size.Width / 2 + ani_w;

                lr_start = this.rect.Size.Height / 2 - ani_w;
                lr_end = this.rect.Size.Height / 2 + ani_w;
                if(line_start < 5)
                {
                    break_freq[0] = 1;
                    line_start = 5;
                }
                if(line_end > this.rect.Size.Width - 5)
                {
                    break_freq[1] = 1; 
                    line_end = this.rect.Size.Width - 5;
                }
                if (updown_start <= 0)
                {
                    break_freq[2] = 1;
                    updown_start = 0;
                }
                if (updown_end >= this.rect.Size.Width)
                {
                    break_freq[3] = 1;
                    updown_end = this.rect.Size.Width;
                }
                if (lr_start <= 0)
                {
                    break_freq[4] = 1;
                    lr_start = 0;
                }
                if (lr_end >= this.rect.Size.Height)
                {
                    break_freq[5] = 1;
                    lr_end = this.rect.Size.Height;
                }
                try
                {
                    Graphics g = this.CreateGraphics();
                    g.DrawLine(color, line_start, this.rect.Height - 7, line_end, this.rect.Height - 7);

                    g.DrawLine(color, updown_start, 0, updown_end,0);
                    g.DrawLine(color, updown_start, this.rect.Height, updown_end, this.rect.Height);

                    g.DrawLine(color, 0, lr_start, 0, lr_end);
                    g.DrawLine(color, this.rect.Width, lr_start, this.rect.Width, lr_end);
                }
                catch(System.InvalidOperationException)
                {
                    continue;
                }
                ani_w = fibo(i);
                Thread.Sleep(11);
            }
        }

        private void Panel_Click(object sender,EventArgs e)
        {
            this.txtBox.Focus();

            if (this.m_leave_flag == true) return;
            this.m_leave_flag = true;

            this.Paint_String(Brushes.SkyBlue);

            Thread ani_thread = new Thread(this.Ani_Line);
            ani_thread.IsBackground = true;
            ani_thread.Start(Pens.SkyBlue);
        }
        private void Paint_String(Brush brush)
        {
            Graphics g = this.CreateGraphics();
            Font normalFont = new Font(FontFamily.GenericSansSerif, 8);
            g.DrawString(this.Text,normalFont, brush, new PointF(5, 5));
        }

        private void Panel_MouseEnter(object sender,EventArgs e)
        {
           //Done
        }

        private void Panel_MouseLeave(object sender,EventArgs e)
        {
            if (this.GetChildAtPoint(this.PointToClient(Cursor.Position)) != null)
                return;
            this.txtBox.Enabled = false;
            this.txtBox.Enabled = true;

            if (this.m_leave_flag == false) return;
            m_leave_flag = false;
            this.Paint_String(new SolidBrush(Color.FromArgb(207, 236, 248)));

            Thread ani_thread = new Thread(this.Ani_Line);
            ani_thread.IsBackground = true;
            ani_thread.Start(new Pen(Color.FromArgb(207, 236, 248)));
        }

        private void Panel_Paint(object sender,PaintEventArgs e)
        {
            e.Graphics.DrawRectangle(new Pen(Color.FromArgb(207,236,248)), rect);
            e.Graphics.DrawLine(new Pen(Color.FromArgb(207, 236, 248)), 5, this.rect.Size.Height - 7, this.rect.Size.Width - 5, this.rect.Size.Height - 7);
            this.Paint_String(new SolidBrush(Color.FromArgb(207, 236, 248)));
        }

    }

    public class StyleButton : Panel
    {
        private Rectangle rect;
        private const int FONTSIZE = 12;
        private Color SkyBlue_Light = Color.FromArgb(207, 236, 248);
        public String Caption
        {
            get
            {
                return this.Text;
            }
            set
            {
                this.Text = value;
            }
        }

        public StyleButton(int x,int y,int cx,int cy) // Locatin = (x,y) Size = (cx,cy)
        {

            this.Location = new Point(x, y);
            this.Size = new Size(cx, cy);

            this.rect = new Rectangle(0, 0, cx,cy);

            this.Click += this.Panel_Click;
            this.Paint += this.Panel_Paint;
            this.MouseEnter += this.Panel_MouseEnter;
            this.MouseLeave += this.Panel_MouseLeave;
            this.MouseDown += this.Panel_MouseDown;
            this.MouseUp += this.Panel_MouseUp;

            this.Text = "StyleButton";
        }

        private void Panel_Click(object sender, EventArgs e)
        {
            //done
        }

        private void Paint_String()
        {
            Graphics g = this.CreateGraphics();
            Font normalFont = new Font(FontFamily.GenericSansSerif, FONTSIZE);
            SizeF outputSize = g.MeasureString(this.Text, normalFont);
            g.DrawString(this.Text, normalFont, Brushes.White, new PointF((this.Size.Width - outputSize.Width) / 2, (this.Size.Height - outputSize.Height) / 2));
        }

        private void Panel_MouseEnter(object sender,EventArgs e)
        {
            Graphics g = this.CreateGraphics();
            g.FillRectangle(Brushes.SkyBlue, rect);
            this.Paint_String();
        }

        private void Panel_MouseLeave(object sender,EventArgs e)
        {
            Graphics g = this.CreateGraphics();
            g.FillRectangle(Brushes.LightSkyBlue, rect);
            this.Paint_String();
        }

        private void Panel_MouseDown(object sender,EventArgs e)
        {
            Graphics g = this.CreateGraphics();
            g.FillRectangle(Brushes.CornflowerBlue, rect);
            this.Paint_String();
        }
        private void Panel_MouseUp(object sender, EventArgs e)
        {
            Graphics g = this.CreateGraphics();
            g.FillRectangle(Brushes.LightSkyBlue, rect);
            this.Paint_String();
        }
        private void Panel_Paint(object sender,PaintEventArgs e)
        {
            e.Graphics.FillRectangle(Brushes.LightSkyBlue, rect);
            this.Paint_String();
        }
    }
}
