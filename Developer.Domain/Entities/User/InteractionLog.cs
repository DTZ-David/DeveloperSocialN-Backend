using Developer.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Developer.Domain.Entities.User;

public class InteractionLog : BaseEntity<string>
{
    public string UserId { get; set; }
    public string TargetPostId { get; set; }
    public InteractionType Type { get; set; }
    public string? Content { get; set; }

    public InteractionLog(string userId, string targetPostId, InteractionType type, string? content)
    {
      
        UserId = userId;
        TargetPostId = targetPostId;
        Type = type;
        Content = content;
    }
}
public enum InteractionType
{
    Careful,
    Comment,
    Verify
}

