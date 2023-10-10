using MyGarage;

public class Program
{
    static void Main()
    {
        GarageHandler<Vehicle> garageHandler = new GarageHandler<Vehicle>();

        while (true)
        {
            Console.Clear();
            Console.WriteLine("Garage Management System");
            Console.WriteLine("1. Create a garage");
            Console.WriteLine("2. Close the garage");
            Console.WriteLine("3. Add new vehicle to garage");
            Console.WriteLine("4. List all vehicles");
            Console.WriteLine("5. Search for Vehicles based on characteristics");
            Console.WriteLine("6. Remove vehicle from garage");
            Console.WriteLine("7. List all vehicle types and count");
            Console.WriteLine("8. Extend garage capacity");
            Console.WriteLine("9. Reduce garage capacity");
            Console.WriteLine("9. Exit");

            Console.Write("Enter your choice: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    Console.Write("Enter garage capacity: ");
                    if (int.TryParse(Console.ReadLine(), out int capacity) && capacity > 0)
                    {
                        garageHandler.CreateGarage(capacity);
                    }
                    else
                    {
                        Console.WriteLine("Invalid input. Please enter a valid capacity.");
                    }
                    break;

                case "2":
                    garageHandler.CloseGarage();
                    break;

                case "3":
                    garageHandler.AddVehicleToGarage();
                    break;

                case "4":
                    garageHandler.ListAllVehicles();
                    break;

                case "5":
                    garageHandler.SearchVehiclesByCharacteristics();
                    break;

                case "6":
                    garageHandler.RemoveVehicleFromGarage();
                    break;

                case "7":
                    garageHandler.ListVehicleTypesAndCount();
                    break;

                case "8":
                    garageHandler.ExtendGarageCapacity();
                    break;
                case "9":
                    garageHandler.ReduceGarageCapacity();
                    break;

                case "10":
                    Console.WriteLine("Exiting the application. Goodbye!");
                    Environment.Exit(0);
                    break;

                default:
                    Console.WriteLine("Invalid choice. Please select a valid option.");
                    break;
            }

            Console.WriteLine("Press Enter to continue...");
            Console.ReadLine();
        }
    }
}
