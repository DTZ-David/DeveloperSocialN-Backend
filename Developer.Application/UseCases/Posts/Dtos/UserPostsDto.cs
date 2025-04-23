using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Developer.Application.UseCases.Posts.Dtos;

public record UserPostsDto()
{
    public string Description { get; init; }
    public List<string> Tags { get; init; }
    public DateTime CreatedAt { get; init; }
}

