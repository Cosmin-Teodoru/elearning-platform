using System.ComponentModel.DataAnnotations;
using Elearn.Application.LogicInterfaces;
using Elearn.Application.ServiceContracts;
using Elearn.Shared.Dtos;
using Elearn.Shared.Models;

namespace Elearn.Application.Logic;

public class AuthLogic : IAuthLogic
{
    private readonly IUserService _userService;
    private readonly IUniversityService _universityService;
    private readonly ICountryService _countryService;

    public AuthLogic(IUserService userService, IUniversityService universityService, ICountryService countryService)
    {
        _userService = userService;
        _universityService = universityService;
        _countryService = countryService;
    }

    public async Task<User> ValidateUserAsync(string username, string password)
    {
        User? existingUser = await _userService.GetUserByUsernameAsync(username);
        
        if (existingUser == null)
        {
            throw new Exception("User not found");
        }

        if (!existingUser.Password.Equals(password))
        {
            throw new Exception("Password mismatch");
        }

        return existingUser;
    }

    public async Task<User> GetUserAsync(string username, string password)
    {
        throw new NotImplementedException();
    }

    public async Task<User> RegisterUserAsync(UserCreationDto dto)
    {
        if (string.IsNullOrEmpty(dto.Username))
        {
            throw new ValidationException("Username cannot be null");
        }
        if (string.IsNullOrEmpty(dto.Password))
        {
            throw new ValidationException("Password cannot be null");
        }
        if (string.IsNullOrEmpty(dto.Email))
        {
            throw new ValidationException("Email cannot be null");
        }
        if (string.IsNullOrEmpty(dto.Name))
        {
            throw new ValidationException("Name cannot be null");
        }
        
        if (dto.UniversityId == 0)
        {
            throw new ValidationException("University cannot be null");
        }

        University university = await _universityService.GetUniversityByIdAsync(dto.UniversityId);
        if (university is null)
        {
            throw new ValidationException("University does not exist");
        }

        Country country = await _countryService.GetCountryByIdAsync(dto.CountryId);
        if (country is null)
        {
            throw new ValidationException("Country does not exist");
        }
        User user = new User(dto.Username, dto.Password, dto.Email, dto.Name, dto.Role, dto.Image ,dto.SecurityLevel,university,dto.Approved, country);
        User created = await _userService.CreateNewUserAsync(user);
        return created;
    }
}