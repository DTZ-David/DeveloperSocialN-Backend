using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Developer.Domain.Entities.User;

public class SocialInfo
{
    public List<string> Followers { get; set; } = new List<string>();
    public List<string> Following { get; set; } = new List<string>();
}
