using MyTodoWebApi.Data.Contracts;
using System;
using System.Collections.Generic;
using System.Text;
namespace MyTodoWebApi.Data
{
    public interface IUnitOfWork
    {
        void Commit();

        ICustomerRepository Customer { get; }
        IUserRepository user { get; }


    }
}
