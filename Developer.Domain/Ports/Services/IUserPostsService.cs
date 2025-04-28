using Developer.Domain.Entities.Posts;
using System.Threading.Tasks;

namespace Developer.Domain.Ports.Services
{
    public interface IUserPostsService
    {
        Task<UserPosts> CreatePostAsync(UserPosts post);
        Task<List<UserPosts>> GetUserPostForFeed(List<string> userFollowedIds, string id);
        Task<bool> UpdateCommentAsync(string postId, string commentId, string newContent);
        Task UpdateReactionAsync(string postId, string userId, string reactionType);

    }
}