using Developer.Domain.Entities.Base;
using MongoDB.Bson.Serialization.Attributes;

namespace Developer.Domain.Entities;


[BsonIgnoreExtraElements]
public class User : BaseEntity<string>
{

    public string Email { get; set; }
    public string UserName { get; set; }
    public string UserLastname { get; set; }
    public string Password { get; set; }
    public bool Eliminated { get; set; }

    public User(
                string email,
                string userName,
                string userLastName,
                string password
        )
    {

        Email = email;
        UserName = userName;
        UserLastname = userLastName;
        Password = password;
    }
}
