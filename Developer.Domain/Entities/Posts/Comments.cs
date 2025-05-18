using Developer.Domain.Entities.Base;
using Developer.Domain.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Developer.Domain.Entities.Posts
{
    public class Comments : BaseEntity<string>
    {
        public Comments(string authorId, string comment, InteractionType interactionType)
        {
          
            AuthorId = authorId;
            Comment = comment;
            InteractionType = interactionType;
        }

        public string AuthorId { get; set; }
        public string Comment { get; set; }
        public InteractionType InteractionType  { get; set; }
        
    }
}
