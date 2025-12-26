using Microsoft.AspNetCore.Mvc;

namespace PokemonMinimalControllerAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PokemonController : ControllerBase
{
    private readonly IPokemonService _pokemonService;

    public PokemonController(IPokemonService pokemonService)
    {
        _pokemonService = pokemonService;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        return Ok(_pokemonService.GetAll());
    }

    [HttpGet("{name}")]
    public IActionResult GetByName(string name)
    {
        var pokemon = _pokemonService.GetByName(name);

        if (pokemon == null)
            return NotFound($"Pokémon '{name}' not found");

        return Ok(pokemon);
    }

    [HttpPost]
    public IActionResult Create([FromBody] Pokemon newPokemon)
    {
        if (_pokemonService.Exists(newPokemon.Name))
            return Conflict($"Pokémon '{newPokemon.Name}' already exists");

        _pokemonService.Create(newPokemon);

        return CreatedAtAction(
            nameof(GetByName),
            new { name = newPokemon.Name },
            newPokemon);
    }

    [HttpDelete("{name}")]
    public IActionResult Delete(string name)
    {
        var success = _pokemonService.Delete(name);

        if (!success)
            return NotFound($"Pokémon '{name}' not found");

        return Ok();
    }

    [HttpPost("{name}/train/{amount}")]
    public IActionResult Train(string name, int amount)
    {
        var pokemon = _pokemonService.Train(name, amount);

        if (pokemon == null)
            return NotFound($"Pokémon '{name}' not found");

        return Ok(pokemon);
    }
}
