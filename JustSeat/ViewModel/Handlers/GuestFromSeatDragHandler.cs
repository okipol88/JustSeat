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
    class GuestFromSeatDragHandler : IDragSource
    {
        public bool CanStartDrag(IDragInfo dragInfo)
        {
            var canStart = (dragInfo.SourceItem as Chair)?.Person != null;

            return canStart;
        }

        public void DragCancelled()
        {
           
        }

        public void Dropped(IDropInfo dropInfo)
        {
            var chair = (dropInfo.DragInfo.SourceItem as Chair);
            if (chair != null)
                chair.Person = null;
        }

        public void StartDrag(IDragInfo dragInfo)
        {
            dragInfo.Data = (dragInfo.SourceItem as Chair).Person;

            dragInfo.Effects = (dragInfo.Data != null) ?
                           DragDropEffects.Copy | DragDropEffects.Move :
                           DragDropEffects.None;
        }

        public bool TryCatchOccurredException(Exception exception)
        {

            return false;
        }
    }
}
