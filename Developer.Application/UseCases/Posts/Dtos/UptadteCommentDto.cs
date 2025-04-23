namespace Developer.Application.UseCases.Posts.Dtos;

public record UpdateCommentDto
{
    public string CommentId { get; init; }
    public string NewContent { get; init; }
}
