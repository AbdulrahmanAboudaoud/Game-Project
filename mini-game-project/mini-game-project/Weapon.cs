public class Weapon
{
    public int Id;
    public string Name;
    public int Damage;
    public bool Equipped;

    public Weapon(int id, string name, int damage)
    {
        Id = id;
        Name = name;
        Damage = damage;
        Equipped = false; // Default to not equipped.
    }

    public void Use()
    {
        Console.WriteLine($"You used {Name} and dealt {Damage} damage!");
    }

    public void Drop()
    {
        Console.WriteLine($"You dropped {Name}.");
    }

    public void Equip()
    {
        Equipped = true;
        Console.WriteLine($"You equipped {Name}.");
    }
}
