public interface IPokemonService
{
    List<Pokemon> GetAll();
    Pokemon? GetByName(string name);
    bool Exists(string name);
    Pokemon Create(Pokemon pokemon);
    bool Delete(string name);
    Pokemon? Train(string name, int amount);
}
