using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApp2
{

    public partial class Form2 : Form
    {
        Helper h = new Helper();
        private Point _mouseMovePos = new Point(40, 40);
        private Point _cursorPos;
        private int _mX;
        private int _mY;


        public Form2()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Bitmap bufl = new Bitmap(panel1.Width, panel1.Height);
            using (Graphics g = Graphics.FromImage(bufl))
            {
                g.FillRectangle(Brushes.White, new Rectangle(0, 0, panel1.Width, panel1.Height));

                //Point lastPos = _cursorPos;
                _cursorPos = _mouseMovePos;


                using (Pen pen = new Pen(Color.LightSeaGreen, 2))
                {
                    pen.DashStyle = DashStyle.Dot;
                    g.DrawLine(pen, _cursorPos.X, 0, _cursorPos.X, this.Height);
                    g.DrawLine(pen, 0, _cursorPos.Y, this.Width, _cursorPos.Y);
                }

                panel1.CreateGraphics().DrawImageUnscaled(bufl, 0, 0);
            }
        }

        public static void MakeGuiRectangle(ref Rectangle rect)
        {
            if (rect.Width < 0)
            {
                rect.X += rect.Width;
                rect.Width = -rect.Width;
            }
            if (rect.Height < 0)
            {
                rect.Y += rect.Height;
                rect.Height = -rect.Height;
            }
        }
        public static Rectangle GetGuiRectangle(int x, int y, int w, int h)
        {
            Rectangle rect = new Rectangle(x, y, w, h);
            MakeGuiRectangle(ref rect);
            return rect;
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            _mouseMovePos = Helper.FixMouseCoordinates(User32.GetCursorLocation());
            _mouseMovePos = Helper.GetLocationRelativeToScreenBounds(_mouseMovePos);

        }


        private void animation()
        {
            Point lastPos = _cursorPos;
            _cursorPos = _mouseMovePos;

            Rectangle invalidateRectangle;
            int x1 = Math.Min(_mX, lastPos.X);
            int x2 = Math.Max(_mX, lastPos.X);
            int y1 = Math.Min(_mY, lastPos.Y);
            int y2 = Math.Max(_mY, lastPos.Y);
            x1 = Math.Min(x1, _cursorPos.X);
            x2 = Math.Max(x2, _cursorPos.X);
            y1 = Math.Min(y1, _cursorPos.Y);
            y2 = Math.Max(y2, _cursorPos.Y);

            // Safety correction
            x2 += 2;
            y2 += 2;

            int textForWidth = Math.Max(Math.Abs(_mX - _cursorPos.X), Math.Abs(_mX - lastPos.X));
            int textForHeight = Math.Max(Math.Abs(_mY - _cursorPos.Y), Math.Abs(_mY - lastPos.Y));

            using (Font rulerFont = new Font(FontFamily.GenericSansSerif, 8))
            {
                Size measureWidth = TextRenderer.MeasureText(textForWidth.ToString(CultureInfo.InvariantCulture), rulerFont);
                x1 -= measureWidth.Width + 15;

                Size measureHeight = TextRenderer.MeasureText(textForHeight.ToString(CultureInfo.InvariantCulture), rulerFont);
                y1 -= measureHeight.Height + 10;
            }
            invalidateRectangle = new Rectangle(x1, y1, x2 - x1, y2 - y1);
            Invalidate(invalidateRectangle);

            //const int safetySize = 30;
        }
    }
}
