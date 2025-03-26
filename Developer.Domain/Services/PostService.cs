using Developer.Domain.Entities;
using Developer.Domain.Ports.Configuration.Localization;
using Developer.Domain.Ports;
using Developer.Domain.Ports.Services;
using System.Threading.Tasks;

namespace Developer.Domain.Services
{
    [ApplicationService]
    public class PostService : IPostService
    {
        private readonly IGenericRepository<Post> _postRepository;
        private readonly ILocalizationService _localizationService;

        public PostService(IGenericRepository<Post> postRepository, ILocalizationService localizationService)
        {
            _postRepository = postRepository;
            _localizationService = localizationService;
        }

        public async Task<Post> CreatePostAsync(Post post)
        {
            await _postRepository.Add(post);
            return post;
        }
    }
}