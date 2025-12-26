public class PokemonService : IPokemonService
{
    private static List<Pokemon> _pokemons = new()
    {
        new Pokemon("Pikachu"),
        new Pokemon("Charmander"),
        new Pokemon("yoo")
    };

    public List<Pokemon> GetAll()
    {
        return _pokemons;
    }

    public Pokemon? GetByName(string name)
    {
        return _pokemons.FirstOrDefault(p => p.Name == name);
    }

    public bool Exists(string name)
    {
        return _pokemons.Any(p => p.Name == name);
    }

    public Pokemon Create(Pokemon pokemon)
    {
        _pokemons.Add(pokemon);
        return pokemon;
    }

    public bool Delete(string name)
    {
        var pokemon = GetByName(name);
        if (pokemon == null) return false;

        _pokemons.Remove(pokemon);
        return true;
    }

    public Pokemon? Train(string name, int amount)
    {
        var pokemon = GetByName(name);
        if (pokemon == null) return null;

        pokemon.GainExperience(amount);
        return pokemon;
    }
}
