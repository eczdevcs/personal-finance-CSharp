using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace PersonalFinanceMng.Domain.Entities
{
     public class User
    {
        public Guid Id { get; private set; }

        public string Name { get; private set; }
        public string Email { get; private set; }

        public DateTime CreatedAt { get; private set; }
        public DateTime UpdatedAt { get; private set; }

      

       public User (string name, string email)
       {
            Id = Guid.NewGuid ();
            Name = name;
            Email = email;
            CreatedAt = DateTime.Now;
            UpdatedAt = DateTime.Now;
       }

        public User(Guid id ,string name, string email)
        {
            Id = id;
            Name = name;
            Email = email;
            CreatedAt = DateTime.Now;
            UpdatedAt = DateTime.Now;
        }
     }
}
