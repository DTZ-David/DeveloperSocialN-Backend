using Developer.Domain.Entities;
using System.Threading.Tasks;

namespace Developer.Domain.Ports.Services
{
    public interface IPostService
    {
        Task<Post> CreatePostAsync(Post post);
    }
}