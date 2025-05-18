using Developer.Domain.Entities.Posts;
using System.Threading.Tasks;

namespace Developer.Domain.Ports.Services
{
    public interface IUserPostsService
    {
        Task<UserPosts> CreatePostAsync(UserPosts post);
        Task<List<UserPosts>> GetUserPostForFeed(List<string> userFollowedIds, string id);
        Task<List<UserPosts>> GetUserPostById(string id);
        Task<UserPosts> GetUserPostByPostId(string id);
        Task<bool> AddCommentAsync(Comments comments);
        Task UpdateReactionAsync(string postId, string userId, string reactionType);
        Task<UserPosts> UpdatePostAsync(UserPosts id);
        
    }
}