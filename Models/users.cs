using System;
using System.Collections.Generic;

namespace DOTNET.Models
{
    public class Users
    {
        public int Id { get; set; }
        public string EmailId { get; set; }
        public string Name { get; set; }
        public string mobile { get; set; }
        public string email { get; set; }
        
    }

    public class SignIn
    {   

        public int Id { get; set; }
        public string email { get; set;}
        public string password { get; set;}
    }

    public class Tasks
    {
        public int Id { get; set;}
        public int UserId { get; set;}
        public string Project { get; set;}
        public string Describtion { get; set;}
    }
}
