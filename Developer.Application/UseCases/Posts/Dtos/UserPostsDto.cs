using Developer.Domain.Entities.Posts;

public record UserPostsDto(
    string AuthorId,
    string CodeSnippet,
    List<Comments> Comments,
    string Description,
    List<string> Tags,
    int Likes,
    string UserName,
    string ProfilePicture
);

