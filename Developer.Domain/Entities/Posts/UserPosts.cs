using Developer.Domain.Entities.Base; 
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;

namespace Developer.Domain.Entities.Posts
{
    [BsonIgnoreExtraElements]
    public class UserPosts : BaseEntity<string> 
    {
        public UserPosts(string authorId, string codeLanguage, string codeSnippet, List<Comments> comments, string description, int likes, List<string> tags, Dictionary<string, int> reactions)
        {
            AuthorId = authorId;
            CodeLanguage = codeLanguage;
            CodeSnippet = codeSnippet;
            Comments = comments;
            Description = description;
            Likes = likes;
            Tags = tags;
            Reactions = reactions;
        }
        public UserPosts()
        {
            
        }

        public string AuthorId { get;  set; }
        public string CodeLanguage { get; set; }
        public string CodeSnippet { get;  set; }
        public List<Comments> Comments { get;  set; } 
        public string Description { get;  set; }
        public int Likes { get;  set; }
        public List<string> Tags { get;  set; } 
        public Dictionary<string, int> Reactions { get; set; }


    }
}