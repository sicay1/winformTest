using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApp2
{
    public class Helper
    {
		
		
		private enum FixMode { None, Initiated, Horizontal, Vertical };
		private static FixMode _fixMode = FixMode.None;
		private static Point _previousMousePos = Point.Empty;
		public static Point FixMouseCoordinates(Point currentMouse)
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
