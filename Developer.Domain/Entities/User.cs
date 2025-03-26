using Developer.Domain.Entities.Base;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Developer.Domain.Entities;

[BsonIgnoreExtraElements]
public class User : BaseEntity<string>
{
    [BsonElement("bio")]
    public string Bio { get; private set; }

    [BsonElement("createdAt")]
    public DateTime CreatedAt { get; private set; }

    [BsonElement("email")]
    public string Email { get; private set; }

    [BsonElement("followers")]
    [BsonRepresentation(BsonType.ObjectId)]
    public List<string> Followers { get; private set; } = new List<string>();

    [BsonElement("following")]
    [BsonRepresentation(BsonType.ObjectId)]
    public List<string> Following { get; private set; } = new List<string>();

    [BsonElement("password")]
    public string Password { get; private set; }  // Ahora almacena la contraseña en texto plano.

    [BsonElement("profilePicture")]
    public string ProfilePicture { get; private set; }

    [BsonElement("updatedAt")]
    public DateTime UpdatedAt { get; private set; }

    [BsonElement("username")]
    public string UserName { get; private set; }

    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; private set; }

    // Constructor para crear un nuevo usuario
    public User(
        string email,
        string userName,
        string password,  // Se usa en texto plano.
        string bio = "",
        string profilePicture = ""
    )
    {
        ValidateEmail(email);
        ValidateUserName(userName);
        ValidatePassword(password);

        Id = ObjectId.GenerateNewId().ToString();
        Email = email;
        UserName = userName;
        Password = password;  // Se almacena sin encriptación.
        Bio = bio;
        ProfilePicture = profilePicture;
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }

    // Método para actualizar el perfil del usuario
    public void UpdateProfile(string bio, string profilePicture)
    {
        Bio = bio;
        ProfilePicture = profilePicture;
        UpdatedAt = DateTime.UtcNow;
    }

    // Método para eliminar un seguidor
    public void RemoveFollower(string followerId)
    {
        if (string.IsNullOrEmpty(followerId))
            throw new ArgumentException("Follower ID cannot be null or empty.");

        if (!Followers.Contains(followerId))
            throw new InvalidOperationException("User is not following this user.");

        Followers.Remove(followerId);
        UpdatedAt = DateTime.UtcNow;
    }

    // Método para seguir a otro usuario
    public void FollowUser(string userId)
    {
        if (string.IsNullOrEmpty(userId))
            throw new ArgumentException("User ID cannot be null or empty.");

        if (Following.Contains(userId))
            throw new InvalidOperationException("User is already following this user.");

        Following.Add(userId);
        UpdatedAt = DateTime.UtcNow;
    }

    // Método para dejar de seguir a otro usuario
    public void UnfollowUser(string userId)
    {
        if (string.IsNullOrEmpty(userId))
            throw new ArgumentException("User ID cannot be null or empty.");

        if (!Following.Contains(userId))
            throw new InvalidOperationException("User is not following this user.");

        Following.Remove(userId);
        UpdatedAt = DateTime.UtcNow;
    }

    // Validación de correo electrónico
    private void ValidateEmail(string email)
    {
        if (string.IsNullOrEmpty(email))
            throw new ArgumentNullException(nameof(email), "Email cannot be null or empty.");

        var emailRegex = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$");
        if (!emailRegex.IsMatch(email))
            throw new ArgumentException("Invalid email format.", nameof(email));
    }

    // Validación de nombre de usuario
    private void ValidateUserName(string userName)
    {
        if (string.IsNullOrEmpty(userName))
            throw new ArgumentNullException(nameof(userName), "Username cannot be null or empty.");

        if (userName.Length < 3 || userName.Length > 50)
            throw new ArgumentException("Username must be between 3 and 50 characters.", nameof(userName));
    }

    // Validación de contraseña
    private void ValidatePassword(string password)
    {
        if (string.IsNullOrEmpty(password))
            throw new ArgumentNullException(nameof(password), "Password cannot be null or empty.");

        if (password.Length < 6)
            throw new ArgumentException("Password must be at least 6 characters long.", nameof(password));
    }
}
