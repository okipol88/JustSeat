using GalaSoft.MvvmLight;
using System.Collections.Generic;

namespace JustSeat.Model
{
    public class Guest: ViewModelBase
    {
        private string _name;
        private string _surname;

        public GuestType GuestType { get; set; }

        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                RaisePropertyChanged(() => Name);
            }
        }

        public string Surname
        {
            get { return _surname; }
            set
            {
                _surname = value;
                RaisePropertyChanged(() => Surname);
            }
        }

        public override string ToString()
        {
            var items = new List<string>();
            if (!string.IsNullOrEmpty(Name))
                items.Add(Name);
            if (!string.IsNullOrEmpty(Surname))
                items.Add(Surname);

            return string.Join(System.Environment.NewLine, items);
        }
    }
}
