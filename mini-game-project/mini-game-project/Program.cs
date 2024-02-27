using mini_game_project;
using System;
using System.Security.Cryptography.X509Certificates;

class Program
{
    public static bool QuitGame = false;

    public static void Main()
    {
        Random random = new Random();
        Player player = new Player("John Doe", 25, "Male", 100, "Town Square", "Rusty Sword", 100);
        Location home = World.LocationByID(World.LOCATION_ID_HOME);
        Location townSquare = World.LocationByID(World.LOCATION_ID_TOWN_SQUARE);
        Location farmhouse = World.LocationByID(World.LOCATION_ID_FARMHOUSE);
        Location AlchemistGarden = World.LocationByID(World.LOCATION_ID_ALCHEMISTS_GARDEN);
        player.CurrentLocation = home.Name;
        Console.ForegroundColor = ConsoleColor.DarkGreen;
        Console.WriteLine("Welcome to the Giant Spider Adventure!\n");
        Console.WriteLine("In the serene town you call home, a looming threat from giant spiders casts a shadow,");
        Console.WriteLine("prompting you to embark on a courageous quest to safeguard your fellow villagers.");
        Console.WriteLine("Whispers suggest that the spiders have nested in a foreboding dark forest, compelling");
        Console.WriteLine("you to venture into various locations, confront menacing monsters, and fulfill quests");
        Console.WriteLine("to rid the town of this arachnid menace.\n");
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("\nWhere would you like to go?");
        Console.WriteLine($"You are at: {player.CurrentLocation}.");
        home.DisplayDetails();

        bool FirstTwoQuestsCompleted = false;

        string direction;

       
        do
        {
            direction = Console.ReadLine().ToUpper().Trim();
            townSquare = home.ChangeLocation(player, direction);
        } while (townSquare == null);

        Console.WriteLine($"\nYou have arrived at: {player.CurrentLocation}");
        townSquare.DisplayDetails();

        Location newLocation;

        while (!FirstTwoQuestsCompleted)
        {
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
                Location nextLocation = AlchemistGarden.StartAlchemistQuest(player);

                // Check if the next location is not null, indicating the player declined the quest
                if (nextLocation == AlchemistGarden)
                {
                    player.CurrentLocation = nextLocation.Name; // Update the player's current location
                    Console.WriteLine($"\nYou have arrived at: {player.CurrentLocation}");

                    // Start the mini-game or any other logic related to the quest
                    StartAlchemistGardenMiniGame(player);
                }
            }
            else if (player.CurrentLocation == "Guard post")
            {
                if (World.Inventory.Contains("Elixir Essence") && World.Inventory.Contains("Elixir Harvest Hoard"))
                {
                    Console.WriteLine("You are on a mission ahead from winning this game!");
                    Console.WriteLine("The fate of the town rests in your hands, and your bravery and cunning will determine its future.");
                    Console.WriteLine("Press on with courage, for you are the town's last hope against the encroaching darkness.");
                    FirstTwoQuestsCompleted = true;
                    StartClearSpidersForest(player);
                }
                else
                {
                    Console.WriteLine("Access denied to Guard post. You need to complete the quests (Alchemist's garden & Farmers field) first.");
                }
            }
        }

        if (World.Inventory.Contains("Elixir Essence") && World.Inventory.Contains("Elixir Harvest Hoard") && World.Inventory.Contains("Silk"))
        {
            Console.WriteLine("\nYou have completed all the quests!\nCongratulations!");
            Environment.Exit(0);
        }

    }

    public static void StartFarmersFieldMiniGame(Player player)
    {
        Console.WriteLine("\nYou enter the Farmer's field. Snakes are lurking in the tall grass!");
        Console.WriteLine("Your goal is to kill three snakes within 5 seconds to complete the quest.\n");

        int timeLimitInSeconds = 5; // Time limit for completing the quest

        while (true) // Loop until the player completes the quest or decides to leave
        {
            int snakesKilled = 0;
            DateTime startTime = DateTime.Now;

            while (snakesKilled < 3)
            {
                // Check if time limit has been exceeded
                if ((DateTime.Now - startTime).TotalSeconds >= timeLimitInSeconds)
                {
                    Console.WriteLine("\nTime's up! You failed to complete the quest in time.");
                    break; // Break out of the inner loop
                }

                Console.WriteLine($"Snakes killed: {snakesKilled}/3");
                Console.WriteLine($"Time remaining: {timeLimitInSeconds - (int)(DateTime.Now - startTime).TotalSeconds} seconds");

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
                    break; // Break out of the inner loop
                }
                else
                {
                    Console.WriteLine("Invalid input. Please choose (A)ttack or (R)un.");
                }
            }

            if (snakesKilled == 3)
            {
                Console.WriteLine("\nCongratulations! You successfully cleared the farmer's field of snakes!");
                Console.WriteLine("You have come back to the Town Square. Where would you like to go?");

                World.Inventory.Add("Elixir Harvest Hoard");
                // Update the player's quest or add logic related to completing the quest

                player.CurrentLocation = "Town Square";
                World.QuestByID(World.QUEST_ID_CLEAR_FARMERS_FIELD).Complete();
                break; // Break out of the outer loop
            }
            else
            {
                Console.WriteLine("Do you want to retry the quest? (Y/N)");

                string retry = Console.ReadLine().ToUpper().Trim();
                if (retry != "Y")
                {
                    break; // Break out of the outer loop if the player chooses not to retry
                }
            }
        }
    }


    public static void StartAlchemistGardenMiniGame(Player player)
    {
        Console.WriteLine("\nYou enter the Alchemist's garden. Rats are scurrying amidst the herb beds, hidden among the foliage.");
        Console.WriteLine("Your need to kill three rats within 5 turns to complete this mission.\n");

        while (true) // Loop until the player completes the quest or decides not to retry
        {
            int ratsKilled = 0;
            int turnsRemaining = 6; // Number of turns allowed to complete the quest

            while (ratsKilled < 3 && turnsRemaining > 0)
            {
                Console.WriteLine($"Rats killed: {ratsKilled}/3");
                Console.WriteLine($"Turns remaining: {turnsRemaining}");

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
                        rat.CurrentHitPoints = rat.MaximumHitPoints; // Reset rat's HP
                    }
                }
                else if (action == "R")
                {
                    Console.WriteLine("You run away from the Rats. What a coward!");
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid input. Please choose (A)ttack or (R)un.");
                }

                // Monster attacks back
                if (ratsKilled < 3)
                {
                    Console.WriteLine("The Rats bite back!");
                    int damageTaken = World.RandomGenerator.Next(15, 20); // Random damage between 3 to 6
                    player.CurrentHitPoints -= damageTaken;
                    Console.WriteLine($"You took {damageTaken} damage.");

                    if (player.CurrentHitPoints > 0)
                    {
                        Console.WriteLine($"You have {player.CurrentHitPoints} HP left.");

                    }

                    else if (player.CurrentHitPoints <= 0)
                    {
                        Console.WriteLine("You have been defeated!");
                        break; // Break out of the inner loop if the player dies
                    }
                }

                turnsRemaining--; // Decrease the number of turns remaining
            }

            if (ratsKilled == 3)
            {
                Console.WriteLine("\nYou successfully cleared the Alchemist's garden of all rats!");
                player.CurrentLocation = "Alchemist's hut";
                World.Inventory.Add("Elixir Essence");
                World.QuestByID(World.QUEST_ID_CLEAR_ALCHEMIST_GARDEN).Complete();
                break; // Break out of the outer loop if the player completes the quest
            }
            else
            {
                Console.WriteLine("\nYou failed to clear the Alchemist's garden of rats in time.");

                Console.WriteLine("Do you want to retry the quest? (Y/N)");
                string retry = Console.ReadLine().ToUpper().Trim();
                if (retry != "Y")
                {
                    break; // Break out of the outer loop if the player chooses not to retry
                }
                else
                {
                    player.CurrentHitPoints = 100;
                }
            }
        }
    }

    public static void StartClearSpidersForest(Player player)
    {
        Random random = new Random();  // Add this line to create a Random instance
        Console.WriteLine("\nYou enter the Spiders's Nest to collect Silk.");
        Console.WriteLine("Your need to kill and collect 3 spider silks to complete this quest.\n");
        Console.WriteLine("There is only 1 in 2 chance of silk dropping from Spider!");
        int silkCollected = 0;
        while (silkCollected < 3)
        {
            Console.WriteLine($"Silk collected: {silkCollected}/3");
            Console.WriteLine("Choose your action:");
            Console.WriteLine("(A)ttack");
            Console.WriteLine("(R)un");
            string action = Console.ReadLine().ToUpper().Trim();

            if (action == "A")
            {
                int damage = player.Attack();
                Monster spider = World.MonsterByID(World.MONSTER_ID_GIANT_SPIDER);
                spider.CurrentHitPoints -= damage;

                Console.WriteLine($"You dealt {damage} damage to the Spider!");

                if (spider.CurrentHitPoints <= 0)
                {
                    Console.WriteLine("You killed the Spider!");
                    double randomdroprate = random.NextDouble();
                    double dropChance = 0.50;
                    bool itdropped = randomdroprate <= dropChance;
                    if (itdropped) 
                    {
                        silkCollected++;
                    }
                    else
                    {
                        Console.WriteLine("That one didn't have any Silk! Try again!");
                    }
                }
            }
            else if (action == "R")
            {
                Console.WriteLine("You coward you have been surrounded by Giant Spiders!! There is no way of running away now! GAME OVER!");
                Environment.Exit(0);
            }
            else
            {
                Console.WriteLine("Invalid input. Please choose (A)ttack or (R)un.");
            }
        }

        if (silkCollected == 3)
        {
            Console.WriteLine("\nYou successfully collected all necessary silk! Well Done!");
            player.CurrentLocation = "Spider forest";
            World.Inventory.Add("Silk");
        }

        
    }

}
