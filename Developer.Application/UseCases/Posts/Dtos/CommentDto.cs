using Developer.Domain.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Developer.Application.UseCases.Posts.Dtos;

public record CommentDto(
 string Id,
 string PostId,
 string CommentText,
 InteractionType InteractionType,
 string AuthorId,
 string AuthorProfilePic, 
 string UserName, 
 string SentAt,
 bool IsOwnComment
);


