﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace WindowsFormsApp2.helper
{/// <summary>
 /// Helper class for creating rectangles with positive dimensions, regardless of input coordinates
 /// </summary>
	public static class GuiRectangle
	{

		public static Rectangle GetGuiRectangle(int x, int y, int w, int h)
		{
			Rectangle rect = new Rectangle(x, y, w, h);
			MakeGuiRectangle(ref rect);
			return rect;
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

		public static RectangleF GetGuiRectangleF(float x, float y, float w, float h)
		{
			RectangleF rect = new RectangleF(x, y, w, h);
			MakeGuiRectangleF(ref rect);
			return rect;
		}

		public static void MakeGuiRectangleF(ref RectangleF rect)
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
	}
}
