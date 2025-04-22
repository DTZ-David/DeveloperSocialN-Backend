namespace Developer.Application.UseCases.Posts.Dtos;

public record AddReactionDto
{
    public string PostId { get; init; }
    public string ReactionType { get; init; } 
}
