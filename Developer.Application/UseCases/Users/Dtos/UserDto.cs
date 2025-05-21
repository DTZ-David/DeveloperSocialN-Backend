using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Developer.Application.UseCases.User.Dtos;

public record UserDto(
    string Email,
    string UserName,
    string ProfilePicture,
    string Bio,
    int PostsCount,
    int FollowersCount,
    bool CurrentFollow
    );
