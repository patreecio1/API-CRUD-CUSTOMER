using System;
using System.Collections.Generic;
using System.Text;

namespace MyTodoWebApi.Data.ApiModel
{
    public class users
    {
        public int id { get; set; }
        public string username { get; set; }
        public string Passwords { get; set; }
       
    }

    public class Jwt
    {
        public string key { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public string Subject { get; set; }

    }
}
