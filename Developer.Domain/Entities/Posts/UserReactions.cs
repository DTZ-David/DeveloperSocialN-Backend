namespace Developer.Domain.Entities.Posts
{
    public class Reaction
    {
        public string UserId { get; set; }  // El ID del usuario que reacciona
        public string Type { get; set; }  // El tipo de reacción, como "like", "dislike"
        

        public Reaction(string userId, string type)
        {
            UserId = userId;
            Type = type;
            
        }
    }
}
