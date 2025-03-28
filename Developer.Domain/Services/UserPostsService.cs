using Developer.Domain.Ports.Configuration.Localization;
using Developer.Domain.Ports;
using Developer.Domain.Ports.Services;
using System.Threading.Tasks;
using Developer.Domain.Entities.Posts;

namespace Developer.Domain.Services
{
    [ApplicationService]
    public class UserPostsService : IUserPostsService
    {
        private readonly IGenericRepository<userPosts> _postRepository;
        private readonly ILocalizationService _localizationService;

        public UserPostsService(IGenericRepository<userPosts> postRepository, ILocalizationService localizationService)
        {
            _postRepository = postRepository;
            _localizationService = localizationService;
        }

        public async Task<userPosts> CreatePostAsync(userPosts userPosts)
        {
            await _postRepository.Add(userPosts);
            return userPosts;
        }
    }
}