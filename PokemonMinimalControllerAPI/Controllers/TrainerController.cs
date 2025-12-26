using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace PokemonMinimalControllerAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TrainerController : ControllerBase
{
    private static List<Trainer> _trainers = new()
    {
        new Trainer { Id = 1, Name = "Ash", Age = 10, City = "Pallet Town", PokemonNames = new List<string> { "Pikachu" } },
        new Trainer { Id = 2, Name = "Misty", Age = 10, City = "Cerulean City", PokemonNames = new List<string> { "Staryu", "Starmie" } }
    };
    
    // GET: api/trainers
    [HttpGet]
    public IActionResult GetAll() => Ok(_trainers);
    
    // GET: api/trainers/{id}
    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var trainer = _trainers.FirstOrDefault(t => t.Id == id);
        return trainer == null ? NotFound($"Trainer with ID {id} not found") : Ok(trainer);
    }
    
    // POST: api/trainers
    [HttpPost]
    public IActionResult Create([FromBody] Trainer newTrainer)
    {
        if (string.IsNullOrEmpty(newTrainer.Name))
            return BadRequest("Trainer name is required");
        
        newTrainer.Id = _trainers.Count > 0 ? _trainers.Max(t => t.Id) + 1 : 1;
        newTrainer.PokemonNames ??= new List<string>();
        
        _trainers.Add(newTrainer);
        return CreatedAtAction(nameof(GetById), new { id = newTrainer.Id }, newTrainer);
    }
    
    // POST: api/trainers/{id}/pokemon/{pokemonName}
    [HttpPost("{id}/pokemon/{pokemonName}")]
    public IActionResult AddPokemonToTrainer(int id, string pokemonName)
    {
        var trainer = _trainers.FirstOrDefault(t => t.Id == id);
        
        if (trainer == null)
            return NotFound($"Trainer with ID {id} not found");
        
        if (trainer.PokemonNames.Contains(pokemonName))
            return BadRequest($"Pokémon '{pokemonName}' is already assigned to trainer '{trainer.Name}'");
        
        trainer.PokemonNames.Add(pokemonName);
        return Ok(trainer);
    }
}