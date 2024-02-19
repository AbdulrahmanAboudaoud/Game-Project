using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mini_game_project
{
    internal class Location
    {
        public int ID { get; }
        public string Name { get; }
        public string Description { get; }
        public Quest QuestAvailableHere { get; set; }
        public Monster MonsterLivingHere { get; set; }
        public Location LocationToNorth { get; set; }
        public Location LocationToEast { get; set; }
        public Location LocationToSouth { get; set; }
        public Location LocationToWest { get; set; }

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
            Console.WriteLine($"Location ID: {ID}");
            Console.WriteLine($"Name: {Name}");
            Console.WriteLine($"Description: {Description}");

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

            Console.WriteLine("Adjacent Locations:");
            DisplayAdjacentLocations();
        }

        // Method to show available locations.
        private void DisplayAdjacentLocations()
        {
            Console.WriteLine($"To North: {(LocationToNorth != null ? LocationToNorth.Name : "None")}");
            Console.WriteLine($"To East: {(LocationToEast != null ? LocationToEast.Name : "None")}");
            Console.WriteLine($"To South: {(LocationToSouth != null ? LocationToSouth.Name : "None")}");
            Console.WriteLine($"To West: {(LocationToWest != null ? LocationToWest.Name : "None")}");
        }
    }
}
