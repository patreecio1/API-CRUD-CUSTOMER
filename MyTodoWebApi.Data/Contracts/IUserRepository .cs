
using MyTodoWebApi.ApiModel;
using MyTodoWebApi.Data.ApiModel;
using MyTodoWebApi.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MyTodoWebApi.Data.Contracts
{
    public interface IUserRepository
    {
        Task<users> GetUser(string username, string password);

    }
}
