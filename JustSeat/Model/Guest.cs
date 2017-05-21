namespace JustSeat.Model
{
    public class Guest
    {
        public GuestType GuestType { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public override string ToString()
        {
            return Name + Surname;
        }
    }
}
