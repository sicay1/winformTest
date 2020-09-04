using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp2.helper;

namespace WindowsFormsApp2
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //action to take picture - rectangle
            this.Hide();
            Form2 f2 = new Form2();
            f2.FormClosed += (s, args) => this.Show();
            f2.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {

            this.Width = 300;
            this.Height = 300;
            Form3 f3 = new Form3();

            f3.FormClosed += (s, args) =>
            {
                //Point a = new Point();
                Point a = f3._mouseMovePos;
                this.label1.Text = $"X: {a.X} , Y: {a.Y}";
                this.ClientSize = new System.Drawing.Size(800, 450);
                this.Show();
            };

            f3.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Width = 300;
            this.Height = 300;
            FormCapturePicutre f4 = new FormCapturePicutre();

            f4.FormClosed += (s, args) =>
            {
                Rectangle rec = f4.rec;
                this.label2.Text = $"X: {rec.X} , Y: {rec.Y}, width: {rec.Width}, height: {rec.Height}";
                var bit = ScreenCapture.CaptureRectangle(rec);
                pictureBox1.Image = bit;
                pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
                this.ClientSize = new System.Drawing.Size(800, 450);
                this.Show();
            };

            f4.Show();
        }
    }
}
