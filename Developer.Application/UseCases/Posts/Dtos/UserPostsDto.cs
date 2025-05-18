using Developer.Domain.Entities.Posts;

public record class UserPostsDto(
    string Id,
    string AuthorId,
    string FechaPublicacion,
    string CodeLanguage,
    string CodeSnippet,
    string Description,
    List<string> Tags,
    int Likes,
    string UserName,
    string ProfilePicture,
    Dictionary<string, int> Reactions
);
