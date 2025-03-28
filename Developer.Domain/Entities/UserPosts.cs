using Developer.Domain.Entities.Base; // Asegúrate de incluir esta directiva
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;

namespace Developer.Domain.Entities
{
    [BsonIgnoreExtraElements]
    public class userPosts : BaseEntity<string> 
    {
        public string AuthorId { get; private set; }
        public string CodeSnippet { get; private set; }
        public List<string> Comments { get; private set; } = new List<string>();
        public DateTime CreatedAt { get; private set; }
        public string Description { get; private set; }
        public string Language { get; private set; }
        public List<string> Likes { get; private set; } = new List<string>();
        public List<string> Tags { get; private set; } = new List<string>();
        public DateTime UpdatedAt { get; private set; }
        public userPosts(
            string authorId,
            string codeSnippet,
            string description,
            string language,
            List<string> tags = null
        )
        {
            Id = ObjectId.GenerateNewId().ToString(); // Generar un ID único
            AuthorId = authorId ?? throw new ArgumentNullException(nameof(authorId));
            CodeSnippet = codeSnippet ?? throw new ArgumentNullException(nameof(codeSnippet));
            Description = description ?? throw new ArgumentNullException(nameof(description));
            Language = language ?? throw new ArgumentNullException(nameof(language));
            Tags = tags ?? new List<string>();
            CreatedAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;
        }

        public void AddComment(string commentId)
        {
            if (!Comments.Contains(commentId))
            {
                Comments.Add(commentId);
                UpdatedAt = DateTime.UtcNow;
            }
        }

        public void AddLike(string userId)
        {
            if (!Likes.Contains(userId))
            {
                Likes.Add(userId);
                UpdatedAt = DateTime.UtcNow;
            }
        }

        public void RemoveLike(string userId)
        {
            if (Likes.Contains(userId))
            {
                Likes.Remove(userId);
                UpdatedAt = DateTime.UtcNow;
            }
        }
    }
}