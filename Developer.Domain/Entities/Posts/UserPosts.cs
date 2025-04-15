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
        public UserPosts(string authorId, string codeSnippet, List<Comments> comments, string description, int likes, List<string> tags)
        {
            AuthorId = authorId;
            CodeSnippet = codeSnippet;
            Comments = comments;
            Description = description;
            Likes = likes;
            Tags = tags;
        }

        public string AuthorId { get;  set; }
        public string CodeSnippet { get;  set; }
        public List<Comments> Comments { get;  set; } 
        public string Description { get;  set; }
        public int Likes { get;  set; }
        public List<string> Tags { get;  set; } 
       
    }
}