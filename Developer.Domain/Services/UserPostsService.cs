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
        private readonly IGenericRepository<UserPosts> _postRepository;
        private readonly ILocalizationService _localizationService;

        public UserPostsService(IGenericRepository<UserPosts> postRepository, ILocalizationService localizationService)
        {
            _postRepository = postRepository;
            _localizationService = localizationService;
        }

        public async Task<UserPosts> CreatePostAsync(UserPosts userPosts)
        {
            await _postRepository.Add(userPosts);

            return userPosts;
        }

        public async Task<List<UserPosts>> GetUserPostForFeed()
        {
            var responsePost = await _postRepository.GetAll();

            return responsePost.ToList();
        }
    }
}