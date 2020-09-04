using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApp2
{
    public partial class FormCapturePicutre : Form
    {
        public FormCapturePicutre()
        {
            InitializeComponent();
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            _mouseMovePos = Helper.FixMouseCoordinates(User32.GetCursorLocation());
            _mouseMovePos = Helper.GetLocationRelativeToScreenBounds(_mouseMovePos);
            rec.Location = _mouseMovePos;
        }

        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            _mouseMovePos = Helper.FixMouseCoordinates(User32.GetCursorLocation());
            _mouseMovePos = Helper.GetLocationRelativeToScreenBounds(_mouseMovePos);
            var w = _mouseMovePos.X - rec.X;
            var h = _mouseMovePos.Y - rec.Y;
            rec.Width = w;
            rec.Height = h;
            this.Close();
        }
    }
}
