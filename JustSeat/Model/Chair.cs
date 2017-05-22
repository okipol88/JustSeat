using System;
using GongSolutions.Wpf.DragDrop;
using GalaSoft.MvvmLight;
using System.Windows;

namespace JustSeat.Model
{
    public class Chair: ViewModelBase
    {
        private Guest _person;

        public Guest Person
        {
            get { return _person; }
            set
            { 
                _person = value;
                RaisePropertyChanged(() => Person, _person, value, true);
            }
        }
    }
}
