﻿using System;
using GoddamnConsole.Drawing;

namespace GoddamnConsole.Controls
{
    public class ScrollViewer : ContentControl
    {
        public ScrollViewer()
        {
            Focusable = true;
        }

        private int _scrollX;
        private int _scrollY;

        protected override void OnKeyPress(ConsoleKeyInfo key)
        {
            if (key.Key == ConsoleKey.UpArrow && _scrollY < 0) _scrollY++;
            if (key.Key == ConsoleKey.DownArrow && 
                Content.AssumedHeight - ActualHeight >= -_scrollY) _scrollY--;
            if (key.Key == ConsoleKey.LeftArrow && _scrollX < 0)
            {
                _scrollX++;
            }
            if (key.Key == ConsoleKey.RightArrow &&
                Content.AssumedWidth - ActualWidth >= -_scrollX)
            {
                _scrollX--;
            }
            Invalidate();
        }

        public override void Render(DrawingContext context)
        {
            if (_scrollY > 0) _scrollY = 0;
            else
            {
                var contentHeight = Content.AssumedHeight;
                if (contentHeight < ActualHeight) _scrollY = 0;
                else if (contentHeight - ActualHeight < -_scrollY)
                    _scrollY = -(contentHeight - ActualHeight);
            }
            if (_scrollX > 0) _scrollX = 0;
            else
            {
                var contentWidth = Content.AssumedWidth;
                if (contentWidth < ActualWidth) _scrollX = 0;
                else if (contentWidth - ActualWidth < -_scrollX)
                    _scrollX = -(contentWidth - ActualWidth);
            }
            var scrolled = context.Scroll(new Point(_scrollX, _scrollY));
            Content.Render(scrolled);
        }
        
        public override Size MeasureChild(Control child)
        {
            return new Size(child.AssumedWidth, child.AssumedHeight);
        }
    }
}
