using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Developer.Application.UseCases.Users.Dtos;

public record AuthenticationUserDto(
    string Token,
    string Username,
    string ProfilePicture,
    string Bio,
    int PostsCount,
    int FollowersCount
);

public record AccountDto(string Email, string Password);