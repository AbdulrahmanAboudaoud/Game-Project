using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Weapon
{
    public int Id;
    public string Name;
    public int Damage;

    public Weapon(int id, string name, int damage)
    {
        Id = id;
        Name = name;
        Damage = damage;
    }

}