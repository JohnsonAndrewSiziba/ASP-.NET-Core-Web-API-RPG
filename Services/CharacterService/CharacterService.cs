using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RPG.Data;
using RPG.Dtos.Character;
using RPG.Models;

namespace RPG.Services.CharacterService;

public class CharacterService : ICharacterService
{
    private readonly IMapper _mapper;
    private readonly DataContext _context;

    public CharacterService(IMapper mapper, DataContext context)
    {
        _mapper = mapper;
        _context = context;
    }
    
    private static List<Character> _characters = new List<Character>
    {
        new Character(),
        new Character { Id = 1, Name = "Sam" },
        new Character { Id = 2,  Name = "Sally" }
    };
    
    public async Task<ServiceResponse<List<GetCharacterDto>>> GetAllCharacters()
    {
        var serviceResponse = new ServiceResponse<List<GetCharacterDto>>();
        var dbCharacters =  await _context.Characters.ToListAsync();
        serviceResponse.Data = dbCharacters.Select(c => _mapper.Map<GetCharacterDto>(c)).ToList();
        return serviceResponse;
    }

    public async Task<ServiceResponse<GetCharacterDto>> GetCharacterById(int id)
    {
        var serviceResponse = new ServiceResponse<GetCharacterDto>();
        var dbCharacter =  await _context.Characters.FirstOrDefaultAsync(c => c.Id == id);
        serviceResponse.Data = _mapper.Map<GetCharacterDto>(dbCharacter);
        return serviceResponse;
    }

    public async Task<ServiceResponse<List<GetCharacterDto>>> AddCharacter(AddCharacterDto newCharacter)
    {
        _characters.Add(_mapper.Map<Character>(newCharacter));
        
        var serviceResponse = new ServiceResponse<List<GetCharacterDto>>();
        serviceResponse.Data = _characters.Select(c => _mapper.Map<GetCharacterDto>(c)).ToList();;
        return serviceResponse;
    }

    public async Task<ServiceResponse<List<GetCharacterDto>>> EditCharacter(GetCharacterDto editCharacter)
    {
        var serviceResponse = new ServiceResponse<List<GetCharacterDto>>();
        var character = _characters.FirstOrDefault(c => c.Id == editCharacter.Id);
        if (character != null)
        {
            character.Name = editCharacter.Name;
            character.HitPoints = editCharacter.HitPoints;
            character.Strength = editCharacter.Strength;
            character.Defense = editCharacter.Defense;
            character.Intelligence = editCharacter.Intelligence;
            character.Class = editCharacter.Class;
        }

        else
        {
            serviceResponse.Success = false;
            serviceResponse.Message = "Character not found.";
        }
        
        serviceResponse.Data = _characters.Select(c => _mapper.Map<GetCharacterDto>(c)).ToList();
        return serviceResponse;
    }
}