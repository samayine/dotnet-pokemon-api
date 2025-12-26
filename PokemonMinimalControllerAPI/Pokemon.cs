using System;

public class Pokemon
{
    public string Name { get; set; }
    public int Level { get; set; }
    
    // EVENT: This will notify when level changes
    public event Action<string, int>? LeveledUp;
    
    public Pokemon(string name)
    {
        Name = name;
        Level = 1;
    }
    
    public void GainExperience(int amount)
    {
        Level += amount;
        LeveledUp?.Invoke(Name, amount);
    }
}