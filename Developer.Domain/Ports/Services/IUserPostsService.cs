using Developer.Domain.Entities.Posts;
using System.Threading.Tasks;

namespace Developer.Domain.Ports.Services
{
    public interface IUserPostsService
    {
        Task<UserPosts> CreatePostAsync(UserPosts post);
        Task<List<UserPosts>> GetUserPostForFeed();
    }
}