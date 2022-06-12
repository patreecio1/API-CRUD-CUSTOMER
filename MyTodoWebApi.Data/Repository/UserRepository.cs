using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using MyTodoWebApi.ApiModel;
using MyTodoWebApi.Data.ApiModel;
using MyTodoWebApi.Data.Contracts;
using MyTodoWebApi.ViewModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
namespace MyTodoWebApi.Data.Repository
{
    class UserRepository : EFRepository<users>, IUserRepository
    {
        private readonly customerinfoContext Db;
        public UserRepository(customerinfoContext context) : base(context)
        {
            Db = context;

        }

        public async Task<users> GetUser(string username, string password)
        {
            try
            {
                return await Db.Users.Where(u => u.username == username && u.Passwords == password).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
    }




