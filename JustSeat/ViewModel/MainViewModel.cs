using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GongSolutions.Wpf.DragDrop;
using JustSeat.Model;
using JustSeat.ViewModel.Handlers;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

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

                RemoveGuestCommand = new RelayCommand<Chair>(
                (c) =>
                {
                    var person = c.Person;
                    if (c == null || person == null)
                        return;
                    c.Person = null;

                    Guests.Add(person);
                },
                (c) =>
                {
                    if (c == null)
                        return false;

                    return c.Person != null;
                });
            }

            var dropHandler = new GuestToSeatDropHandler();
            dropHandler.GuestDropped += (o, e) =>
            {
                RemoveGuestCommand.RaiseCanExecuteChanged();
            };
            GuestOnChairDropHandler = dropHandler;
        }

        public ObservableCollection<Guest> Guests { get; set; } = new ObservableCollection<Guest>();
        public ObservableCollection<ICanvasDisplayItem> Items { get; set; } = new ObservableCollection<ICanvasDisplayItem>();

        public IDropTarget GuestOnChairDropHandler { get; private set; }

        public RelayCommand<Chair> RemoveGuestCommand { get; private set; }

        public RelayCommand<Table> AddTopChairCommand { get; } = new RelayCommand<Table>((table) => { table.TopChairs.Add(new Chair()); });
        public RelayCommand<Table> AddBottomChairCommand { get; } = new RelayCommand<Table>((table) => { table.BottomChairs.Add(new Chair()); });
        public RelayCommand<Table> AddLeftChairCommand { get; } = new RelayCommand<Table>((table) => { table.LeftChairs.Add(new Chair()); });
        public RelayCommand<Table> AddRighthairCommand { get; } = new RelayCommand<Table>((table) => { table.RightChairs.Add(new Chair()); });
    }
}