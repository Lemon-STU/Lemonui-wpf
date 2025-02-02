﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace LM.WPF.Controls
{
    public class LMBackgroundMediaElement:MediaElement
    {
        private bool isBufferOver = false;

        public ImageSource CoverImage
        {
            get { return (ImageSource)GetValue(CoverImageProperty); }
            set { SetValue(CoverImageProperty, value); }
        }
        public static readonly DependencyProperty CoverImageProperty =
            DependencyProperty.Register("CoverImage", typeof(ImageSource), typeof(LMBackgroundMediaElement));


        public LMBackgroundMediaElement() : base()
        {
            this.MediaEnded += BackgroundMediaElement_MediaEnded;
            this.MediaOpened += BackgroundMediaElement_MediaOpened;
            this.RenderTransformOrigin = new Point(.5,.5);
        }

        private void BackgroundMediaElement_MediaOpened(object sender, RoutedEventArgs e)
        {
            isBufferOver = true;
        }

        private void BackgroundMediaElement_MediaEnded(object sender, RoutedEventArgs e)
        {
            this.Position=TimeSpan.Zero;
        }
        protected override void OnRender(DrawingContext drawingContext)
        {
            if(!isBufferOver)
            {              
                drawingContext.DrawRectangle(Brushes.Gray, new Pen(Brushes.Black, 1), new Rect(-this.Width/2, -this.Height/2, this.Width, this.Height));
                drawingContext.DrawImage(CoverImage, new Rect(-this.Width / 2, -this.Height / 2, this.Width, this.Height));
            }
            else
            {
                base.OnRender(drawingContext);
            }
        }
    }
}
