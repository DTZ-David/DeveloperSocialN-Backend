using Developer.Domain.Entities.Base; // Asegúrate de incluir esta directiva
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;

namespace Developer.Domain.Entities
{
    [BsonIgnoreExtraElements]
    public class Post : BaseEntity<string> 
    {
        [BsonElement("authorId")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string AuthorId { get; private set; }

        [BsonElement("codeSnippet")]
        public string CodeSnippet { get; private set; }

        [BsonElement("comments")]
        public List<string> Comments { get; private set; } = new List<string>();

        [BsonElement("createdAt")]
        public DateTime CreatedAt { get; private set; }

        [BsonElement("description")]
        public string Description { get; private set; }

        [BsonElement("language")]
        public string Language { get; private set; }

        [BsonElement("likes")]
        public List<string> Likes { get; private set; } = new List<string>();

        [BsonElement("tags")]
        public List<string> Tags { get; private set; } = new List<string>();

        [BsonElement("updatedAt")]
        public DateTime UpdatedAt { get; private set; }

        public Post(
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