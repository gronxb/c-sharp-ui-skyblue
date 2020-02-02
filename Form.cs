using System;
using System.Windows.Forms;
using System.Drawing;

namespace c_sharp_ui
{
    public class Form1 : Form
    {
        StyleButton btn;
        StyleTextBox id_box;
        StyleTextBox pw_box;

        Bitmap animatedImage = new Bitmap("whale.gif");
        bool currentlyAnimating = false;

        public void AnimateImage()
        {
            if (!currentlyAnimating)
            {

                //Begin the animation only once.
                ImageAnimator.Animate(animatedImage, new EventHandler(this.OnFrameChanged));
                currentlyAnimating = true;
            }
        }

        private void OnFrameChanged(object o, EventArgs e)
        {

            //Force a call to the Paint event handler.
            this.Invalidate();
        }

        public Form1()
        {
            this.SetStyle(ControlStyles.DoubleBuffer, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.UserPaint, true);
	    
            this.Text = "SkyBlue UI";
            this.Size = new Size(400, 540);
            this.BackColor = Color.White;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;

            this.id_box = new StyleTextBox(10,310,380,200);
            this.id_box.Multiline = false;
            this.id_box.Caption = "ID";
            this.pw_box = new StyleTextBox(10,375,380,200);
            this.pw_box.Multiline = false;
            this.pw_box.Caption = "PASSWORD";

            this.btn = new StyleButton(10,435,378,60);
            this.btn.Caption = "LOGIN";
	    this.btn.Click += this.btn_Click;

         
            this.Controls.Add(this.id_box);
            this.Controls.Add(this.pw_box);
            this.Controls.Add(this.btn);
            this.Paint += this.Form1_Paint;
        }

	private void btn_Click(object sender,EventArgs e)
	{
		MessageBox.Show(String.Format("[{0}] Hello!!",this.id_box.GetText()));
	}

        private void Form1_Paint(object sender,PaintEventArgs e)
        {
            AnimateImage();

            //Get the next frame ready for rendering.
            ImageAnimator.UpdateFrames();

            //Draw the next frame in the animation.
            e.Graphics.DrawImage(this.animatedImage,0,0);
        }
    }

    public class MainClass
    {
        public static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}