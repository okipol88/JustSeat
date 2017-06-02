using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JustSeat.Model
{
    public enum GuestType
    {
        None,
        YoungCouple,
        GroomsMen,
        BridesWoman,
        GroomFamily,
        BrideFamily
    }

    public class Table: ViewModelBase, ICanvasDisplayItem
    {
        private double _length;
        private double _width;
        private double _x;
        private double _y;

        public Table()
        {
            Enumerable.Range(0, 2)
                .ToList().ForEach((x) => 
                {
                    TopChairs.Add(new Chair());
                    BottomChairs.Add(new Chair());
                    LeftChairs.Add(new Chair());
                    RightChairs.Add(new Chair());
                });
        }

        public double Length
        {
            get { return _length; }
            set
            {
                if (value != _length)
                {
                    _length = value;
                    RaisePropertyChanged(() => Length);
                }
                        
            }
        }

        public double Width
        {
            get { return _width; }
            set
            {
                if (value != _width)
                {
                    _width = value;
                    RaisePropertyChanged(() => Width);
                }
            }
        }

        public double X
        {
            get { return _x; }
            set
            {
                if (value != _x)
                {
                    _x = value;
                    RaisePropertyChanged(() => X);
                }
            }
        }

        public double Y
        {
            get { return _y; }
            set
            {
                if (value != _y)
                {
                    _y = value;
                    RaisePropertyChanged(() => Y);
                }
            }
        }

        public ObservableCollection<Chair> TopChairs { get; set; } = new ObservableCollection<Chair>();
        public ObservableCollection<Chair> BottomChairs { get; set; } = new ObservableCollection<Chair>();
        public ObservableCollection<Chair> LeftChairs { get; set; } = new ObservableCollection<Chair>();
        public ObservableCollection<Chair> RightChairs { get; set; } = new ObservableCollection<Chair>();
    }

    public interface ICanvasDisplayItem
    {
        double Width { get; }
        double Length { get; }

        double X { get; }
        double Y { get; }
    }
}
