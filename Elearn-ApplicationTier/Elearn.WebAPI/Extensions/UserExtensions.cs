﻿using Elearn.Shared.Dtos;
using Elearn.Shared.Models;

namespace Shared.Extensions;

public static class UserExtensions
{
    public static UserDto AsDto(this User user) 
    {
        return new UserDto
        {
            Id = user.Id,
            Username = user.Username,
            Password = user.Password,
            Email = user.Email,
            Name = user.Name,
            Role = user.Role,
            Image = user.Image,
            SecurityLevel = user.SecurityLevel,
            
        };
    }
    
    public static UserForCommentDto AsUserForCommentDto(this User user) 
    {
        return new UserForCommentDto
        {
         
            Username = user.Username,
            Name = user.Name,
            Image = user.Image,
         
            
        };
    }
}