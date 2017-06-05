using System;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace JustSeat.Controls
{
    public class AutoResizeCanvas : Canvas
    {
        private DependencyPropertyDescriptor LeftPropertyDescriptor = DependencyPropertyDescriptor.FromProperty(Canvas.LeftProperty, typeof(Canvas));
        private DependencyPropertyDescriptor TopPropertyDescriptor = DependencyPropertyDescriptor.FromProperty(Canvas.TopProperty, typeof(Canvas));

        protected override void OnVisualChildrenChanged(DependencyObject visualAdded, DependencyObject visualRemoved)
        {
            base.OnVisualChildrenChanged(visualAdded, visualRemoved);

            if (visualAdded != null)
            {
                LeftPropertyDescriptor.AddValueChanged(visualAdded, OnElementLayoutChanged);
                TopPropertyDescriptor.AddValueChanged(visualAdded, OnElementLayoutChanged);
            }

            if (visualRemoved != null)
            {
                LeftPropertyDescriptor.AddValueChanged(visualRemoved, OnElementLayoutChanged);
                TopPropertyDescriptor.AddValueChanged(visualRemoved, OnElementLayoutChanged);
            }
        }

        private void OnElementLayoutChanged(object sender, EventArgs e)
        {
            InvalidateMeasure();
        }

        protected override Size MeasureOverride(Size constraint)
        {
            double bottomMost = 0d;
            double rightMost = 0d;

            foreach (object obj in Children)
            {
                FrameworkElement child = obj as FrameworkElement;

                if (child != null)
                {
                    child.Measure(constraint);

                    bottomMost = Math.Max(bottomMost, GetTop(child) + child.DesiredSize.Height);
                    rightMost = Math.Max(rightMost, GetLeft(child) + child.DesiredSize.Width);
                }
            }
            return new Size(rightMost, bottomMost);
        }

    }
}
