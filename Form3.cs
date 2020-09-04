using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApp2
{
    public partial class Form3 : Form
    {
        public Point _mouseMovePos = new Point(40, 40);
        private Point _cursorPos;
        private enum FixMode { None, Initiated, Horizontal, Vertical };
        private FixMode _fixMode = FixMode.None;
        private Point _previousMousePos = Point.Empty;
        //private Rectangle _captureRect = Rectangle.Empty;
        private Rectangle _captureRect = new Rectangle(0, 0, 800, 600);
        private static readonly Brush GreenOverlayBrush = new SolidBrush(Color.FromArgb(50, Color.MediumSeaGreen));
        private static readonly Pen OverlayPen = new Pen(Color.FromArgb(50, Color.Black));
        public Form3()
        {
            InitializeComponent();
        }

        private void panel1_MouseClick(object sender, MouseEventArgs e)
        {
            _mouseMovePos = FixMouseCoordinates(User32.GetCursorLocation());
            _mouseMovePos = GetLocationRelativeToScreenBounds(_mouseMovePos);
            this.Close();
        }


        private Point FixMouseCoordinates(Point currentMouse)
        {
            if (_fixMode == FixMode.Initiated)
            {
                if (_previousMousePos.X != currentMouse.X)
                {
                    _fixMode = FixMode.Vertical;
                }
                else if (_previousMousePos.Y != currentMouse.Y)
                {
                    _fixMode = FixMode.Horizontal;
                }
            }
            else if (_fixMode == FixMode.Vertical)
            {
                currentMouse = new Point(currentMouse.X, _previousMousePos.Y);
            }
            else if (_fixMode == FixMode.Horizontal)
            {
                currentMouse = new Point(_previousMousePos.X, currentMouse.Y);
            }
            _previousMousePos = currentMouse;
            return currentMouse;
        }


        /// <summary>
        /// Converts locationRelativeToScreenOrigin to be relative to top left corner of all screen bounds, which might
        /// be different in multiscreen setups. This implementation
        /// can conveniently be used when the cursor location is needed to deal with a fullscreen bitmap.
        /// </summary>
        /// <param name="locationRelativeToScreenOrigin"></param>
        /// <returns></returns>
        public static Point GetLocationRelativeToScreenBounds(Point locationRelativeToScreenOrigin)
        {
            Point ret = locationRelativeToScreenOrigin;
            Rectangle bounds = GetScreenBounds();
            ret.Offset(-bounds.X, -bounds.Y);
            return ret;
        }

        /////////////////////////////////
        /// <summary>
        /// Get the bounds of all screens combined.
        /// </summary>
        /// <returns>A Rectangle of the bounds of the entire display area.</returns>
        public static Rectangle GetScreenBounds()
        {
            int left = 0, top = 0, bottom = 0, right = 0;
            foreach (Screen screen in Screen.AllScreens)
            {
                left = Math.Min(left, screen.Bounds.X);
                top = Math.Min(top, screen.Bounds.Y);
                int screenAbsRight = screen.Bounds.X + screen.Bounds.Width;
                int screenAbsBottom = screen.Bounds.Y + screen.Bounds.Height;
                right = Math.Max(right, screenAbsRight);
                bottom = Math.Max(bottom, screenAbsBottom);
            }
            return new Rectangle(left, top, (right + Math.Abs(left)), (bottom + Math.Abs(top)));
        }
    }
}
