using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interactivity;
using System.Windows.Media;

namespace JustSeat.Behaviors
{
    public class DragBehavior: Behavior<UIElement>
    {
        public readonly TranslateTransform Transform = new TranslateTransform();
        private System.Windows.Point _elementStartPosition2;
        private System.Windows.Point _mouseStartPosition2;

        protected override void OnAttached()
        {
            base.OnAttached();

            AssociatedObject.MouseLeftButtonDown += ElementOnMouseLeftButtonDown;
            AssociatedObject.MouseLeftButtonUp += ElementOnMouseLeftButtonUp;
            AssociatedObject.MouseMove += ElementOnMouseMove;

            AssociatedObject.RenderTransform = Transform;
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();

            AssociatedObject.MouseLeftButtonDown -= ElementOnMouseLeftButtonDown;
            AssociatedObject.MouseLeftButtonUp -= ElementOnMouseLeftButtonUp;
            AssociatedObject.MouseMove -= ElementOnMouseMove;
        }

        private void ElementOnMouseLeftButtonDown(object sender, MouseButtonEventArgs mouseButtonEventArgs)
        {
            var parent = Application.Current.MainWindow;
            _mouseStartPosition2 = mouseButtonEventArgs.GetPosition(parent);
            ((UIElement)sender).CaptureMouse();

            Debug.WriteLine("Starting position {0}", _mouseStartPosition2);
        }

        private void ElementOnMouseLeftButtonUp(object sender, MouseButtonEventArgs mouseButtonEventArgs)
        {
            ((UIElement)sender).ReleaseMouseCapture();
            _elementStartPosition2.X = Transform.X;
            _elementStartPosition2.Y = Transform.Y;
        }

        private void ElementOnMouseMove(object sender, MouseEventArgs mouseEventArgs)
        {
            var parent = Application.Current.MainWindow;
            var mousePos = mouseEventArgs.GetPosition(parent);
            var diff = (mousePos - _mouseStartPosition2);
            if (!((UIElement)sender).IsMouseCaptured) return;
            Transform.X = _elementStartPosition2.X + diff.X;
            Transform.Y = _elementStartPosition2.Y + diff.Y;

            Debug.WriteLine("Go to position {0}, {1}", Transform.X, Transform.Y);
        }
    }
}
