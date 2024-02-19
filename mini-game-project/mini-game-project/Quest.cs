using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mini_game_project
{
    internal class Quest
    {
        public int ID { get; }
        public string Name { get; }
        public string Description { get; }
        public bool IsCompleted { get; set; }

        public Quest(int id, string name, string description)
        {
            ID = id;
            Name = name;
            Description = description;
            IsCompleted = false; // Initially, the quest is not completed.
        }

        public void Start()
        {
            // Logic to start the quest.
            Console.WriteLine($"Quest '{Name}' started!");
        }

        public void Complete()
        {
            // Logic to complete the quest.
            IsCompleted = true;
            Console.WriteLine($"Quest '{Name}' completed!");
        }

        public void CheckProgress()
        {
            // Method to check the progress of the quest.
            if (IsCompleted)
            {
                Console.WriteLine($"Quest '{Name}' is completed.");
            }
            else
            {
                Console.WriteLine($"Quest '{Name}' is not completed yet.");
            }
        }

        // Method to display details of the quest.
        public void DisplayDetails()
        {
            Console.WriteLine($"Quest ID: {ID}");
            Console.WriteLine($"Name: {Name}");
            Console.WriteLine($"Description: {Description}");
            Console.WriteLine($"Status: {(IsCompleted ? "Completed" : "Not Completed")}");
        }
    }
}
