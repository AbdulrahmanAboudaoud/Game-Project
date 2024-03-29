using mini_game_project;

using System;
class Player
{
    // Fields (attributes)
    //... ID field toevoegen?
    public string Name;
    public int Age;
    public string Gender;
    public int CurrentHitPoints;
    public string CurrentLocation;
    public string CurrentWeapon;
    public int MaximumHitPoints;

    // Constructor
    public Player(string name, int age, string gender, int currenthp, string currentlocation, string currentweapon, int maxhp)
    {
        Name = name;
        Age = age;
        Gender = gender;
        CurrentHitPoints = currenthp;
        CurrentLocation = currentlocation;
        CurrentWeapon = currentweapon;
        MaximumHitPoints = maxhp;
    }
    // Print info about the player to confirm data
    public void Confirm()
    {
        Console.WriteLine($"Name: {Name}, Age: {Age}, Gender: {Gender}");
        //Speler een mogelijkheid geven om alles te kunnen lezen en bevestigen
        //Als iets niet klopt kunnen ze altijd "n" typen dan kunnen ze alles opnieuw typen
    }

    public int Attack()
    {
        // Assuming you have a Weapon object representing the player's equipped weapon
        Weapon equippedWeapon = World.WeaponByID(World.WEAPON_ID_RUSTY_SWORD); // Change this based on your game logic

        // Simulating an attack using the weapon's damage
        int damage = equippedWeapon.Damage;

        // You can add more complex logic here if needed

        return damage;
    }

}
