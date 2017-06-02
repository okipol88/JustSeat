using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GongSolutions.Wpf.DragDrop;
using JustSeat.Model;
using JustSeat.Parameters;
using JustSeat.ViewModel.Handlers;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using System;
using JustSeat.Commands;
using JustSeat.Commands.Model;

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
        int side = 80;
        private double _zoomLevel = 1;
        private Guest _newGuest;

        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel()
        {
            var posMultiplier = 90;
            if (IsInDesignMode)
            {

                Enumerable.Range(0, 1).ToList().ForEach(i =>
                {

                    Guests.Add(new Guest() { Name = "Guest " + (i + 1) });
                    Items.Add(new Table() { X = i * posMultiplier, Y = i * posMultiplier, Width = side, Length = side });
                });

                ((Table)Items[0]).TopChairs[0].Person = Guests.First();
            }

            AddItemHandlingAction();

            AddDropHandler();

            AddMouseWheelZoomHandling();
        }

        private void AddMouseWheelZoomHandling()
        {
            MouseScrollCommand = new RelayCommand<MouseWheelInfo>((mouseInfo) =>
            {
                if (mouseInfo.Delta > 0)
                    ZoomLevel++;
                else if (mouseInfo.Delta < 0)
                {
                    if (ZoomLevel <= 1)
                        ZoomLevel -= 0.1;
                    else ZoomLevel -= 1;
                }
            });
        }

        private void AddItemHandlingAction()
        {
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

            RemovePersonOrChairCommand = new RelayCommand<ChairRemovalParameter>((param) =>
            {
                if (param == null)
                    return;

                if (param.ChairToRemove.Person != null)
                {
                    Guests.Add(param.ChairToRemove.Person);
                    param.ChairToRemove.Person = null;
                }
                else
                {
                    param.Table.TopChairs.Remove(param.ChairToRemove);
                    param.Table.BottomChairs.Remove(param.ChairToRemove);
                    param.Table.LeftChairs.Remove(param.ChairToRemove);
                    param.Table.RightChairs.Remove(param.ChairToRemove);
                }

            });

            AddTableCommand = new RelayCommand(() =>
            {
                AddNewTable();
            });

            RemoveTableCommand = new RelayCommand<Table>((table) =>
            {
                var items = table.LeftChairs.ToList();
                items.AddRange(table.TopChairs);
                items.AddRange(table.BottomChairs);
                items.AddRange(table.RightChairs);

                items.Where(x => x.Person != null).Select(x => x.Person)
                .ToList().ForEach(p => Guests.Add(p));

                Items.Remove(table);
            });

            CreateNewGuestScaffold();
            AddNewGuestCommand = new RelayCommand<Guest>(g =>
            {
                Guests.Add(g);
                g.PropertyChanged -= _newGuest_PropertyChanged;

                CreateNewGuestScaffold();
                AddNewGuestCommand.RaiseCanExecuteChanged();
            }, 
            g => 
            {
                return g != null && !string.IsNullOrWhiteSpace(g.Name) && !string.IsNullOrWhiteSpace(g.Surname);
            });
        }

        private void CreateNewGuestScaffold()
        {

            _newGuest = new Guest();
            _newGuest.PropertyChanged += _newGuest_PropertyChanged;
            RaisePropertyChanged(() => NewGuest);
        }

        private void _newGuest_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            AddNewGuestCommand.RaiseCanExecuteChanged();
        }

        private void AddDropHandler()
        {
            var dropHandler = new GuestToSeatDropHandler();
            dropHandler.GuestDropped += (o, e) =>
            {
                RemoveGuestCommand.RaiseCanExecuteChanged();
                RemovePersonOrChairCommand.RaiseCanExecuteChanged();
            };
            GuestOnChairDropHandler = dropHandler;
        }

        private void AddNewTable()
        {
            Items.Add(new Table() { Width = side, Length = side });
        }

        public Guest NewGuest
        {
           get
            {
                return _newGuest;
            }
        }

        public ObservableCollection<Guest> Guests { get; } = new ObservableCollection<Guest>();
        public ObservableCollection<ICanvasDisplayItem> Items { get; } = new ObservableCollection<ICanvasDisplayItem>();

        public IDropTarget GuestOnChairDropHandler { get; private set; }

        public RelayCommand<Chair> RemoveGuestCommand { get; private set; }

        public RelayCommand<Table> AddTopChairCommand { get; } = new RelayCommand<Table>((table) => { table.TopChairs.Add(new Chair()); });
        public RelayCommand<Table> AddBottomChairCommand { get; } = new RelayCommand<Table>((table) => { table.BottomChairs.Add(new Chair()); });
        public RelayCommand<Table> AddLeftChairCommand { get; } = new RelayCommand<Table>((table) => { table.LeftChairs.Add(new Chair()); });
        public RelayCommand<Table> AddRightChairCommand { get; } = new RelayCommand<Table>((table) => { table.RightChairs.Add(new Chair()); });

        public RelayCommand<ChairRemovalParameter> RemovePersonOrChairCommand { get; private set; }
        public RelayCommand AddTableCommand { get; private set; }
        public RelayCommand<Table> RemoveTableCommand { get; private set; }
        public RelayCommand<Guest> AddNewGuestCommand { get; private set; }

        public ICommand MouseScrollCommand { get; private set; }

        public double ZoomLevel
        {
            get => _zoomLevel; 
            set
            {
                if (_zoomLevel != value)
                {
                    _zoomLevel = value;
                    RaisePropertyChanged(() => ZoomLevel);
                }
            }
        }
    }
}