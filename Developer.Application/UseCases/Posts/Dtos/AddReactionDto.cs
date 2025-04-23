namespace Developer.Application.UseCases.Posts.Dtos;

public record AddReactionDto
{
    public string ReactionType { get; init; }
}