using Microsoft.AspNetCore.Mvc;

namespace PokemonMinimalControllerAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PokemonController : ControllerBase
{
    // Pokémon storage 
    private static List<Pokemon> _pokemons = new()
    {
        new Pokemon("Pikachu"),
        new Pokemon("Charmander"),
        new Pokemon("yoo")
    };
    
    // GET: api/pokemon
    [HttpGet]
    public IActionResult GetAll()
    {
        return Ok(_pokemons);
    }

    // GET: api/pokemon/{name}
    [HttpGet("{name}")]
    public IActionResult GetByName(string name)
    {
        var pokemon = _pokemons.FirstOrDefault(p => p.Name == name);
        
        if (pokemon == null)
        {
            return NotFound($"Pokémon '{name}' not found");
        }
        
        return Ok(pokemon);
    }

    // POST: api/pokemon
    [HttpPost]
    public IActionResult Create([FromBody] Pokemon newPokemon)
    {
        // Check if Pokémon already exists
        if (_pokemons.Any(p => p.Name == newPokemon.Name))
        {
            return Conflict($"Pokémon '{newPokemon.Name}' already exists");
        }
        
        // Add to our list
        _pokemons.Add(newPokemon);
        
        // Return 201 Created with location header
        return CreatedAtAction(
            nameof(GetByName), 
            new { name = newPokemon.Name }, 
            newPokemon);
    }

    // DELETE: api/pokemon/{name}
    [HttpDelete("{name}")]
    public IActionResult Delete(string name)
    {
        var pokemon = _pokemons.FirstOrDefault(p => p.Name == name);
        
        if (pokemon == null)
        {
            return NotFound($"Pokémon '{name}' not found");
        }
        
        _pokemons.Remove(pokemon);
        
        return Ok(_pokemons);
    }

    // POST: api/pokemon/{name}/train/{amount}
    [HttpPost("{name}/train/{amount}")]
    public IActionResult Train(string name, int amount)
    {
        var pokemon = _pokemons.FirstOrDefault(p => p.Name == name);
        
        if (pokemon == null)
        {
            return NotFound($"Pokémon '{name}' not found");
        }
        
        pokemon.GainExperience(amount);
        
        return Ok(pokemon);
    }
    
}
