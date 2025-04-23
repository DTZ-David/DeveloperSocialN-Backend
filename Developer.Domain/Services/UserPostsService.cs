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

        public async Task<bool> UpdateCommentAsync(string postId, string commentId, string newContent)
        {
            var userPost = await _postRepository.GetById(postId);
            if (userPost == null) return false;

            var comment = userPost.Comments.FirstOrDefault(c => c.Id == commentId);
            if (comment == null) return false;

            comment.Comment = newContent;
            comment.CreationDate = DateTime.UtcNow;

            await _postRepository.Update(userPost); // <== Aquí usamos la sobrecarga correcta
            return true;
        }

        public async Task UpdateReactionAsync(string postId, string userId, string reactionType)
        {
            var post = await _postRepository.GetById(postId);
            if (post == null) throw new Exception("Post no encontrado");

          
            post.Likes += 1;

            await _postRepository.Update(post);
        }



    }
}