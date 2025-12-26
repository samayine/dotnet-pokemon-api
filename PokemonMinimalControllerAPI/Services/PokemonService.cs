using MongoDB.Driver;
using Microsoft.Extensions.Options;

public class PokemonService : IPokemonService
{
    private readonly IMongoCollection<Pokemon> _pokemonCollection;

    public PokemonService(IOptions<DBSettings> dbSettings)
    {
        var mongoClient = new MongoClient(dbSettings.Value.ConnectionString);
        var database = mongoClient.GetDatabase(dbSettings.Value.DatabaseName);
        _pokemonCollection = database.GetCollection<Pokemon>("Pokemon");
    }

    public List<Pokemon> GetAll()
    {
        return _pokemonCollection.Find(_ => true).ToList();
    }

    public Pokemon? GetByName(string name)
    {
        return _pokemonCollection.Find(p => p.Name == name).FirstOrDefault();
    }

    public bool Exists(string name)
    {
        return _pokemonCollection.Find(p => p.Name == name).Any();
    }

    public Pokemon Create(Pokemon pokemon)
    {
        _pokemonCollection.InsertOne(pokemon);
        return pokemon;
    }

    public bool Delete(string name)
    {
        var result = _pokemonCollection.DeleteOne(p => p.Name == name);
        return result.DeletedCount > 0;
    }

    public Pokemon? Train(string name, int amount)
    {
        var pokemon = GetByName(name);
        if (pokemon == null || pokemon.Id == null) return null;

        pokemon.GainExperience(amount);
        _pokemonCollection.ReplaceOne(p => p.Id == pokemon.Id, pokemon);
        return pokemon;
    }
}
