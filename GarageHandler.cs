using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace MyGarage
{
    public class GarageHandler<T> where T : Vehicle
    {
        private Garage<T> garage;

        public void CreateGarage(int capacity)
        {
            garage = new Garage<T>(capacity);
            Console.WriteLine($"Garage with {capacity} parking spaces created.");
        }

        public void CloseGarage()
        {
            garage = null;
            Console.WriteLine("Garage is closed.");
        }

        public void AddVehicleToGarage()
        {
            if (garage == null)
            {
                Console.WriteLine("Garage is not created yet. Please create a garage first.");
                return;
            }

            if (garage.Capacity == garage.Count)
            {
                Console.WriteLine("Garage is full. Please increase the capacity of the garage.");
                return;
            }

            Console.WriteLine("Enter vehicle details:");

            string regNumber;
            string color;
            int wheels;
            int vehicleType;
            T newVehicle;

            Console.Write("Registration Number: ");
            regNumber = Console.ReadLine();

            Console.Write("Color: ");
            color = Console.ReadLine();

            while (true)
            {
                Console.Write("Number of Wheels: ");
                if (!int.TryParse(Console.ReadLine(), out wheels) || wheels < 0)
                {
                    Console.WriteLine("Invalid number of wheels. Please enter a valid number.");
                }
                else
                {
                    break; 
                }
            }

            while (true)
            {
                Console.WriteLine("Select Vehicle Type:");
                Console.WriteLine("1. Car");
                Console.WriteLine("2. Motorcycle");
                Console.WriteLine("3. Bus");
                Console.WriteLine("4. Boat");
                Console.WriteLine("5. Airplane");

                Console.Write("Enter your choice: ");
                if (!int.TryParse(Console.ReadLine(), out vehicleType) || vehicleType < 1 || vehicleType > 5)
                {
                    Console.WriteLine("Invalid choice. Please enter a valid vehicle type.");
                }
                else
                {
                    break;
                }
            }

            switch (vehicleType)
            {
                case 1:
                    // Car
                    int doors;
                    while (true)
                    {
                        Console.Write("Number of Doors: ");
                        if (!int.TryParse(Console.ReadLine(), out doors) || doors < 0)
                        {
                            Console.WriteLine("Invalid number of doors. Please enter a valid number.");
                        }
                        else
                        {
                            break; 
                        }
                    }
                    newVehicle = (T)(Vehicle)new Car
                    {
                        RegistrationNumber = regNumber,
                        Color = color,
                        NumberOfWheels = wheels,
                        NumberOfDoors = doors
                    };
                    break;

                case 2:
                    // Motorcycle
                    bool hasSidecar;
                    while (true)
                    {
                        Console.Write("Has Sidecar (true/false): ");
                        if (!bool.TryParse(Console.ReadLine(), out hasSidecar))
                        {
                            Console.WriteLine("Invalid input for sidecar. Please enter true or false.");
                        }
                        else
                        {
                            break; 
                        }
                    }
                    newVehicle = (T)(Vehicle)new Motorcycle
                    {
                        RegistrationNumber = regNumber,
                        Color = color,
                        NumberOfWheels = wheels,
                        HasSideCar = hasSidecar
                    };
                    break;

                case 3:
                    // Bus
                    int capacity;
                    while (true)
                    {
                        Console.Write("Capacity: ");
                        if (!int.TryParse(Console.ReadLine(), out capacity) || capacity < 0)
                        {
                            Console.WriteLine("Invalid capacity. Please enter a valid number.");
                        }
                        else
                        {
                            break; 
                        }
                    }
                    newVehicle = (T)(Vehicle)new Bus
                    {
                        RegistrationNumber = regNumber,
                        Color = color,
                        NumberOfWheels = wheels,
                        Capacity = capacity
                    };
                    break;

                case 4:
                    // Boat
                    double length;
                    while (true)
                    {
                        Console.Write("Length (in meters): ");
                        if (!double.TryParse(Console.ReadLine(), out length) || length < 0)
                        {
                            Console.WriteLine("Invalid length. Please enter a valid number.");
                        }
                        else
                        {
                            break; 
                        }
                    }
                    newVehicle = (T)(Vehicle)new Boat
                    {
                        RegistrationNumber = regNumber,
                        Color = color,
                        NumberOfWheels = wheels,
                        Length = length
                    };
                    break;

                case 5:
                    // Airplane
                    int numberOfEngines;
                    while (true)
                    {
                        Console.Write("Number of Engines: ");
                        if (!int.TryParse(Console.ReadLine(), out numberOfEngines) || numberOfEngines < 0)
                        {
                            Console.WriteLine("Invalid number of engines. Please enter a valid number.");
                        }
                        else
                        {
                            break;                         }
                    }
                    newVehicle = (T)(Vehicle)new Airplane
                    {
                        RegistrationNumber = regNumber,
                        Color = color,
                        NumberOfWheels = wheels,
                        NumberOfEngines = numberOfEngines
                    };
                    break;

                default:
                    Console.WriteLine("Invalid vehicle type.");
                    return;
            }

            if (garage.Park(newVehicle))
            {
                Console.WriteLine("Vehicle parked successfully.");
            }
            else
            {
                Console.WriteLine("Failed to park the vehicle.");
            }
        }

        public void ListAllVehicles()
        {
            if (garage == null)
            {
                Console.WriteLine("Garage is not created yet. Please create a garage first.");
                return;
            }

            Console.WriteLine("List of parked vehicles:");
            foreach (var vehicle in garage)
            {
                Console.WriteLine(vehicle);
            }
        }

        public void SearchVehiclesByCharacteristics()
        {
            if (garage == null)
            {
                Console.WriteLine("Garage is not created yet. Please create a garage first.");
                return;
            }

            Console.WriteLine("Search for Vehicles based on characteristics:");

            Console.Write("Enter vehicle color: ");
            string color = Console.ReadLine();

            int wheels;
            while (true)
            {
                Console.Write("Enter the number of wheels (or '0' to skip): ");
                if (int.TryParse(Console.ReadLine(), out wheels) && wheels >= 0)
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid input for the number of wheels. Please enter a valid number or '0' to skip.");
                }
            }

            Console.WriteLine("Select Vehicle Type:");
            Console.WriteLine("1. Car");
            Console.WriteLine("2. Motorcycle");
            Console.WriteLine("3. Bus");
            Console.WriteLine("4. Boat");
            Console.WriteLine("5. Airplane");
            Console.Write("Enter your choice (or '0' to skip): ");
            int vehicleType;
            if (int.TryParse(Console.ReadLine(), out vehicleType) && vehicleType >= 0 && vehicleType <= 5)
            {
                Func<T, bool> predicate = v =>
                    (string.IsNullOrEmpty(color) || v.Color.Equals(color, StringComparison.OrdinalIgnoreCase)) &&
                    (wheels == 0 || v.NumberOfWheels == wheels) &&
                    (vehicleType == 0 || (vehicleType == 1 && v is Car) || (vehicleType == 2 && v is Motorcycle) || (vehicleType == 3 && v is Bus) || (vehicleType == 4 && v is Boat) || (vehicleType == 5 && v is Airplane));

                IEnumerable<T> matchingVehicles = garage.SearchVehicles(predicate);

                if (matchingVehicles.Any())
                {
                    Console.WriteLine("Matching Vehicles:");
                    foreach (T vehicle in matchingVehicles)
                    {
                        Console.WriteLine(vehicle);
                    }
                }
                else
                {
                    Console.WriteLine("No vehicles found with the matching criteria.");
                }
            }
            else
            {
                Console.WriteLine("Invalid input for vehicle type. Please enter a valid choice.");
            }
        }

        public void RemoveVehicleFromGarage()
        {
            if (garage == null)
            {
                Console.WriteLine("Garage is not created yet. Please create a garage first.");
                return;
            }

            Console.Write("Enter Registration Number to Remove: ");
            string registrationNumberToRemove = Console.ReadLine();

            T vehicleToRemove = garage.FindVehicleByRegistrationNumber(registrationNumberToRemove);

            if (vehicleToRemove != null)
            {
                bool removed = garage.Remove(vehicleToRemove);

                if (removed)
                {
                    Console.WriteLine($"Vehicle with registration number {registrationNumberToRemove} removed successfully.");
                }
                else
                {
                    Console.WriteLine("An error occurred while removing the vehicle.");
                }
            }
            else
            {
                Console.WriteLine($"Vehicle with registration number {registrationNumberToRemove} not found in the garage.");
            }
        }

        public void ListVehicleTypesAndCount()
        {
            if (garage == null)
            {
                Console.WriteLine("Garage is not created yet. Please create a garage first.");
                return;
            }

            var vehicleTypeCounts = garage.ListVehicleTypes();
            if (vehicleTypeCounts.Count > 0)
            {
                Console.WriteLine("List of vehicle types and count:");
                foreach (var kvp in vehicleTypeCounts)
                {
                    Console.WriteLine($"{kvp.Key.Name}: {kvp.Value}");
                }

            }
            else
            {
                Console.WriteLine("No vehicles exist in the garage. Please add vehicles.");
            }
        }

        public void ExtendGarageCapacity()
        {
            if (garage == null)
            {
                Console.WriteLine("Garage is not created yet. Please create a garage first.");
                return;
            }

            Console.Write("Enter the new capacity: ");
            if (!int.TryParse(Console.ReadLine(), out int newCapacity) || newCapacity <= garage.Capacity)
            {
                Console.WriteLine($"Invalid new capacity.Current capacity is "+garage.Capacity+". Please enter a valid capacity greater than the current capacity.");
                return;
            }

            if (garage.ExtendCapacity(newCapacity))
            {
                Console.WriteLine("Garage capacity extended successfully.");
            }
            else
            {
                Console.WriteLine("Failed to extend the garage capacity.");
            }
        }
        public void ReduceGarageCapacity()
        {
            if (garage == null)
            {
                Console.WriteLine("Garage is not created yet. Please create a garage first.");
                return;
            }

            Console.Write("Enter the new capacity: ");
            if (!int.TryParse(Console.ReadLine(), out int newCapacity) || newCapacity >= garage.Capacity)
            {
                Console.WriteLine("Invalid new capacity. Please enter a valid capacity less than the current capacity.");
                return;
            }

            if (garage.ReduceCapacity(newCapacity))
            {
                Console.WriteLine("Garage capacity reduced successfully.");
            }
            else
            {
                Console.WriteLine("Failed to reduce the garage capacity.");
            }
        }

    }
}
