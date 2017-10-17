using System;
using System.Text.RegularExpressions;

namespace Passenger.Core.Domain
{

    public class User
    {
        private static readonly Regex NameRegex= new Regex("^(?![_.-])(?!.*[_.-]{2})[a-zA-Z0-9._.-]+(?<![_.-])$");
        public Guid Id { get; protected set; }
        public string Email { get; protected set; }
        public string Password { get; protected set; }
        public string Role { get; set; }
        public string Salt { get; protected set; }
        public DateTime CreatedAt { get; protected set; }
        public string UserName { get; protected set; }
        public string FullName { get; protected set; }

        protected User()
        {
        }

        public User(Guid userId,string email, string username, 
            string password,string role, string salt)
        {
            if (!NameRegex.IsMatch(username))
            {
                throw new Exception("Username is not valid");
            }
            Id = userId;
            Email = email;
            UserName = username;
            Password = password;
            Role = role;
            Salt = salt;
            CreatedAt = DateTime.UtcNow;
        }
    }
}