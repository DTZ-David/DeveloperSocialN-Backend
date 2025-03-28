using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Developer.Domain.Entities.User;

public class Preferences
{
    public List<string> ProgrammingLanguages { get; set; } = new List<string>();
    public bool DarkMode { get; set; } = false;
    public Preferences(List<string> ProgrammingLanguages)
    {
        this.ProgrammingLanguages = ProgrammingLanguages;
    }
}
