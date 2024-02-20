using mini_game_project;
using System;

class Program
{
    public static bool QuitGame = false;

    public static void Main()
    {
        Player player = new Player("John Doe", 25, "Male", 100, "Town Square", "Rusty Sword", 100);
        Location home = World.LocationByID(World.LOCATION_ID_HOME);
        player.CurrentLocation = home.Name;

        Console.WriteLine("Welcome to the Giant Spider Adventure!\n");
        Console.WriteLine("In the serene town you call home, a looming threat from giant spiders casts a shadow,");
        Console.WriteLine("prompting you to embark on a courageous quest to safeguard your fellow villagers.");
        Console.WriteLine("Whispers suggest that the spiders have nested in a foreboding dark forest, compelling");
        Console.WriteLine("you to venture into various locations, confront menacing monsters, and fulfill quests");
        Console.WriteLine("to rid the town of this arachnid menace.\n");

        while (!QuitGame)
        {
            Console.WriteLine("What do you want to do?\n1: Play\n2: Quit");
            int choice = Convert.ToInt32(Console.ReadLine().Trim());

            if (choice == 2)
            {
                QuitGame = true;
                break;
            }

            Console.WriteLine("\nWhere would you like to go?");
            Console.WriteLine($"You are at: {player.CurrentLocation}. From here you can go:");
            home.DisplayAdjacentLocations();

            string direction;
            Location newLocation;

            do
            {
                direction = Console.ReadLine().ToUpper().Trim();
                newLocation = home.ChangeLocation(player, direction);
            } while (newLocation == null);
        }
    }
}
