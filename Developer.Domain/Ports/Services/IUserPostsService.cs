using Developer.Domain.Entities;
using System.Threading.Tasks;

namespace Developer.Domain.Ports.Services
{
    public interface IUserPostsService
    {
        Task<userPosts> CreatePostAsync(userPosts post);
    }
}