using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Developer.Application.UseCases.Users.Dtos;

public record FollowersDto
(
    string UserId,
    int FollowersCount, 
    List<string> Followers
);
