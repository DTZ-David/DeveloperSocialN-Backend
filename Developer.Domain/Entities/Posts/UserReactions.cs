namespace Developer.Domain.Entities.Posts
{
    public class Reaction
    {
        public string UserId { get; set; } 
        public string Type { get; set; }  
        

        public Reaction(string userId, string type)
        {
            UserId = userId;
            Type = type;
            
        }
    }
}
