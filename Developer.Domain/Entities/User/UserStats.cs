using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Developer.Domain.Entities.User;

public class UserStats
{
    public int PostsCount { get; set; } = 0;
    public int LikesReceived { get; set; } = 0;
    public int CommentsMade { get; set; } = 0;
}
