using Developer.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Developer.Domain.Entities.User;

public class User : BaseEntity<string>
{
    public User(string email, string username, string password, string bio, string profilePicture, List<string> preferences)
    {
        Email = email;
        Username = username;
        Password = password;
        Bio = bio;
        ProfilePicture = profilePicture;
        Social = new SocialInfo();
        Preferences = new Preferences(preferences);
        Stats = new UserStats();
    }

    public string Email { get; set; }
    public string Password { get; set; }
    public string Username { get; set; }
    public string? ProfilePicture { get; set; }
    public string? Bio { get; set; }
    public InteractionLog interactionLog { get; set; }
    public SocialInfo Social { get; set; }
    public Preferences Preferences { get; set; }
    public UserStats Stats { get; set; }
}
