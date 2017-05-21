using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interactivity;
using System.Windows.Media;

namespace JustSeat.Behaviors
{
    public class DragBehavior: Behavior<UIElement>
    {
        System.Windows.Point? _dragStart;
        private Canvas _canvas;

        protected override void OnAttached()
        {
            base.OnAttached();

            AssociatedObject.MouseLeftButtonDown += ElementOnMouseLeftButtonDown;
            AssociatedObject.MouseLeftButtonUp += ElementOnMouseLeftButtonUp;
            AssociatedObject.MouseMove += ElementOnMouseMove;

            _canvas = GetParentCanvas();
            if (_canvas == null)
                throw new InvalidOperationException("Canvas could not be found");
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();

            AssociatedObject.MouseLeftButtonDown -= ElementOnMouseLeftButtonDown;
            AssociatedObject.MouseLeftButtonUp -= ElementOnMouseLeftButtonUp;
            AssociatedObject.MouseMove -= ElementOnMouseMove;
        }

        private Canvas GetParentCanvas()
        {
            Canvas canvas = null;
            var parentItem = AssociatedObject as DependencyObject;
            while (canvas == null && parentItem != null)
            {
                DependencyObject parent = VisualTreeHelper.GetParent(parentItem);
                if (parent is Canvas)
                    canvas = (Canvas)parent;
                else
                    parentItem = parent;
            }

            return canvas;
        }

        private void ElementOnMouseLeftButtonDown(object sender, MouseButtonEventArgs mouseButtonEventArgs)
        {
            _dragStart = mouseButtonEventArgs.GetPosition(AssociatedObject);
            AssociatedObject.CaptureMouse();
        }

        private void ElementOnMouseLeftButtonUp(object sender, MouseButtonEventArgs mouseButtonEventArgs)
        {
            _dragStart = null;
            AssociatedObject.ReleaseMouseCapture();
        }

        private void ElementOnMouseMove(object sender, MouseEventArgs mouseEventArgs)
        {
            var start = _dragStart;
            if (start != null && mouseEventArgs.LeftButton == MouseButtonState.Pressed)
            {
                var p2 = mouseEventArgs.GetPosition(_canvas);
                Canvas.SetLeft(AssociatedObject, p2.X -_dragStart.Value.X);
                Canvas.SetTop(AssociatedObject, p2.Y - _dragStart.Value.Y);
            }
        }
    }
}
