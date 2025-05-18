using Developer.Domain.Ports.Configuration.Localization;
using Developer.Domain.Ports;
using Developer.Domain.Ports.Services;
using System.Threading.Tasks;
using Developer.Domain.Entities.Posts;
using Developer.Domain.Entities.User;

namespace Developer.Domain.Services
{
    [ApplicationService]
    public class UserPostsService : IUserPostsService
    {
        private readonly IGenericRepository<UserPosts> _postRepository;
        private readonly IGenericRepository<Comments> _commentRepository;
        private readonly ILocalizationService _localizationService;

        

        public UserPostsService(IGenericRepository<UserPosts> postRepository, IGenericRepository<Comments> commentRepository, ILocalizationService localizationService)
        {
            _postRepository = postRepository;
            _commentRepository = commentRepository;
            _localizationService = localizationService;
        }

        public async Task<UserPosts> CreatePostAsync(UserPosts userPosts)
        {
            await _postRepository.Add(userPosts);

            return userPosts;
        }

        public async Task<List<UserPosts>> GetUserPostById(string id)
        {
            var allPosts = await _postRepository.FindAsync(
                post => post.AuthorId == id);

            return allPosts
                .OrderByDescending(p => p.CreationDate)
                .ToList();
        }

        public async Task<List<UserPosts>> GetUserPostForFeed(List<string> followedUserIds, string userId)
        {
           
            var allPosts = await _postRepository.FindAsync(
                post => post.AuthorId == userId || followedUserIds.Contains(post.AuthorId)
            );

            return allPosts
                .OrderByDescending(p => p.CreationDate)
                .ToList();
        }


        public async Task<bool> AddCommentAsync(Comments commentAdd)
        {
           
            await _commentRepository.Add(commentAdd);
            

            return true;
        }

        public async Task UpdateReactionAsync(string postId, string userId, string reactionType)
        {
            var post = await _postRepository.GetById(postId);
            if (post == null) throw new Exception("Post no encontrado");

          
            post.Likes += 1;

            await _postRepository.Update(post);
        }

        public async Task<UserPosts> GetUserPostByPostId(string id)
        {
            var post = await _postRepository.GetById(id);

            return post;
        }

        public async Task<UserPosts> UpdatePostAsync(UserPosts post)
        {
            await _postRepository.Update(post);

            return post;
        }

    }
}