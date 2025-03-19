using Developer.Domain.Entities.Base;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Developer.Domain.Entities;



public class RegisterUser : BaseEntity<string>
{
    public RegisterUser( string email,string userName, string password)
    {
    
        this.email = email;
        this.UserName = userName;
        this.password = password;
    }
    public string email { get; set; }
    public string password { get; set; }

    public string UserName { get; set; }


}