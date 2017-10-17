namespace Passenger.Core.Domain
{
    public class Vehicle
    {
        public string Name { get; protected set; }
        public int Seats { get; protected set; }
        public string Brand { get; protected set; }
        private Vehicle(string name, int seats, string brand)
        {
            Name = name;
            Seats = seats;
            Brand = brand;
        }

        public static Vehicle Create(string name, int seats, string brand)
        {
            var vehicle = new Vehicle(name, seats, brand);
            return vehicle;
        }

        
    }
}