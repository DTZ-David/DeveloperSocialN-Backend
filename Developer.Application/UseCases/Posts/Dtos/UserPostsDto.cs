using Developer.Domain.Entities.Posts;

public class UserPostsDto
{
    public string Id { get; set; }
    public string AuthorId { get; set; }
    public string FechaPublicacion { get; set; }
    public string CodeLanguage { get; set; }
    public string CodeSnippet { get; set; }
    public string Description { get; set; }
    public List<string> Tags { get; set; } = new();
    public int Likes { get; set; }
    public string UserName { get; set; }
    public string ProfilePicture { get; set; }
    public Dictionary<string, int> Reactions { get; set; } = new();

    public UserPostsDto() { }

    public UserPostsDto(string id, string authorId, string fechaPublicacion, string codeLanguage, string codeSnippet, string description, List<string> tags, int likes, string userName, string profilePicture, Dictionary<string, int> reactions)
    {
        Id = id;
        AuthorId = authorId;
        FechaPublicacion = fechaPublicacion;
        CodeLanguage = codeLanguage;
        CodeSnippet = codeSnippet;
        Description = description;
        Tags = tags;
        Likes = likes;
        UserName = userName;
        ProfilePicture = profilePicture;
        Reactions = reactions;
    }
}
