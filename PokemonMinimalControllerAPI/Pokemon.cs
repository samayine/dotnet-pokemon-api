using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

public class Pokemon
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }
    public required string Name { get; set; }
    public int Level { get; private set; }
    
    // EVENT: This will notify when level changes
    public event Action<string, int>? LeveledUp;
    
    public Pokemon()
    {
        Level = 1;
    }

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