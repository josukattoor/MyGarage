using System.Linq.Expressions;

namespace MyGarage
{
    public class Garage<T> : IEnumerable<T> where T : Vehicle
    {
        private T[] vehicles;

        public int Capacity { get; private set; }
        public int Count { get; private set; }

        public Garage(int capacity)
        {
            Capacity = capacity;
            vehicles = new T[capacity];
        }

        public bool Park(T vehicle)
        {
            // Check if garage is full
            if (Count >= Capacity)
            {
                Console.WriteLine("The garage is full. Cannot park more vehicles.");
                return false;
            }

            // Check if vehicle with same registration number already exists
            if (vehicles.Take(Count).Any(v => v.RegistrationNumber.Equals(vehicle.RegistrationNumber, StringComparison.OrdinalIgnoreCase)))
            {
                Console.WriteLine("A vehicle with the same registration number is already parked.");
                return false;
            }

            // Park vehicle
            vehicles[Count] = vehicle;
            Count++;
            Console.WriteLine($"Vehicle with registration number {vehicle.RegistrationNumber} is parked.");
            return true;
        }
        public T FindVehicleByRegistrationNumber(string registrationNumber)
        {
            //case-insensitive comparison
            string searchRegistrationNumber = registrationNumber.ToLower();

            //find vehicle with matching registration number
            return vehicles.Take(Count).FirstOrDefault(v => v.RegistrationNumber.ToLower() == searchRegistrationNumber);
        }

        public IEnumerable<T> SearchVehicles(Func<T, bool> characteristic)
        {
            // search for vehicles based on the provided characteristics
            return vehicles.Take(Count).Where(characteristic);
        }

        public bool Remove(T vehicle)
        {
            // Find the index of the vehicle in the garage
            int indexToRemove = -1;
            for (int i = 0; i < Count; i++)
            {
                if (ReferenceEquals(vehicles[i], vehicle))
                {
                    indexToRemove = i;
                    break;
                }
            }

            //remove from the garage
            if (indexToRemove >= 0)
            {
                // Shift vehicles next to the removed one to the left to fill the gap
                for (int i = indexToRemove; i < Count - 1; i++)
                {
                    vehicles[i] = vehicles[i + 1];
                }

                // Set last element to null to remove  reference
                vehicles[Count - 1] = null;

                Count--;
                return true;
            }
            else
            {
                Console.WriteLine("Vehicle not found in the garage.");
                return false;
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            return vehicles.Take(Count).GetEnumerator();
        }
        public Dictionary<Type, int> ListVehicleTypes()
        {
            var typeCounts = new Dictionary<Type, int>();

            foreach (var vehicle in vehicles.Take(Count))
            {
                var vehicleType = vehicle.GetType();

                if (typeCounts.ContainsKey(vehicleType))
                {
                    typeCounts[vehicleType]++;
                }
                else
                {
                    typeCounts[vehicleType] = 1;
                }
            }

            return typeCounts;
        }
        public bool ExtendCapacity(int newCapacity)
        {
            if (newCapacity <= Capacity)
            {
                Console.WriteLine("New capacity should be greater than the current capacity.");
                return false;
            }

            T[] newVehicles = new T[newCapacity];

            for (int i = 0; i < Count; i++)
            {
                newVehicles[i] = vehicles[i];
            }

            vehicles = newVehicles;
            Capacity = newCapacity;

            Console.WriteLine($"Garage capacity extended to {newCapacity}.");
            return true;
        }
        public bool ReduceCapacity(int newCapacity)
        {
            if (newCapacity < Count)
            {
                Console.WriteLine($"Cannot reduce capacity to {newCapacity}. There are {Count} vehicles parked.");
                return false;
            }

            T[] newVehicles = new T[newCapacity];

            for (int i = 0; i < Count; i++)
            {
                newVehicles[i] = vehicles[i];
            }

            vehicles = newVehicles;
            Capacity = newCapacity;

            Console.WriteLine($"Garage capacity reduced to {newCapacity}.");
            return true;
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }




    }
}
