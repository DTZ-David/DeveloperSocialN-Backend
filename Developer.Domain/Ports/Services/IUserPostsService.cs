using Developer.Domain.Entities.Posts;
using System.Threading.Tasks;

namespace Developer.Domain.Ports.Services
{
    public interface IUserPostsService
    {
        Task<userPosts> CreatePostAsync(userPosts post);
    }
}