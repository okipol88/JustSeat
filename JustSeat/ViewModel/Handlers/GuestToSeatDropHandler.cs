using GongSolutions.Wpf.DragDrop;
using JustSeat.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace JustSeat.ViewModel.Handlers
{
    internal class GuestDroppedEventArgs:EventArgs
    {
        public GuestDroppedEventArgs(Guest targetChairGuest, Guest sourceChairGuest)
        {
            TargetChairGuest = targetChairGuest ?? throw new ArgumentNullException(nameof(targetChairGuest));
            SourceChairGuest = sourceChairGuest;
        }

        public Guest TargetChairGuest { get; private set; }
        public Guest SourceChairGuest { get; private set; }
    }

    public class GuestToSeatDropHandler : IDropTarget
    {
        internal event EventHandler<GuestDroppedEventArgs> GuestDropped;

        public void DragOver(IDropInfo dropInfo)
        {
            // sketchy - But i dont know the nuts and bolts of the framework to assign the target item in a clean way
            Chair chair = ExtractChair(dropInfo);
            if (chair == null)
                return;

            dropInfo.DropTargetAdorner = typeof(DropTargetHighlightAdorner);
            dropInfo.Effects = DragDropEffects.Move;
        }

        private static Chair ExtractChair(IDropInfo dropInfo)
        {
            return (dropInfo.VisualTarget as FrameworkElement)?.DataContext as Chair;
        }

        public void Drop(IDropInfo dropInfo)
        {            
            // sketchy - But i dont know the nuts and bolts of the framework to assign the target item in a clean way
            Chair chair = ExtractChair(dropInfo);
            var guest = dropInfo.Data as Guest;
            if (chair == null || guest == null)
                return;

            var personOnChair = chair.Person;
            chair.Person = guest;

            var collection = dropInfo.DragInfo.SourceCollection as ICollection<Guest>;
            if (collection != null)
                collection.Remove(guest);

            GuestDropped?.Invoke(this, new GuestDroppedEventArgs(guest, personOnChair));
        }
    }
}