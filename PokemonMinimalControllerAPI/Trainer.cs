public class Trainer
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Age { get; set; }
    public string City { get; set; }
    public List<string> PokemonNames { get; set; } = new();
}