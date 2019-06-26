using System;

namespace design_patterns
{
    /// <summary>
    /// Factory Method is a creational design pattern that provides an interface for creating objects in a superclass, 
    /// but allows subclasses to alter the type of objects that will be created.
    /// </summary>
    class FactoryMethodPattern : DesignPattern
    {
        public override string PatternName => "Factory Method";

        public override void ExecuteSample()
        {
            base.ExecuteSample();

            var shipLogistics = new ShipLogistics(new GeoLocation(10, 20), DateTime.Now.AddMonths(3));
            var truckLogistics = new TruckLogistics(new GeoLocation(100, 120), DateTime.Now.AddMonths(1));

            shipLogistics.PlanDelivery();
            truckLogistics.PlanDelivery();

            shipLogistics.Deliver();
            truckLogistics.Deliver();

            Console.WriteLine($"Ship distance left: {shipLogistics.CalculateDistanceLeft()}");
            Console.WriteLine($"Truck distance left: {truckLogistics.CalculateDistanceLeft()}");
        }
    }

    class GeoLocation
    {
        public GeoLocation()
        {
            Longitude = 0;
            Latitude = 0;
        }
        public GeoLocation(int latitude, int longitude)
        {
            Longitude = longitude;
            Latitude = latitude;
        }
        public int Longitude { get; set; }
        public int Latitude { get; set; }
    }

    interface ITransport
    {
        string VIN { get; }
        GeoLocation CheckLocation();
        void Deliver();
    }

    class Ship : ITransport
    {
        public string VIN => Guid.NewGuid().ToString();
        private Random _random = new Random();

        public GeoLocation CheckLocation()
        {
            return new GeoLocation()
            {
                Latitude = _random.Next(360),
                Longitude = _random.Next(360)
            };
        }

        public void Deliver()
        {
            Console.WriteLine("Deliver by see in a container");
        }
    }

    class Truck : ITransport
    {
        public string VIN => Guid.NewGuid().ToString();
        private Random _random = new Random();

        public GeoLocation CheckLocation()
        {
            return new GeoLocation()
            {
                Latitude = _random.Next(360),
                Longitude = _random.Next(360)
            };
        }

        public void Deliver()
        {
            Console.WriteLine("Deliver by land in a boxes");
        }
    }

    abstract class Logistics
    {
        protected ITransport _transport;
        private GeoLocation _targetLocation;
        private DateTime _dueDate;
        public Logistics(GeoLocation targetLocation, DateTime dueDate)
        {
            _targetLocation = targetLocation;
            _dueDate = dueDate;
        }

        public GeoLocation TargetLocation { get { return _targetLocation; } }
        public DateTime DueDate { get { return _dueDate; } }

        protected abstract void CreateTransport();

        public void PlanDelivery()
        {
            CreateTransport();
        }

        public int CalculateDistanceLeft()
        {
            var currentLocation = _transport.CheckLocation();
            var latitude = _targetLocation.Latitude - currentLocation.Latitude;
            var longitude = _targetLocation.Longitude - currentLocation.Longitude;
            return latitude * latitude + longitude * longitude;
        }

        public void Deliver()
        {
            _transport.Deliver();
        }
    }

    class TruckLogistics : Logistics
    {
        public TruckLogistics(GeoLocation targetLocation, DateTime dueDate) : base(targetLocation, dueDate)
        {
        }

        protected override void CreateTransport()
        {
            _transport = new Truck();
        }
    }

    class ShipLogistics : Logistics
    {
        public ShipLogistics(GeoLocation targetLocation, DateTime dueDate) : base(targetLocation, dueDate)
        {
        }

        protected override void CreateTransport()
        {
            _transport = new Ship();
        }
    }
}
