using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace mini_game_project
{
    internal class Location
    {
        public int ID { get; }
        public string Name { get; }
        public string Description { get; }
        public Quest? QuestAvailableHere { get; set; }
        public Monster? MonsterLivingHere { get; set; }
        public Location? LocationToNorth { get; set; }
        public Location? LocationToEast { get; set; }
        public Location? LocationToSouth { get; set; }
        public Location? LocationToWest { get; set; }

        // Constructor method.
        public Location(int id, string name, string description, Quest questAvailableHere, Monster monsterLivingHere)
        {
            ID = id;
            Name = name;
            Description = description;
            QuestAvailableHere = questAvailableHere;
            MonsterLivingHere = monsterLivingHere;
        }

        public void DisplayDetails()
        {
            // Method to display details of the location.
            Console.WriteLine($"\nLocation ID: {ID}");
            Console.WriteLine($"Name: {Name}");
            Console.WriteLine($"Description: {Description}\n");

            if (QuestAvailableHere != null)
            {
                Console.WriteLine($"Quest Available Here: {QuestAvailableHere.Name}");
            }
            else
            {
                Console.WriteLine("No quest available here.");
            }

            if (MonsterLivingHere != null)
            {
                Console.WriteLine($"Monster Living Here: {MonsterLivingHere.Name}");
            }
            else
            {
                Console.WriteLine("No monster living here.");
            }

            Console.WriteLine("\nAdjacent Locations:");
            DisplayAdjacentLocations();
        }

        // Modify the StartFarmersFieldQuest method to return the next location
        public Location StartFarmersFieldQuest(Player player)
        {
            // Check if the quest has already been completed
            if (World.QuestByID(World.QUEST_ID_CLEAR_FARMERS_FIELD).IsCompleted)
            {
                Console.WriteLine("You have already completed this quest.");
                Console.WriteLine($"You've been returned to Town Sqaure.");
                return this; // Return the player to their current location
            }

            if (this.ID == World.LOCATION_ID_FARMHOUSE)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("\nA farmer approaches you and says:");
                Console.WriteLine("Farmer: Welcome, traveler! We have a problem with snakes in our field.");
                Console.WriteLine("Would you be willing to help us clear the field?");

                Console.Write("(Y)es or (N)o: ");
                string response = Console.ReadLine().ToUpper().Trim();

                if (response == "Y")
                {
                    Console.WriteLine("\nFarmer: Thank you, kind adventurer! The field is to the west. Watch out for those snakes!");

                    // You might want to update the player's quest or add logic related to the quest here.
                    return World.LocationByID(World.LOCATION_ID_FARM_FIELD); // Return the next location (Farmer's field).
                }
                else if (response == "N")
                {
                    Console.WriteLine("\nFarmer: Oh, that's unfortunate. If you change your mind, we'll be here.");
                    // You might want to handle the case where the player declines the quest.
                    return this; // Stay in the farmhouse if the player declines the quest.
                }
                else
                {
                    Console.WriteLine("Invalid input. Please choose (Y)es or (N)o.");
                    return this; // Stay in the farmhouse if there's an invalid input.
                }
            }
            else
            {
                Console.WriteLine("There's nothing special here.");
                return this; // Stay in the current location if it's not the farmhouse.
            }
        }


        public Location StartAlchemistQuest(Player player)
        {
            if (World.QuestByID(World.QUEST_ID_CLEAR_ALCHEMIST_GARDEN).IsCompleted)
            {
                Console.WriteLine("You have already completed this quest.");
                return World.LocationByID(World.LOCATION_ID_TOWN_SQUARE);
            }

            if (ID == World.LOCATION_ID_ALCHEMISTS_GARDEN)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("\nAn Alchemist approaches you and says:");
                Console.WriteLine("Hail, traveler! Might I have a moment of thy time? I am in dire need of aid.");
                Console.WriteLine("Those cursed rodents art nibbling on mine own precious herbs! 'Tis a vexing nuisance that dost threaten mine livelihood");
                Console.WriteLine("Wouldst thou be willing to venture forth and rid my garden of these troublesome pests? I would be forever indebted to thee for thy bravery");
                Console.WriteLine("(Y)es or (N)o");

                string response = Console.ReadLine().ToUpper().Trim();

                if (response == "Y")
                {
                    Console.WriteLine("\nAlchemist: Thank you, kind adventurer! The garden is to the north. Keep an eye on the rats!");
                    return World.LocationByID(World.LOCATION_ID_ALCHEMISTS_GARDEN);
                }
                else if (response == "N")
                {
                    Console.WriteLine("\nAlchemist: Oh, that's unfortunate. If you change your mind, Your help will always be appreciated.");
                    return World.LocationByID(World.LOCATION_ID_TOWN_SQUARE);
                }
                else
                {
                    Console.WriteLine("Invalid input. Please choose (Y)es or (N)o.");
                    return this;
                }
            }
            else
            {
                Console.WriteLine("There's nothing special here.");
                return this;
            }
        }


        // Method to show available locations.
        public void DisplayAdjacentLocations()
        {
            Console.WriteLine($"To North: {(LocationToNorth != null ? LocationToNorth.Name : "None")}");
            Console.WriteLine($"To East: {(LocationToEast != null ? LocationToEast.Name : "None")}");
            Console.WriteLine($"To South: {(LocationToSouth != null ? LocationToSouth.Name : "None")}");
            Console.WriteLine($"To West: {(LocationToWest != null ? LocationToWest.Name : "None")}");
            Console.WriteLine("To quit the game: Insert 'Q'");
        }

        public Location ChangeLocation(Player player, string direction)
        {
            Location newLocation = null;

            switch (direction)
            {
                case "N":
                    if (LocationToNorth != null)
                    {
                        newLocation = LocationToNorth;
                        player.CurrentLocation = newLocation.Name;
                    }
                    else
                    {
                        Console.WriteLine("Invalid direction. Please choose a valid direction.");
                    }
                    break;
                case "S":
                    if (LocationToSouth != null)
                    {
                        newLocation = LocationToSouth;
                        player.CurrentLocation = newLocation.Name;
                    }
                    else
                    {
                        Console.WriteLine("Invalid direction. Please choose a valid direction.");
                    }
                    break;
                case "E":
                    if (LocationToEast != null)
                    {
                        newLocation = LocationToEast;
                        player.CurrentLocation = newLocation.Name;
                    }
                    else
                    {
                        Console.WriteLine("Invalid direction. Please choose a valid direction.");
                    }
                    break;
                case "W":
                    if (LocationToWest != null)
                    {
                        newLocation = LocationToWest;
                        player.CurrentLocation = newLocation.Name;
                    }
                    else
                    {
                        Console.WriteLine("Invalid direction. Please choose a valid direction.");
                    }
                    break;
                // Add cases for other directions (S, E, W) if needed
                case "Q":
                    Console.WriteLine("Game closed");
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Invalid direction. Please choose a valid direction.");
                    break;
            }

            return newLocation;
        }
    }
}
