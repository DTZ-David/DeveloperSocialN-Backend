using Developer.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Developer.Domain.Entities.Posts
{
    public class Comments : BaseEntity<string>
    {
        public string AuthorId { get; set; }
        public string Content { get; set; }
        public Comments()
        {
            
        }
    }
}
