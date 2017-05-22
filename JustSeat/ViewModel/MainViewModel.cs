using GalaSoft.MvvmLight;
using GongSolutions.Wpf.DragDrop;
using JustSeat.Model;
using JustSeat.ViewModel.Handlers;
using System.Collections.ObjectModel;
using System.Linq;

namespace JustSeat.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// <para>
    /// You can also use Blend to data bind with the tool's support.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel()
        {
            //if (IsInDesignMode)
            {
                var side = 80;
                var posMultiplier = 90;

                Enumerable.Range(0, 5).ToList().ForEach(i =>
                 {

                     Guests.Add(new Guest() { Name = "Guest " + (i + 1) });
                     Items.Add(new Table() { X = i * posMultiplier, Y = i * posMultiplier, Width = side, Length = side });
                 });

                ((Table)Items[0]).TopChairs[0].Person = Guests.First();
            }
        }

        public ObservableCollection<Guest> Guests { get; set; } = new ObservableCollection<Guest>();
        public ObservableCollection<ICanvasDisplayItem> Items { get; set; } = new ObservableCollection<ICanvasDisplayItem>();
        public ObservableCollection<ICanvasDisplayItem> Items2 { get; set; } = new ObservableCollection<ICanvasDisplayItem>();

        public IDropTarget GuestOnChairDropHandler { get; set; } = new GuestToSeatDropHandler();
        
    }
}