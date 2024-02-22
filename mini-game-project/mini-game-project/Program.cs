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
        Location AlchemistGarden = World.LocationByID(World.LOCATION_ID_ALCHEMISTS_GARDEN);
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


        // Call the StartFarmersFieldMiniGame method here, after player arrives at the farmhouse
        if (player.CurrentLocation == "Farmhouse")
        {
            Location nextLocation = farmhouse.StartFarmersFieldQuest(player);

            // Check if the next location is different, indicating a quest was started
            if (nextLocation != farmhouse)
            {
                player.CurrentLocation = nextLocation.Name; // Update the player's current location
                Console.WriteLine($"\nYou have arrived at: {player.CurrentLocation}");

                // Start the mini-game or any other logic related to the quest
                StartFarmersFieldMiniGame(player);
            }
        }
        else if (player.CurrentLocation == "Alchemist's hut")
        {
            AlchemistGarden.StartAlchemistQuest(player);
            StartAlchemistGardenMiniGame(player);
        }
        else if (player.CurrentLocation == "Guard post")
        {
            if (World.Inventory.Contains("Elixir Essence") && World.Inventory.Contains("Harvest Hoard"))
            {
                Console.WriteLine("You are on a mission ahead from winning this game!");
                Console.WriteLine("The fate of the town rests in your hands, and your bravery and cunning will determine its future.");
                Console.WriteLine("Press on with courage, for you are the town's last hope against the encroaching darkness.");
            }
            else
            {
                Console.WriteLine("Access denied to Guard post. You need to complete the quests (Alchemist's garden & Farmers field) first.");
            }
        }
    }

    public static void StartFarmersFieldMiniGame(Player player)
    {
        Console.WriteLine("\nYou enter the Farmer's field. Snakes are lurking in the tall grass!");
        Console.WriteLine("Your goal is to kill three snakes to complete the quest.\n");

        int snakesKilled = 0;

        while (snakesKilled < 3)
        {
            Console.WriteLine($"Snakes killed: {snakesKilled}/3");

            Console.WriteLine("Choose your action:");
            Console.WriteLine("(A)ttack");
            Console.WriteLine("(R)un");

            string action = Console.ReadLine().ToUpper().Trim();

            if (action == "A")
            {
                int damage = player.Attack();
                Monster snake = World.MonsterByID(World.MONSTER_ID_SNAKE);
                snake.CurrentHitPoints -= damage;

                Console.WriteLine($"You dealt {damage} damage to the snake!");

                if (snake.CurrentHitPoints <= 0)
                {
                    Console.WriteLine("You killed the snake!");
                    snakesKilled++;
                    snake.CurrentHitPoints = snake.MaximumHitPoints; // Reset snake's HP
                }
            }
            else if (action == "R")
            {
                Console.WriteLine("You run away from the snakes.");
                // You might want to handle consequences or exit the mini-game here
                break;
            }
            else
            {
                Console.WriteLine("Invalid input. Please choose (A)ttack or (R)un.");
            }
        }

        if (snakesKilled == 3)
        {
            Console.WriteLine("\nYou successfully cleared the farmer's field of snakes!");
            World.Inventory.Add("Elixir Harvest Hoard");
            // Update the player's quest or add logic related to completing the quest
            player.CurrentLocation = "Farmhouse"; // Return the player to the farmhouse after completing the quest
        }
        else
        {
            Console.WriteLine("\nYou decide to leave the farmer's field.");
            // You might want to handle consequences or exit the mini-game here
        }
    }

    public static void StartAlchemistGardenMiniGame(Player player)
    {
        Console.WriteLine("\nYou enter the Alchemist's garden. Rats are scurrying amidst the herb beds, hidden among the foliage");
        Console.WriteLine("Your need to kill three rats to complete this mission.\n");

        int ratsKilled = 0;

        while (ratsKilled < 3)
        {
            Console.WriteLine($"Rats killed: {ratsKilled}/3");

            Console.WriteLine("Choose your action:");
            Console.WriteLine("(A)ttack");
            Console.WriteLine("(R)un");

            string action = Console.ReadLine().ToUpper().Trim();

            if (action == "A")
            {
                int damage = player.Attack();
                Monster rat = World.MonsterByID(World.MONSTER_ID_RAT);
                rat.CurrentHitPoints -= damage;

                Console.WriteLine($"You dealt {damage} damage to the Rat!");

                if (rat.CurrentHitPoints <= 0)
                {
                    Console.WriteLine("You killed the Rat!");
                    ratsKilled++;
                    rat.CurrentHitPoints = rat.MaximumHitPoints;
                }
            }
            else if (action == "R")
            {
                Console.WriteLine("You run away from the Rats. what a coward !");
                break;
            }
            else
            {
                Console.WriteLine("Invalid input. Please choose (A)ttack or (R)un.");
            }
        }

        if (ratsKilled == 3)
        {
            Console.WriteLine("\nYou successfully cleared the alchemist’s garden from all rats!");
            player.CurrentLocation = "Alchemist's hut";
            World.Inventory.Add("Elixir Essence");
        }
        else
        {
            Console.WriteLine("\nYou decide to leave alchemist’s garden.");
        }
    }
}



