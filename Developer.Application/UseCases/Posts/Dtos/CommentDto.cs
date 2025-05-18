using Developer.Domain.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Developer.Application.UseCases.Posts.Dtos;

public record CommentDto(
 string PostId,
 string CommentText,
 InteractionType InteractionType,
 string AuthorId,
 bool IsOwnComment // true si el usuario lo hizo, false si se lo hicieron
);
