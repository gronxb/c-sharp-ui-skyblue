using System.Windows.Forms;
using System.Drawing;

namespace c_sharp_ui
{
    public class Form1 : Form
    {
        StyleButton btn;
        StyleTextBox id_box;
        StyleTextBox pw_box;

        public Form1()
        {
            this.Size = new Size(320, 400);
            this.BackColor = Color.White;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;

            this.id_box = new StyleTextBox(10,10,290,200);
            this.id_box.Multiline = false;
            this.id_box.Caption = "ID";
            this.pw_box = new StyleTextBox(10,75,290,200);
            this.pw_box.Multiline = false;
            this.pw_box.Caption = "PASSWORD";

            this.btn = new StyleButton(10,135,288,60);
            this.btn.Caption = "LOGIN";

            this.Controls.Add(this.id_box);
            this.Controls.Add(this.pw_box);
            this.Controls.Add(this.btn);
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