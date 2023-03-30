using Microsoft.AspNetCore.Mvc;
using RPG.Dtos.Character;
using RPG.Models;
using RPG.Services.CharacterService;

namespace RPG.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CharacterController : ControllerBase
{
    private readonly ICharacterService _characterService;


    public CharacterController(ICharacterService characterService)
    {
        _characterService = characterService;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ServiceResponse<GetCharacterDto>>> GetCharacter(int id)
    {
        return Ok(await _characterService.GetCharacterById(id));
    }
    
    [HttpGet("GetAll")]
    public async Task<ActionResult<ServiceResponse<List<GetCharacterDto>>>> GetAll()
    {
        return Ok(await _characterService.GetAllCharacters());
    }
    
    [HttpPost]
    public async Task<ActionResult<ServiceResponse<List<GetCharacterDto>>>> AddCharacter(AddCharacterDto newCharacter)
    {
        return Ok(await _characterService.AddCharacter(newCharacter));
    }
    
    [HttpPut]
    public async Task<ActionResult<ServiceResponse<List<GetCharacterDto>>>> EditCharacter(GetCharacterDto editCharacter)
    {
        return Ok(await _characterService.EditCharacter(editCharacter));
    }
    
    
}