﻿using System.Diagnostics;
using Elearn.Application.LogicInterfaces;
using Elearn.Application.ServiceContracts;
using Elearn.Shared.Dtos;
using Elearn.Shared.Models;

namespace Elearn.Application.Logic;

public class UserLogic : IUserLogic
{

    private readonly IUserService _userService;

    public UserLogic(IUserService userService)
    {
        _userService = userService;
    }
    

    public async Task<User> UpdateUserAsync(UpdateUserDto dto)
    {
        User? user = await _userService.GetUserByNameAsync(dto.Name);
        Console.WriteLine(user);
        if (user == null)
         {
             throw new Exception($"User with name {dto.Name} was not found.");
         }
         var updated = user;
         updated.Password = dto.Password;
         updated.Email = dto.Email;
         updated.Image = dto.Image;
         updated.Approved = dto.Approved;
         await _userService.UpdateUserAsync(updated);
         return updated;
    }

    public async Task<User?> GetUserByUsernameAsync(string username)
    {
        User? user = await _userService.GetUserByUsernameAsync(username);
        if (user == null)
        {
            throw new Exception($"User with name {username} was not found.");
        }
        return user;
    }

    public async Task DeleteUserAsync(string username)
    {
        User? user = await _userService.GetUserByUsernameAsync(username);
        if (user == null)
        {
            throw new Exception($"User with name {username} was not found.");
        }
        await _userService.DeleteUserAsync(user);
    }

    public async Task<List<User>> GetAllUsersAsync()
    {
        return await _userService.GetAllUsersAsync();
    }
    
    public async Task<User> GetUserByNameAsync(string name)
    {
        User? user = await _userService.GetUserByNameAsync(name);
        if (user == null)
        {
            throw new Exception($"User with name {name} was not found.");
        }
        return user;
    }
}