using Developer.Domain.Entities.Posts;

public class UserPostsDto
{
   
    public UserPostsDto(string authorId, string codeSnippet, List<Comments> comments, string description, List<string> tags, int likes, string username, string profilePicture)
    {
        AuthorId = authorId;
        CodeSnippet = codeSnippet;
        Comments = comments;
        Description = description;
        Tags = tags;
        Likes = likes;
        UserName = username;
        ProfilePicture = profilePicture;
    }
    public UserPostsDto()
    {
        
    }

    public string AuthorId { get; set; }
    public string CodeSnippet { get; set; }
    public List<Comments> Comments { get; set; }
    public string Description { get; set; }
    public List<string> Tags { get; set; }
    public int Likes { get; set; }
    public string UserName { get; set; }
    public string ProfilePicture { get; set; }

    
}
