using mini_game_project;
using System;

class Program
{
    public static bool QuitGame = false;

    public static void Main()
    {
        Player player = new Player("John Doe", 25, "Male", 100, "Town Square", "Rusty Sword", 100);
        Location home = World.LocationByID(World.LOCATION_ID_HOME);
        Location townSquare = World.LocationByID(World.LOCATION_ID_TOWN_SQUARE);
        Location farmhouse = World.LocationByID(World.LOCATION_ID_FARMHOUSE);
        player.CurrentLocation = home.Name;

        Console.WriteLine("Welcome to the Giant Spider Adventure!\n");
        Console.WriteLine("In the serene town you call home, a looming threat from giant spiders casts a shadow,");
        Console.WriteLine("prompting you to embark on a courageous quest to safeguard your fellow villagers.");
        Console.WriteLine("Whispers suggest that the spiders have nested in a foreboding dark forest, compelling");
        Console.WriteLine("you to venture into various locations, confront menacing monsters, and fulfill quests");
        Console.WriteLine("to rid the town of this arachnid menace.\n");

        Console.WriteLine("\nWhere would you like to go?");
        Console.WriteLine($"You are at: {player.CurrentLocation}.");
        home.DisplayDetails();

        string direction;

        do
        {
            direction = Console.ReadLine().ToUpper().Trim();
            townSquare = home.ChangeLocation(player, direction);
        } while (townSquare == null);

        Console.WriteLine($"\nYou have arrived at: {player.CurrentLocation}");
        townSquare.DisplayDetails();

        Location newLocation;

        do
        {
            direction = Console.ReadLine().ToUpper().Trim();
            newLocation = townSquare.ChangeLocation(player, direction);
        } while (newLocation == null);

        Console.WriteLine($"\nYou have arrived at: {player.CurrentLocation}");

        // After the player arrives at the farmhouse in Program.cs
        if (player.CurrentLocation == "Farmhouse")
        {
            Location nextLocation = farmhouse.StartFarmersFieldQuest(player);

            // Check if the next location is different, indicating a quest was started
            if (nextLocation != farmhouse)
            {
                player.CurrentLocation = nextLocation.Name; // Update the player's current location
                Console.WriteLine($"\nYou have arrived at: {player.CurrentLocation}");

                // Start the mini-game or any other logic related to the quest
                //StartFarmersFieldMiniGame(player);
            }
        }


    }
}