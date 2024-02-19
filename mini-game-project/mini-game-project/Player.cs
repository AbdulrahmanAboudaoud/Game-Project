using System;
class Player
{
    // Fields (attributes)
    //... ID field toevoegen?
    public string Name;
    public int Age;
    public string Gender;

    // Constructor
    public Player(string name, int age, string gender)
    {
        Name = name;
        Age = age;
        Gender = gender;
    }
    // Print info about the player to confirm data
    public void Confirm()
    {
        Console.WriteLine($"Name: {Name}, Age: {Age}, Gender: {Gender}");
        //Speler een mogelijkheid geven om alles te kunnen lezen en bevestigen
        //Als iets niet klopt kunnen ze altijd "n" typen dan kunnen ze alles opnieuw typen
    }
    public int attack()
    {
        //Implementing weapon (weapon damage stats and implement attack logic here)
        //it returns amount of the damage made
    }
}

class Test
{
    static void Main()
    {
        Player player = new Player("testplayer", 18, "Male");
        player.Confirm();
    }
}
