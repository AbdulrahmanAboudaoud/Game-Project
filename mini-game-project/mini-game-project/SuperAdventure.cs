using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mini_game_project
{
    internal class SuperAdventure
    {
        public string CurrentMonster;
        public string ThePlayer;

        public SuperAdventure(string currentMonster, string thePlayer)
        {
            CurrentMonster = currentMonster;
            ThePlayer = thePlayer;
        }
    }
}
