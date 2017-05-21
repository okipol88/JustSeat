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

    public class Guest
    {
        public GuestType GuestType { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }
    }

    public class Chair
    {
        public Guest Person { get; set; }
    }

    public class Table: ICanvasDisplayItem
    {
        public double Length { get; set; }
        public double Width { get; set; }

        public double X { get; set; }
        public double Y { get; set; }

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
