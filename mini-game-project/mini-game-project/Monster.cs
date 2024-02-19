using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mini_game_project
{
    internal class Monster
    {
        public string Name;
        public int ID;
        public int CurrentHitPoints;
        public int MaximumDamagepoints;
        public int MaximunHitPoints;

        public Monster(int id, string name, int currentHitPoints, int maximumDamagepoints, int maximunHitPoints)
        {
            ID = id;
            Name = name;
            CurrentHitPoints = currentHitPoints;
            MaximumDamagepoints = maximumDamagepoints;
            MaximunHitPoints = maximunHitPoints;

        }
    }
}