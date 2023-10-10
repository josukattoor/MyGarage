namespace MyGarage
{
    public class Vehicle : IVehicle
    {
        public string RegistrationNumber { get; set; }
        public string Color { get; set; }
        public int NumberOfWheels { get; set; }

        public override string ToString()
        {
            return $"Registration Number: {RegistrationNumber}, Color: {Color}, Wheels: {NumberOfWheels}";
        }
    }
    public class Car : Vehicle
    {
        public int NumberOfDoors { get; set; }
    }

    public class Motorcycle : Vehicle
    {
        public bool HasSideCar { get; set; }
    }

    public class Bus : Vehicle
    {
        public int Capacity { get; set; }
    }

    public class Boat : Vehicle
    {
        public double Length { get; set; }
    }

    public class Airplane : Vehicle
    {
        public int NumberOfEngines { get; set; }
    }

}
