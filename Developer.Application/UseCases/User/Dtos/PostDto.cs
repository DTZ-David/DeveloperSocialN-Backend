using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Developer.Application.UseCases.User.Dtos;

public record PostDto(
         string Id,
         string AuthorId,
         string CodeSnippet,
         string Description,
         string Language,
         List<string> Tags,
         DateTime CreatedAt,
         DateTime UpdatedAt
);
    